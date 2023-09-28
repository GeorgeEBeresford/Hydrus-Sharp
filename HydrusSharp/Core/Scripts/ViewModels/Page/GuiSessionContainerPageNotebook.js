var GuiSessionContainerPageNotebook = (function () {
    function GuiSessionContainerPageNotebook(view) {
        var _this = this;
        this.serialisableType = ko.observable(view[0]);
        this.name = ko.observable(view[1]);
        this.items = ko.observableArray(this.getItems(view[3]));
        this.pageNotebooks = ko.computed(function () { return _this.items().filter(function (item) { return item.serialisableType() === 106; }); });
        this.pageSingles = ko.computed(function () { return _this.items().filter(function (item) { return item.serialisableType() === 107; }); });
        this.dropdownId = ko.observable(Math.random().toString());
    }
    GuiSessionContainerPageNotebook.prototype.getItems = function (list) {
        var shortcutSets = list[2];
        return shortcutSets.map(function (shortcutSet) {
            var item = shortcutSet[1];
            if (item[0] === 106) {
                return new GuiSessionContainerPageNotebook(item);
            }
            else if (item[0] === 107) {
                return new GuiSessionContainerPageSingle(item);
            }
            console.warn("Unsupported serialisable type in list.");
            console.debug(item);
            return null;
        });
    };
    return GuiSessionContainerPageNotebook;
}());
//# sourceMappingURL=GuiSessionContainerPageNotebook.js.map