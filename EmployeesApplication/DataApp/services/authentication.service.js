(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthenticationService', AuthenticationService);
    AuthenticationService.$inject = ['$http', '$cookies', '$rootScope', '$timeout'];
    function AuthenticationService($http, $cookies, $rootScope, $timeout) {

    }


});