//== Class definition
var LayoutBuilder = function() {

	var exporter = {
		init: function() {
			$('#m-btn-howto').click(function(e) {
				e.preventDefault();
				$('#m-howto').slideToggle();
			});
			this.exportHtml();
			this.exportHtmlStatic();
			this.exportAngular();
		},
		startLoad: function(options) {
			$('#builder_export').
				addClass('m-loader m-loader--light m-loader--right').
				find('> span > span').
				text('Exporting...').
				closest('.m-form__actions').
				find('.btn').
				prop('disabled', true);
			$.notify(options);
		},
		doneLoad: function() {
			$('#builder_export').
				removeClass('m-loader m-loader--light m-loader--right').
				find('> span > span').
				text('Export').
				closest('.m-form__actions').
				find('.btn').
				prop('disabled', false);
		},
		exportHtml: function() {
			$('#builder_export_html').click(function(e) {
				e.preventDefault();
				if (!envato.isVerified()) return;

				var _self = $(this);

				exporter.startLoad({
					title: 'Generate HTML Partials',
					message: 'Process started and it may take about 1 to 10 minutes.',
				});

				$.ajax('index.php', {
					method: 'POST',
					data: {
						builder_export: 1,
						export_type: 'partial',
						demo: $(_self).data('demo'),
					},
				}).done(function(r) {
					var result = JSON.parse(r);
					if (result.message) {
						exporter.stopWithNotify(result.message);
						return;
					}

					var timer = setInterval(function() {
						$.ajax('index.php', {
							method: 'POST',
							data: {
								builder_export: 1,
								builder_check: result.id,
							},
						}).done(function(r) {
							var result = JSON.parse(r);
							if (typeof result === 'undefined') return;
							// export status 1 is completed
							if (result.export_status !== 1) return;

							$('<iframe/>').attr({
								src: 'index.php?builder_export&builder_download&id=' +
								result.id,
								style: 'visibility:hidden;display:none',
							}).ready(function() {
								$.notify({
									title: 'Export HTML Version Layout',
									message: 'HTML version exported.',
								}, {type: 'success'});
								exporter.doneLoad();
								// stop the timer
								clearInterval(timer);
							}).appendTo(_self);
						});
					}, 15000);
				});
			});
		},
		exportHtmlStatic: function() {
			$('#builder_export_html_static').click(function(e) {
				e.preventDefault();
				if (!envato.isVerified()) return;

				var _self = $(this);

				exporter.startLoad({
					title: 'Generate HTML Static Version',
					message: 'Process started and it may take about 1 to 10 minutes.',
				});

				$.ajax('index.php', {
					method: 'POST',
					data: {
						builder_export: 1,
						export_type: 'html',
						demo: $(_self).data('demo'),
					},
				}).done(function(r) {
					var result = JSON.parse(r);
					if (result.message) {
						exporter.stopWithNotify(result.message);
						return;
					}

					var timer = setInterval(function() {
						$.ajax('index.php', {
							method: 'POST',
							data: {
								builder_export: 1,
								builder_check: result.id,
							},
						}).done(function(r) {
							var result = JSON.parse(r);
							if (typeof result === 'undefined') return;
							// export status 1 is completed
							if (result.export_status !== 1) return;

							$('<iframe/>').attr({
								src: 'index.php?builder_export&builder_download&id=' +
								result.id,
								style: 'visibility:hidden;display:none',
							}).ready(function() {
								$.notify({
									title: 'Export Default Version',
									message: 'Default HTML version exported with current configured layout.',
								}, {type: 'success'});
								exporter.doneLoad();
								// stop the timer
								clearInterval(timer);
							}).appendTo(_self);
						});
					}, 15000);
				});

			});
		},
		exportAngular: function() {
			$('#builder_export_angular').click(function(e) {
				e.preventDefault();
				if (!envato.isVerified()) return;

				var _self = $(this);

				exporter.startLoad({
					title: 'Export Angular Version',
					message: 'Process started and it may take about 1 to 10 minutes.',
				});

				$.ajax('index.php', {
					method: 'POST',
					data: {
						builder_export: 1,
						export_type: 'angular',
						demo: $(_self).data('demo'),
					},
				}).done(function(r) {
					var result = JSON.parse(r);
					if (result.message) {
						exporter.stopWithNotify(result.message);
						return;
					}

					var timer = setInterval(function() {
						$.ajax('index.php', {
							method: 'POST',
							data: {
								builder_export: 1,
								builder_check: result.id,
							},
						}).done(function(r) {
							var result = JSON.parse(r);
							if (typeof result === 'undefined') return;
							// export status 1 is completed
							if (result.export_status !== 1) return;

							$('<iframe/>').attr({
								src: 'index.php?builder_export&builder_download&id=' +
								result.id,
								style: 'visibility:hidden;display:none',
							}).ready(function() {
								$.notify({
									title: 'Export Angular Version',
									message: 'Angular App version exported with current configured layout.',
								}, {type: 'success'});
								exporter.doneLoad();
								// stop the timer
								clearInterval(timer);
							}).appendTo(_self);
						});
					}, 15000);
				});
			});
		},
		stopWithNotify: function(message, type) {
			type = type || 'danger';
			$.notify({
				title: 'Verification failed',
				message: message,
			}, {type: type});
			exporter.doneLoad();
		},
	};

	//== Private functions
	var preview = function() {
		$('[name="builder_submit"]').click(function(e) {
			e.preventDefault();
			var _self = $(this);
			$(_self).
				addClass('m-loader m-loader--light m-loader--right').
				closest('.m-form__actions').
				find('.btn').
				prop('disabled', true);

			$.ajax('index.php?demo=' + $(_self).data('demo'), {
				method: 'POST',
				data: $('[name]').serialize(),
			}).done(function(r) {
				$.notify({
					title: 'Preview updated',
					message: 'Preview has been updated with current configured layout.',
				}, {type: 'success'});
			}).always(function() {
				setTimeout(function() {
					location.reload();
				}, 600);
			});
		});
	};

	var reset = function() {
		$('[name="builder_reset"]').click(function(e) {
			e.preventDefault();
			var _self = $(this);
			$(_self).
				addClass('m-loader m-loader--primary m-loader--right').
				closest('.m-form__actions').
				find('.btn').
				prop('disabled', true);

			$.ajax('index.php?demo=' + $(_self).data('demo'), {
				method: 'POST',
				data: {
					builder_reset: 1,
					demo: $(_self).data('demo'),
				},
			}).done(function(r) {
			}).always(function() {
				location.reload();
			});
		});
	};

	var keepActiveTab = function() {
		$('[href^="#m_builder_"]').click(function(e) {
			var which = $(this).attr('href');
			var btn = $('[name="builder_submit"]');
			var tab = $('[name="builder[tab]"]');
			if ($(tab).length === 0) {
				$('<input/>').
					attr('type', 'hidden').
					attr('name', 'builder[tab]').
					val(which).
					insertBefore(btn);
			} else {
				$(tab).val(which);
			}
		}).each(function() {
			if ($(this).hasClass('active')) {
				var which = $(this).attr('href');
				var btn = $('[name="builder_submit"]');
				var tab = $('[name="builder[tab]"]');
				if ($(tab).length === 0) {
					$('<input/>').
						attr('type', 'hidden').
						attr('name', 'builder[tab]').
						val(which).
						insertBefore(btn);
				} else {
					$(tab).val(which);
				}
			}
		});
	};

	
	// basic demo
	var init = function() {
		exporter.init();
		keepActiveTab();
		preview();
		reset();
	};

	return {
		// public functions
		init: function() {
			envato.verifyEvent();
			init();
		},
	};
}();

jQuery(document).ready(function() {
	LayoutBuilder.init();
});