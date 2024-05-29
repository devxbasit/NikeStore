$(function () {

    //   $('.btn-link[aria-expanded="true"]').closest('.accordion-item').addClass('active');
    // $('.collapse').on('show.bs.collapse', function () {
    //   $(this).closest('.accordion-item').addClass('active');
    // });
    //
    // $('.collapse').on('hidden.bs.collapse', function () {
    //   $(this).closest('.accordion-item').removeClass('active');
    // });
    //
    //

    $('#formOtp').submit(function (e) {

        e.preventDefault();

    })
});

function verifyOtp() {
    $.ajax({
        headers: {
            'Accept': 'application/json',
        },
        method: 'DELETE',
        url: `/Auth/Otp?opt=${$('#formOtp input').val()}`,
        success: function (response) {
            if (response.isSuccess) {
                Swal.fire("Success!", "Account Created Successfully!", "success").then(function () {
                    window.location = `/`;
                });

            } else {
                Swal.fire("Error!", `${response.message}`, "error");
            }
        }
    });
}


