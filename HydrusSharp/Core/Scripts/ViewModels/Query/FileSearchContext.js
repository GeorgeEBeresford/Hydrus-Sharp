var FileSearchContext = (function () {
    function FileSearchContext(view) {
        var fileSearchContext = view[1];
        var properties = fileSearchContext[1];
        var contexts = properties[2];
        var locationContextWrapper = contexts[0];
        var locationContext = locationContextWrapper[2];
        var tagContext = contexts[1];
        var searchedPredicates = contexts[3];
        this.locationContexts = ko.observableArray(locationContext[0]);
        var predicates = searchedPredicates.map(function (searchedPredicate) { return new SearchPredicate(searchedPredicate); });
        var sortedPredicates = predicates.sort(function (previousPredicate, nextPredicate) {
            if (previousPredicate.searchType() === 0 && nextPredicate.searchType() !== 0) {
                return -1;
            }
            else if (previousPredicate.searchType() !== 0 && nextPredicate.searchType() === 0) {
                return 1;
            }
            return previousPredicate.friendlySearchType() > nextPredicate.friendlySearchType() ? 1 : -1;
        });
        this.predicates = ko.observableArray(sortedPredicates);
    }
    return FileSearchContext;
}());
//# sourceMappingURL=FileSearchContext.js.map