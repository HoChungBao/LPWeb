$(function () {
  var bannerIcons = $(".banner__icon");
  var currentBannerIcon = 0;
  var intervalRef;
  var timeAutoPlay = 3000;
  var autoStart = true;

  function updateNav() {
    if (window.innerWidth < 991) {
      $('.navbar').addClass('bg-light');
      return;
    }

    checkNav();

    $(window).on('scroll', function (event) {
      checkNav();
      showFixBanner()
    });
  }

  function checkNav() {
    if (window.scrollY > 50) {
      $('.navbar').addClass('bg-light');
    } else {
      $('.navbar').removeClass('bg-light');
    }
  }

  function showFixBanner() {
    if (window.scrollY > $(".banner").height()) {
      $(".fix-banner").addClass("fix-banner--active");
    } else {
      $(".fix-banner").removeClass("fix-banner--active");
    }
  }

  /**
   * onClick
   * @param {Node} event 
   */
  function onBannerIconClick(event) {
    var numberIcon = event.target.getAttribute("data-number");
    if (numberIcon != currentBannerIcon) {
      currentBannerIcon = numberIcon;
      changeBanner();
    } else {
      // changeIconBanner(currentBannerIcon);
    }
    stopAutoPlay();
  }

  $(".banner__icons").on("click", ".js-banner", onBannerIconClick);

  $('.navbar-nav').on('click', ".nav-link", function (event) {
    let NAV_HEIGHT = $('.navbar').height > 50 ? $('.navbar ').height : 50;
    let position = $($(this).attr('href')).offset().top - NAV_HEIGHT;
    $('html, body').animate({
      scrollTop: position,
    }, 500);
    if ($('.navbar-collapse').hasClass('show')) {
      $('.js-menuToggle').click();
    }
    event.preventDefault();
  });

  $(".js-servicesFor").slick({
    slidesToShow: 1,
    slidesToScroll: 1,
    arrows: false,
    fade: true,
  });

  $('.js-servicesNav').slick({
    slidesToShow: 4,
    slidesToScroll: 1,
    asNavFor: '.js-servicesFor',
    focusOnSelect: true,
  });

  $('.js-hospitalSlice').slick({
    slidesToShow: 4,
    focusOnSelect: true,
    prevArrow: '<button type="button" class="slick-prev slick-prev--big slick-prev--big slick-arrow">Previous</button>',
    nextArrow: '<button type="button" class="slick-next slick-next--big slick-arrow">Next</button>',
    responsive: [{
        breakpoint: 678,
        settings: {
          slidesToShow: 2,
          slidesToScroll: 2
        }
      },
      {
        breakpoint: 480,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1
        }
      }
    ]
  });

  /**
   * remove loading when page loaded
   */
 
  updateNav();
  var wow = new WOW({
    offset: 50,
    mobile: false,
  });
  wow.init();

  jQuery.extend(jQuery.validator.messages, {
    required: "Thông tin bắt buộc điền.",
    remote: "Please fix this field.",
    email: "Email không hợp lệ!.",
    url: "Please enter a valid URL.",
    date: "Please enter a valid date.",
    dateISO: "Please enter a valid date (ISO).",
    number: "Vui lòng chỉ sử dụng số.",
    digits: "Please enter only digits.",
    creditcard: "Please enter a valid credit card number.",
    equalTo: "Please enter the same value again.",
    accept: "Please enter a value with a valid extension.",
    maxlength: jQuery.validator.format("Please enter no more than {0} characters."),
    minlength: jQuery.validator.format("Please enter at least {0} characters."),
    rangelength: jQuery.validator.format("Please enter a value between {0} and {1} characters long."),
    range: jQuery.validator.format("Please enter a value between {0} and {1}."),
    max: jQuery.validator.format("Please enter a value less than or equal to {0}."),
    min: jQuery.validator.format("Please enter a value greater than or equal to {0}.")
  });

  $("#contact-form").validate({});

  if ($(".js-typed").length) {
      var typed = new Typed(".js-typed", {
          strings: [
              'CÔNG TY TRUYỀN THÔNG TIẾP THỊ \n<span class="banner__slogan--big">B2B và B2C</span> VỀ Y TẾ LỚN NHẤT',
              'MEDIA CHANNELS TẠI CÁC \n<span class="banner__slogan--big">BỆNH VIỆN VÀ NHÀ THUỐC LỚN</span> \nTRÊN TOÀN QUỐC',
              'CHỦ ĐỘNG CHỌN \n<span class="banner__slogan--big">KHÁCH HÀNG MỤC TIÊU</span>\n VÀ GÓI <span class="banner__slogan--big">NGÂN SÁCH PHÙ HỢP</span>'
          ],
          backSpeed: 0,
          fadeOut: true,
          loop: true,
          cursorChar: '',
          typeSpeed: 25,
      });
  }
});




/**
 * Show alert that tell, this cotent still need update, and it empty
 */
function showUpdateAlert(e) {
  e.preventDefault();
  swal({
    title: 'Nôi dung chưa cập nhập!',
    text: 'Phần nội dung này sẽ được cập nhập trong thời gian tới.',
    imageUrl: '/MediHubTheme/images/update.png',
    imageWidth: 400,
    imageHeight: 182.5,
    imageAlt: 'Updating image'
  })
}
