var SearchPredicate = (function () {
    function SearchPredicate(view) {
        var _this = this;
        var searchPredicate = view[2];
        this.searchType = ko.observable(searchPredicate[0]);
        this.searchData = ko.observableArray(Array.isArray(searchPredicate[1]) ? searchPredicate[1] : [searchPredicate[1]]);
        this.mustBeTrue = ko.observable(searchPredicate[2]);
        this.friendlySearchType = ko.computed(function () {
            return _this.prettifySearchData();
        });
    }
    SearchPredicate.prototype.toJson = function () {
        return {
            SearchType: this.searchType(),
            SearchData: this.searchData(),
            MustBeTrue: this.mustBeTrue()
        };
    };
    SearchPredicate.prototype.prettifySearchData = function () {
        if (this.searchType() === 0) {
            return this.searchData()[0];
        }
        switch (this.searchType()) {
            case 1: return "".concat(this.searchData()[0], ":anything");
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
            case 13: return "system:width is ".concat(this.searchData().join(' '));
            case 14: return "system:height is ".concat(this.searchData().join(' '));
            case 15: return "system:file ratio";
            case 16: return "system:duration";
            case 17: return "system:".concat(this.searchData().map(this.getMimeType).join(' or '));
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
            default: throw "Search type ".concat(this.searchType(), " found. Not supported");
        }
    };
    SearchPredicate.prototype.getMimeType = function (mimeTypeId) {
        switch (mimeTypeId) {
            case 1: return "jpeg";
            case 2: return "png";
            case 3: return "gif";
            case 4: return "bitmap";
            case 5: return "flash";
            case 6: return "yaml";
            case 7: return "icon";
            case 8: return "html";
            case 9: return "flv";
            case 10: return "pdf";
            case 11: return "zip";
            case 12: return "encrypted zip";
            case 13: return "mp3";
            case 14: return "mp4";
            case 15: return "ogg";
            case 16: return "flac";
            case 17: return "wma";
            case 18: return "wmv";
            case 19: return "unknown windows media file";
            case 20: return "audio mkv";
            case 21: return "webm";
            case 22: return "json";
            case 23: return "apng";
            case 24: return "unknown png";
            case 25: return "mpeg";
            case 26: return "mov";
            case 27: return "avi";
            case 28: return "hydrus update definition";
            case 29: return "hydrus update content";
            case 30: return "plain text";
            case 31: return "rar";
            case 32: return "7zip";
            case 33: return "webp";
            case 34: return "tiff";
            case 35: return "photoshop document";
            case 36: return "m4a";
            case 37: return "realmedia video";
            case 38: return "realmedia audio";
            case 39: return "trueaudio audio";
            case 40: return "sound";
            case 41: return "image";
            case 42: return "video";
            case 43: return "application";
            case 44: return "animation";
            case 45: return "application clip";
            case 46: return "wave";
            case 47: return "ogv";
            case 48: return "video mkv";
            case 49: return "audio mp4";
            case 50: return "unknown mp4";
            case 51: return "cbor";
            case 52: return "exe";
            case 53: return "wavpack";
            case 54: return "sai2";
            case 55: return "krita";
            case 56: return "svg";
            case 100: return "octet stream";
            case 101: return "unknown";
        }
    };
    return SearchPredicate;
}());
//# sourceMappingURL=SearchPredicate.js.map