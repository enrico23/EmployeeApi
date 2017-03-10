(function () {

    'use strict';

    angular.module('app')
	.controller('LoginController',LoginController);

    LoginController.$inject = ['$scope'];
    function LoginController($scope) {
        var vm = this;
        // console.log('logincontroller');
        vm.login = login;

        function login() {
            // console.log("Login username: " + vm.username + " password: " + vm.password);
            console.log(btoa(vm.username + ':' + vm.password));
        }
    }

})();