//== Class definition

    function DatatableJsonRemoteCreens(value) {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: value,
                pageSize: 20,
            },

            // layout definition
            layout: {
                theme: 'default', // datatable theme
                class: '', // custom wrapper class
                scroll: true, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false, // display/hide footer
                height: 500,
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#generalSearch')
            },

            // columns definition
            columns: [
     //           {
     //               field: "Actions",
     //               width: 155,
     //               title: "Actions",
     //               sortable: false,
     //               locked: { left: 'xl' },
     //               overflow: 'visible',
     //               template: function (row, index, datatable) {
     //                   var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
     //                   return '\
     // <div class="dropdown '+ dropup + '">\
     //  <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
     //                           <i class="la la-ellipsis-h"></i>\
     //                       </a>\
     //    <div class="dropdown-menu dropdown-menu-left">\
     //      <a class="dropdown-item" href="#"><i class="la la-edit"></i> Edit Details</a>\
     //      <a class="dropdown-item" href="#"><i class="la la-leaf"></i> Update Status</a>\
     //      <a class="dropdown-item" href="#"><i class="la la-print"></i> Generate Report</a>\
     //    </div>\
     // </div>\
     // <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Edit details">\
     //  <i class="la la-edit"></i>\
     // </a>\
     // <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Delete">\
     //  <i class="la la-trash"></i>\
     // </a>\
     //';
     //               }
     //           }, 
                {
                    field: "Actions",
                    width: 110,
                    title: "Actions",
                    sortable: false,
                    locked: { left: 'xl' },
                    overflow: 'visible',
                    template: function (row, index, datatable) {
                        var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                        return '\
						<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-dismiss="modal" data-toggle="modal" data-target="#myModalCustomer" data-code="'+ row.id + '" data-code2="' + row.name + '" title="Ghi chú">\
							<i class="flaticon-edit"></i>\
						</a>\
						<a href="/Customer/HistoryById?Id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Chi tiết ghi chú" target="_blank">\
							<i class="la la-file"></i>\
						</a>\
                            <div class="dropdown ' + dropup + '">\
                            <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                            <i class="la la-ellipsis-h"></i>\
                            </a>\
                            <div class="dropdown-menu dropdown-menu-right">\
                            <a class="dropdown-item" href="/Customer/EditCustomer?Id='+ row.id +'"><i class="la la-edit"></i>Sửa Khách Hàng</a>\
                            <a class="dropdown-item" href="/Customer/ContractById?id='+ row.id+ '&userId=' + row.userName +  '"><i class="la la-edit"></i>Hợp Đồng</a>\
                            <a class="dropdown-item" href="/Orders/CreateNewOrder?name='+ row.name + '&email=' + row.email + '&phone=' + row.phone + '"><i class="la la-edit"></i>Tạo mới Order</a>\
                            </div>\
                            </div>\
					';
                    }
                },
                {
                field: "name",
                    title: "Client - Brand"

            },
                {
                    field: "pic",
                    title: "Pic"

                },
                {
                field: "type",
                title: "Category"

            }
                , {
                field: "phone",
                title: "Phone",
                width: 110
            }, {
                field: "email",
                title: "Email",
                    responsive: { visible: 'lg' },
                    width: 230
            }, {
                field: "contact",
            title: "Contact Person",
                responsive: { visible: 'lg' }
                }
                , {
                    field: "position",
                    title: "Position",
                    responsive: { visible: 'lg' }
                }
                , {
                field: "status",
                title: "Status",
                responsive: { visible: 'lg' }
            },
                {
                    field: "dateUpdated",
                    title: "INTERACTION (last day)",
                    template: function (row, index, datatable) {
                        if (row.dateUpdated) {
                            var date = new Date(row.dateUpdated);
                            var month = date.getMonth() + 1;
                            return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear() + " " + date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
                        }
                        return "";
                    }

                },
            {
                field: "dayCall",
                title: "THE DAY SHOULD CALL",
                template: function (row, index, datatable) {
                    if (row.dayCall) {
                    var date = new Date(row.dayCall);
                    var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear()
                    }
                    return "";
                }

            },
            {
                field: "unevenDay",
                title: "UNEVEN DAY",
                template: function (row, index, datatable) {
                    if (row.unevenDay) {
                        var day = row.unevenDay;
                        if (day < 4) {
                            return '<span class="btn m-btn--icon m-btn--air" style="background-color: #ff0000; color:white">' + day + '</div>';
                        }
                            return '<span class="btn m-btn--icon m-btn--air" style="background-color: #41cc44; color:white">' + day + '</div>';
                    }
                    return "";
                }


            },{
                field: "note",
                title: "Note"

            }]
        });

        var query = datatable.getDataSourceQuery();

        $('#myModalCustomer').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            id = button.data('code'); // Extract info from data-*         
            name = button.data('code2');
            $('#idCustomer').val(id);
            $('#nameCustomer').val(name);

        });

        $('#m_form_type').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "type");
            }
            else {
                datatable.search($(this).val(), "type");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_123').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "pic");
            }
            else {
                datatable.search($(this).val(), "pic");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_type, #m_form_123').selectpicker();

    };

        return {
            // public functions
            init: function () {
                demo();
            }
        };
};

function deleteCustomer(k) {
    swal({
        title: "Are you sure?",
        text: "Bạn có muốn xóa khách hàng này không? Tất cả dữ liệu trong này đều xóa hết",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'Có',
        cancelButtonText: "Không",
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function (isConfirm) {
        if (isConfirm.value) {
            $.ajax({
                url: '/Customer/DeleteCustomer?id=' + k,
                type: 'GET',
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
    function AjaxSubmitCreateHistory() {
        if (checkInput1().result) {
            var formData = new FormData();
            formData.append("CustomerId", $("#idCustomer").val());
            formData.append("Name", $("#nameCustomer").val());
            formData.append("Note", $("#noteCustomer").val());        
            $.ajax({
                url: '/Customer/InsertHistoryCustomer',
                type: 'POST',
                data: formData, // serializes the form's elements.
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.result) {
                        swal("", response.message, "success");
                    }
                    else {
                        swal("", response.message, "warning");

                    } setTimeout(function () {

                        location.reload();

                        //End
                    }, 5000);

                },
                error: function (error) {
                    alert("Đã bị lỗi");
                }
            })
        }
    }
    $("#butCustomer").on("click", AjaxSubmitCreateHistory);
});