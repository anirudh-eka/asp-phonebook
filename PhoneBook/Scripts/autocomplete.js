$(function () {
    console.log("Hi im runnnin");

    $("#searchBox").autocomplete({
        source: function(request, response) {
            console.log("oh you wanna ajax huh?");
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
                            label: contact.Name + ", " + contact.Number,
                            value: contact.Name,
                            ID: contact.ID
                        }
                    }));

                }
            });
        },
        minLength: 2,
        select: function(event, ui) {
            window.location = 'contact/details/' + ui.item.ID;
        }
    });
});