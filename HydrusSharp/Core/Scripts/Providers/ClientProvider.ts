class ClientProvider {

    public static getSessionsAsync(): JQueryPromise<Array<NamedJsonDump>> {

        const deferred: JQueryDeferred<Array<NamedJsonDump>> = $.Deferred();

        HttpRequester.getAsync("Sessions", "Client")
            .then(result => {

                const sessions = result.Value as Array<INamedJsonDump>;
                const viewModels = sessions.map(session => new NamedJsonDump(session));

                deferred.resolve(viewModels);
            })
            .fail(() => {

                deferred.reject();
            })

        return deferred.promise();
    }

    public static getHashedJsonDump(hash: string): JQueryPromise<HashedJsonDump> {

        const deferred: JQueryDeferred<HashedJsonDump> = $.Deferred();

        HttpRequester.getAsync("HashedJsonDump", "Client", { hash: hash })
            .then(result => {

                const hashedJsonDump = result.Value as IHashedJsonDump;

                const viewModel = new HashedJsonDump(hashedJsonDump);

                deferred.resolve(viewModel);
            })
            .fail(() => {

                deferred.reject();
            })

        return deferred.promise();
    }
}