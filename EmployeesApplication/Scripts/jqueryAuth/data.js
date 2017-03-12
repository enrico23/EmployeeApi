$(function () {
    if (sessionStorage.getItem('accessToken') == null) {
        window.location.href = "/home/login";
    }

    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });

    $('#errorModal').on('hidden.bs.modal', function () {
        window.location.href = "Login.html";
    });

    $('#btnLoadEmployees').click(function () {
        $.ajax({
            url: '/api/departments',
            method: 'GET',
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            },
            success: function (data) {
                console.log(data);
                $('#divData').removeClass('hidden');
                $('#tblBody').empty();
                $.each(data, function (index, value) {
                    var row = $('<tr><td>' + value.Id + '</td><td>'
                        + value.Name + '</td>'+
                     '</tr>');
                    $('#tblData').append(row);
                });
            },
            error: function (jQXHR) {
                // If status code is 401, access token expired, so
                // redirect the user to the login page
                if (jQXHR.status == "401") {
                    $('#errorModal').modal('show');
                }
                else {
                    $('#divErrorText').text(jqXHR.responseText);
                    $('#divError').show('fade');
                }
            }
        });
    });

    $('#btnLogoff').click(function () {
        sessionStorage.removeItem('accessToken');
        window.location.href = "/home/login";
    });
});