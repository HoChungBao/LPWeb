//== Class definition

var DatatableJsonRemoteOrders = function () {
	//== Private functions

	// basic demo
	var demo = function () {

		var datatable = $('.m_datatable').mDatatable({
			// datasource definition
			data: {
				type: 'remote',
                source: '/Orders/GetOrders?keySearch=&pageIndex&pageNum',
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
				title: "Order ID"
			}, {
                field: "customerName",
				title: "Tên khách hàng"
				//template: function (row) {
				//	// callback function support for column rendering
				//	return row.ShipCountry + ' - ' + row.ShipCity;
				//}
			}, {
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
			}, {
                field: "createDateFormat",
				title: "Ngày đặt",
                type: "date"
              ,
				format: "DD-MM-YYYY"
			}, {
				field: "status",
				title: "Status",
				 
				template: function (row) {
					var status = {
						"New": {'title': 'Mới', 'class': 'm-badge--brand'},
						"Delivered": {'title': 'Delivered', 'class': ' m-badge--metal'},
						"Canceled": {'title': 'Canceled', 'class': ' m-badge--primary'},
						"Success": {'title': 'Success', 'class': ' m-badge--success'},
						"Info": {'title': 'Info', 'class': ' m-badge--info'},
						"Danger": {'title': 'Danger', 'class': ' m-badge--danger'},
						"Warning": {'title': 'Warning', 'class': ' m-badge--warning'}
					};
					return '<span class="m-badge ' + status[row.status].class + ' m-badge--wide">' + status[row.status].title + '</span>';
				}
			}, {
                  field: "productType",
                  title: "ProductType"
				// callback function support for column rendering
				//template: function (row) {
				//	var status = {
				//		1: {'title': 'Online', 'state': 'danger'},
				//		2: {'title': 'Retail', 'state': 'primary'},
				//		3: {'title': 'Direct', 'state': 'accent'}
				//	};
				//	return '<span class="m-badge m-badge--' + status[row.Type].state + ' m-badge--dot"></span>&nbsp;<span class="m--font-bold m--font-' + status[row.Type].state + '">' + status[row.Type].title + '</span>';
				//}
            },
			    {
                    field: "productName",
			        title: "Tên Sản phẩm",
			        responsive: { visible: 'lg' }
            } ,{
                field: "startDateFormat",
		        title: "Ngày BD dự kiến",
		        type: "date",
		        format: "DD/MM/YYYY"
                }
			    , {
                    field: "endDateFormat",
			        title: "Ngày KT dự kiến",
			        type: "date",
                    format: "DD/MM/YYYY"
            },
			    {
                    field: "totalMoneyFormat",
                    title: "Tổng tiền"
                  
			        
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
						    	<a class="dropdown-item" href="#"><i class="la la-edit"></i> Edit Details</a>\
						    	<a class="dropdown-item" href="#"><i class="la la-leaf"></i> Update Status</a>\
						    	<a class="dropdown-item" href="#"><i class="la la-print"></i> Generate Report</a>\
						  	</div>\
						</div>\
						<a href="#" class="m-portlet__nav-link btn m-btn m-btn--hover-accent m-btn--icon m-btn--icon-only m-btn--pill" title="Download">\
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

		$('#m_form_status').on('change', function () {
			datatable.search($(this).val(), 'status');
		}).val(typeof query.Status !== 'undefined' ? query.Status : '');

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
    DatatableJsonRemoteOrders.init();
});