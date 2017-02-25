$(function () {
    
    var ulEmployees = $('#ulEmployees');


    $('#btnfemale').click(function () {

        var username = $('#txtUsername').val();
        var password = $('#txtPassword').val();

        $.ajax({
            type: 'GET',
            url: 'http://localhost:45208/api/Employees/' + username + '/basic',
            dataType: 'json',
            
            success: function (data) {
                ulEmployees.empty();
                $.each(data, function (index, val) {
                    var fullName = val.FirstName + ' ' + val.LastName;
                    ulEmployees.append('<li>' + fullName + ' (' + val.Gender + ')</li>')
                });
            },
            complete: function (jqXHR) {
                if (jqXHR.status == '401') {
                    ulEmployees.empty();
                    ulEmployees.append('<li style="color:red">'
                        + jqXHR.status + ' : ' + jqXHR.statusText + '</li>')
                }
            }
        });
    });


    
    $('#btn').click(function () {

        var username = $('#txtUsername').val();
        var password = $('#txtPassword').val();

        // console.log(btoa(username + ':' + password));

        $.ajax({
            type: 'GET',
            url: 'http://localhost:45208/api/Employees/' + username + '/basic',
            dataType: 'json',
            headers: {
                'Authorization': 'Basic ' + btoa(username + ':' + password)
            },
            success: function (data) {
                ulEmployees.empty();
                $.each(data, function (index, val) {
                    var fullName = val.FirstName + ' ' + val.LastName;
                    ulEmployees.append('<li>' + fullName + ' (' + val.Gender + ')</li>')
                });
            },
            complete: function (jqXHR) {
                if (jqXHR.status == '401') {
                    ulEmployees.empty();
                    ulEmployees.append('<li style="color:red">'
                        + jqXHR.status + ' : ' + jqXHR.statusText + '</li>')
                }
            }
        });
    });

    $('#btnClear').click(function () {
        ulEmployees.empty();
    });
});