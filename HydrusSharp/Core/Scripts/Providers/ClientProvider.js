var ClientProvider = (function () {
    function ClientProvider() {
    }
    ClientProvider.getSessionsAsync = function () {
        var deferred = $.Deferred();
        HttpRequester.getAsync("Sessions", "Client")
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
        HttpRequester.getAsync("HashedJsonDump", "Client", { hash: hash })
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
        var payload = {
            collect: collect.toJson(),
            sort: sort.toJson(),
            predicates: predicates.map(function (predicate) { return predicate.toJson(); }),
            skip: skip,
            take: take
        };
        HttpRequester.postAsync("MatchingFileInfo", "Client", JSON.stringify(payload))
            .then(function (result) {
            var fileInfos = result.Value;
            var viewModels = fileInfos.map(function (fileInfo) { return new FileInfo(fileInfo); });
            deferred.resolve(viewModels);
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    ClientProvider.getOptionAsync = function (optionName) {
        var deferred = $.Deferred();
        var payload = {
            optionName: optionName
        };
        HttpRequester.getAsync("Option", "Client", payload)
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