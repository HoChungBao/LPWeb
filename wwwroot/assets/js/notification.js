var m = '<div class="m-list-timeline__item">\
                    <span class="m-list-timeline__badge m-list-timeline__badge--brand id-{2}"></span>\
                    <span class="m-list-timeline__text">\
                       {1}\
                     </span>\
                    <span class="m-list-timeline__time">\
                    </span>\
                </div>' ;

//var transport = signalR.TransportType.WebSockets;

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub", {
    accessTokenFactory: () => {
        // Get and return the access token.
        // This function can return a JavaScript Promise if asynchronous
        // logic is required to retrieve the access token.
    }
}).build();
var k1 = false;
var title = document.title;
var idclass;
var count = 0;
connection.on("ReceiveMessage", function (message) {
    //var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    count++
    // Notification
    if (!$(".m-topbar__notifications").hasClass("m-dropdown--open")) {
        $(".alert").addClass("m-nav__link-badge m-badge m-badge--dot m-badge--dot-small m-badge--danger");
        document.title = "("+count+")" + title;
    } else {
        removeNotification();
    }

    // Insert
    convert(message, m);
    $(".m-dropdown__header-title").html("");
    $(".m-dropdown__header-title").append(count+" Mới");
    //Click seen remove
    $(".m-list-timeline__item").click(function () {
        //$(this).children("."+idclass).removeClass("m-animate-blink");
        //connection.invoke("ClickMessage", idclass).catch(function (err) {
        //    return console.error(err.toString());
        //});
    });
    k1 = true;
});

//Get forcus title
connection.on("Focus", function (message) {
    if (message) {
        document.title = title;
        k = !message;
    }
    count = 0;
});
// Get remove notification
connection.on("Notification", function (message) {
    if (message) {
        $(".alert").removeClass("m-nav__link-badge m-badge m-badge--dot m-badge--dot-small m-badge--danger");
    }
});
//Get click message
connection.on("ClickMessage", function (idClass) {
    //$(".id-" + idClass).removeClass("m-animate-blink");
});

function convert(message, html) {
    if (message) {
        //var d = JSON.parse(message);
        var k = html;
        //idclass= d.Id;
        //k = k.replace("{1}", d.Value);
        //k = k.replace("{2}", d.Id);
        k = k.replace("{1}", message);
        k += $(".m-list-timeline__items").html();
        $(".m-list-timeline__items").html("");
        $(".m-list-timeline__items").append(k);
    }
}

connection.start().catch(function (err) {
    return console.error(err.toString());
});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    var user = document.getElementById("userInput").value;
//    var message = document.getElementById("messageInput").value;
//    connection.invoke("SendMessage", user, message).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

console.log(connection);

//connection.stop();

//connection.hub.stop();

connection.onclose(error => {
    //alert("1");
});

// Remove notification
$("#m_topbar_notification_icon").on('click', function () {
    $(".alert").removeClass("m-nav__link-badge m-badge m-badge--dot m-badge--dot-small m-badge--danger");
    removeNotification();
});

//$(".m-dropdown__wrapper").on('close', function () {
//});

$(window).focus(function () {
    document.title = title;
    if (k1) {
        connection.invoke("Focus").catch(function (err) {
            return console.error(err.toString());
        }); 
        k1 = false;
        count = 0;
    }
});
function removeNotification() {
    connection.invoke("Notification").catch(function (err) {
        return console.error(err.toString());
    });
}