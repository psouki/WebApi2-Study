var crudNs = crudNs || {};

crudNs.crud = function (url, verb, data) {
    this.url = url;
    this.verb = verb;
    this.data = data;
}
crudNs.crud.prototype = function () {
   
    var sendRequest = function (successCallback) {
        var self = this;

        var options = {
            type: self.verb,
            success: successCallback
        }
        if (self.data) {
            options.data = self.data;
        }

        $.ajax(self.url, options);
    };
    return { sendRequest: sendRequest };
}();