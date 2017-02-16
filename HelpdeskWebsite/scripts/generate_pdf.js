$(function () {
    $("#pdfbutton").click(function (e) {
        $("#lblstatus").text("generating report on the server - please wait...");
        ajaxCall("Get", "api/helloreport", "").done(function (data) {
            if (data == "report generated") {
                window.open('/pdfs/HelloWorld.pdf');
                return false
            }

        }).fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    });
});

function ajaxCall(type, url, data) {
    return $.ajax({
        type: type,
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: true
    });

    function errorRoutine(jqXHR) {
        if (jqXHR.responseJSON == null) {
            $("#lblstatus").text(jqXHR.responseText);
        }
        else {
            $("#lblstatus").text(jqXHR.responseJSON.Message);
        }
    }
}