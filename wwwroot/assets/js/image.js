function imageshow(value) {
    var fileName = JSON.parse(value);
    if (fileName.length > 0) {
        var html = "";
        var carouselLinks = []
        var baseUrl
        // Add the demo images as links with thumbnails to the page:
        fileName.forEach(function (i) {
            baseUrl = '/' + i;
            $('<a/>')
                .append($('<img>').prop('src', baseUrl))
                .attr('data-gallery', '')
            carouselLinks.push({
                href: baseUrl,
                title: ""
            })
        })
        // Initialize the Gallery as image carousel:
        blueimp.Gallery(carouselLinks, {
            container: '#blueimp-image-carousel',
            carousel: true
        })
    } else {
        $("#img").html("");
    }
    //$("#result-log").modal();
};