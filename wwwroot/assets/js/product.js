//== Class definition

var DatatableJsonRemoteCreens = function () {
    //== Private functions

    // basic demo
    var demo = function () {

        var datatable = $('.m_datatable').mDatatable({
            // datasource definition
            data: {
                type: 'remote',
                source: '/Orders/GetDataProduct',
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
                title: "Id"
            }, {
                field: "name",
                title: "Tên"

            }, {
                field: "brandId",
                title: "Loại sản phẩm"
            }
                , {
                    field: "slug",
                    title: "Slug",
                width: 110
            }, {
                    field: "metaTitle",
                    title: "Tiêu đề Meta",
                responsive: { visible: 'lg' }
                },
                {
                    field: "thumbnailImage",
                    title: "Image",
                    responsive: { visible: 'lg' },
                    template: function (row, index, datatable) {
                        var html = "";                    
                        if (row.thumbnailImage != "") {
                            var img = JSON.parse(row.thumbnailImage);
                            for (var i = 0; i < img.length; i++) {
                                html += "<a href='/" + img[i].Url + "' target='_blank'>" + img[i].FileName + "</a>";
                            }
                        }
                        return html;
                    }
                }, {
                    field: "price",
                    title: "Giá bán",
                responsive: { visible: 'lg' }
            }
                , {
                    field: "oldPrice",
                    title: "Giá gốc",
                responsive: { visible: 'lg' }
            }, {
                    field: "specialPrice",
                    title: "Giá đặc biệt"

            }
                ,
            {
                field: "specialPriceStart",
                title: "Bắt đầu Giá Đặc biệt",
                template: function (row, index, datatable) {
                    var date = new Date(row.specialPriceStart);
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }


            },
            {
                field: "specialPriceEnd",
                title: "Kết thúc giá đặc biệt",
                template: function (row, index, datatable) {
                    var date = new Date(row.specialPriceEnd);
                    var month = date.getMonth() + 1;
                    return date.getDate() + "/" + (month.length > 1 ? month : "0" + month) + "/" + date.getFullYear();
                }

            },
            {
                field: "reviewsCount",
                title: "Lượt xem",
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
						<div class="dropdown ' + dropup + '">\
							<a href="#" class="btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown">\
                                <i class="la la-ellipsis-h"></i>\
                            </a>\
						  	<div class="dropdown-menu dropdown-menu-right">\
						    	<a class="dropdown-item" href="/Orders/UpdateProduct?id='+ row.id + '"><i class="la la-edit"></i> Edit Details</a></div>\
						</div>\
                        <a href="/Orders/ContactFormById?id='+ row.id + '" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Contact">\
							<i class="la la-cog"></i>\
						</a>\
					';
                }
            }]
        });

        var query = datatable.getDataSourceQuery();
    
        $('#m_form_type').on('change', function () {
            if ($(this).val() === "") {
                datatable.search("", "brandId");
            }
            else {
                datatable.search($(this).val(), "brandId");
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