var ManagementController = (function () {
    function ManagementController(view) {
        this.serialisableType = ko.observable(view[0]);
        var managementType = view[2];
        var dictionary = managementType[2];
        var options = dictionary[2];
        console.log(options);
        this.name = ko.observable(managementType[0]);
        this.mediaSort = ko.observable(new MediaSort(options[0]));
        this.mediaCollect = ko.observable(new MediaCollect(options[1]));
        this.fileSearchContext = ko.observable(new FileSearchContext(options[2]));
    }
    ManagementController.prototype.toJson = function () {
        return {
            Collect: this.mediaCollect().toJson(),
            Sort: this.mediaSort().toJson(),
            Predicates: this.fileSearchContext().predicates().map(function (predicate) { return predicate.toJson(); })
        };
    };
    return ManagementController;
}());
//# sourceMappingURL=ManagementController.js.map