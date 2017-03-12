$(function () {
   
    
    $('#linkClose').click(function () {
        $('#divError').hide('fade');
    });

  
    $('#btnRegister').click(function () {
        var email = $('#txtEmail').val();
        var password = $('#txtPassword').val();
        var confirm = $('#txtConfirmPassword').val();

        $.ajax({
            url: '/api/account/register',
            method: 'POST',
            data: {
                email: email,
                password: password,
                confirmPassword: confirm
            },
            success: function () {
                $('#successModal').modal('show');
                $('#txtEmail').val("");
                $('#txtPassword').val("");
                $('#txtConfirmPassword').val("");

            },
            error: function (jqXHR) {
                $('#divErrorText').text(jqXHR.responseText);
                $('#divError').show('fade');
            }
        });
    });


});