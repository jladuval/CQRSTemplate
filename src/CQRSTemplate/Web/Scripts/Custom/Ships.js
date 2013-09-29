$(function () {

    // Declare a proxy to reference the hub. 
    var chat = $.connection.eventHub;
    // Create a function that the hub can call to broadcast messages.
    chat.client.eventStatus = function (name) {
        // Html encode display name and message. 
        var encodedMsg = $('<div />').text(name).html();
        // Add the message to the page. 
        $('#events').append('<li><strong>Update:</strong>:&nbsp;&nbsp;' + encodedMsg + '</li>');
    };

    chat.client.shipCreated = function (name, id) {
        // Add the message to the page. 
        var encodedname = $('<div />').text(name).html();
        var encodedid = $('<div />').text(id).html();
        $('#shipList').append('<li><strong>' + encodedname + '</strong> has an Id of ' + encodedid + '</li>');
    };
    // Set initial focus to message input box.  
    $('#name').focus();
    // Start the connection.
    $.connection.hub.start().done(function () {
        $('#createShip').click(function () {
            $.post('/Home/CreateCruiseShip', { name: $('#name').val() });
            // Call the Send method on the hub. 
            $('#name').val('').focus();
        });
    });
});