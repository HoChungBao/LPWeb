function ajaxSubmitUpdateDsPlayerLog(event) {
    event.preventDefault();
    //if (checkInputMediHub().result) {
    $("#btnDsplayerUpdate").addClass("m-loader m-loader--light m-loader--right disable");
    var formData = new FormData();
    formData.append("Id", $("#Id").val());
    formData.append("PlayerId", $("#PlayerId").val());
    formData.append("PlayerName", $("#PlayerName").val());
    formData.append("Require", $("#Require").val());
    formData.append("Note", $("#Note").val());
    $.ajax({
        url: '/Common/UpdateDsPlayerLog/',
        type: 'POST',
        data: formData, // serializes the form's elements.
        contentType: false,
        processData: false,
        success: function (response) {

            if (response != null) {
                $("#js-reason").text("");
                $("#btnDsplayerUpdate").removeClass("m-loader m-loader--light m-loader--right disable");
                if (!response.result) {

                    swal("", response.message, "warning");


                }
                else {

                    swal("", response.message, "success");
                    setTimeout(function () {

                        window.location.href = window.location.origin + "/Common/DsPlayer";

                        //End
                    }, 5000);


                }
            }
        },
        fail: function () {
        }
    });
    //}

}
$("#btnDsplayerUpdate").on("click", ajaxSubmitUpdateDsPlayerLog);
$("#myModalDsPlayerUpdate").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var value = button.data('code');
    $("#Id").val(value.Id);
    $("#PlayerId").val(value.PlayerId);
    $("#PlayerName").val(value.PlayerName);
    $("#Require").val(value.Require);
    $("#Note").val(value.Note);
});

$("#myModalDsPlayerUpdate").on("hide.bs.modal", function (event) {
    $("#Id").val("");
    $("#PlayerId").val("");
    $("#PlayerName").val("");
    $("#Require").val("");
    $("#Note").val("");

});