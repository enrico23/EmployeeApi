$(function () {
    getAccessToken();

    $('#linkClose').click(function () {
        $('#divError').hide();
    });

    $('.btn-google').click(function () {
        // console.log('google');
        window.location.href = "/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A45208%2Fhome%2Flogin&state=P31ESED3RO1UzOfSP9NMjTZulsWrVVWvaTL_Jk5GmwI1";

    });
    $('.btn-facebook').click(function () {
        // console.log('facebook');
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
                // console.log(JSON.stringify(response));
               localStorage.setItem("accessToken", response.access_token);
               localStorage.setItem("userName", response.userName);
               window.location.href = "/home/data/";

                
               
            },
           
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show();
            }
        });
    });
});