var HttpRequester = (function () {
    function HttpRequester() {
    }
    HttpRequester.getAsync = function (url, data, returnType) {
        var deferred = $.Deferred();
        $.ajax({
            url: url,
            method: "GET",
            data: data,
            contentType: "application/json;utf-8",
            dataType: returnType,
            success: function (result) {
                if (result.Error === null) {
                    deferred.resolve(result);
                    return;
                }
                console.log(result.Error);
                deferred.reject();
            },
            error: function () {
                console.log("Internal server error");
                deferred.reject();
            }
        });
        return deferred.promise();
    };
    HttpRequester.postAsync = function (url, data, returnType) {
        var deferred = $.Deferred();
        $.ajax({
            url: url,
            method: "POST",
            data: data,
            contentType: "application/json;utf-8",
            dataType: returnType,
            success: function (result) {
                if (result.Error === null) {
                    deferred.resolve(result);
                    return;
                }
                console.log(result.Error);
                deferred.reject();
            },
            error: function () {
                console.log("Internal server error");
                deferred.reject();
            }
        });
        return deferred.promise();
    };
    return HttpRequester;
}());
//# sourceMappingURL=HttpRequester.js.map