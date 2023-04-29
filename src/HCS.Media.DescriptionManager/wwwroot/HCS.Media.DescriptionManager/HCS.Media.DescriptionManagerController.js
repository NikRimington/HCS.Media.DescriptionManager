(function () {
    "use strict";

    function DescriptionManagerController($scope, $http, mediaHelper, navigationService)
    {
        console.log(Umbraco.Sys.ServerVariables);
        var vm = this;
        vm.data = {
            items:[],
            total: 0
        };
        vm.loading = true;

        load();

        function load()
        { 
            vm.loading = true;
            vm.data   
            $http.get(Umbraco.Sys.ServerVariables.HCSMedia.DescriptionApi + "Fetch").then(function (res){
                vm.data = res.data;
                vm.loading = false;
            });
        }

        vm.load = load;
        vm.save = save;
        vm.changed = changed;
        vm.isImage = isImageCheck;

        function save($index)
        {
            var item = vm.data.items[$index];
            if(item == undefined) return;

            item.state = "waiting";

            $http.put(Umbraco.Sys.ServerVariables.HCSMedia.DescriptionApi + "Save",
                {
                  Index: $index,
                  Description: item.description,
                  MediaId: item.key
                }).then(function(res)
                {
                    item.state = "success";
                    item.btnColor = "positive";
                    item.updated = true;
                    var el = document.getElementById("description-form-" + res.data.index);
                    removeFadeOut(el, 250);
                    navigationService.syncTree({tree: 'media', path: ["-1", String(item.key)], forceReload: true});
                    
                }, function(res){
                    item.state = "error";
                    item.btnColor = "negative";
                    setTimeout(function() {
                        item.btnColor = "default";
                    }, 250);
                });

            console.log($index);
        }

        function changed($index, $event)
        {
            try {
                vm.data.items[$index].description = $event.target.value;
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

        function removeFadeOut(el, speed ) {
            var seconds = speed/1000;
            el.style.transition = "opacity "+seconds+"s ease";
        
            el.style.opacity = 0;
            setTimeout(function() {
                el.parentNode.removeChild(el);
            }, speed - 5);
        }
    }


    angular.module("umbraco").controller("hcs.media.DescriptionManagerController",
        ['$scope', '$http', 'mediaHelper', 'navigationService', DescriptionManagerController]);
})();