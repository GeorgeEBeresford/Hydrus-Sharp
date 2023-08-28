class GuiSessionContainerPageNotebook {

    public serialisableType: ko.Observable<number>;

    public name: ko.Observable<string>;

    public items: ko.ObservableArray<GuiSessionContainerPageNotebook | GuiSessionContainerPageSingle>

    public constructor(view: Array<any>) {

        this.serialisableType = ko.observable(view[0]);
        this.name = ko.observable(view[1]);
        this.items = ko.observableArray(this.getItems(view[3]));
    }

    private getItems(list: Array<any>): Array<GuiSessionContainerPageNotebook | GuiSessionContainerPageSingle> {

        const shortcutSets: Array<any> = list[2];

        //const items = [];
        //shortcutSets.forEach(shortcutSet => {



        //    //(shortcutSet[1] as Array<any>).forEach((shortcut: Array<any>) => {

        //    //    items.push(shortcut[])
        //    //})
        //})
        //for (let shortcutIndex = 0; index < shortcutSets.length; index++) {

        //    for ()
        //}

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