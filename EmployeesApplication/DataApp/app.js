(function () {
    // console.log('app inizialised');

    angular.module('app', ['ngRoute'])
        .config(config);


    config.$inject = ['$routeProvider', '$locationProvider'];
    function config($routeProvider, $locationProvider) {
        $routeProvider
            .when('/', {
                // controller: 'HomeController',
                templateUrl: '/dataapp/home/home.html',
                controllerAs: 'vm'
            })

            .when('/login', {
                // controller: 'LoginController',
                templateUrl: '/dataapp/login/login.html',
                // controllerAs: 'vm'
            })

            .when('/register', {
                // controller: 'RegisterController',
                templateUrl: '/dataapp/register/register.html',
                // controllerAs: 'vm'
            })

            .otherwise({ redirectTo: '/login' });
    }
})();