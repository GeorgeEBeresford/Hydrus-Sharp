class ManagementController {

    public serialisableType: ko.Observable<number>;

    public name: ko.Observable<string>;

    public mediaSort: ko.Observable<MediaSort>;

    public mediaCollect: ko.Observable<MediaCollect>;

    public fileSearchContext: ko.Observable<FileSearchContext>;

    public constructor(view: Array<any>) {

        this.serialisableType = ko.observable(view[0]);

        const managementType = view[2];
        const dictionary = managementType[2];
        const options = dictionary[2];

        //console.log(options);

        this.name = ko.observable(managementType[0]);

        this.mediaSort = ko.observable(new MediaSort(options[0]));
        this.mediaCollect = ko.observable(new MediaCollect(options[1]));
        this.fileSearchContext = ko.observable(new FileSearchContext(options[2]));
    }

    public toJson(): any {

        return {

            Collect: this.mediaCollect().toJson(),
            Sort: this.mediaSort().toJson(),
            Predicates: this.fileSearchContext().predicates().map(predicate => predicate.toJson())
        };
    }
}