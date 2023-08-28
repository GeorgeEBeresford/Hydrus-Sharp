class GuiSessionContainerPageSingle {

    public serialisableType: ko.Observable<number>;

    public name: ko.Observable<string>;

    public hash: ko.Observable<string>;

    public constructor(view: Array<any>) {

        this.serialisableType = ko.observable(view[0]);
        this.name = ko.observable(view[1]);
        this.hash = ko.observable(view[3]);
    }
}