var Index = (function () {
    function Index() {
        var _this = this;
        this.sessions = ko.observableArray([]);
        this.currentSession = ko.observable(null);
        this.hashedJsonDump = ko.observable(null);
        this.fileInfos = ko.observableArray([]);
        this.thumbnailWidth = ko.observable(0);
        this.thumbnailHeight = ko.observable(0);
        this.numberOfFiles = ko.observable(0);
        this.filesPerPage = ko.observable(20);
        this.selectedPage = ko.observable(1);
        this.loadingFiles = ko.observable(false);
        this.numberOfPages = ko.computed(function () { return Math.ceil(_this.numberOfFiles() / _this.filesPerPage()); });
        this.selectablePages = ko.computed(function () {
            var selectablePages = [];
            var startOfPages = Math.max(_this.selectedPage() - 4, 1);
            var extraPages = Math.max(5 - startOfPages, 0);
            var endOfPages = Math.min(_this.selectedPage() + 5 + extraPages, _this.numberOfPages());
            for (var index = startOfPages; index <= endOfPages; index++) {
                selectablePages.push(index);
            }
            return selectablePages;
        });
        this.sessions.subscribe(function () {
            _this.selectFirstPageAsync();
        });
        this.hashedJsonDump.subscribe(function () {
            _this.searchForMatchingMedia();
        });
        this.selectedPage.subscribe(function () {
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
            deferred.resolve();
            return deferred.promise();
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
        this.selectedPage(1);
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
        var skip = (this.selectedPage() - 1) * this.filesPerPage();
        var take = this.filesPerPage();
        this.loadingFiles(true);
        ClientProvider.getMatchingFileInfoAsync(managementController.mediaCollect(), managementController.mediaSort(), managementController.fileSearchContext().predicates(), skip, take)
            .then(function (paginatedFileInfos) {
            _this.numberOfFiles(paginatedFileInfos.count());
            _this.fileInfos(paginatedFileInfos.items());
            _this.loadingFiles(false);
            deferred.resolve();
        })
            .fail(function () {
            _this.loadingFiles(false);
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