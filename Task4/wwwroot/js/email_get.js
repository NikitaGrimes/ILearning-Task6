"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", function (id, name, title, body, dateTime) {
    var htmlBefor = document.getElementById("messagesList").innerHTML;
    var htmlString = "<a class=\"media-body text-decoration-none text-reset\" data-bs-toggle=\"collapse\" href=\"#" + id + "\" role=\"button\" aria-expanded=\"false\" aria-controls=\"" + id + "\">" +
        "<p class=\"mb-1\"><strong>" + name + "</strong></p>" +
        "<p class=\"mb-1\"><strong>" + title + "</strong></p>" +
        "<p class=\"small text-muted\">" + dateTime + "</p>" +
        "</a>" +
        "<div class=\"collapse\" id=\"" + id + "\">" +
        "<div class=\"card card - body\">" +
        "<p class=\"mb - 1\">" + body + "</p>" +
        " </div> " +
        "</div>";
    document.getElementById("messagesList").innerHTML = htmlString + htmlBefor;
});
connection.start();