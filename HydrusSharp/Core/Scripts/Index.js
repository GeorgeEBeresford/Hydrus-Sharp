var Index = (function () {
    function Index() {
        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);
    }
    Index.prototype.initialiseAsync = function () {
        var deferred = $.Deferred();
        this.refreshSessionsAsync()
            .then(function () {
            deferred.resolve();
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    Index.prototype.loadHashedJsonDump = function (hash) {
        var _this = this;
        var deferred = $.Deferred();
        ClientProvider.getHashedJsonDump(hash)
            .then(function (hashedJsonDump) {
            _this.hashedJsonDump(hashedJsonDump);
            deferred.resolve();
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    Index.prototype.refreshSessionsAsync = function () {
        var _this = this;
        var deferred = $.Deferred();
        ClientProvider.getSessionsAsync()
            .then(function (sessions) {
            _this.sessions(sessions);
            deferred.resolve();
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    return Index;
}());
(function () {
    var index = new Index();
    index.initialiseAsync();
    ko.applyBindings(index, $("body")[0]);
})();
//# sourceMappingURL=Index.js.map