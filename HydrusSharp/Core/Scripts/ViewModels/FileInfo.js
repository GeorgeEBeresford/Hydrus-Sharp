var FileInfo = (function () {
    function FileInfo(view) {
        this.hashId = ko.observable(view.HashId);
        this.size = ko.observable(view.Size);
        this.mimeType = ko.observable(view.MimeType);
        this.width = ko.observable(view.Width);
        this.height = ko.observable(view.Height);
        this.duration = ko.observable(view.Duration);
        this.frameCount = ko.observable(view.FrameCount);
        this.hasAudio = ko.observable(view.HasAudio);
        this.wordCount = ko.observable(view.WordCount);
    }
    return FileInfo;
}());
//# sourceMappingURL=FileInfo.js.map