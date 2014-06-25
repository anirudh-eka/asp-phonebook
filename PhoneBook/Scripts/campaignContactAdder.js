$(function () {

    $("#addContactToCampaign").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "contact/getContacts",
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
                            label: contact.Name + ", " + contact.Number,
                            value: contact.Name,
                            ID: contact.ID
                        }
                    }));

                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            window.location = 'contact/details/' + ui.item.ID;
        }
    });
});

var addToCampaign = function (contact) {
    buildContactElement(contact);
    $("#campaign-attendees").append();
}