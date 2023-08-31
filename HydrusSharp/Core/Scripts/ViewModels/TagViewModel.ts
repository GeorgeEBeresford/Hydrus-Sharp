class TagViewModel {

    private tagId: ko.Observable<number>;

    private namespaceId: ko.Observable<number>;

    private namespace: ko.Observable<string>;

    public subTagId: ko.Observable<number>;

    public subTag: ko.Observable<string>;

    public constructor(view: ITag) {

        this.tagId = ko.observable(view.TagId);
        this.namespaceId = ko.observable(view.NamespaceId);
        this.namespace = ko.observable(view.Namespace);
        this.subTagId = ko.observable(view.SubTagId);
        this.subTag = ko.observable(view.SubTag);
    }
}