class HttpRequester {

    public static getAsync(action: string, controller: string, data?: any, returnType?: string): JQueryPromise<IResultViewModel> {

        const deferred: JQueryDeferred<IResultViewModel> = $.Deferred();

        $.ajax({

            url: `${controller}/${action}`,
            method: "GET",
            data: data,
            contentType: "application/json;utf-8",
            dataType: returnType,
            success: (result: IResultViewModel) => {

                if (result.Error === null) {

                    deferred.resolve(result);
                    return;
                }

                console.log(result.Error);
                deferred.reject();
            },
            error: () => {

                console.log("Internal server error");
                deferred.reject();
            }
        });

        return deferred.promise();
    }

    public static postAsync(action: string, controller: string, data: string, returnType?: string): JQueryPromise<IResultViewModel> {

        const deferred: JQueryDeferred<IResultViewModel> = $.Deferred();

        $.ajax({

            url: `${controller}/${action}`,
            method: "POST",
            data: data,
            contentType: "application/json;utf-8",
            dataType: returnType,
            success: (result: IResultViewModel) => {

                if (result.Error === null) {

                    deferred.resolve(result);
                    return;
                }

                console.log(result.Error);
                deferred.reject();
            },
            error: () => {

                console.log("Internal server error");
                deferred.reject();
            }
        });

        return deferred.promise();
    }
}