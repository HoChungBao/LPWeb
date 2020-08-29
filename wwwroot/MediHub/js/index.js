$(document).ready(function() {
  let pageFooter = $('.page-footer');

  /**
   * Add fake footer
   */
  function addFakeFooter() {
    let footerHeight = pageFooter.outerHeight();
    let fakeFooter = '<div class=\'fakeFooter\'></div>';

    $('body').append(fakeFooter);
    $('.fakeFooter').css('height', footerHeight);
    pageFooter.addClass('page-footer--fixed');
  }

  /**
   * checkNav base
   * if screen 991 always add class
   */
  function updateNav() {
    if (window.innerWidth < 991) {
      $('.navbar').addClass('bg-light');
      return;
    }

    checkNav();

    $(window).on('scroll', function(event) {
      checkNav();
    });
  }

  /**
   * updateNav base on
   */
  function checkNav() {
    if (window.scrollY > 50) {
      $('.navbar').addClass('bg-light');
    } else {
      $('.navbar').removeClass('bg-light');
    }
  }

  $('.page-scroll a').on('click', function(event) {
    let NAV_HEIGHT = $('.navbar ').height > 50 ? $('.navbar ').height : 50;
    let position = $($(this).attr('href')).offset().top - NAV_HEIGHT;
    $('html, body').animate({
      scrollTop: position,
    }, 500);
    if ($('.navbar-collapse').hasClass('in')) {
      $('.js-menuToggle').click();
    }
    event.preventDefault();
  });

  Array.prototype.map.call($('.service-item'), function(item, index) {
    let delayTime = 0.2;
    item.setAttribute('data-wow-delay', delayTime * index + 's');
  });

  // Init new WOW
  new WOW().init({
    offset: 200,
    mobile: false,
  });

  // Typejs
  new Typed('.js-type', {
    strings: ['Are Medihub Company.', 'Have Communication Solutions.'],
    typeSpeed: 60,
    backSpeed: 50,
    loop: true,
    backDelay: 1000,
  });

  // mixitup
  if ($('.js-mixitup').length) {
    mixitup('.js-mixitup', {
      load: {
        filter: 'all',
      },
    });
  }

  if ($('#particles').length) {
    particlesJS.load('particles', '/particles.json');
  }

  if (window.outerWidth > 1000) {
    // addFakeFooter();
  }

  updateNav();

  /* Start Smooth Page Scrolling */
  // Select all links with hashes
  $('a[href^="#"]')
    // Remove links that don't actually link to anything
    .not('[hrep^="#"]')
    .not('[hrep^="#0"]')
    .on('click', function(e) {
      e.preventDefault();

      let target = this.hash;
      let $target = $(target);

      $('html, body').stop().animate({
        'scrollTop': $target.offset().top,
      }, 900, 'swing', function() {
        window.location.hash = target;
      });
    });
  /* End Smooth Page Scrolling */

  // loading page
  function loading() {
    $('.loading').fadeOut();
  }
  window.onload = loading;

  // scrollTop
  $(window).scroll(function() {
    if ($(this).scrollTop() > 100) $('.scroll__top').fadeIn();
    else $('.scroll__top').fadeOut();
  });
  $('.scroll__top').click(function() {
    $('body,html').animate({
      scrollTop: 0,
    }, 'slow');
  });

  // counter-up
  $('.counter__number').each(function() {
    let $this = $(this),
      countTo = $this.attr('data-count');

    $({
      countNum: $this.text(),
    }).animate({
      countNum: countTo,
    }, {
      duration: 8000,
      easing: 'linear',
      step: function() {
        $this.text(Math.floor(this.countNum));
      },
      complete: function() {
        $this.text(this.countNum);
        // alert('finished');
      },
    });
  });

  // portfolio-img-gallery
  $(".fancybox").fancybox({
  });

  let marker;
  /**
 *
 * @param {Number} id
 * @param {Number} lat
 * @param {Number} lng
 * @param {Number} zoom
 * @param {String} icon
 * @param {String} primaryColor
 * @param {String} secondaryColor
 * @param {String} waterColor
 */
  function btGmapInit(id, lat, lng, zoom, icon, primaryColor, secondaryColor, waterColor) {
    let myLatLng = new google.maps.LatLng(lat, lng);
    let mapOptions = {
      zoom: zoom,
      center: myLatLng,
      scrollwheel: false,
      scaleControl: true,
      zoomControl: true,
      zoomControlOptions: {
        style: google.maps.ZoomControlStyle.SMALL,
        position: google.maps.ControlPosition.RIGHTCENTER,
      },
      streetViewControl: true,
      mapTypeControl: true,
    };
    let map = new google.maps.Map(id, mapOptions);

    let marker = new google.maps.Marker({
      position: myLatLng,
      map: map,
      icon: icon,
    });
  }

  /**
 * Toggle Bounce
 */
  function toggleBounce() {
    if (marker.getAnimation() !== null) {
      marker.setAnimation(null);
    } else {
      marker.setAnimation(google.maps.Animation.BOUNCE);
    }
  }

  google.maps.event.addDomListener(window, 'load', function() {
    let mapCanvas597f87bb9aaad = document.getElementById('map');
    btGmapInit(mapCanvas597f87bb9aaad, 10.796997, 106.665382, 18);
  });
});

/**
 * View more info
 * @param {Node} node 
 */
function RevealMore(node)
{
  var parent = node.closest('.team__description');

  parent.find('.team__description--mix').css('height', 'auto');
  parent.find('.team__description--fade-out').css('display', 'none');
  node.css('display', 'none');
}

$('.team__description--btnmore').on('click', function(){
  RevealMore($(this));
})

//fancybox gallery
	$("[.fancybox]").fancybox({
    // Options will go here
    
	});
