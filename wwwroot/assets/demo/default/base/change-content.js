$(document).ready(function() {

  /**
   * Load data for Select
   */
  $.ajax({
    type: 'GET',
    url: 'https://next.json-generator.com/api/json/get/4kzm_PQUE',
    success: function (data) {
      $.each(data, function (index, element) {
        var op = new Option('text', element.no);
        $(op).html(element.name);
        $('.js-categoryProduct').append(op);
      });
    }
  })

  /**
   * Change info when change select
   * @param {Node} container 
   */
  function changeContent(container) {
    $(container).empty();
    $.ajax({
      type: 'GET',
      url: 'https://next.json-generator.com/api/json/get/4kzm_PQUE',
      success: function (data) {
        $.each(data, function (index, element) {
          if (selectedVal == element.no)
            for (i = 0; i < element.relatedDoc.length; i++) {
              $(container).append('<span class="m-form__info">_ ' + element.relatedDoc[i] + ' <a href="#" class="m-form__info-link">Xem mẫu</a></span>');
            }
        });
      }
    })
  }

  /**
   * Load header by select Type value
   * @param {Node} container 
   * @param {Array} listName 
   */
  hospitalHead = ["STT", "Tên Bệnh Viện", "Chuyên Khoa", "Địa Chỉ", "Tỉnh/Thành Phố"];
  pharmacyHead = ["STT", "Tên Nhà Thuốc", "Địa Chỉ", "Tỉnh/Thành Phố", "Quận/Huyện"];
  function showTableHeader(container, listName) {
    $(container).empty();
    $(container).append('<tr>');
    $.each(listName, function(index, item) {
      $(container).append('<th scope="col">' + item + '</th>');
    })
    $(container).append('</tr>');
  }

  /**
   * show Permission Document on step 2
   * @param {Node} container 
   */


  /**
   * show Hospital Table Data (Dashboard)
   * @param {Node} container 
   */
  //function showHospitalTableData(container) {
  //  $(container).empty();
  //  showTableHeader('.js-dashboard-table-header', hospitalHead);
  //  $.ajax({
  //    type: 'GET',
  //    url: 'https://next.json-generator.com/api/json/get/NyaDtv3vE',
  //    success: function (data) {
  //      $.each(data, function (index, element) {
  //        i = index + 1;
  //        $(container).append('<tr>');
  //        $(container).append('<th scope="row">' + i + '</th>');
  //        $(container).append('<td>' + element.name + '</td>');
  //        $(container).append('<td>' + element.specialty + '</td>');
  //        $(container).append('<td>' + element.address + '</td>');
  //        $(container).append('<td>' + element.city + '</td>');
  //        $(container).append('</tr>');
  //        // console.log(i);
  //      });
  //    }
  //  });
  //}

  /**
   * show Pharmacy Table Data (Dashboard)
   * @param {Node} container 
   */
  //function showPharmacyTableData(container) {
  //  $(container).empty();
  //  showTableHeader('.js-dashboard-table-header', pharmacyHead);
  //  $.ajax({
  //    type: 'GET',
  //    url: 'https://next.json-generator.com/api/json/get/E1FDWdnvE',
  //    success: function (data) {
  //      $.each(data, function (index, element) {
  //        i = index + 1;
  //        $(container).append('<tr>');
  //        $(container).append('<th scope="row">' + i + '</th>');
  //        $(container).append('<td>' + element.name + '</td>');
  //        $(container).append('<td>' + element.address + '</td>');
  //        $(container).append('<td>' + element.city + '</td>');
  //        $(container).append('<td>' + element.district + '</td>');
  //        $(container).append('</tr>');
  //        // console.log(i);
  //      });
  //    }
  //  });
  //}


  //$('.js-categoryProduct').on('change', function () {
  //  selectedVal = $('select[name=productype]').val();
  //  //changeContent('.js-relatedDoc');
  //  showPermissionDoc('.permission-doc__container');
  //});

  $('.js-checkBox').on('change', function () {
    $('.permission-doc').slideToggle();
  });

  // Load data table (Dashboard)
  /**
   *  Load Hospital data by default
   */
  //showHospitalTableData('.js-dashboard-table-body');
  /**
   * Load when change select Type
   */
  $('.js-dropdown-type').on('change', function () {
    console.log($(this).val());
    if ($(this).val() == 2)
      showPharmacyTableData('.js-dashboard-table-body');
    else
      showHospitalTableData('.js-dashboard-table-body');
  })

  /**
   * Load when click on Label
   */
  $('.js-show-hospital-data').on('click', function() {
    showHospitalTableData('.js-dashboard-table-body');
  });
  $('.js-show-pharmacy-data').on('click', function() {
    showPharmacyTableData('.js-dashboard-table-body');
  });
});
$('.numeric').inputmask("numeric", {
    radixPoint: ".",
    groupSeparator: ",",
    digits: 2,
    autoGroup: true,
    //prefix: '$', //No Space, this will truncate the first character
    rightAlign: false,
    oncleared: function () { /*self.Value('');*/ }
});