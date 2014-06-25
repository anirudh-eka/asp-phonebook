$(function () {

    $("#addContactToCampaign").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "http://localhost:1147/contact/getContacts",
                dataType: "json",
                data: {
                    featureClass: "P",
                    style: "full",
                    maxRows: 12,
                    searchString: request.term
                },
                success: function (data) {
                    response($.map(data, function (contact) {
                        return {
                            nameAndNumber: contact.Name + ", " + contact.Number,
                            label: "Name: " + contact.Name + " Number: " + contact.Number,
                            value: contact.Name,
                            ID: contact.ID
                        }
                    }));

                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            addToCampaignView(ui.item);
        }
    });
});

var addToCampaignView = function (contact) {
    addToContactList(contact.nameAndNumber);

    
}

var addToContactList = function log(message) {
    $("<div>").text(message).prependTo("#campaign-attendees");
    $("#campaign-attendees").scrollTop(0);
}