class FileSearchContext {

    public locationContexts: ko.ObservableArray<string>;
    public predicates: ko.ObservableArray<SearchPredicate>;
    //public tagContexts: ko.ObservableArray<string>;

    constructor(view: Array<any>) {

        const fileSearchContext = view[1];
        const properties = fileSearchContext[1];
        const contexts = properties[2];
        const locationContextWrapper = contexts[0];
        const locationContext = locationContextWrapper[2];
        const tagContext = contexts[1];

        const searchedPredicates = contexts[3] as Array<any>;

        this.locationContexts = ko.observableArray(locationContext[0]);

            const predicates = searchedPredicates.map(searchedPredicate => new SearchPredicate(searchedPredicate));
            const sortedPredicates = predicates.sort((previousPredicate, nextPredicate) => {

                // System tags should come last
                if (previousPredicate.searchType() === 0 && nextPredicate.searchType() !== 0) {

                    return -1;
                }
                else if (previousPredicate.searchType() !== 0 && nextPredicate.searchType() === 0) {

                    return 1;
                }

                // Sort by name
                return previousPredicate.friendlySearchType() > nextPredicate.friendlySearchType() ? 1 : -1;
            });

            this.predicates = ko.observableArray(sortedPredicates);
    }

        //this.tagContexts = ko.observableArray(tagContext[2][0]);

    }
}