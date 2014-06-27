/// <reference path="../Views/Campaign/AddContacts.cshtml" />
var campaignAttendees = {};
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
            if (contact.ID in campaignAttendees && campaignAttendees[contact.ID] > 0) {
                $("#add-duplicate-confirm").dialog({
                    resizable: false,
                    height: 180,
                    modal: true,
                    buttons: {
                        "Add anyways": function() {
                            $(this).dialog("close");
                            addToCampaignView(contact);
                        },
                        Cancel: function() {
                            $(this).dialog("close");
                            $('#addContactToCampaign').val("");
                            $('#addContactToCampaign').focus();
                        }
                    }
                });
            } else {
                console.log("here");
                campaignAttendees[contact.ID] = 0;
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
            console.log("campaign attendee: " + contact.ID + "| " + campaignAttendees[contact.ID]);
            campaignAttendees[contact.ID] += 1;
            console.log("-> " + campaignAttendees[contact.ID]);
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
    var newestDeleteButtonIndex = 2 + (campaignAttendees[contactID] - 1) * 2;
    console.log("new index: " + newestDeleteButtonIndex);
    var deleteButton = contactRow[newestDeleteButtonIndex];
    console.log(deleteButton.className);
    $(deleteButton).on('click', function () {
        $("#delete-confirm").dialog({
            resizable: false,
            height: 180,
            modal: true,
            buttons: {
                "Delete": function () {
                    $(this).dialog("close");
                    removeFromCampaignDatabase(contactID, contactRow);
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
var removeFromCampaignDatabase = function (contactID, contactRow) {
    var campaignID = $('#ID').attr('value');
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
            campaignAttendees[contactID] -= 1;
            if (campaignAttendees <= 0) {
                delete campaignAttendees[contactID];
            }
        },

        error: function (xhr, ajaxOptions, thrownError) {
            alert(xhr.status);
            alert(thrownError);
        }
    });
};