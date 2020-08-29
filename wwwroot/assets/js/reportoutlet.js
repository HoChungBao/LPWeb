//== Class definition
getTable();
var datatable;
var value=[];
function DatatableJsonRemoteCreens() {
    //== Private functions

    // basic demo
    var demo = function () {

        datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'local',
                source: value,
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
                field: "drugName",
                title: "Tên nhà thuốc"
            },
                {
                    field: "customerName",
                    title: "Khách hàng"
                },
                {
                    field: "projectName",
                    title: "Dự án"
                },
                {
                    field: "label",
                    title: "Nhãn hiệu"
                },
                {
                    field: "component",
                    title: "Hạng mục"
                },
               {
                field: "district",
                title: "Quận / huyện"

            }, {
                field: "province",
                title: "TP / tỉnh"
            }
                , {
                field: "drugStoreAddress",
                title: "Địa chỉ",
                width: 110
            }, {
                field: "area",
                title: "Khu vực"

            },
            {
                field: "storeOwner",
                title: "Tên chủ Nhà thuốc"
            },
            {
                field: "storePhoneNumber",
                title: "Di động"

            },
            {
                field: "mediGroupCode",
                title: "Mã MediGroup"
            },
            {
                field: "position",
                title: "Diện tích"
            },
            {
                field: "unitCost",
                title: "Giá"
            },
            {
                field: "numOfArea",
                title: "Diện tích"
            },
            {
                field: "bankAccount",
                title: "Tài khoản ngân hàng",
                template: function (row, index, datatable) {
                    if (row.bankAccount) {
                        var bank = JSON.parse(row.bankAccount);
                        return bank.AccountName + '</br>' + bank.AccountNumber + '</br>' + bank.BankName + '</br>' + bank.BankBranch;
                    }
                    return "";
                }
            },
            {
                field: "status",
                title: "Trạng thái",
                width: 116,
                template: function (row, index, datatable) {
                    if (row.status) {
                        var status = {
                            1: { 'title': 'Hoạt động', 'class': 'm-badge--brand' },
                            0: { 'title': 'Không hoạt động', 'class': ' m-badge--danger' },

                        };
                        return '<span class="m-badge ' + status[row.status].class + ' m-badge--wide">' + status[row.status].title + '</span>';
                    }
                    return "";
                }
            },
           {
                field: "image",
                title: "Image",
                template: function (row, index, datatable) {
                    if (row.avatarUrl) {
                        var img = JSON.parse(row.avatarUrl);
                        if (img.length > 0) {
                            var html = [];
                            for (var i = 0; i < img.length; i++) {
                                html.push(img[i].Url);
                            }
                            return "<a href='#' onclick=imageshow('" + JSON.stringify(html) + "')>Image</a>";
                        }
                        return "";
                    }
                    return "";
                }
            },
            {
                field: "note",
                title: "Ghi chú",
                responsive: { visible: 'lg' }
            },
            {
                field: "Actions",
                width: 110,
                title: "Actions",
                sortable: false,
                overflow: 'visible',
                template: function (row, index, datatable) {
                    var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                    return '\
						<a href="/Outlet/UpdateOutletStore?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update" target="_blank">\
                        <i class="la la-edit" ></i >\
                        </a >\
                        <a href="/Outlet/UpdateProject?id='+ row.projectId + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Chi tiết dự án" target="_blank">\
                        <i class="la la-folder" ></i >\
                        </a >\
                        <a href = "/Outlet/DrugProject?id='+ row.customerId + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title = "Khách hàng" target = "_blank" >\
                        <i class="la la-user" ></i >\
                        </a > '
                        ;
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

        //$('#m_form_type').on('change', function () {
        //    if ($(this).val() === "") {
        //        $('#m_form_district').change();
        //        datatable.search("", "province");

        //    }
        //    else {
        //        //toLowerCase();
        //        $('#m_form_district').change();
        //        datatable.search(change($('#m_form_type option:selected').text()).toUpperCase(), "province");
        //    }
        //}).val(typeof query.Type !== 'undefined' ? query.Type : '');
        //$('#m_form_district').on('change', function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "district");

        //    }
        //    else {
        //        datatable.search(change($('#m_form_123 option:selected').text()).toUpperCase(), "district");
        //    }
        //}).val(typeof query.Type !== 'undefined' ? query.Type : '');
        //$('#m_form_label').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "label");

        //    }
        //    else {
        //        datatable.search($(this).val(), "label");
        //    }
        //});
        //$('#m_form_customer').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "customerName");

        //    }
        //    else {
        //        datatable.search($(this).val(), "customerName");
        //    }
        //});
        //$('#m_form_outlet').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "type");

        //    }
        //    else {
        //        datatable.search($(this).val(), "type");
        //    }
        //});
        //$('#m_form_cost').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "unitCost");

        //    }
        //    else {
        //        datatable.search($(this).val(), "unitCost");
        //    }
        //});
        //$('#m_form_time').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "dateBeginRent");

        //    }
        //    else {
        //        datatable.search($(this).val(), "dateBeginRent");
        //    }
        //});
        //$('#m_form_position').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "position");

        //    }
        //    else {
        //        datatable.search($(this).val(), "position");
        //    }
        //});
        //$('#m_form_component').change(function () {
        //    if ($(this).val() === "") {
        //        datatable.search("", "component");

        //    }
        //    else {
        //        datatable.search($(this).val(), "component");
        //    }
        //});
        $('#m_form_type, #m_form_district, #m_form_label, #m_form_customer,#m_form_outlet,#m_form_cost,#m_form_time,#m_form_position,#m_form_component').selectpicker();
    };
    demo();
    //return {
    //    // public functions
    //    init: function () {
    //        demo();
    //    }
    //};
}

jQuery(document).ready(function () {
    //value = [];
    //DatatableJsonRemoteCreens();
    //console.log(datatable);
});

function getDataReport() {
    $("#report").addClass("m-loader m-loader--light m-loader--right disable");
    var formData = new FormData();
    if ($("#m_form_city").val() != "") {
        formData.append("Province", $("#m_form_city option:selected").text());
    }
    if ($("#m_form_district").val() != "") {
        formData.append("District", $("#m_form_district option:selected").text());
    }
    formData.append("Position", $("#m_form_position").val());
    formData.append("DateBeginRent", $("#m_form_time").val());
    formData.append("CustomerId", $("#m_form_customer").val());
    formData.append("CostPayForDrugStore", $("#m_form_cost").val());
    formData.append("Label", $("#m_form_label").val());
    formData.append("Component", $("#m_form_component").val());
    formData.append("Type", $("#m_form_outlet").val());
    $.ajax({
        url: '/Outlet/GetDataReportOutlet',
        type: 'POST',
        data: formData, // serializes the form's elements.
        contentType: false,
        processData: false,
        success: function (response) {
            $("#report").removeClass("m-loader m-loader--light m-loader--right disable");
            value = response;
            if (response.length == 0) {
                value = [{}];
            }
            if (datatable == undefined) {
                DatatableJsonRemoteCreens();
                
            }
            else {
                $("#reloadTable").html("");
                getTable();
                DatatableJsonRemoteCreens();
            }
        },
        error: function (error) {
            $("#report").removeClass("m-loader m-loader--light m-loader--right disable");
            alert("Đã bị lỗi");
        }
    })
}
function getTable() {
    $("#reloadTable").append("<div class='m_datatable' id='scrolling_vertical'></div>");
}