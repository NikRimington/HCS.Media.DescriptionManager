(function () {
    "use strict";

    function DescriptionManagerController($scope, $http, mediaHelper)
    {
        console.log(Umbraco.Sys.ServerVariables);
        var vm = this;
        vm.items = [];
        vm.loading = true;
        

        load();

        function load()
        {            
            $http.get(Umbraco.Sys.ServerVariables.HCSMedia.DescriptionApi + "Fetch").then(function (res){
                vm.items = res.data.items;
                vm.pageSize = res.data.pageSize;
                vm.totalItems = res.data.total;
                vm.loading = false;
            });
        }

        vm.save = save;
        vm.changed = changed;
        vm.isImage = isImageCheck;

        function save($group, $index)
        {
            var item = vm.items[$group][$index];
            if(item == undefined) return;

            item.state = "waiting";

            $http.put(Umbraco.Sys.ServerVariables.HCSMedia.DescriptionApi + "Save",
                {
                  GroupIndex: $group,
                  Index: $index,
                  Description: item.description,
                  MediaId: item.key
                }).then(function(res)
                {
                    item.state = "success";
                }, function(res){
                    item.state = "error";
                });

            console.log($index);
        }

        function changed($group, $index, $event)
        {
            try {
                vm.items[$group][$index].description = $event.target.value;
            } catch (error) {
                console.error(error);
            }
        }

        function isImageCheck(item)
        {   
            if(item.isImage == undefined)
            {
                item.isImage = mediaHelper.detectIfImageByExtension(item.url.substr(0, item.url.lastIndexOf('?')));
            }
            return item.isImage;
        }
    }


    angular.module("umbraco").controller("hcs.media.DescriptionManagerController",
        ['$scope', '$http', 'mediaHelper', DescriptionManagerController]);
})();