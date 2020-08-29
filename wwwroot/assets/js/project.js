//== Class definition
var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Outlet/GetDataProject',
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
            columns: [
                
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
						        <a class="dropdown-item" href="/Outlet/UpdateProject?id='+ row.id + '" target="_blank"><i class="la la-edit"></i> Edit Details</a>\
                            </div>\
						</div>\
                    <a href="/Outlet/ProjectDetail?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Dự án chi tiết">\
							<i class="la la-file"></i>\
						</a>\
					';
                    }
                }  
            , {
                    field: "name",
                    title: "Tên"
                }
                //}, {
            //    field: "projectType",
            //    title: "Loại dự án",
            //    template: function (row, index, datatable) {
            //        if (row.projectType) {
            //            var ProjectType = {
            //                audit: { 'title': 'Audit', 'class': 'm-badge--brand' },
            //                khaosat: { 'title': 'Khảo sát', 'class': ' m-badge--metal' },
            //                nghiemthu: { 'title': 'Nghiệm thu', 'class': 'm-badge--brand' },
            //                 Sanxuat: { 'title': 'Sản xuất', 'class': 'm-badge--brand' },
            //                Posm: { 'title': 'POSM', 'class': 'm-badge--brand' },
            //                thicong: { 'title': 'Thi công', 'class': 'm-badge--brand' },
            //                thuevitri: { 'title': 'Thuê vị trí', 'class': 'm-badge--brand' },
            //                baohanhbaotri: { 'title': 'Bảo hành/ bảo trì', 'class': 'm-badge--brand' },
            //                thaodo: { 'title': 'Tháo dỡ ', 'class': 'm-badge--brand' },
            //                thaodo: { 'all': 'Trọn gói ', 'class': 'm-badge--brand' },


            //            };
            //            return '<span class="m-badge ' + ProjectType[row.projectType].class + ' m-badge--wide">' + ProjectType[row.projectType].title + '</span>';
            //        }
            //        return "";
            //    }
            //}
                
          , {
                    field: "isCompleted",
                    title: "Trạng thái",
                    responsive: { visible: 'lg' },
                    template: function (row, index, datatable) {
                        if (row.isCompleted != "null") {
                            var IsCompleted = {
                                true: { 'title': 'Hoàn tất', 'class': 'm-badge--brand' },
                                false: { 'title': 'Đang thực hiện', 'class': ' m-badge--danger' }

                            };
                            return '<span class="m-badge ' + IsCompleted[row.isCompleted].class + ' m-badge--wide">' + IsCompleted[row.isCompleted].title + '</span>';
                        }
                        return "";
                    }
                }, {
                    field: 'customerName',
                    title: 'Tên khách hàng',
                },
                {
                    field: 'customerType',
                    title: 'Loại khách hàng',
                    width: 110,
                },
                {
                    field: 'customerPhone',
                    title: 'Điện thoại',
                    width: 130,
                },
                {
                    field: 'customerEmail',
                    title: 'Email',
                    width: 164,
                }
                ]
        });

        var query = datatable.getDataSourceQuery();

        $('#m_form_project').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "projectType");

            }
            else {
                datatable.search($(this).val(), "projectType");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');
        $('#m_form_project').selectpicker();

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