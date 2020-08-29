//== Class definition
var DatatableJsonRemoteOrders = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Orders/GetOrders?keySearch=&pageIndex&pageNum',
                pageSize: 10,
            },

            // layout definition
            layout: {
                theme: 'default', // datatable theme
                class: '', // custom wrapper class
                scroll: false, // enable/disable datatable scroll both horizontal and vertical when needed.
                footer: false // display/hide footer
            },

            // column sorting
            sortable: true,

            pagination: true,

            search: {
                input: $('#generalSearch')
            },

            // columns definition
            columns: [{
                field: "id",
                title: "Order ID"
            }, {
                field: "customerName",
                title: "Customer Name"
                //template: function (row) {
                //	// callback function support for column rendering
                //	return row.ShipCountry + ' - ' + row.ShipCity;
                //}
            }, {
                field: "phone",
                title: "Phone",
                width: 110
            }, {
                field: "email",
                title: "Email",
                responsive: { visible: 'lg' }
            }, {
                field: "address",
                title: "Address",
                responsive: { visible: 'lg' }
            }, {
                field: "createDateFormat",
                title: "Create Date",
                type: "date"
                ,
                format: "DD-MM-YYYY"
            }, {
                    field: "Status",
                title: "Status",

                template: function (row) {
                	var status = {
                		"New": {'title': 'Mới', 'class': 'm-badge--brand'},
                        "Proccessing": { 'title': 'Proccessing', 'class': ' m-badge--metal'},
                        "Success": { 'title': 'Success', 'class': ' m-badge--success' },
                        "Air": { 'title': 'Air', 'class': ' m-badge--success' },
                        "RequireAir": { 'title': 'RequireAir', 'class': ' m-badge--info'},
                        "Danger": { 'title': 'Sắp hết hạn hợp đồng', 'class': ' m-badge--danger'},
                        "Pending": { 'title': 'Pending', 'class': ' m-badge--pending' },
                        "NoAuthorization": { 'title': 'Cần xin giấy phép', 'class': ' m-badge--primary' },
                        "Delete": { 'title': 'Đã xóa', 'class': ' m-badge--danger' },
                	};
                	return '<span class="m-badge'  + status[row.status].class +  'm-badge--wide">' + status[row.status].title + '</span>';
                }
            }, {
                field: "productType",
                title: "Product Type",
                // callback function support for column rendering
                //template: function (row) {
                //	var status = {
                //		1: {'title': 'Online', 'state': 'danger'},
                //		2: {'title': 'Retail', 'state': 'primary'},
                //		3: {'title': 'Direct', 'state': 'accent'}
                //	};
                //	return '<span class="m-badge m-badge--' + status[row.Type].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + status[row.Type].state + '">' + status[row.Type].title + '</span>';
                //}
            },
            {
                field: "productName",
                title: "Product Name",
                responsive: { visible: 'lg' }
            }, {
                field: "startDateFormat",
                title: "Start Date",
                type: "date",
                format: "DD/MM/YYYY"
            }
                , {
                field: "endDateFormat",
                title: "End Date",
                type: "date",
                format: "DD/MM/YYYY"
            },
            {
                field: "totalMoneyFormat",
                title: "Total Money"
            },
            {
                field: "Actions",
                width: 110,
                title: "Actions",         
                sortable: false,
                overflow: 'visible',
                template: function (row, index, datatable) {
                    var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                    var customer = '<a href="#" title="Phân công" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" data-dismiss="modal" data-toggle="modal" data-target="#myModalCustomer" data-code="' + row.email + '" data-code2="' + row.customerName + '" data-code3="' + row.phone + '" > <i class="la la-edit"></i></a >';
                    if (!manager())
                        customer = "";
                    return '<div class="dropdown ' + dropup + '">\
							<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown"><i class="la la-ellipsis-h"></i></a>\
						  	<div class="dropdown-menu dropdown-menu-right">\
						    	<a class="dropdown-item" href="/Orders/OrderDetails/?id='+row.id+'"><i class="la la-edit"></i> Edit Details</a>\
						    	<a href="#" class="dropdown-item js-slideToggle" data-dismiss="modal" data-toggle="modal" data-target="#myModel" data-code="'+ row.id + '"data-code2="' + row.productName + '"data-code3="' + row.status + '"><i class="la la-leaf"></i> Update Status</a>\
						    	<a class="dropdown-item" href="#"><i class="la la-print"></i>Generate Report</a>\
						  	</div>\
						</div>\
						<a href="/Orders/OrderDetails/?id='+ row.id +'" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="OrderDetails">\
							<i class="fa fa-eye"></i>\
						</a>\
						<a href="#" onClick = "ajaxDeleteOrderForm('+ row.id +');" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Xóa đơn hàng">\
							<i class="fa fa-remove"></i>\
						</a>'+customer
					;
                }
            }],      
            fnCreatedRow: function (nRow, aData, iDataIndex) {
                console.log('DataTables has finished its initialisation.');       
            }
        });


        var orderId;
        var productName;
        var status;
        var query = datatable.getDataSourceQuery();

        $('#m_form_status').on('change', function () {
            datatable.search($(this).val(), 'status');
        }).val(typeof query.Status !== 'undefined' ? query.Status : '');

        $('#m_form_type').on('change', function () {
            datatable.search($(this).val(), 'productType');
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_status, #m_form_type').selectpicker();

        $('#myModel').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            orderDetailId = button.data('code'); // Extract info from data-*
            productName = button.data('code2');
            status = button.data('code3');
            $("#productName").val(productName);
            $("#statusUpdate").val(status);
            $("#idUpdate").val(orderDetailId);
        });
        $('#myModalCustomer').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            Email = button.data('code'); // Extract info from data-*
            Name = button.data('code2');
            Phone = button.data('code3');     
            $('input[name="Email"]').val(Email);
            $('input[name="customerName"]').val(Name);
            $('input[name="Phone"]').val(Phone);
        });

        $("#button-edit").click(function () {
            status = $("#statusUpdate").val();
            orderId = $("#idUpdate").val();
            //var formData = $("#form-order").serialize();

            $.ajax({
                url: '/Orders/UpdateStatusOrder?id=' + orderId + '&status=' + status,
                //data: formData,
                type: 'Get',
                success: function (response) {
                    if (response != null) {
                        if (!response.result) {
                            swal("Update fail", response.message, "warning");
                        } else {
                            
                            
                            swal("Update Success", response.message, "success");
                            setTimeout(function () {

                                window.location.reload();
                                //End
                            }, 3000);
                            
                        }
                    }

                },
                error: function (response) {


                }

            })
        });

    };

    return {
        // public functions
        init: function () {
            demo();
        }
    };
}();

jQuery(document).ready(function () {
    DatatableJsonRemoteOrders.init();
});

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
                    $('#myModalCustomer').hide();
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
$("#butCustomer").on("click", AjaxSubmitCreateCustomer);