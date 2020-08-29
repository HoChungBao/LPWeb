////== Class definition

//var DatatableJsonRemoteCreens = function () {
//    //== Private functions

//    // basic demo
//    var demo = function () {

//        var datatable = $('.m_datatable').mDatatable({
//            // datasource definition
//            data: {
//                type: 'remote',
//                source: '/Orders/GetDataJsonContactFormPlan',
//                pageSize: 10,
//            },

//            // layout definition
//            layout: {
//                theme: 'default', // datatable theme
//                class: '', // custom wrapper class
//                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
//                footer: false // display/hide footer
//            },

//            // column sorting
//            sortable: true,

//            pagination: true,

//            search: {
//                input: $('#generalSearch')
//            },

//            // columns definition
//            columns: [{
//                field: "name",
//                title: "Tên"

//            }, {
//                field: "phone",
//                title: "Phone",
//                width: 110
//            }, {
//                field: "email",
//                title: "Email",
//                responsive: { visible: 'lg' }
//            }, {
//                field: "address",
//                title: "Address",
//                responsive: { visible: 'lg' }
//            },{
//                field: "videoLink",
//                title: "Video Link",
//                template: function (row) {
//                    return '<a href="/' + JSON.parse(row.videoLink).Url + '" target="_blank">Link<a>';
//                }

//            },{
//                field: "budgetTotal",
//                title: "Tổng ngân sách",
//                type: "any",
//                responsive: { visible: 'lg' }
//            }, {
//                field: "productName",
//                title: "Tên sản phẩm"

//            }
//                , {
//                field: "productType",
//                title: "Loại sản phẩm"

//            }
//                ,
//            {
//                field: "note",
//                title: "Ghi chú"


//            }
//                ,
//                {
//                    field: "dateCreated",
//                    title: "Ngày "


//                }
//            ,

//            {
//                field: "Actions",
//                width: 110,
//                title: "Actions",
//                sortable: false,
//                overflow: 'visible',
//                template: function (row, index, datatable) {
//                    var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
//                    return '\
//						<div class="dropdown ' + dropup + '">\
//							<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
//                                <i class="la la-ellipsis-h"></i>\
//                            </a>\
//						<div class="dropdown-menu dropdown-menu-right">\
//						    <a class="dropdown-item" href="/Orders/EditContactFormPlan/?id='+ row.id + '"><i class="la la-edit"></i> Edit Details</a>\
//						    <a class="dropdown-item" href="#" onclick="deleteContact('+ row.id + ')"><i class="la la-leaf"></i> Delete Details</a>\
//						    \
//						</div>\
//                        <a class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" href="#" data-dismiss="modal" data-code="'+row.name+","row.email+","+row.phone+","+row. +'" data-toggle="modal" data-target="#myModalCustomer"><i class="la la-edit"></i></a>\
//						</div>\
//					';
//                }
//            }]
//        });

//        var query = datatable.getDataSourceQuery();

        $('#myModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            Images = button.data('code'); // Extract info from data-*            
            $('#Images').val(Images);

        });
        $('#myModalCustomer').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            Images = button.data('code'); // Extract info from data-*            
            $('input[name="Name"]').val(Images.Name);
            $('input[name="Email"]').val(Images.Email);
            $('input[name="Phone"]').val(Images.Phone);
});
        $('#generalSearch').on('keyup', function () {
            if ($('#generalSearch').val() == "") {
                timKiem("");
                return;
            }
            var inputValue = $('#generalSearch').val().toLowerCase();
            timKiem(inputValue);
        });
        $('#m_form_type').on('change', function () {
            if ($('#m_form_type').val() == "") {
                timKiem("");
                return;
            }
            var inputValue = $('#m_form_type option:selected').text().toLowerCase();
            timKiem(inputValue);
        });

        function timKiem(kitu) {
            $("#customerTable tr").filter(function () {
                $(this).toggle($(this).text().toLowerCase().indexOf(kitu) > -1)
            });
        }
        //$('#m_form_status').on('change', function () {
        //    if ($(this).val() === "true") { //Boi vi tra ve kieu du lieu String 
        //        datatable.search(true, 'isHospital');
        //    }
        //    if ($(this).val() === "false") {
        //        datatable.search(false, 'isHospital');
        //    }
        //    if ($(this).val() === "") {
        //        datatable.search("", "isHospital");
        //    }
        //}).val(typeof query.Type !== 'undefined' ? query.Type : '');

        //$('#m_form_type').on('change', function () {
        //    datatable.search($(this).val(), 'productType');
        //}).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_status, #m_form_type').selectpicker();      
     
    //};

//    return {
//        // public functions
//        init: function () {
//            demo();
//        }
//    };
//}();

//jQuery(document).ready(function () {
//    DatatableJsonRemoteCreens.init();

//});
function deleteContact(k) {
    swal({
        title: "Are you sure?",
        text: "Bạn có muốn xóa contact này không?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'No',
        cancelButtonText: "Yes",
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function (isConfirm) {
        if (isConfirm.dismiss == "cancel") {
            $.ajax({
                url: '/Orders/DeleteContactFormPlan?id=' + k,
                type: 'GET',
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.result) {
                        swal("", response.message, "success");
                    }
                    else {
                        swal("", response.message, "warning");

                    }
                    location.reload();
                },
                error: function (err) {
                    alert("Đã bị lỗi");
                }
            })
        }
        else {
            swal("Your imaginary file is safe!");
        }
    });
}
$(document).ready(function () {
    function AjaxSubmitCreateCustomer() {
        if (checkInput().result) {
            var formData = new FormData();
            formData.append("Name", $("input[name=customerName]").val());
            formData.append("Type", $("#typeCustomer option:selected").text());
            formData.append("Email", $("input[name=Email]").val());
            formData.append("Phone", $("input[name=Phone]").val());
            formData.append("Status", $("#statusCustomer option:selected").text());
            formData.append("Contact", $('input[name=Contact]').val());
            formData.append("Note", $('textarea[name=Note]').val());
            formData.append("Pic", $("#typePic option:selected").text());
            formData.append("UserPic", $("#typePic option:selected").val());
            formData.append("Position", $('input[name=customerPosition]').val());
            $.ajax({
                url: '/Customer/InsertCustomer',
                type: 'POST',
                data: formData, // serializes the form's elements.
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.result) {
                        swal("", response.message, "success");
                        setTimeout(function () {

                            location.reload();

                            //End
                        }, 5000);
                    }
                    else {
                        swal("", response.message, "warning");

                    }

                },
                error: function (error) {
                    alert("Đã bị lỗi");
                }
            })
        }
    }
    $("#butCustomer").on("click", AjaxSubmitCreateCustomer);
});
