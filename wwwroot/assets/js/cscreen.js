//== Class definition

var DatatableJsonRemoteCreens = function () {
	//== Private functions

	// basic demo
	var demo = function () {

		var datatable = $('.m_datatable').mDatatable({
			// datasource definition
			data: {
				type: 'remote',
                source: '/Common/GetDataCsCreens',
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
			columns: [ {
				field: "id",
				title: "Id"
			}, {
                field: "name",
				title: "Tên"
				
                }, {
                    field: "isHospital",
                    title: "Loại"
                   , template: function (row) {
                        var status = {
                             true: { 'title': 'Hospital', 'class': 'm-badge--brand' },
                            false: { 'title': 'Pharmacy', 'class': ' m-badge--metal' }
                       
                        };
                        return '<span class="m-badge ' + status[row.isHospital].class + ' m-badge--wide">' + status[row.isHospital].title + '</span>';
                    }

			    }
			    , {
                 field: "phone",
                 title: "Phone",
				width: 110
			}, {
				field: "email",
				title: "Email",
				responsive: {visible: 'lg'}
			}, {
				field: "address",
				title: "Address",
				responsive: {visible: 'lg'}
            }
			  , {
                  field: "speciality",
			        title: "Chuyên khoa",
			        responsive: { visible: 'lg' }
			    }, {
                  field: "productType",
                  title: "ProductType"
				
                }
			    ,
			    {
			        field: "totalLed",
			        title: "Sl màn hình"


			    },
			    {
			        field: "trafficDaily",
			        title: "Traffic/ngày"

			    },
			    {
                    field: "priceWeekly",
			        title: "Giá/Tuần",
			        responsive: { visible: 'lg' }
            } ,{
               field: "priceMonopoly",
		        title: "Giá độc quyền"
                }
			    , {
                    field: "advertiseProductsType",
			        title: "Các sp được quảng cáo"
            },
			     {
                    field: "numberApprovedDay",
			        title: "Số ngày bv duyệt"
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
						    	<a class="dropdown-item" href="/common/EditSCreen/'+ row.id+ '"><i class="la la-edit"></i> Edit Details</a>\
						    	<a class="dropdown-item" href="/common/DeleteCsCreen/'+ row.id + '"><i class="la la-leaf"></i> Delete Details</a>\
                                <a class="dropdown-item" href="/common/CreateMediHubAsset?id='+ row.id + '&address=' + row.address + '&speciality=' + row.speciality+'"><i class="la la-tv"></i>Create MediHubAsset</a>\
						    	\
						  	</div>\
						</div>\
						<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" data-dismiss="modal" data-toggle="modal" data-target="#myModal" data-code="@data.Images" title="Xem hình ảnh">\
							<i class="la la-download"></i>\
						</a>\
						<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-danger m-btn--icon m-btn--icon-only m-btn--pill" title="Edit settings">\
							<i class="la la-cog"></i>\
						</a>\
					';
				}
			}]
		});

		var query = datatable.getDataSourceQuery();

        $('#myModal').on('show.bs.modal', function (event) {
            var button = $(event.relatedTarget); // Button that triggered the modal
            Images = button.data('code'); // Extract info from data-*            
            $('#Images').val(Images);
        
        });
       
        $('#m_form_status').on('change', function () {
            if ($(this).val() === "true") { //Boi vi tra ve kieu du lieu String 
                datatable.search(true, 'isHospital');
            }
            if ($(this).val() === "false"){
                datatable.search(false, 'isHospital');
            }
            if ($(this).val() === "") {
                datatable.search("", "isHospital");
            }
        }).val(typeof query.Type !== 'undefined' ? query.Type : '');

		$('#m_form_type').on('change', function () {
            datatable.search($(this).val(), 'speciality');
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