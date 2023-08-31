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
    public selectablePages: ko.Computed<Array<number>>;

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
        this.selectablePages = ko.computed(() => {

            const numberOfPages = Math.ceil(this.numberOfFiles() / this.filesPerPage());

            const selectablePages = [];
            for (let index = 1; index <= numberOfPages; index++) {

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

        ClientProvider.getMatchingFileInfoAsync(managementController.mediaCollect(), managementController.mediaSort(), managementController.fileSearchContext().predicates(), skip, take)
            .then(paginatedFileInfos => {

                this.numberOfFiles(paginatedFileInfos.count());
                this.fileInfos(paginatedFileInfos.items());

                deferred.resolve();
            })
            .fail(() => {

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