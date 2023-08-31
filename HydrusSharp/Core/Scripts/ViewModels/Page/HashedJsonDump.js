var HashedJsonDump = (function () {
    function HashedJsonDump(view) {
        this.dumpType = ko.observable(view.DumpType);
        this.version = ko.observable(view.Version);
        this.dump = ko.observable(view.Dump);
        this.hash = ko.observable(view.HashedString);
        this.managementController = ko.observable(new ManagementController(JSON.parse(view.Dump)[0]));
    }
    return HashedJsonDump;
}());
//# sourceMappingURL=HashedJsonDump.js.map