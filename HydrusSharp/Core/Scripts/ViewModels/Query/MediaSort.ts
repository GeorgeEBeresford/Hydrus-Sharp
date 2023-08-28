class MediaSort {

    public sortBy: ko.Observable<string>;

    public namespaces: ko.ObservableArray<string>;

    public systemSortType: ko.Observable<number>;

    public ascending: ko.Observable<boolean>;

    public friendlySystemSortType: ko.Computed<string>;

    public constructor(view: Array<any>) {

        const mediaSort = view[1];
        const properties = mediaSort[1];
        const sortBy = properties[2];

        this.sortBy = ko.observable(sortBy[0]);

        if (this.sortBy() !== "system") {

            this.namespaces = ko.observableArray(sortBy[1][0]);
            this.systemSortType = ko.observable(0);
        }
        else {

            this.namespaces = ko.observableArray([]);
            this.systemSortType = ko.observable(sortBy[1]);
        }

        this.ascending = ko.observable(sortBy[1][1] === 1);

        this.friendlySystemSortType = ko.computed(() => { 

            if (this.systemSortType() === null) {

                return null;
            }

            switch (this.systemSortType()) {

                case 0: return "File size";
                case 1: return "Duration";
                case 2: return "Import time";
                case 3: return "Mime type";
                case 4: return "Random";
                case 5: return "Width";
                case 6: return "Height";
                case 7: return "Pixel ratio";
                case 8: return "Number of pixels";
                case 9: return "Number of tags";
                case 10: return "Media views";
                case 11: return "Media viewtime";
                case 12: return "Approximate bitrate";
                case 13: return "Has audio";
                case 14: return "Date of last modification";
                case 15: return "Framerate";
                case 16: return "Number of frames";
                case 17: return "Number of files in collection";
                case 18: return "Last viewed time";
                case 19: return "Date of archiving"
                case 20: return "Hash";
            }
        })
    }
}