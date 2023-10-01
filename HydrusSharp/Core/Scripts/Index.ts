class Index {

    public sessions: ko.ObservableArray<NamedJsonDump>;
    public currentSession: ko.Observable<NamedJsonDump>;
    public hashedJsonDump: ko.Observable<HashedJsonDump>;
    public fileInfos: ko.ObservableArray<FileInfo>;
    public thumbnailWidth: ko.Observable<number>;
    public thumbnailHeight: ko.Observable<number>;
    public numberOfFiles: ko.Observable<number>;
    public filesPerPage: ko.Observable<number>;
    public selectedPage: ko.Observable<number>;
    public numberOfPages: ko.Computed<number>;
    public selectablePages: ko.Computed<Array<number>>;
    public loadingFiles: ko.Observable<boolean>;

    public constructor() {

        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);
        this.fileInfos = ko.observableArray([]);
        this.thumbnailWidth = ko.observable(0);
        this.thumbnailHeight = ko.observable(0);
        this.numberOfFiles = ko.observable(0);
        this.filesPerPage = ko.observable(20);
        this.selectedPage = ko.observable(1);
        this.loadingFiles = ko.observable(false);
        this.numberOfPages = ko.computed(() => Math.ceil(this.numberOfFiles() / this.filesPerPage()));

        this.selectablePages = ko.computed(() => {

            const selectablePages = [];

            // We only want to display the previous 4 pages at most
            const startOfPages = Math.max(this.selectedPage() - 4, 1);

            // If we're viewing pages 1 - 4, we want to make up the difference by displaying more pages at the end
            const extraPages = Math.max(5 - startOfPages, 0);

            // We only want to display the next 5 pages at most (plus any extra pages)
            const endOfPages = Math.min(this.selectedPage() + 5 + extraPages, this.numberOfPages());

            for (let index = startOfPages; index <= endOfPages; index++) {

                selectablePages.push(index);
            }

            return selectablePages;
        });

        this.sessions.subscribe(() => {

            this.selectFirstPageAsync();
        })

        this.hashedJsonDump.subscribe(() => {

            this.searchForMatchingMedia();
        });

        this.selectedPage.subscribe(() => {

            this.searchForMatchingMedia();
        });
    }

    public initialiseAsync(): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();

        $.when(this.initialiseOptionsAsync(), this.refreshSessionsAsync())
            .then(() => {

                deferred.resolve();
            })
            .fail(() => {

                deferred.reject();
            });

        return deferred.promise();
    }

    public selectFirstPageAsync(): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();
        const sessions = this.sessions();

        let firstItem = sessions[0].notebook().items().find(item => item.serialisableType() === 107);
        if (typeof (firstItem) === "undefined") {

            firstItem = (sessions[0].notebook().items()[0] as GuiSessionContainerPageNotebook).items().find(item => item.serialisableType() === 107);
            deferred.resolve();
            return deferred.promise();
        }

        this.loadHashedJsonDumpAsync((firstItem as GuiSessionContainerPageSingle).hash())
            .then(() => {

                deferred.resolve();
            })
            .fail(() => {

                deferred.reject();
            });

        return deferred.promise();
    }

    public loadHashedJsonDumpAsync(hash: string): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();

        this.selectedPage(1);

        ClientProvider.getHashedJsonDumpAsync(hash)
            .then(hashedJsonDump => {

                this.hashedJsonDump(hashedJsonDump);
                deferred.resolve();
            })
            .fail(() => {

                deferred.reject();
            });

        return deferred.promise();
    }

    public searchForMatchingMedia(): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();
        const managementController = this.hashedJsonDump().managementController();
        const skip = (this.selectedPage() - 1) * this.filesPerPage();
        const take = this.filesPerPage();

        this.loadingFiles(true);

        ClientProvider.getMatchingFileInfoAsync(managementController.mediaCollect(), managementController.mediaSort(), managementController.fileSearchContext().predicates(), skip, take)
            .then(paginatedFileInfos => {

                this.numberOfFiles(paginatedFileInfos.count());
                this.fileInfos(paginatedFileInfos.items());
                this.loadingFiles(false);

                deferred.resolve();
            })
            .fail(() => {

                this.loadingFiles(false);
                deferred.reject();
            });

        return deferred.promise();
    }

    private initialiseOptionsAsync(): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();

        ClientProvider.getOptionAsync("thumbnail_dimensions")
            .then(thumbnailDimensions => {

                const dimensions = thumbnailDimensions.split("\n");
                this.thumbnailWidth(+dimensions[0]);
                this.thumbnailHeight(+dimensions[1]);

                deferred.resolve();
            })
            .fail(() => {

                deferred.reject();
            });

        return deferred.promise();
    }

    private refreshSessionsAsync(): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();

        ClientProvider.getSessionsAsync()
            .then(sessions => {

                this.sessions(sessions);

                // Select the first available session
                if (sessions.length === 0) {

                    deferred.resolve();
                    return;
                }

                this.currentSession(sessions[0]);
                deferred.resolve();
            })
            .fail(() => {

                deferred.reject();
            });

        return deferred.promise();
    }
}

(() => {

    const index = new Index();
    index.initialiseAsync();

    ko.applyBindings(index, $("body")[0]);
})();