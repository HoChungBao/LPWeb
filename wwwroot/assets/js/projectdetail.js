//== Class definition
function DatatableJsonRemoteCreens(value) {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Outlet/GetDataProjectDetail?id=' + value,
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
            }, {
                    field: "drugName",
                    title: "Tên nhà thuốc"

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
                            return row.hsize + " mm";
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
                            return row.vsize + " mm";
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
                    var medigroup = "";
                    if (f) {
                        var countMediGroup = "({1} dự án đã có mã, {2} dự án chưa có mã)";
                        countMediGroup= countMediGroup.replace("{1}", row.countMediGroup);
                        countMediGroup= countMediGroup.replace("{2}", row.countMediGroupNot);
                        $("#count").append(countMediGroup);
                        f = false;
                        if (row.countMediGroupNot != 0) {
                            $(".createOutlet").show();
                        }
                    }
                    if (!row.mediGroupCode) {
                        var d = row.are + "," +   row.id + ","+row.drugName + "," + row.address + "," + row.ward + "," + row.district + "," + row.province ;
                            medigroup = '<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Map medigroup code" data-dismiss="modal" data-toggle="modal" data-target="#myModalProjectDetail" data-code="' + d+'">\
							<i class="la la-connectdevelop"></i>\
                            <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Tạo mới nhà thuốc" data-dismiss="modal" data-toggle="modal" data-target="#myModalCreateDrug" data-code="project-'+ row.id +'">\
							<i class="la la-plus"></i>\
						</a>' ;
                        }
                        return '\
						<div class="dropdown ' + dropup + '">\
							<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                <i class="la la-ellipsis-h"></i>\
                            </a>\
						    <div class="dropdown-menu dropdown-menu-right">\
						        <a class="dropdown-item" href="/Outlet/UpdateProjectDetail?id='+ row.id + '" target="_blank"><i class="la la-edit"></i> Edit Details</a>\
                            </div>\
						</div>'+
                            medigroup;
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
                $('#m_form_district').change();
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
        $('#m_form_medigroup').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "isMediGroup");
            }
            else{
                datatable.search($(this).val(), "isMediGroup");
            }
        });
        $('#m_form_province, #m_form_district, #m_form_medigroup').selectpicker();

    };
    demo();
    //return {
    //    // public functions
    //    init: function () {
    //        demo();
    //    }
    //};
}
function mediGroupCode() {
    $("#btnProjectDetail").addClass("m-loader m-loader--light m-loader--right disable");
    var mediGroup = $("#mediGroupCode").val();
    if (mediGroup) {
        $.ajax({
            url: '/Outlet/ProjectDetailMediGroup?medigroup=' + mediGroup + '&id=' + $("#Id").val(),
            type: 'GET',
            contentType: false,
            processData: false,
            success: function (response) {
                $("#btnProjectDetail").removeClass("m-loader m-loader--light m-loader--right disable");
                if (response.result) {
                    swal("", response.message, "success");
                    setTimeout(function () {

                        location.reload();

                        //End
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
}
function createMediGroup() {
    //var lattitue = "";
    //var longtitue = "";
    //var geocoder = new google.maps.Geocoder();
    //var address = $("input[name=WardCode]").val() + "," + $('.District option:selected').text() + "," + $('.Province option:selected').text();
    //geocoder.geocode({ 'address': address }, function (results, status) {
    //    lattitue = results[0].geometry.location.lat().toString();
    //    longtitue = results[0].geometry.location.lng().toString();
    //});
    var k = $("#projectId").val() + '-' + $("#Type").val();
    var check = $("#projectId").val().split("-")[0] =="projectmany" ? "tạo mới tất cả nhà thuốc có thể tốn nhiều thời gian" : "tạo mới nhà thuốc";
    swal({
        title: "Are you sure?",
        text: "Bạn có muốn " + check + "? ",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'Có',
        cancelButtonText: "Không",
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function (isConfirm) {
        if (isConfirm.value) {
            $("#btnDrug").addClass("m-loader m-loader--light m-loader--right disable");
            mApp.block('.m_blockuicontent', {
                overlayColor: '#000000',
                type: 'loader',
                state: 'success',
                message: 'Vui lòng chờ...'
            });
            //swal("Vui lòng chờ...", "", "warning");
            $.ajax({
                url: '/Outlet/CreateDrugProjectDetail?project=' + k,
                type: 'GET',
                contentType: false,
                processData: false,
                success: function (response) {
                    mApp.unblock('.m_blockuicontent');
                    $("#btnDrug").removeClass("m-loader m-loader--light m-loader--right disable");
                    if (response.result) {
                        swal("", response.message, "success");
                        setTimeout(function () {

                            location.reload();

                            //End
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
    })
}
$("#myModalProjectDetail").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    var projectDetail = button.data('code').split(",");
    $("#Area").val(projectDetail[0]);
    $("#Id").val(projectDetail[1]);
    $("#DrugName").val(projectDetail[2]);
    $("#Address").val(projectDetail[3]);
    $("#Ward").val(projectDetail[4]);
    $("#District").val(projectDetail[5]);
    $("#Province").val(projectDetail[6]);
});
$("#myModalCreateDrug").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    $("#projectId").val(button.data('code'));
});
var f = true;