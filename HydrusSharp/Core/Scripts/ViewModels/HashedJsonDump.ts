class HashedJsonDump {

    public dumpType: ko.Observable<number>;

    public version: ko.Observable<number>;

    public dump: ko.Observable<string>;

    public hash: ko.Observable<string>;

    public managementController: ko.Observable<ManagementController>;

    public constructor(view: IHashedJsonDump) {

        this.dumpType = ko.observable(view.DumpType);
        this.version = ko.observable(view.Version);
        this.dump = ko.observable(view.Dump);
        this.hash = ko.observable(view.HashedString);

        this.managementController = ko.observable(new ManagementController(JSON.parse(view.Dump)[0]));
    }
}