var crudNs = crudNs || {};

crudNs.crud = function (url, verb, data, version) {
    this.url = url;
    this.verb = verb;
    this.data = data;
    this.version = version;
    this.origin =  'http://localhost:8187';
}
crudNs.crud.prototype = function () {
   
    var sendRequest = function (successCallback) {
        var self = this;

        if (self.version) {
            $.ajaxSetup({
                headers: { 'api-version': self.version}
            });
        }
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