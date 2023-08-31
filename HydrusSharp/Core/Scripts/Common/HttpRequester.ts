class HttpRequester {

    public static getAsync(url: string, data?: any, returnType?: string): JQueryPromise<IResultViewModel> {

        const deferred: JQueryDeferred<IResultViewModel> = $.Deferred();

        $.ajax({

            url: url,
            method: "GET",
            data: data,
            contentType: "application/json;utf-8",
            dataType: returnType,
            success: (result: IResultViewModel) => {

                if (result.Error === null) {

                    deferred.resolve(result);
                    return;
                }

                console.error(result.Error);
                deferred.reject();
            },
            error: () => {

                console.error("Internal server error");
                deferred.reject();
            }
        });

        return deferred.promise();
    }

    public static postAsync(url: string, data: string, returnType?: string): JQueryPromise<IResultViewModel> {

        const deferred: JQueryDeferred<IResultViewModel> = $.Deferred();

        $.ajax({

            url: url,
            method: "POST",
            data: data,
            contentType: "application/json;utf-8",
            dataType: returnType,
            success: (result: IResultViewModel) => {

                if (result.Error === null) {

                    deferred.resolve(result);
                    return;
                }

                console.error(result.Error);
                deferred.reject();
            },
            error: () => {

                console.error("Internal server error");
                deferred.reject();
            }
        });

        return deferred.promise();
    }
}