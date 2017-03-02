(function () {

    'use strict';

    angular.module('app')
	.controller('LoginController',LoginController);

    LoginController.$inject = ['$scope'];
    function LoginController($scope) {
        console.log('logincontroller');
    }

})();