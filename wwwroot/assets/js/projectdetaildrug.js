//== Class definition
function DatatableJsonRemoteCreens(value) {
    //== Private functions

    // basic demo
    var demoData = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Outlet/GetDataDrugProjectDetail?medigroup=' + value,
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
                title: "Id",
                template: function (row, index, datatable) {
                    return index + 1;
                },
                width: 20,
            },
                {
                    field: "drugName",
                    title: "Tên nhà thuốc"

                },{
                    field: "projectName",
                title: "Tên dự án"

            }, {
                field: "address",
                title: "Địa chỉ",
            }
                , {
                field: "ward",
                title: "Phường",
                width: 110,
            },
            {
                field: "district",
                title: "Quận",
                width: 110,
            },
            {
                field: "province",
                title: "Tỉnh",
                width: 110,
            },
            {
                field: "area",
                title: "Khu vực",
                width: 110,
            },
            {
                field: "label",
                title: "Nhãn hàng",
                width: 110,
            },
            {
                field: "component",
                title: "Hạng mục",
                width: 110,
            },
            {
                field: "position",
                title: "Vị trí",
                width: 110,
            },
            {
                field: "hsize",
                title: "Chiều ngang",
                width: 110,
                template: function (row, index, datatable) {
                    if (row.hsize) {
                        return row.hsize ;
                    }
                    return "";
                }
            },
            {
                field: "vsize",
                title: "Chiều dọc",
                width: 110,
                template: function (row, index, datatable) {
                    if (row.vsize) {
                        return row.vsize;
                    }
                    return "";
                }
            },
            {
                field: "numOfArea",
                title: "Diện tích",
                width: 110,
            },
            {
                field: "costPayForDrugStore",
                title: "Chi phí nhà thuốc",
                width: 110,
            },
            {
                field: "costPayForProduction",
                title: "Chi phí sản xuất",
                width: 110,
            },
            {
                field: "dateBeginRent",
                title: "Ngày thuê",
                width: 110,
                template: function (row, index, datatable) {
                    if (row.dateBeginRent) {
                        var date = new Date(row.dateBeginRent);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    }
                    return "";
                }
            },
            {
                field: "monthRent",
                title: "Thời gian thuê",
                width: 110,
            },

            {
                field: "mediGroupCode",
                title: "Mã NT",
                width: 110,
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
						<div class="dropdown ' + dropup + '">\
							<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                <i class="la la-ellipsis-h"></i>\
                            </a>\
						    <div class="dropdown-menu dropdown-menu-right">\
						        <a class="dropdown-item" href="/Drug/UpdateProjectDetail?id='+ row.id + '" target="_blank"><i class="la la-edit"></i> Edit Details</a>\
                            </div>\
						</div>\
                        <a href="/Outlet/UpdateProject?id='+ row.projectId + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Dự án" target="_blank">\
							<i class="la la-file"></i>\
						</a>\
                        <a href = "/Outlet/UpdateProject?id='+ row.projectCustomerId + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title = "Khách hàng" target="_blank">\
                        <i class="la la-user"></i>\
						</a >';
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

        $('#m_form_province').on('change', function () {
            if ($(this).val() === "") {
                $('#m_form_district').change();
                datatable.search("", "province");

            }
            else {
                $('#m_form_123').change();
                datatable.search(change($('#m_form_province option:selected').text()).toUpperCase(), "province");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');
        $('#m_form_district').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "district");

            }
            else {
                datatable.search(change($('#m_form_district option:selected').text()).toUpperCase(), "district");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');
        $('#m_form_province, #m_form_district, #m_form_type_drug, #m_form_status').selectpicker();

    };
    demoData();
    //return {
    //    // public functions
    //    init: function () {
    //        demo();
    //    }
    //};
}