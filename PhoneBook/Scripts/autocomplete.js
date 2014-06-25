$(function () {
    console.log("Hi im runnnin");
//    function log(message) {
//        $("<div>").text(message).prependTo("#log");
//        $("#log").scrollTop(0);
//    }
        var availableTags = [
        "ActionScript",
        "AppleScript",
        "Asp",
        "BASIC",
        "C",
        "C++",
        "Clojure",
        "COBOL",
        "ColdFusion",
        "Erlang",
        "Fortran",
        "Groovy",
        "Haskell",
        "Java",
        "JavaScript",
        "Lisp",
        "Perl",
        "PHP",
        "Python",
        "Ruby",
        "Scala",
        "Scheme"
        ];
    $("#searchBox").autocomplete({
        source: function (request, response) {
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
                    console.log(data);
                    console.log(data[0]);
                    console.log(data[0].Name);
                    response($.map(data, function (contact) {
                        return {
                            label: contact.Name +  ", " + contact.Number,
                            value: contact.Name
                        }
                    }));
                    
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
//            log(ui.item ?
//            "Selected: " + ui.item.label :
//            "Nothing selected, input was " + this.value);
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
});