//== Class definition

var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Orders/GetDataContact',
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
                }              
            }, {
                field: "name",
                    title: "Tên"

            }, {
                    field: "email",
                    title: "Email"
            }
                , {
                    field: "address",
                    title: "Địa chỉ",
                width: 110
            }, {
                    field: "phone",
                    title: "Số điện thoại",
                responsive: { visible: 'lg' }
            }, {
                    field: "productId",
                    title: "Loại sản phẩm",
                responsive: { visible: 'lg' }
            }
                , {
                    field: "note",
                    title: "Ghi chú",
                responsive: { visible: 'lg' }
            }, {
                    field: "status",
                    title: "Trạng thái",
                    template: function (row, index, datatable) {
                        if (row.status) {
                            var status = {
                                1: { 'title': 'Thành công', 'class': 'm-badge--brand' },
                                0: { 'title': 'Mới', 'class': ' m-badge--danger' }

                            };
                            return '<span class="m-badge ' + status[row.status].class + ' m-badge--wide">' + status[row.status].title + '</span>';
                        }
                        return "";
                    }

            },            
            {
                field: "Actions",
                width: 110,
                title: "Actions",
                sortable: false,
                overflow: 'visible',
                template: function (row, index, datatable) {               
                    var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                    var html = "";
                    if (row.status=="0") {
                        html = '<a href="/Orders/UpdateStatusContactForm?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update Status">\
                            <i class="la la-cog"></i>\
                            </a>\''
                            ;
                    }
                    return '\
						<a href="/Orders/UpdateContactForm?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update" target="_blank">\
                        <i class="la la-edit" ></i >\
                        </a >\
                        <a href="#" onclick="deleteContract('+ row.id+')" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Xóa">\
                        <i class="la la-remove"></i>\
                        </a>\ '+ html
					;
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

        $('#m_form_type').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "productId");
            }
            else {
                datatable.search($(this).val(), "productId");
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
