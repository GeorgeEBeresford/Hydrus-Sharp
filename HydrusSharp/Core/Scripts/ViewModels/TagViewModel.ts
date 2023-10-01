/**
 * Represents a single tag
 */
class TagViewModel {

    /**
     * The full tag
     */
    public tag: ko.Observable<string>;

    /**
     * The namespace of the tag
     */
    public namespace: ko.Observable<string>;

    /**
     * The subtag of the tag
     */
    public subTag: ko.Observable<string>;

    /**
     * Creates a new TagViewModel
     * @param tag
     */
    public constructor(tag: string) {

        this.tag = ko.observable(tag);

        // Tag may have multiple ":"s and we're only interested in splitting on the first instance
        const splitLocation = tag.indexOf(":");
        const namespace = tag.substring(0, splitLocation);
        const subtag = tag.substring(splitLocation + 1);

        this.namespace = ko.observable(namespace);
        this.subTag = ko.observable(subtag);
    }
}