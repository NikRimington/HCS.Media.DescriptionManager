(function () {
    "use strict";

    function altDescriptionManagerController($scope)
    {
        var vm = this;
        vm.items = [];

        vm.items.push({
            name: "abc"
        });
        vm.items.push({
            name: "def"
        });
    }


    angular.module("umbraco").controller("hcs.media.altDescriptionManagerController",
        ['$scope', altDescriptionManagerController]);
})();