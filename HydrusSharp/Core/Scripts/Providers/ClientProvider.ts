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

    public static getHashedJsonDumpAsync(hash: string): JQueryPromise<HashedJsonDump> {

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

    public static getMatchingFileInfoAsync(collect: MediaCollect, sort: MediaSort, predicates: Array<SearchPredicate>, skip: number, take: number): JQueryPromise<Array<FileInfo>> {

        const deferred: JQueryDeferred<Array<FileInfo>> = $.Deferred();
        const payload = {

            collect: collect.toJson(),
            sort: sort.toJson(),
            predicates: predicates.map(predicate => predicate.toJson()),
            skip: skip,
            take: take
        };

        HttpRequester.postAsync("MatchingFileInfo", "Client", JSON.stringify(payload))
            .then(result => {

                const fileInfos = result.Value as Array<IFileInfo>;
                const viewModels = fileInfos.map(fileInfo => new FileInfo(fileInfo));

                deferred.resolve(viewModels);
            })
            .fail(() => {

                deferred.reject();
            })

        return deferred.promise();
    }

    public static getOptionAsync(optionName: string): JQueryPromise<string> {

        const deferred: JQueryDeferred<string> = $.Deferred();
        const payload = {

            optionName: optionName
        };

        HttpRequester.getAsync("Option", "Client", payload)
            .then(result => {

                const option = result.Value as string;

                deferred.resolve(option);
            })
            .fail(() => {

                deferred.reject();
            })

        return deferred.promise();
    }
}