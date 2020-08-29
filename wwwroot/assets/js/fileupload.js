//== Class definition

var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Common/GetDataFile',
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
                    return index+ 1;
                }
            },
                {
                field: "fileName",
                title: "Tên"

                },
                {
                    field: "url",
                    title: "File",
                    template: function (row, index, datatable) {
                        var html = "";
                        if (row.url) {
                            var f = JSON.parse(row.url);
                            for (var i = 0; i < f.length; i++) {
                                html += "<a href='ReadFileUpload?id="+row.id+"&filename="+f[i].FileName+"' target='_blank'>" + f[i].FileName + "</a></br>";
                            }
                        }
                        return html;
                    }

                }, {
                field: "userName",
                title: "User"
                },
                {
                    field: "isPublic",
                    title: "Public",
                    width: 110,
                    template: function (row, index, datatable) {
                        if (row.isPublic) {
                        var IsPublic = {
                            true: { 'title': 'true', 'class': 'm-badge--brand' },
                            false: { 'title': 'false', 'class': ' m-badge--danger' },

                        };
                            return '<span class="m-badge ' + IsPublic[row.isPublic].class + ' m-badge--wide">' + IsPublic[row.isPublic].title + '</span>';
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
                    var k= ""
                    if (row.url) {
                        var f = JSON.parse(row.url);
                        for (var i = 0; i < f.length; i++) {
                            k += f[i].FileName+",";
                        }
                    }
                    return '<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Chi tiết" data-dismiss="modal" data-toggle="modal" data-target="#myModalFileUpdate" data-code="' + row.id + '" data-code2="' + row.fileName + '" data-code3="' + k + '" data-code4="' + row.isPublic+'">\
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
$("#myModalFileUpdate").on("show.bs.modal", function (event) {
    var button = $(event.relatedTarget); // Button that triggered the modal
    $('input[name="Id"]').val(button.data('code'));
    $('input[name="Name"]').val(button.data('code2'));
    $("#fileNameUpload").html("");
    if (button.data('code4')) {
        $('input[name="IsPublic"]').prop("checked", true);
    }
    var html = button.data('code3');
    if (html) {
        var k = html.split(",");
        html = "";
        for (var i = 0; i < k.length; i++) {
            if (k[i]) {
                html += '<li class="m-nav__item">\
                 <span class="m-badge m-badge--info m-badge--dot"></span>\
                <span class="m-nav__link-title">\
                    <span class="m-nav__link-wrap">\
                        <span class="m-nav__link-text">\
                            '+ k[i] + '</span>\
                        <span class="m-nav__link-badge">\
                            <a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" onclick=deleteFile("'+ k[i] +'")><i class="m-nav__link-icon fa fa-remove"></i></a>\
                        </span>\
                    </span>\
                </span>\
            </li>' ;
            }
        }
        $("#fileNameUpload").append(html);
    }
});

$("#myModalFileUpdate").on("hide.bs.modal", function (event) {
    $("#fileUpdate").val("");
    $(".custom-file-label").html("");
    $('input[name="Id"]').val("");
    $('input[name="Name"]').val("");
    $("#fileNameUpload").html("");
    $('input[name="IsPublic"]').prop("checked", false);
});

$("#myModelFileUpload").on("hide.bs.modal", function (event) {
    $("#fileupload").val("");
    $(".custom-file-label").html("");
});

function deleteFile(k) {
    swal({
        title: "Are you sure?",
        text: "Bạn có muốn xóa file này không?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'Có',
        cancelButtonText: "Không",
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function (isConfirm) {
        if (isConfirm.value) {
            //var formData = new FormData();
            //formData.append("id", k);
            //formData.append("filename", $('input[name="Id"]').val());
            $.ajax({
                url: '/Common/DeleteFileUpload?id=' + $('input[name="Id"]').val() + '&filename=' + k,
                type: 'GET',
                contentType: false,
                processData: false,
                success: function (response) {
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
    });
}