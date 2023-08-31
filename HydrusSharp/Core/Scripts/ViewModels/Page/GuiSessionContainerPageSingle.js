var GuiSessionContainerPageSingle = (function () {
    function GuiSessionContainerPageSingle(view) {
        this.serialisableType = ko.observable(view[0]);
        this.name = ko.observable(view[1]);
        this.hash = ko.observable(view[3]);
    }
    return GuiSessionContainerPageSingle;
}());
//# sourceMappingURL=GuiSessionContainerPageSingle.js.map