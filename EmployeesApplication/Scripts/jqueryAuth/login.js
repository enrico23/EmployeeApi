$(function () {

    $('#linkClose').click(function () {
        $('#divError').hide();
    });

    $('#btnLogin').click(function () {
        var username = $('#txtUsername').val();
        var  password = $('#txtPassword').val();
        var grant_type = 'password';

        $.ajax({
            
            url: '/token',
            method: 'POST',
            contentType: 'application/json',
            data: {
                username: username,
                password: password,
                grant_type: grant_type
            },
            
            success: function (response) {
                sessionStorage.setItem("accessToken", response.access_token);
                window.location.href = "/home/data/";
            },
           
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show();
            }
        });
    });
});