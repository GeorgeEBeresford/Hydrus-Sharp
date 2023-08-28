var NamedJsonDump = (function () {
    function NamedJsonDump(view) {
        var _this = this;
        this.type = ko.observable(view.DumpType);
        this.name = ko.observable(view.DumpName);
        this.version = ko.observable(view.Version);
        this.timestamp = ko.observable(view.Timestamp);
        this.dump = ko.observable(view.Dump);
        this.notebook = ko.observable(new GuiSessionContainerPageNotebook(JSON.parse(view.Dump)));
        this.lastModifiedOn = ko.computed(function () {
            var date = new Date(_this.timestamp());
            return date.toString();
        });
    }
    return NamedJsonDump;
}());
//# sourceMappingURL=NamedJsonDump.js.map