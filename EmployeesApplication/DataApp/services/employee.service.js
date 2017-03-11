(function () {
    'use strict';
 
    angular
        .module('app')
        .factory('EmployeeService', EmployeeService);
 
    EmployeeService.$inject = ['$http'];
    function EmployeeService($http) {
        var service = {};
 
        service.GetAll = GetAll;
        service.GetById = GetById;
        service.GetByGender = GetByGender;
        service.Create = Create;
        service.Update = Update;
        service.Delete = Delete;
 
        return service;
 
        function GetAll() {
            return $http.get('/api/employees').then(handleSuccess, handleError('Error getting all employees'));
        }
 
        function GetById(id) {
            return $http.get('/api/employees/' + id).then(handleSuccess, handleError('Error getting user by id'));
        }
 
        function GetByGender(gender) {
            return $http.get('/api/employees/' + gender).then(handleSuccess, handleError('Error getting user by username'));
        }
 
        function Create(employee) {
            return $http.post('/api/employees', employee).then(handleSuccess, handleError('Error creating employee'));
        }
 
        function Update(employee) {
            return $http.put('/api/employees/' + employee.id, employee).then(handleSuccess, handleError('Error updating employee'));
        }
 
        function Delete(id) {
            return $http.delete('/api/employees/' + id).then(handleSuccess, handleError('Error deleting employee'));
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