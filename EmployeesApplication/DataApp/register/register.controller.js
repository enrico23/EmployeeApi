(function () {

    'use strict';

    angular.module('app')
	.controller('RegisterController', RegisterController);

   RegisterController.$inject = ['$scope'];
    function RegisterController($scope) {
        console.log('register controller...');
    }

})();