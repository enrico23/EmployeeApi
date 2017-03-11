(function () {
    'use strict';

    angular
        .module('app')
        .factory('logger', logger);

    function logger() {
       
        var service = {};
        service.sayHello = sayHello;
        return service;

        function sayHello() {
            console.log("say hello");
        }
        
    }
})();
