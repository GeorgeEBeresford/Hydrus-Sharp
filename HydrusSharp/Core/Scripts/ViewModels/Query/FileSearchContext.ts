class FileSearchContext {

    public locationContexts: ko.ObservableArray<string>;
    public searchedTags: ko.ObservableArray<string>;
    //public tagContexts: ko.ObservableArray<string>;

    constructor(view: Array<any>) {

        const fileSearchContext = view[1];
        const properties = fileSearchContext[1];
        const contexts = properties[2];
        const locationContextWrapper = contexts[0];
        const locationContext = locationContextWrapper[2];
        const tagContext = contexts[1];

        const searchedTags = contexts[3];

        this.locationContexts = ko.observableArray(locationContext[0]);
        this.searchedTags = ko.observableArray(searchedTags.map(searchedTag => new SearchPredicate(searchedTag)));
        //this.tagContexts = ko.observableArray(tagContext[2][0]);

    }
}