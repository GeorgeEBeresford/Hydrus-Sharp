var Index = (function () {
    function Index() {
        var _this = this;
        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);
        this.sessions.subscribe(function () {
            _this.selectFirstPageAsync();
        });
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
    Index.prototype.selectFirstPageAsync = function () {
        var deferred = $.Deferred();
        var sessions = this.sessions();
        var firstItem = sessions[0].notebook().items().find(function (item) { return item.serialisableType() === 107; });
        if (typeof (firstItem) === "undefined") {
            firstItem = sessions[0].notebook().items()[0].items().find(function (item) { return item.serialisableType() === 107; });
        }
        this.loadHashedJsonDumpAsync(firstItem.hash())
            .then(function () {
            deferred.resolve();
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    Index.prototype.loadHashedJsonDumpAsync = function (hash) {
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
            if (sessions.length === 0) {
                deferred.resolve();
                return;
            }
            _this.currentSession(sessions[0]);
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