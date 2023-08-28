var HttpRequester = (function () {
    function HttpRequester() {
    }
    HttpRequester.getAsync = function (action, controller, data, returnType) {
        var deferred = $.Deferred();
        $.ajax({
            url: "".concat(controller, "/").concat(action),
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