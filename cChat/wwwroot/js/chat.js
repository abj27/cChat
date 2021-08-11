var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var messages = [];
document.getElementById("sendButton").disabled = true;

var updateUI = function () {
    var $container = $("#messagesList");
    $container.empty();
    for (var i = 0; i < messages.length; i++) {
        var message = messages[i];
        var li = $(`<li>${message.senderName.toLowerCase()} says ${message.text}</li>`);
        $container.append(li);
    }
}
connection.on("ReceiveMessage", function (message) {
    if (messages.length > 49) {
        messages.pop();
    }
    messages.unshift(message);
    updateUI();
});

connection.on("LoadMessages", function (loadedMessages) {
    messages = loadedMessages;
    updateUI();
});


var start = function () {
    connection.start().then(function () {
        document.getElementById("sendButton").disabled = false;
        connection.invoke("Connected").catch(function (err) {
            return console.error(err.toString());
        });
    }).catch(function (err) {
        setTimeout(start, 5000);
        return console.error(err.toString());
    });
}
connection.onclose(function () {
    start();
});
start();

document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage",  message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
     document.getElementById("messageInput").value = "";
});