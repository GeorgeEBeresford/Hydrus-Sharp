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
    return ManagementController;
}());
//# sourceMappingURL=ManagementController.js.map