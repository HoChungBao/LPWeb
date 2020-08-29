//== Class definition

var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Common/GetDataUser',
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
                    field: "userCode",
                    title: "Mã nhân viên"

                },
                {
                field: "name",
                title: "Tên"

                }, {
                    field: "companyName",
                    title: "Tên bộ phận",
                    width: 160
                },
                {
                    field: "userName",
                    title: "Email",
                    width: 180
                },
                {
                    field: "noContract",
                    title: "Mã hợp đồng"

                },
                {
                    field: "contractType",
                    title: "Loại hợp đồng",
                    template: function (row, index, datatable) {
                        if (row.contractType) {
                            if (row.contractType!=0)
                                return row.contractType + " năm";
                            return "Không xác định";
                        }
                        return "Thử việc";
                    }

                },
                {
                    field: "tghd",
                    title: "Thời hạn hợp đồng",
                    width: 140

                },
                {
                    field: "date",
                    title: "Gia hạn",
                    width: 80,
                    template: function (row, index, datatable) {
                        var i = {
                            0: { 'title': '', 'class': 'm-badge--brand' },
                            1: { 'title': '', 'class': ' m-badge--danger' },
                        };
                        if (row.tghd) {
                            var d = row.tghd.split("-");
                            if (d.length>1) {
                                var from = d[1].split("/");
                                var date = new Date(from[1] + "/" + from[0] + "/" + from[2]);
                                var day = datediff(new Date(), date);
                                if (day > 7) {
                                    return '<span class="m-badge ' + i[0].class + ' m-badge--wide">'+day+' ngày</span>';
                                }
                                else {
                                    return '<span class="m-badge ' + i[1].class + ' m-badge--wide">'+day+'ngày</span>';
                                }

                            }
                        }
                        return '<span class="m-badge m-badge--brand m-badge--wide">...... </span>';
                    }

                },
               {
                    field: "phoneNumber",
                    title: "Điện thoại",
                    width: 130
                }
                , {
                    field: "male",
                    title: "Giới tính",
                    width: 110,
                    template: function (row, index, datatable) {
                        if (row.male) {
                        var i = {
                           0: { 'title': 'Nam', 'class': 'm-badge--brand' },
                           1: { 'title': 'Nữ', 'class': ' m-badge--danger' },
                        };
                            return '<span class="m-badge ' + i[row.male].class + ' m-badge--wide">' + i[row.male].title + '</span>';
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
                    if (row.lock) {
                        html = '<a href="#" onclick=deleteUser("' + row.userName + '") class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Nghỉ việc">\
                        <i class="la la-remove" ></i >\
                        </a>' ;
                    }
                    return '\
						<a href="/Account/MyProfileAdmin?id='+ row.userName + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Sửa" target="_blank">\
                        <i class="la la-edit" ></i >\
                        </a>'+ html
                        ;
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

        $('#m_form_type').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "lock");
            }
            else {
                datatable.search($(this).val(), "lock");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        //datatable.search(true, "lock");
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
    //datatable.search("", "lock");
});

function datediff(first, second) {
    // Take the difference between the dates and divide by milliseconds per day.
    // Round to nearest whole number to deal with DST.
    return Math.round((second - first) / (1000 * 60 * 60 * 24));
}

function deleteUser(k) {
    swal({
        title: "Are you sure?",
        text: "Bạn có muốn xóa nhân viên này không?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'Có',
        cancelButtonText: "Không",
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function (isConfirm) {
        if (isConfirm.value) {
            $.ajax({
                url: '/Common/DeleteUser?id=' + k,
                type: 'GET',
                contentType: false,
                processData: false,
                success: function (response) {
                    if (response.result) {
                        swal("", response.message, "success");
                        setTimeout(function () {
                            location.reload();
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