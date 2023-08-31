class NamedJsonDump {

    public type: ko.Observable<number>;
    public name: ko.Observable<string>;
    public version: ko.Observable<number>;
    public timestamp: ko.Observable<number>;
    public dump: ko.Observable<string>;
    public notebook: ko.Observable<GuiSessionContainerPageNotebook>;
    public lastModifiedOn: ko.Computed<string>;

    public constructor(view: INamedJsonDump) {

        this.type = ko.observable(view.DumpType);
        this.name = ko.observable(view.DumpName);
        this.version = ko.observable(view.Version);
        this.timestamp = ko.observable(view.Timestamp);
        this.dump = ko.observable(view.Dump);
        this.notebook = ko.observable(new GuiSessionContainerPageNotebook(JSON.parse(view.Dump)));

        this.lastModifiedOn = ko.computed(() => {

            const date = new Date(this.timestamp());

            return date.toString();

            //const hour = date.getHours().toString().padStart(2, "0");
            //const minute = date.getMinutes().toString().padStart(2, "0");
            //const seconds = date.getSeconds().toString().padStart(2, "0");

            //return `${hour}:${minute}:${seconds}`;
        });
    }
}