//== Class definition
var datatable;
    var DatatableJsonRemoteCreens = function () {
        //== Private functions

        // basic demo
        var demo = function () {

            datatable = $('.m_datatable').mDatatable({
                // datasource definition
                data: {
                    type: 'remote',
                    source: '/Outlet/GetDataOutletStore',
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
						<a href="/Outlet/UpdateOutletStore?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Update" target="_blank">\
                        <i class="la la-edit" ></i >\
                        </a >\
                        <a href="/Outlet/outletdetail?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Chi tiết dự án" target="_blank">\
                        <i class="la la-file" ></i >\
                        </a >'
                                ;
                        }
                    },
                    {
                    field: "drugStoreName",
                    title: "Tên nhà thuốc"

                }, {
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
                    }
                //,{
                //    field: "area",
                //    title: "Khu vực"

                //    }
                    ,
                    {
                        field: "staffNumm",
                        title: "SL NV"
                    }, 

                {
                    field: "storePhoneNumber",
                    title: "Di động"

                    },
                    {
                        field: "sizeCode",
                        title: "Diện tích"
                    },
                    {
                        field: "storeOwner",
                        title: "Chủ Nhà thuốc"
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
                    field: "createdDate",
                    title: "Ngày tạo",
                    template: function (row, index, datatable) {
                        if (row.createdDate) {
                            var date = new Date(row.createdDate);
                            var month = date.getMonth() + 1;
                            return date.getDate() + "/" + (month.length = 1 ? month : "0" + month) + "/" + date.getFullYear();
                        }
                        return "";
                    }
                },
                {
                    field: "updatedDate",
                    title: "Date updated",
                    template: function (row, index, datatable) {
                        if (row.updatedDate) {
                            var date = new Date(row.updatedDate);
                            var month = date.getMonth() + 1;
                            return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                        }
                        return "";
                    }
                    },
                    {
                        field: "avatarUrl",
                        title: "Avatar",
                        template: function (row, index, datatable) {
                            if (row.avatarUrl) {
                                return "<a href='#' onclick=imageshow('" + row.avatarUrl + "')>Xem hình</a>";
                            }
                            return "";
                        }
                    },
                {
                    field: "note",
                    title: "Ghi chú",
                    responsive: { visible: 'lg' }
                }
               ]
            });

            var query = datatable.getDataSourceQuery();
            $('#m_form_Area').on('change', function () {
                datatable.search($(this).val(), 'area');
            }).val(typeof query.area !== 'undefined' ? query.area : '');

            $('#m_form_type').on('change', function () {
                if ($(this).val() === "") {
                    datatable.search("", "province");
                    setTimeout(function () {
                        $('#m_form_district').change();
                    }, 1000);

                }
                else {
                    datatable.search(change($('#m_form_type option:selected').text()).toUpperCase(), "province");
                    setTimeout(function () {
                        $('#m_form_district').change();
                    }, 1000);
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
            $('#m_form_type, #m_form_district').selectpicker();

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
    $(".m-datatable__table").on('mouseover', 'tbody tr', function () {
        $(this).css('cursor', 'hand');
    });
    $(".m-datatable__table").on('dblclick', 'tbody tr', function () {
        $("#projectDetail-modal").modal('show');
        var tableData = $(this).children("td").map(function () {
            return $(this).text();
        });
        var outlet;
        datatable.originalDataSet.forEach(function (item) {
            if (item.drugStoreName == tableData[1] && item.district == tableData[2] && item.province == tableData[3] && item.drugStoreAddress == tableData[4]) {
                outlet = item
                return;
            }
        });
        $("#modalTable").html("");
        $("#exampleModalLabel").html("");
        $("#exampleModalLabel").append("Cửa hàng : "+ tableData[1]);
        if (outlet != null) {
            var temp = '<td class="m-datatable__cell"><span style="width: 110px;">{1}</span></td>'; 
            outlet.projectDetail.forEach(function (projetDetail) {
                var html = '<tr class="m-datatable__row">';
                html += temp.replace("{1}", projetDetail.projectName);
                html += temp.replace("{1}", projetDetail.address);
                html += temp.replace("{1}", projetDetail.label);
                html += temp.replace("{1}", projetDetail.component);
                html += temp.replace("{1}", projetDetail.position);
                html += temp.replace("{1}", projetDetail.hsize.toFixed(3)+ " m");
                html += temp.replace("{1}", projetDetail.vsize.toFixed(3)+ " m");
                html += temp.replace("{1}", (projetDetail.hsize * projetDetail.vsize).toFixed(3) + " m2");
                html += temp.replace("{1}", getCurrency(projetDetail.costPayForDrugStore)+ " VNĐ");
                html += temp.replace("{1}", getCurrency(projetDetail.costPayForProduction)+ " VNĐ");
                html += temp.replace("{1}", projetDetail.monthRent);
                html += temp.replace("{1}", convertDate(projetDetail.dateBeginRent));
                var img = "[]";
                if (projetDetail.image) {
                    img = projetDetail.image;
                }
                html += temp.replace("{1}", "<a href='#' onclick=imageshow('" + img + "')>Xem hình</a>");
                html+= "</tr>";
                $("#modalTable").append(html);
            });
        }
    });
});
function convertDate(day) {
    var date = new Date(day);
    var month = date.getMonth() + 1;
    return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
}