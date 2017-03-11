(function () {
    'use strict';

    angular
        .module('app')
        .factory('DepartmentService', DepartmentService);

    DepartmentService.$inject = ['$http'];
    function DepartmentService($http) {
        var service = {};

        service.GetAll = GetAll;
        service.GetById = GetById;
       

        return service;

        function GetAll() {
            return $http.get('/api/department').then(handleSuccess, handleError('Error getting all employees'));
        }

        function GetById(id) {
            return $http.get('/api/department/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }

     

        // private functions

        function handleSuccess(res) {
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();