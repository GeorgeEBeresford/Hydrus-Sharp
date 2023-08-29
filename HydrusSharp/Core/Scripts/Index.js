var Index = (function () {
    function Index() {
        var _this = this;
        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);
        this.fileInfos = ko.observableArray([]);
        this.thumbnailWidth = ko.observable(0);
        this.thumbnailHeight = ko.observable(0);
        this.sessions.subscribe(function () {
            _this.selectFirstPageAsync();
        });
        this.hashedJsonDump.subscribe(function () {
            _this.searchForMatchingMedia();
        });
    }
    Index.prototype.initialiseAsync = function () {
        var deferred = $.Deferred();
        $.when(this.initialiseOptionsAsync(), this.refreshSessionsAsync())
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
        ClientProvider.getHashedJsonDumpAsync(hash)
            .then(function (hashedJsonDump) {
            _this.hashedJsonDump(hashedJsonDump);
            deferred.resolve();
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    Index.prototype.searchForMatchingMedia = function () {
        var _this = this;
        var deferred = $.Deferred();
        var managementController = this.hashedJsonDump().managementController();
        ClientProvider.getMatchingFileInfoAsync(managementController.mediaCollect(), managementController.mediaSort(), managementController.fileSearchContext().predicates(), 0, 20)
            .then(function (fileInfos) {
            _this.fileInfos(fileInfos);
            deferred.resolve();
        })
            .fail(function () {
            deferred.reject();
        });
        return deferred.promise();
    };
    Index.prototype.initialiseOptionsAsync = function () {
        var _this = this;
        var deferred = $.Deferred();
        ClientProvider.getOptionAsync("thumbnail_dimensions")
            .then(function (thumbnailDimensions) {
            var dimensions = thumbnailDimensions.split("\n");
            _this.thumbnailWidth(+dimensions[0]);
            _this.thumbnailHeight(+dimensions[1]);
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