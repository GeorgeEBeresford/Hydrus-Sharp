/**
 * A class which allows the user to specify a predicate for media to match
 */
class SearchPredicate {

    /**
     * The type of search we're performing
     */
    public searchType: ko.Observable<number>;

    /**
     * The tag we're searching for
     */
    public tag: ko.Observable<string>;

    /**
     * Any search data applied to system searches
     */
    public searchData: ko.ObservableArray<number>;

    /**
     * Whether the predicate must be true for the results to return. E.g.
     * If mustBeTrue is true and tag is "Cat" then any media must contain the tag "Cat"
     * If mustBeTrue is false and tag is "Cat" then any media must not contain the tag "Cat"
     */
    public mustBeTrue: ko.Observable<boolean>;

    /**
     * A more display-friendly search type that's more understandable to the user
     */
    public friendlySearchType: ko.Computed<string>;

    /**
     * Creates a new SearchPredicate
     * @param view
     */
    public constructor(view: Array<any>) {

        const searchPredicate = view[2];

        this.searchType = ko.observable(searchPredicate[0]);
        this.tag = ko.observable(this.searchType() === 0 ? searchPredicate[1] : []);
        this.searchData = ko.observableArray(this.searchType() !== 0 ? searchPredicate[1] : 0);
        this.mustBeTrue = ko.observable(searchPredicate[2]);

        this.friendlySearchType = ko.computed(() => {

            switch (this.searchType()) {

                case 0: return "tags";
                case 1: return "namespace";
                case 2: return "parent";
                case 3: return "wildcard";
                case 4: return "system:everything";
                case 5: return "system:inbox";
                case 6: return "system:archive";
                case 7: return "system:untagged";
                case 8: return "system:number of tags";
                case 9: return "system:limit";
                case 10: return "system:size";
                case 11: return "system:age";
                case 12: return "system:hash";
                case 13: return "system:width";
                case 14: return "system:height";
                case 15: return "system:file ratio";
                case 16: return "system:duration";
                case 17: return "system:mime type";
                case 18: return "system:rating";
                case 19: return "system:similar to files";
                case 20: return "system:local";
                case 21: return "system:not local";
                case 22: return "system:number of words";
                case 23: return "system:file service";
                case 24: return "system:number of pixels";
                case 25: return "system:dimensions";
                case 26: return "system:number of file relationships";
                case 27: return "system:number of tags";
                case 28: return "system:known urls";
                case 29: return "system:file viewing statistics";
                case 30: return "OR";
                case 31: return "label";
                case 32: return "system:file relationships king";
                case 33: return "system:file relationships";
                case 34: return "system:has audio";
                case 35: return "system:modified time";
                case 36: return "system:frame rate";
                case 37: return "system:number of frames";
                case 38: return "system:number of notes";
                case 39: return "system:notes";
                case 40: return "system:has note name";
                case 41: return "system:has icc profile";
                case 42: return "system:time";
                case 43: return "system:last viewed time";
                case 44: return "system:has human readable embedded metadata";
                case 45: return "system:embedded metadata";
                case 46: return "system:has exif data";
                case 47: return "system:archived time";
                case 48: return "system:similar to data";
                case 49: return "system:similar to";

                default: throw `Search type ${this.searchType()} found. Not supported`;
            }
        })
    }
}