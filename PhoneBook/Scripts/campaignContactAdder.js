/// <reference path="../Views/Campaign/AddContacts.cshtml" />
$(function () {
    $("#addContactToCampaign").autocomplete({
        source: function(request, response) {
            $.ajax({
                url: "http://localhost:1147/contact/getContacts",
                dataType: "json",
                data: {
                    featureClass: "P",
                    style: "full",
                    maxRows: 12,
                    searchString: request.term
                },
                success: function(data) {
                    response($.map(data, function(contact) {
                        return {
                            label: contact.Name + "    " + contact.Number + " (#" + contact.ID + ")",
                            value: contact.Name,
                            ID: contact.ID,
                            Name: contact.Name,
                            Number: contact.Number
                        };
                    }));
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            var contact = ui.item;
            var contactRow = $("div").find("[contactID='" + contact.ID + "']");
            if (contactRow.length > 0) {
                $("#add-duplicate-confirm").dialog({
                    resizable: false,
                    height: 180,
                    modal: false,
                    buttons: {
                        "OK": function() {
                            $(this).dialog("close");
                        },
                        
                    }
                });
            } else {
                addToCampaignView(contact);
            }
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
 };
var addToContactList = function (contact) {
    prependToContactAttendees(contact);
    setUpDeleteButton(contact.ID);
    $("#campaign-attendees").scrollTop(0);
    $('#addContactToCampaign').val("");
    $('#addContactToCampaign').focus();
};
var setUpDeleteButton = function(contactID) {
    var contactRow = $("div").find("[contactID='" + contactID + "']");
    var deleteButton = contactRow[2];
    $(deleteButton).on('click', function () {
        $("#delete-confirm").dialog({
            resizable: false,
            height: 180,
            modal: true,
            buttons: {
                "Delete": function () {
                    $(this).dialog("close");
                    removeFromCampaignDatabase(contactID);
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });
};
var prependToContactAttendees = function(contact) {
    $("<div contactId=" + contact.ID + ">").text(contact.Name).prependTo(".campaign-attendees-names");
    $("<div contactId=" + contact.ID + ">").text(contact.Number).prependTo(".campaign-attendees-phonenumbers");
    $("<div class='delete-button-container' contactId=" + contact.ID + ">").html("<button class='delete-button'>X</button>").prependTo(".campaign-attendees-delete");
};
var removeFromCampaignDatabase = function (contactID) {
    var campaignID = $('#ID').attr('value');
    var contactRow = $("div").find("[contactID='" + contactID + "']");
    $.ajax({
        url: "http://localhost:1147/campaign/DeleteContact",
        type: "POST",
        data: {
            contactID: contactID,
            campaignID: campaignID
        },
        dataType: "json",
        success: function () {
            contactRow.remove();
        },

        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};