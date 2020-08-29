//$(document).ready(function() {
//  $('.js-checkbox-all').on('change', function() {
//    status = this.checked;
//    console.log(status);
//    $('input[name=CsCreensHospital]').attr('checked', status);
//    //parent = $(this).closest('.m-datatable__table');
//    //$('.js-checkbox-child').each(function() {
//    //  this.checked = status;
//    //});


//  });

//  // $('.js-checkbox-child').on('change', function() {
//  //   parent = $(this).closest('.m-datatable__table');
//  //   if(this.checked == false) {
//  //     parent.find('.js-checkbox-all').checked = false;
//  //   }
//  //   if($('js-checkbox-child:checked').length == $(this).length) {
//  //     parent.find('.js-checkbox-all').checked = true;
//  //   }
//  // });
//});

$(document).ready(function () {
    $('.js-checkbox-allHos').click(function (event) {
        if (this.checked) {
            $('.js-checkbox-childHos').each(function () {
                this.checked = true;
               
            });
        } else {
            $('.js-checkbox-childHos').each(function () {
                this.checked = false;
            });
        }
    });
    $('.js-checkbox-allPharmacy').click(function (event) {
        if (this.checked) {
            $('.js-checkbox-childPhar').each(function () {
                this.checked = true;

            });
        } else {
            $('.js-checkbox-childPhar').each(function () {
                this.checked = false;
            });
        }
    });
    $('.js-acceptInfo').on("change", function (event) {
        if ($("input[name=accept]").is(":checked")) {
            $("#butSubmitOrder").removeClass("disabled");
        } else {
            $('#butSubmitOrder').addClass("disabled");
        }
    });
});