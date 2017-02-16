function ajaxCall(type, url, data)
{
    return $.ajax
    ({
        type: type,
        url: url,
        data: JSON.stringify(data),
        contentType: "application/json; charset-utf-8",
        dataType: "json",
        processData: true,

    });
}

function errorRoutine(jqXHR)
{
    if (jqXHR.responseJSON == null)
    {
        $("#LabelStaus").text(jqXHR.responseJSON.Message);
    }
    else
    {
        $("#LabelStatus").text(jqXHR.responseJSON.Message);
    }
}