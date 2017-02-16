$(function ()
{
    buildTable();
});

$("#main").click(function (e)
{
    var utilId = e.target.parentNode.id;
    if(utilId === "main" || utilId === "")
    {
        utilId = e.target.id;
    }
    if (utilId === "0") {
        LoadCollections();
    }
    if(utilId === "1")
    {
        generate_emp_report();
    }
    if(utilId === "2")
    {
        generate_call_report();
    }
    else { }
});

function buildTable()
{
    $("#main").empty();
    div = $("<div class=\"list-group up-20\"</div>"
        + "<span class=\"col-xs-10 h4\">Available Utitlities</span>"
        + "</div>");
    btn = $("<button class=\"list-group-item\" id=\"0\">"
        + "<span class=\"text-primary\">Re-load Helpdesk Collections</span>");
    btn.appendTo(div);
    div.appendTo($("#main"))
    
    btn = $("<button class=\"list-group-item\" id=\"1\">"
        + "<span class=\"text-primary\">Employee Report</span>");
    btn.appendTo(div);
    div.appendTo($("#main"))
    
    btn = $("<button class=\"list-group-item\" id=\"2\">"
        + "<span class=\"text-primary\">Call Report</span>");
    btn.appendTo(div);
    div.appendTo($("#main"))
}

function LoadCollections()
{
    $("#LabelStatus").text("Deleting and Redefining Collections...");
    ajaxCall('Get', 'api/collections').done(function (data)
    {
        $('#LabelStatus').text(data);

    }).fail(function (jqXHR, textStatus, errorThrown)
    {
        errorRoutine(jqXHR);
    });
}

function generate_emp_report() {

    $("#LabelStatus").text("generating report on the server - please wait...");
    ajaxCall("Get", "api/helloreport", "").done(function (data) {
        if (data == "report generated") {
            window.open('/pdfs/HelloWorld.pdf');
            return false
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function generate_call_report() {

    $("#LabelStatus").text("generating report on the server - please wait...");
    ajaxCall("Get", "api/callreport", "").done(function (data) {
        if (data == "report generated") {
            window.open('/pdfs/CallPdf.pdf');
            return false
        }

    }).fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}