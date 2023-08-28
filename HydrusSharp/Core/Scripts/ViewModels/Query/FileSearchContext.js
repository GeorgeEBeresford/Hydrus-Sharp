var FileSearchContext = (function () {
    function FileSearchContext(view) {
        var fileSearchContext = view[1];
        var properties = fileSearchContext[1];
        var contexts = properties[2];
        var locationContextWrapper = contexts[0];
        var locationContext = locationContextWrapper[2];
        var tagContext = contexts[1];
        var searchedTags = contexts[3];
        this.locationContexts = ko.observableArray(locationContext[0]);
        this.searchedTags = ko.observableArray(searchedTags.map(function (searchedTag) { return new SearchPredicate(searchedTag); }));
    }
    return FileSearchContext;
}());
//# sourceMappingURL=FileSearchContext.js.map