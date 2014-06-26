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
    
    var campaignId = $('#ID').attr('value');
    $.ajax({
        url: "http://localhost:1147/campaign/PostContact",
        type: "POST",
        data: {
            contactID: contact.ID,
            campaignID: campaignId
            },
        dataType: "json",
        success: function () {
            addToContactList(contact);
            },
        
        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
 }   

var addToContactList = function (contact) {
    prependToContactAttendees(contact);
    setUpDeleteButton(contact.ID);
    $("#campaign-attendees").scrollTop(0);
}

var setUpDeleteButton = function(id) {
    var selector = $("div").find("[contactID='" + id + "']");
    var deleteButton = selector[2];

    $(deleteButton).on('click', function () {
        selector.remove();
    });
}

var prependToContactAttendees = function(contact) {
    $("<div contactId=" + contact.ID + ">").text(contact.Name).prependTo(".campaign-attendees-names");
    $("<div contactId=" + contact.ID + ">").text(contact.Number).prependTo(".campaign-attendees-phonenumbers");
    $("<div class='delete-button' contactId=" + contact.ID + ">").text("[X]").prependTo(".campaign-attendees-delete");
}