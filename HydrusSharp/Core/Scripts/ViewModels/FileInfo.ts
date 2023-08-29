class FileInfo {

    public hashId: ko.Observable<number>;

    public size: ko.Observable<number>;

    public mimeType: ko.Observable<number>;

    public width: ko.Observable<number>;

    public height: ko.Observable<number>;

    public duration: ko.Observable<number>;

    public frameCount: ko.Observable<number>;

    public hasAudio: ko.Observable<boolean>;

    public wordCount: ko.Observable<number>;

    constructor(view: IFileInfo) {

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
}