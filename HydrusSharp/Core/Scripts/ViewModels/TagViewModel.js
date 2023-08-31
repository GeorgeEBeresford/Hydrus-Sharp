var TagViewModel = (function () {
    function TagViewModel(view) {
        this.tagId = ko.observable(view.TagId);
        this.namespaceId = ko.observable(view.NamespaceId);
        this.namespace = ko.observable(view.Namespace);
        this.subTagId = ko.observable(view.SubTagId);
        this.subTag = ko.observable(view.SubTag);
    }
    return TagViewModel;
}());
//# sourceMappingURL=TagViewModel.js.map