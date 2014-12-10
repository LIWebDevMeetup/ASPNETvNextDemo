// angular model setup
var mainApp = angular.module('mainApp', ['gadgetsControllers', 'gadgetsServices'])

mainApp.filter('gadgetsFilter', function () {
    return function (gadgets, query) {
        if (!query) {
            return gadgets;
        }

        var filtered = [];
        query = query.toLowerCase();
        angular.forEach(gadgets, function (g) {
            if (g.Title.toLowerCase().indexOf(query) !== -1) {
                filtered.push(g);
            }
        });

        return filtered;
    }
});

var gadgetsService = angular.module('gadgetsServices', ['ngResource']);
var gadgetsControllers = angular.module('gadgetsControllers', []);

gadgetsService.factory('Gadgets', ['$resource',
    function ($resource) {
        return $resource('/Data/Gadgets', {}, {
            query: { method: 'POST', params: {}, isArray: true }
        });
    }]);

gadgetsControllers.controller('gadgetListController',
    ['$scope', 'Gadgets',
        function ($scope, Gadgets) {
            var gadgets = [];
            
            Gadgets.query(function (data) {
                angular.forEach(data, function (g) {
                    g.bid = function () {
                        //debugger;
                        bids.server.placeBid(this.Title);
                    }

                    g.maxBidAmount = function () {
                        var amt = 0;
                        for (var q = 0; q < g.Bids.length; q++) {
                            if (g.Bids[q].BidAmount > amt) {
                                amt = g.Bids[q].BidAmount;
                            }
                        }
                        return "$" + amt.toString();
                    }
                    
                    gadgets.push(g);
                });
                $scope.gadgets = gadgets;
            });

        }]);