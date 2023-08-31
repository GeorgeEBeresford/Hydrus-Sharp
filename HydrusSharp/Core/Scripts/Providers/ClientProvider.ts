class ClientProvider {

    public static getSessionsAsync(): JQueryPromise<Array<NamedJsonDump>> {

        const deferred: JQueryDeferred<Array<NamedJsonDump>> = $.Deferred();
        const url: string = $("input[data-url='Client,Sessions']").val();

        HttpRequester.getAsync(url)
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
        const url: string = $("input[data-url='Client,HashedJsonDump']").val();

        HttpRequester.getAsync(url, { hash: hash })
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

    public static getMatchingFileInfoAsync(collect: MediaCollect, sort: MediaSort, predicates: Array<SearchPredicate>, skip: number, take: number): JQueryPromise<PaginatedCollection<FileInfo>> {

        const deferred: JQueryDeferred<PaginatedCollection<FileInfo>> = $.Deferred();
        const url: string = $("input[data-url='Client,MatchingFileInfo']").val();
        const payload = {

            collect: collect.toJson(),
            sort: sort.toJson(),
            filters: predicates.map(predicate => predicate.toJson()),
            skip: skip,
            take: take
        };

        HttpRequester.postAsync(url, JSON.stringify(payload))
            .then(result => {

                const paginatedFileInfos = result.Value as IPaginatedResultViewModel<IFileInfo>;
                const itemViewModels = paginatedFileInfos.Items.map(fileInfo => new FileInfo(fileInfo));
                const viewModel = new PaginatedCollection<FileInfo>(itemViewModels, paginatedFileInfos.Count);
                deferred.resolve(viewModel);
            })
            .fail(() => {

                deferred.reject();
            });

        return deferred.promise();
    }

    public static getOptionAsync(optionName: string): JQueryPromise<string> {

        const deferred: JQueryDeferred<string> = $.Deferred();
        const url: string = $("input[data-url='Client,Option']").val();
        const payload = {

            optionName: optionName
        };

        HttpRequester.getAsync(url, payload)
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