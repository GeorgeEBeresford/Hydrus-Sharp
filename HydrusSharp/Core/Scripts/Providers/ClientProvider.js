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
    ClientProvider.getHashedJsonDump = function (hash) {
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
    return ClientProvider;
}());
//# sourceMappingURL=ClientProvider.js.map