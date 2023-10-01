var TagViewModel = (function () {
    function TagViewModel(tag) {
        this.tag = ko.observable(tag);
        var splitLocation = tag.indexOf(":");
        var namespace = tag.substring(0, splitLocation);
        var subtag = tag.substring(splitLocation + 1);
        this.namespace = ko.observable(namespace);
        this.subTag = ko.observable(subtag);
    }
    return TagViewModel;
}());
//# sourceMappingURL=TagViewModel.js.map