var ClientProvider = (function () {
    function ClientProvider() {
    }
    ClientProvider.getSessionsAsync = function () {
        var deferred = $.Deferred();
        var url = $("input[data-url='Client,Sessions']").val();
        HttpRequester.getAsync(url)
            .then(function (result) {
            var sessions = result.Value;
            var viewModels = sessions.map(function (session) { return new NamedJsonDump(session); });
            deferred.resolve(viewModels);
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    ClientProvider.getHashedJsonDumpAsync = function (hash) {
        var deferred = $.Deferred();
        var url = $("input[data-url='Client,HashedJsonDump']").val();
        HttpRequester.getAsync(url, { hash: hash })
            .then(function (result) {
            var hashedJsonDump = result.Value;
            var viewModel = new HashedJsonDump(hashedJsonDump);
            deferred.resolve(viewModel);
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    ClientProvider.getMatchingFileInfoAsync = function (collect, sort, predicates, skip, take) {
        var deferred = $.Deferred();
        var url = $("input[data-url='Client,MatchingFileInfo']").val();
        var payload = {
            collect: collect.toJson(),
            sort: sort.toJson(),
            filters: predicates.map(function (predicate) { return predicate.toJson(); }),
            skip: skip,
            take: take
        };
        HttpRequester.postAsync(url, JSON.stringify(payload))
            .then(function (result) {
            var paginatedFileInfos = result.Value;
            var itemViewModels = paginatedFileInfos.Items.map(function (fileInfo) { return new FileInfo(fileInfo); });
            var viewModel = new PaginatedCollection(itemViewModels, paginatedFileInfos.Count);
            deferred.resolve(viewModel);
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    ClientProvider.getOptionAsync = function (optionName) {
        var deferred = $.Deferred();
        var url = $("input[data-url='Client,Option']").val();
        var payload = {
            optionName: optionName
        };
        HttpRequester.getAsync(url, payload)
            .then(function (result) {
            var option = result.Value;
            deferred.resolve(option);
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    return ClientProvider;
}());
//# sourceMappingURL=ClientProvider.js.map