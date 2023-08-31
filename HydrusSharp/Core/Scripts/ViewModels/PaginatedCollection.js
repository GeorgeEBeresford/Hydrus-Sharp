var PaginatedCollection = (function () {
    function PaginatedCollection(items, count) {
        this.items = ko.observableArray(items);
        this.count = ko.observable(count);
    }
    return PaginatedCollection;
}());
//# sourceMappingURL=PaginatedCollection.js.map