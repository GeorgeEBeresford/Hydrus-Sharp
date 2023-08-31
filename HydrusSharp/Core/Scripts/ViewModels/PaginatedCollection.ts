class PaginatedCollection<TItems> {

    public items: ko.ObservableArray<TItems>;

    public count: ko.Observable<number>;

    public constructor(items: Array<TItems>, count: number) {

        this.items = ko.observableArray(items);

        this.count = ko.observable(count);
    }
}