//== Class definition

var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Common/GetDataMediHubAsset',
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
                field: "name",
                title: "Tên"

            }, {
                field: "specicality",
                title: "Khu vực"
            }
            , {
            field: "address",
            title: "Địa chỉ",
            width: 110
            },
            {
                field: "contractType",
                title: "Loại hợp đồng",
            },{
            field: "tvbrand",
            title: "Loại TV",
            responsive: { visible: 'lg' }
            }, {
                field: "tvsize",
                title: "Kích cỡ TV",
                responsive: { visible: 'lg' }
            }
            , 
            {
                field: "seri",
                title: "Seri",            
            },
            {
                field: "box",
                title: "Box",               
            },
            {
                field: "timer",
                title: "Timer",                             
            },{
                field: "createdDate",
                title: "Ngày tạo",  
                template: function (row, index, datatable) {
                    var date = new Date(row.createdDate);
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }
            },{
                field: "userUpdated",
                title: "User",              
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
                        <div class="dropdown ' + dropup + '">\
                            <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                            <i class="la la-ellipsis-h"></i>\
                            </a>\
                        <div class="dropdown-menu dropdown-menu-right">\
                            <a class="dropdown-item" href="/Common/UpdateMediHubAsset?id='+ row.id + '"><i class="la la-cog"></i>Update</a>\
                            <a class="dropdown-item" href="/Common/CreateDsPlayer?id='+ row.id + '&name=' + row.box + '&address=' + row.address + '"><i class="la la-edit"></i>DsPlayer</a>\
                            <a class="dropdown-item" href="/Common/DsPlayerFromMediHubAsset?id='+ row.id + '"><i class="la la-file"></i>Danh sách DsPlayer</a>\
                        </div>\
                        </div>\
                        <a href="#" onclick="deleteMediHub('+ row.id + ')" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Xóa">\
                        <i class="la la-remove"></i>\
                        </a>'
                        ;
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

        $('#m_form_type').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "name");
            }
            else {
                datatable.search($(this).val(), "name");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_123').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "durations");
            }
            else {
                datatable.search($(this).val(), "durations");
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
}();

jQuery(document).ready(function () {
    DatatableJsonRemoteCreens.init();
});
