var MediaSort = (function () {
    function MediaSort(view) {
        var _this = this;
        var mediaSort = view[1];
        var properties = mediaSort[1];
        var sortBy = properties[2];
        this.sortBy = ko.observable(sortBy[0]);
        if (this.sortBy() !== "system") {
            this.namespaces = ko.observableArray(sortBy[1][0]);
            this.systemSortType = ko.observable(null);
        }
        else {
            this.namespaces = ko.observableArray([]);
            this.systemSortType = ko.observable(sortBy[1]);
        }
        this.ascending = ko.observable(sortBy[1][1] === 1);
        this.friendlySystemSortType = ko.computed(function () {
            if (_this.systemSortType() === null) {
                return null;
            }
            switch (_this.systemSortType()) {
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
                case 19: return "Date of archiving";
                case 20: return "Hash";
            }
        });
    }
    MediaSort.prototype.toJson = function () {
        return {
            Namespaces: this.namespaces(),
            SystemSortType: this.systemSortType()
        };
    };
    return MediaSort;
}());
//# sourceMappingURL=MediaSort.js.map