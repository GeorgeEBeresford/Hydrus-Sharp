class GuiSessionContainerPageNotebook {

    public serialisableType: ko.Observable<number>;

    public name: ko.Observable<string>;

    public items: ko.ObservableArray<GuiSessionContainerPageSingle | GuiSessionContainerPageNotebook>;

    public pageSingles: ko.Computed<Array<GuiSessionContainerPageSingle>>

    public pageNotebooks: ko.Computed<Array<GuiSessionContainerPageNotebook>>;

    public dropdownId: ko.Observable<string>;

    /**
     * Creates a new GuiSessionContainerPageNotebook
     */
    public constructor(view: Array<any>) {

        this.serialisableType = ko.observable(view[0]);
        this.name = ko.observable(view[1]);

        this.items = ko.observableArray(this.getItems(view[3]));

        this.pageNotebooks = ko.computed(() => this.items().filter(item => item.serialisableType() === 106) as Array<GuiSessionContainerPageNotebook>);
        this.pageSingles = ko.computed(() => this.items().filter(item => item.serialisableType() === 107) as Array<GuiSessionContainerPageSingle>);
        this.dropdownId = ko.observable(Math.random().toString());
    }

    /**
     * Retrieves any pages from the parsed list of serialised sessions
     */
    private getItems(list: Array<any>): Array<GuiSessionContainerPageNotebook | GuiSessionContainerPageSingle> {

        const shortcutSets: Array<any> = list[2];

        return shortcutSets.map((shortcutSet: Array<any>) => {

            const item = shortcutSet[1];

            // GuiSessionContainerPageNotebook
            if (item[0] === 106) {

                return new GuiSessionContainerPageNotebook(item);
            }

            // GuiSessionContainerPageSingle
            else if (item[0] === 107) {

                return new GuiSessionContainerPageSingle(item);
            }

            console.warn(`Unsupported serialisable type in list.`);
            console.debug(item);
            return null;
        });
    }
}