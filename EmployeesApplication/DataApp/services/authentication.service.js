(function () {
    'use strict';

    angular
        .module('app')
        .factory('AuthenticationService', AuthenticationService);
    AuthenticationService.$inject = ['$http', '$cookies', '$rootScope', '$timeout'];
    function AuthenticationService($http, $cookies, $rootScope, $timeout) {

        var service = {};
        service.sayHello = sayHello;
        service.Login = Login;
        service.SetCredentials = SetCredentials;
        service.ClearCredentials = ClearCredentials;
        return service;

        function sayHello() {
            console.log("say hello");
        }

        function Login(username, password, callback) {
            $http.post('/api/authenticate', { username: username, password: password })
                .success(function (response) {
                    callback(response);
                });
        }

        function SetCredentials(username, password) {
            var authdata = btoa(vm.username + ':' + vm.password);

            $rootScope.globals = {
                currentUser: {
                    username: username,
                    authdata: authdata
                }
            };

            // set default auth header for http requests
            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata;

            // store user details in globals cookie that keeps user logged in for 1 week (or until they logout)
            var cookieExp = new Date();
            cookieExp.setDate(cookieExp.getDate() + 7);
            $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp });
        }

        function ClearCredentials() {
            $rootScope.globals = {};
            $cookies.remove('globals');
            $http.defaults.headers.common.Authorization = 'Basic';
        }


    }
})();




//(function () {
//    'use strict';

//    angular
//        .module('app')
//        .factory('AuthService', AuthService);

//    AuthService.$inject = [];
//    function AuthService() {

//        var service = {};

//        //service.Login = Login;
//        //service.SetCredentials = SetCredentials;
//        //service.ClearCredentials = ClearCredentials;

//        return service;

//        //function Login(username, password, callback) {
//        //    $http.post('/api/authenticate', { username: username, password: password })
//        //        .success(function (response) {
//        //            callback(response);
//        //        });
//        //}

//        //function SetCredentials(username, password) {
//        //    var authdata = btoa(vm.username + ':' + vm.password);

//        //    $rootScope.globals = {
//        //        currentUser: {
//        //            username: username,
//        //            authdata: authdata
//        //        }
//        //    };

//        //    // set default auth header for http requests
//        //    $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata;
 
//        //    // store user details in globals cookie that keeps user logged in for 1 week (or until they logout)
//        //    var cookieExp = new Date();
//        //    cookieExp.setDate(cookieExp.getDate() + 7);
//        //    $cookies.putObject('globals', $rootScope.globals, { expires: cookieExp });
//        //}
        
//        //function ClearCredentials() {
//        //    $rootScope.globals = {};
//        //    $cookies.remove('globals');
//        //    $http.defaults.headers.common.Authorization = 'Basic';
//        //}
//    }


//});