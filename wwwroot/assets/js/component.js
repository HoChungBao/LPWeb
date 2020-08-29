//== Class definition

var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Outlet/GetDataComponent',
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
                width: 110,
                template: function (row, index, datatable) {
                    return index + 1;
                }
            },
            {
                field: "name",
                title: "Tên"

            },
            {
                field: "slug",
                title: "Slug",

            }, {
                field: "createdBy",
                title: "User"
                },
                {
                    field: "updatedBy",
                    title: "User Update"
                },
            {
                field: "isDeleted",
                title: "IsDeleted",
                width: 110,
                template: function (row, index, datatable) {
                    if (row.isDeleted!='null') {
                        var IsDeleted = {
                            true: { 'title': 'true', 'class': 'm-badge--brand' },
                            false: { 'title': 'false', 'class': ' m-badge--danger' },

                        };
                        return '<span class="m-badge ' + IsDeleted[row.isDeleted].class + ' m-badge--wide">' + IsDeleted[row.isDeleted].title + '</span>';
                    }
                    return "";
                }
            },
            {
                field: "createdDate",
                title: "Ngày tạo",
                template: function (row, index, datatable) {
                    if (row.createdDate) {
                        var date = new Date(row.createdDate);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    }
                    return "";
                }
            }
                ,
                {
                    field: "updatedDate",
                    title: "Ngày update",
                    template: function (row, index, datatable) {
                        if (row.updatedDate) {
                            var date = new Date(row.updatedDate);
                            var month = date.getMonth() + 1;
                            return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                        }
                        return "";
                    }
                }
                ,
            {
                field: "Actions",
                width: 110,
                title: "Actions",
                sortable: false,
                overflow: 'visible',
                template: function (row, index, datatable) {
                    var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                    return '<a href="/Drug/UpdateComponent?id=' + row.id +'" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update">\
                            <i class="flaticon-edit"></i>\
                                </a >'
                        ;
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

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