class Index {

    public sessions: ko.ObservableArray<NamedJsonDump>;
    public currentSession: ko.Observable<NamedJsonDump>;
    public hashedJsonDump: ko.Observable<HashedJsonDump>;

    public constructor() {

        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);
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

    public loadHashedJsonDump(hash: string): JQueryPromise<void> {

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