
function GetCustomer(id=0){
    $.ajax({
        url:'/Home/CustomerModal',
        type:'GET',
        data:{id:id},
        success: function(response){
            $("#customerDetailsModalContainer").html(response);
            $("#customerDetailsModal").modal("show");
        },
        error: function () {
            toastr.error("Error generating token");
        }
    })
}


$(document).on("submit", "#customerDetailsForm", function (e) {
    e.preventDefault();

    var form = $(this);
    var formData = form.serialize();

    $.ajax({
        url: '/Home/CustomerModal',
        type: 'POST',
        data: formData,
        success: function (response) {
            $("#customerDetailsModal").modal("hide");
          
            if (response.success) {
                window.location.href= response.redirecturl;
                toastr.success(response.message);
            }
            else{
                toastr.error(response.message);
            }
        },
        error: function () {
            toastr.error(response.message);
        }
    });

});


function Delete(id=0){
    $.ajax({
        url:'/Home/CustomerDelete',
        type:'POST',
        data:{id:id},
        success: function(response){
            if (response.success) {
                window.location.href= response.redirecturl;
                toastr.success(response.message);
            }
        },
        error: function (response) {
            toastr.error(response.message);
        }
    })
}
