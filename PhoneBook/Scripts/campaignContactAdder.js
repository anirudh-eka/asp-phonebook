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
                            ID: contact.ID,
                            Name: contact.Name,
                            Number: contact.Number
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
    addToContactList(contact);
    var campaignId = $('#ID').attr('value');
    $.ajax({
        url: "http://localhost:1147/campaign/postContact",
        type: "POST",
        data: {
            contactName: contact.Name,
            contactNumber: contact.Number,
            contactID: contact.ID,
            campaignID: campaignId,
            },
        dataType: "json",
        success: function (result) {
            switch (result) {
                case true:
                    alert(result);
                    break;
                default:
                    alert(result);
            }
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
    
}

var addToContactList = function log(contact) {
    $("<div>").text(contact.Name).prependTo(".campaign-attendees-names");
    $("<div>").text(contact.Number).prependTo(".campaign-attendees-phonenumbers");
    $("#campaign-attendees").scrollTop(0);
}