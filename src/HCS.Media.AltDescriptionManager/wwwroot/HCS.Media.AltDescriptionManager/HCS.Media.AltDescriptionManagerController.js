(function () {
    "use strict";

    function altDescriptionManagerController($scope, $http)
    {
        console.log(Umbraco.Sys.ServerVariables);
        var vm = this;
        vm.items = [];

        load();

        function load()
        {
            $http.get(Umbraco.Sys.ServerVariables.HCSMedia.DescriptionApi + "Fetch").then(function (res){
                vm.items = res.data.items;
            });
        }

        vm.save = save;
        vm.changed = changed;

        function save($index)
        {
            console.log($index);
        }

        function changed($index, $event)
        {
            try {
                vm.items[$index].description = $event.target.value;
            } catch (error) {
                console.error(error);
            }
        }
    }


    angular.module("umbraco").controller("hcs.media.altDescriptionManagerController",
        ['$scope', '$http', altDescriptionManagerController]);
})();