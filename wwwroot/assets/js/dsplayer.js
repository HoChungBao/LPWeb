//== Class definition
function getDataDsPlayer(value) {
var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
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
                field: "id",
                title: "Id",
                template: function (row, index, datatable) { 
                    var i = "";
                    if (row.playerId) {
                        i= row.playerId;
                    }
                        return '<label class="m-checkbox m-checkbox--solid m-checkbox--single m-checkbox--brand">\
                            <input type="checkbox" class="idPlayer" value="'+i+'">\
                                <span></span>\
					</label >';
                }

            },{
                field: "playerId",
                title: "PlayerId"

            }, {
                    field: "playerName",
                    title: "PlayerName"
            }
                , {
                field: "address",
                title: "Địa chỉ",
                width: 110
                }
                , {
                    field: "status",
                    title: "Trạng thái hiện tại",
                    width: 110,
                    template: function (row, index, datatable) {
                        if (row.status) {
                        var status = {
                            1: { 'title': 'Hoàn tất', 'class': 'm-badge--brand' },
                            0: { 'title': 'Gặp sự cố', 'class': ' m-badge--danger' },

                        };
                            return '<span class="m-badge ' + status[row.status].class + ' m-badge--wide">' + status[row.status].title + '</span>';
                        }
                        return "";
                    }
                } ,
            {
                field: "currentStatus",
                title: "Trạng thái",
                template: function (row, index, datatable) {
                    if (row.currentStatus) {
                    var status = {
                        C: { 'title': 'Chưa sửa chữa', 'class': 'm-badge--danger' },
                        D: { 'title': 'Đã sửa', 'class': ' m-badge--warning' },
                        A: { 'title': 'Duyệt', 'class': ' m-badge--brand' },
                        N: { 'title': 'Mới', 'class': ' m-badge--brand' }

                    };
                        return '<span class="m-badge ' + status[row.currentStatus].class + ' m-badge--wide">' + status[row.currentStatus].title + '</span>';
                    }
                    return "";
                }
            },
                {
                    field: "isOffline",
                    title: "IsOffline",
                    responsive: { visible: 'lg' },
                    template: function (row, index, datatable) {
                        if (row.isOffline) {
                        var IsOffline = {
                            true: { 'title': 'Offiline', 'class': 'm-badge--brand' },
                            false: { 'title': 'Online', 'class': ' m-badge--metal' },
                        };
                            return '<span class="m-badge ' + IsOffline[row.isOffline].class + ' m-badge--wide">' + IsOffline[row.isOffline].title + '</span>';
                        }
                        return "";
                    }
                },
            {
                field: "isHospital",
                title: "Bệnh viện",
                template: function (row, index, datatable) {
                    if (row.isHospital) {
                    var IsHospital = {
                        true: { 'title': 'true', 'class': 'm-badge--brand' },
                        false: { 'title': 'false', 'class': ' m-badge--metal' },

                    };
                        return '<span class="m-badge ' + IsHospital[row.isHospital].class + ' m-badge--wide">' + IsHospital[row.isHospital].title + '</span>';
                    }
                    return "";
                }
            },
                {
                    field: "dateCreated",
                    title: "Ngày tạo",
                    template: function (row, index, datatable) {
                        if (row.dateCreated) {
                            var date = new Date(row.dateCreated);
                            var month = date.getMonth() + 1;
                                return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                        }
                        return "";
                    }
            },
                {
                    field: "dateUpdated",
                    title: "Date updated",
                    template: function (row, index, datatable) {
                        if (row.dateUpdated) {
                        var date = new Date(row.dateUpdated);
                        var month = date.getMonth() + 1;
                            return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                        }
                        return "";
                    }
            }, {
                field: "userName",
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
						<a href="/Common/UpdateDsPlayer?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update" target="_blank">\
                        <i class="la la-edit" ></i >\
                        </a >\
                        </a>\
                        <div class="dropdown ' + dropup + '" >\
                        <a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                        <i class="la la-ellipsis-h"></i>\
                        </a>\
                        <div class="dropdown-menu dropdown-menu-right">\
                        <a href="#"class="dropdown-item" data-dismiss="modal" data-toggle="modal" data-target="#myModalDsPlayer" data-code="'+ row.playerId + '" data-code2="' + row.playerName +'" title="Ghi chú">\
							<i class="flaticon-edit"></i>Cần sửa chửa\
						</a>\
                        <a class="dropdown-item" href="/Common/DsPlayerLog?id='+ row.playerId + '"><i class="la la-file"></i>Danh sách ghi</a>\
                        </div>\
                        </div >'
                        ;
                }
            }]
        });

        var query = datatable.getDataSourceQuery();

        $('#m_form_type').on('change', function () {
            if ($(this).val() === "") {
                $('#m_form_123').change();
                datatable.search("", "province");
                
            }
            else {
                $('#m_form_123').change();
                datatable.search($(this).val(), "province");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');
        $('#m_form_123').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "district"); 

            }
            else {
                datatable.search($('#m_form_123 option:selected').text(), "district");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');
        $('.m_form_type_drug').click(function () {
            var k = [];
            $('.m_form_type_drug:checked').each(function () {
                k.push($(this).val());
            });
            if (k.length > 1) {
                datatable.search("", "isHospital");
                return;
            }
            datatable.search(k[0], "isHospital");
        });
        $('.m_form_status').click(function () {
            var i = [];
            $('.m_form_status:checked').each(function () {
                i.push($(this).val());
            });
            if (i.length > 1) {
                datatable.search("", "isOffline");
                return;
            }
            datatable.search(i[0], "isOffline");
        });
        $('#m_form_type, #m_form_123, #m_form_type_drug, #m_form_status').selectpicker();

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
}
$("#myModalDsPlayer").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal

    $("#PlayerId").val(button.data('code'));
    $("#PlayerName").val(button.data('code2')); 
});
$("#download").on("click", function () {
    var d = {
        province: $('#m_form_type').val(),
        district: $('#m_form_123 option:selected').text(),
        isHospital: true,
        isDrug: true,
        isOffline: true,
        isOn: true
    };
    if ($('#m_form_123').val()=="") {
        d.district = $('#m_form_123').val();
    }
    var drug = [];
    $('.m_form_type_drug').each(function () {
        drug.push($(this).is(':checked'));
    });
    d.isHospital = drug[0];
    d.isDrug = drug[1];
    drug = [];
    $('.m_form_status').each(function () {
        drug.push($(this).is(':checked'));
    });
    d.isOffline = drug[0];
    d.isOn = drug[1];
    district: $('#m_form_123 option:selected').text(),
    window.open(window.location.origin + "/Common/DownloadDsPlayer?province=" + d.province + "&district=" + d.district + "&isHospital=" + d.isHospital + "&isDrug=" + d.isDrug + "&isOffline=" + d.isOffline + "&isOn=" + d.isOn, '_blank');
});