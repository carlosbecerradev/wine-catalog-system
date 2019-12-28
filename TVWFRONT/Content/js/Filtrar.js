$("#btnOrdenDeMenor").click(function () {
    $.ajax({
        type: "GET",
        url: "/Home/precioMayorAMenor/",
        success: function (data) {
            $("#productos").empty();
            $("#productos").append("<p>Funca</p>");
        }
    });


});

