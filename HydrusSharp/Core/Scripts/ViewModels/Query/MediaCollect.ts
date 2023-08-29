class MediaCollect {

    private namespaces: ko.ObservableArray<string>;

    constructor(view: Array<any>) {

        const mediaCollect = view[1];
        const properties = mediaCollect[1];
        const collectBy = properties[2];

        this.namespaces = ko.observableArray(collectBy[0]);
    }

    public toJson(): any {

        return {

            namespaces: this.namespaces()
        };
    }
}