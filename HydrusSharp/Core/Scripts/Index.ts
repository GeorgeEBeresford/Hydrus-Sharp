class Index {

    public sessions: ko.ObservableArray<NamedJsonDump>;
    public currentSession: ko.Observable<NamedJsonDump>;
    public hashedJsonDump: ko.Observable<HashedJsonDump>;

    public constructor() {

        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);

        this.sessions.subscribe(() => {

            this.selectFirstPageAsync();
        })
    }

    public initialiseAsync(): JQueryPromise<void> {

        const deferred: JQueryDeferred<void> = $.Deferred();

        this.refreshSessionsAsync()
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

        ClientProvider.getHashedJsonDump(hash)
            .then(hashedJsonDump => {

                this.hashedJsonDump(hashedJsonDump);
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