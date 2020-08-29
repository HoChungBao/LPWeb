//== Class definition
$('#summernote-title').summernote();
$('#summernote-content').summernote({
    callbacks: {
        onImageUpload: function (files) {
            if (!files.length) return;
            var file = files[0];
            // create FileReader
            var reader = new FileReader();
            reader.onloadend = function () {
                // when loaded file, img's src set datauri
                console.log("img", $("<img>"));
                var img = $("<img>").attr({ src: reader.result, width: "100%" }); // << Add here img attributes !
                console.log("var img", img);
                $("#summernote-content").summernote("insertNode", img[0]);
            }

            if (file) {
                // convert fileObject to datauri
                reader.readAsDataURL(file);
            }
        }
    }
});
var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/News/GetDataNewsPost',
                pageSize: 10
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
                title: "Tiêu đề",
                responsive: {visible:'lg'}
            }, {
                    field: "isPublished",
                    title: "Duyệt",
                    //template: function(row) {
                    //    var value = {
                    //        "true": { 'title': 'Đã duyệt', 'class': 'm-badge--brand' },
                    //        "false": { 'title': 'Đợi duyệt', 'class':'m-badge--brand'}
                    //    };
                    //    return value[row.isPublished].title;
                    //}
                //width: 110
            }, {
                    field: "createdOn",
                    title: "Ngày tạo",
                    type: "date",
                    format: "MM-DD-YYYY hh:mm:ss",
                    template: function (now) {
                        // return format YYY-MM-DD conver to DD-MM-YYYY
                        var date = new Date(now.createdOn.replace("T", " "));
                        // return date;
                        return date.getMonth() + 1 +
                            "-" +
                            date.getDate() +
                            "-" +
                            date.getFullYear() +
                            " " +
                            date.getHours() +
                            ":" +
                            date.getMinutes() +
                            ":" +
                            date.getSeconds();
                    }
            }, 
                
                {
                    field: "linkImage",
                title: "Image link",
                template: function (row) {
                    return '\
                    <a class="preview" href="/' +row.linkImage + '" target="_blank">Link\
                        <img src="/'  + row.linkImage +  +'" class="hide-image" style="z-index:1; position: absolute;display:none;height:200px;padding:8px" />\
                        </a>\
                    ';

                }
                //<a href="#" onClick="MyWindow=window.open('http://www.google.com','MyWindow',width=600,height=300); return false;">Click Here</a>
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
						    <a class="dropdown-item" href="/News/EditNews/?id='+ row.id + '"><i class="la la-edit"></i> Edit Details</a>\
						    <a class="dropdown-item" href="#" onclick="deleteContact('+ row.id + ')"><i class="la la-leaf"></i> Delete Details</a>\
                            <a class="dropdown-item" href="/News/'+ row.slug + '"><i class="la la-edit"></i> Review</a>\
						    \
						</div>\
						</div>\
					';
                }
            }]
        });


        $(document).on("mouseover", ".preview",
            function() {
                $(this).find('img').fadeIn();
            });

        $(document).on("mouseout",
            ".preview",
            function() {
                $(this).find('img').fadeOut();
            });

        var query = datatable.getDataSourceQuery();

        //var currentTime = new Date();
        //var month = currentTime.getMonth() + 1;
        //var day = currentTime.getDate();
        //var year = currentTime.getFullYear();
        //var date = day + "/" + month + "/" + year;
        //alert(date);

        //$('#myModal').on('')


        $('#myModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            Images = button.data('code'); // Extract info from data-*            
            $('#Images').val(Images);

        });

        $('#m_form_status').on('change', function () {
            if ($(this).val() === "true") { //Boi vi tra ve kieu du lieu String 
                datatable.search(true, 'isHospital');
            }
            if ($(this).val() === "false") {
                datatable.search(false, 'isHospital');
            }
            if ($(this).val() === "") {
                datatable.search("", "isHospital");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_type').on('change', function () {
            datatable.search($(this).val(), 'productType');
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

        $('#m_form_status, #m_form_type').selectpicker();

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
function deleteContact(k) {
    swal({
        title: "Are you sure?",
        text: "Bạn có muốn xóa bài viết này không?",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: 'No',
        cancelButtonText: "Yes",
        closeOnConfirm: false,
        closeOnCancel: true
    }).then(function (isConfirm) {
        if (isConfirm.dismiss === "cancel") {
            $.ajax({
                url: '/News/DeleteNewsPost?id=' + k,
                type: 'GET',
                contentType: false,
                processData: false,
                success: function(response) {
                    if (response.result) {
                        swal("", response.message, "success");
                    } else {
                        swal("", response.message, "warning");

                    }
                    location.reload();
                },
                error: function(err) {
                    swal("Xóa không thành công!");
                }
            });
        }
        else {
            swal("Your imaginary file is safe!");
        }
    });
}
