//== Class definition
var DatatableChildDataLocalDemo = function () {
    //== Private functions

    var subTableInit = function (e) {
        $('<div/>').attr('id', 'child_data_local_' + e.data.id).appendTo(e.detailCell).mDatatable({
            data: {
                type: 'local',
                source: e.data.airContentDetail,
                pageSize: 10,
            },

            // layout definition
            layout: {
                theme: 'default',
                scroll: true,
                height: 300,
                footer: false,

                // enable/disable datatable spinner.
                spinner: {
                    type: 1,
                    theme: 'default',
                },
            },

            sortable: true,

            // columns definition
            columns: [
                {
                    field: 'id',
                    title: 'Id',
                    sortable: false,
                    template: function (row, index, datatable) {
                        return index + 1;
                    },
                    width:130
                }, {
                    field: 'name',
                    title: 'Tên bệnh viện',                
                }, {
                    field: 'status',
                    title: 'Trạng thái',
                    responsive: { visible: 'lg' },
                    template: function (row, index, datatable) {
                        if (row.status) {
                            var status = {
                                1: { 'title': 'Đã air', 'class': 'm-badge--brand' },
                                0: { 'title': 'Chưa air', 'class': ' m-badge--metal' }

                            };
                            return '<span class="m-badge ' + status[row.status].class + ' m-badge--wide">' + status[row.status].title + '</span>';
                        }
                        return "";
                    },
                },
                    {
                    field: "Actions",
                    width: 110,
                    title: "Actions",
                    sortable: false,
                    overflow: 'visible',
                    template: function (row, index, datatable) {
                        var dropup = (datatable.getPageSize() - index) <= 4 ? 'dropup' : '';
                        return '<a href="/common/UpdateAirContentDetail?id=' + row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Update Air">\
							<i class="la la-edit"></i>\
						</a>\
						<a href="/common/DeleteAirContentDetail?id=' + row.id + '"  class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Delete">\
							<i class="la la-cog"></i>\
						</a>\
					';
                    }
                },],
        });

    };

    // demo initializer
    var mainTableInit = function () {       

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Common/GetDataAirContent',
                pageSize: 10, // display 20 records per page
            },

            // layout definition
            layout: {
                theme: 'default',
                scroll: false,
                height: null,
                footer: false,
            },

            sortable: true,

            filterable: false,

            pagination: true,

            detail: {
                title: 'Load sub table',
                content: subTableInit,
            },

            search: {
                input: $('#generalSearch'),
            },

            // columns definition
            columns: [{
                field: "id",
                title: "Id",
                template: function (row, index, datatable) {
                    return index + 1;
                },
                width: 20,
            }, {
                field: "name",
                title: "Tên"

            }, {
                field: "startDate",
                title: "Ngày bắt đầu",
                template: function (row, index, datatable) {
                    if (row.startDate) {
                        var date = new Date(row.startDate);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    }
                    return "";
                }

            }
                , {
                field: "endDate",
                title: "Ngày kết thúc",
                width: 110,
                template: function (row, index, datatable) {
                    if (row.endDate) {
                        var date = new Date(row.endDate);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    }
                    return "";
                }
            }, {
                field: "type",
                title: "Loại nội dung",
                responsive: { visible: 'lg' },
                    template: function (row, index, datatable) {
                        if (row.type) {
                            var status = {
                                NOIDUNGBV: { 'title': 'Nội dụng bệnh viện', 'class': 'm-badge--brand' },
                                QUANGCAO: { 'title': 'Quảng cáo', 'class': ' m-badge--metal' }

                            };
                            return status[row.type].title;
                        }
                        return "";
                }
            }, {
                field: "durations",
                title: "Thời lượng",
                responsive: { visible: 'lg' },
                template: function (row, index, datatable) {                  
                    return row.durations+ " giây"
                }
            },
            {
                field: "brand",
                title: "Nhãn hàng",
                responsive: { visible: 'lg' },
            },
            {
                field: "createdDate",
                title: "Ngày tạo",
                responsive: { visible: 'lg' },
                template: function (row, index, datatable) {
                    if (row.createdDate) {
                        var date = new Date(row.createdDate);
                        var month = date.getMonth() + 1;
                        return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                    }
                    return "";
                }
            }, {
                field: "status",
                title: "Trạng thái",
                responsive: { visible: 'lg' },
                template: function (row, index, datatable) {
                    if (row.status) {
                        var status = {
                            1: { 'title': 'Đã air', 'class': 'm-badge--brand' },
                            0: { 'title': 'Chưa air', 'class': ' m-badge--metal' }

                        };
                        return '<span class="m-badge ' + status[row.status].class + ' m-badge--wide">' + status[row.status].title + '</span>';
                    }
                    return "";
                }
             },
                {
                    field: "files",
                    title: "Files",
                    responsive: { visible: 'lg' },
                    template: function (row, index, datatable) {
                        var f = JSON.parse(row.files);
                        var html = "";
                        //if (f != null) {
                        //    for (var i = 0; i < f.length; i++) {
                        //        html += '<a href="/' + f[i].Url + '" target="_blank">' + f[i].FileName + '</a></br>';
                        //    }                          
                        //}
                        return html;
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
                    return '\
						<div class="dropdown ' + dropup + '">\
							<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                <i class="la la-ellipsis-h"></i>\
                            </a>\
						    <div class="dropdown-menu dropdown-menu-right">\
						        <a class="dropdown-item" href="/common/UpdateAirContent?id='+ row.id + '"><i class="la la-edit"></i> Edit Details</a>\
                            </div>\
						</div>\
                        <a href="/common/DeleteAirContent/'+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update">\
							<i class="la la-cog"></i>\
						</a>\
					';
                }
            }]
        });
        var query = datatable.getDataSourceQuery();      

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
                datatable.search("", "durations");
            }
            else {
                datatable.search($(this).val(), "durations");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_type, #m_form_123').selectpicker();

    };

    return {
        //== Public functions
        init: function () {
            // init dmeo
            mainTableInit();
        },
    };
}();

jQuery(document).ready(function () {
    DatatableChildDataLocalDemo.init();
});