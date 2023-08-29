var MediaCollect = (function () {
    function MediaCollect(view) {
        var mediaCollect = view[1];
        var properties = mediaCollect[1];
        var collectBy = properties[2];
        this.namespaces = ko.observableArray(collectBy[0]);
    }
    MediaCollect.prototype.toJson = function () {
        return {
            namespaces: this.namespaces()
        };
    };
    return MediaCollect;
}());
//# sourceMappingURL=MediaCollect.js.map