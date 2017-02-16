$(function () {
    getAll("");

    $("#CallModalForm").validate({
        rules: {
            ddlProblem: {  required: true },
            ddlEmployees: {  required: true },
            ddlTech: { required: true },
            Notes: { maxlength:250, required: true},
            
        },
        ignore: ".ignore, :hidden",
        errorElement: "div",
        wrapper: "div",
        messages: {
            ddlProblem: {
                required: "select a problem."
            },
            ddlEmployees: {
                required: "select an employee."
            },
            ddlTech: {
                required: "select a technician."
            },
            Notes: {
                required: "1-250 chars required.", maxlength: 250,
            }

        }
    });


    $("#main").click(function (e)
    {
        var empId = e.target.parentNode.id;

        if (empId === "main" || empId === "")
        {
            empId = e.target.id;
        }

        if (empId !== "0")
        {
            $("#ButtonAction").prop("value", "Update");
            $("#ButtonDelete").show();
            getById(empId);
        }
        else
        {
            $("#ddlProblem").attr("disabled", false);
            $("#ddlEmployees").attr("disabled", false);
            $("#ddlTech").attr("disabled", false);
            $("#ButtonDelete").hide();
            $("#ButtonAction").prop("value", "Add");
            $("#Notes").val("");
            $("#Notes").attr("disabled", false);
            $("#Notes").attr("readonly", false);
            $("#DateOpened").val(formatDate());
            document.getElementById("CloseCall").checked = false;
            document.getElementById("CloseCall").disabled = false;
            $("#HideCloseCall").hide();
            $("#ButtonAction").show();
            $("#HideDateClosed").hide();
            $("#DateClosed").val("");
            $("#LabelDateOpened").text(formatDate());
            $("#LabelDateClosed").text("");

            localStorage.setItem("Id", "new");

            loadEmployeeDLL(-1);
            loadProblemDLL(-1);
            loadTechDll(-1);

           ($('#CallModalForm').validate()).resetForm();
        }
    });

    $("#ButtonAction").click(function ()
    {
        if ($("#CallModalForm").valid())
        {
            if ($("#ButtonAction").val() === "Update")
            {
                update();
                $("#myModal").modal('hide');
            }
            else
            {
                create();
                $("#myModal").modal('hide');
            }

            return false;
        }
    });

    $("#ButtonDelete").click(function () {
        var deleteEmp = confirm("really delete this call?");
        if (deleteEmp) {
            _delete();
            return !deleteEmp;
        }
        else
            return deleteEmp;
    });

});

function getById(empId)
{
    ajaxCall("Get", "api/Calls/" + empId + "")
    .done(function (emp) {
        copyInfoToModal(emp);

    }).fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

$("#CloseCall").click(function ()
{
    if ($("#CloseCall").is(":checked"))
    {
        $("#LabelDateClosed").text(formatDate());
        $("#DateClosed").val(formatDate());
    }
    else
    {
        $("#LabelDateClosed").text("");
        $("#DateClosed").val("");
    }
});

function copyInfoToModal(call)
{
    var validator = $('#CallModalForm').validate();
    validator.resetForm();

    localStorage.setItem("Id", call.Id);
    localStorage.setItem("Version", call.Version);

    $("#DateOpened").val(formatDate(call.DateOpened));
    $("#LabelDateOpened").text(formatDate(call.DateOpened));
    $("#Notes").val(call.Notes);
    $("#HideCloseCall").show();
    $("#HideDateClosed").show();

    loadProblemDLL(call.ProblemId);
    loadEmployeeDLL(call.EmployeeId);
    loadTechDll(call.TechId);

    if ($("#CloseCall").is(":checked"))
    {
        call.DateClosed = $("#DateClosed").val();
        $("#LabelDateClosed").text(formatDate(call.DateClosed));
        $("#HideDateClosed").show();
        $("#ButtonAction").show();
    }

    if (call.OpenStatus)
    {
        $("#LabelDateClosed").text("");
        $("#ddlEmployees").attr("disabled", false);
        $("#ddlProblem").attr("disabled", false);
        $("#ddlTech").attr("disabled", false);
        $("#Notes").attr("disabled", false);
        $("#Notes").attr("readonly", false);
        document.getElementById("CloseCall").checked = false;
        document.getElementById("CloseCall").disabled = false;
        $("#ButtonAction").show();
    }
    else
    {
        $("#LabelDateClosed").text(formatDate(call.DateClosed));
        $("#ddlEmployees").attr("disabled", true);
        $("#ddlProblem").attr("disabled", true);
        $("#ddlTech").attr("disabled", true);
        $("#Notes").attr("readonly", "readonly");
        $("#CloseCall").prop("checked", true);
        $("#CloseCall").attr("disabled", true);
        $("#ButtonAction").hide();
    }

}

function loadProblemDLL(prob)
{
    $.ajax({
        type: "Get",
        url: "api/problems",
        contentType: "applicaton/json; charset=utf-8"
    })
    .done(function (data)
    {
        html = "";
        $("#ddlProblem").empty();
        $.each(data, function () {
            html += "<option value=\"" + this["Id"] + "\">" + this["Description"] + "</option>";
        });
        $("#ddlProblem").append(html);
        $("#ddlProblem").val(prob);
    }).fail(function (jqXHR, testStatus, errorThrown) {
        alert("error");
    });
}

function loadEmployeeDLL(emps)
{
    $.ajax({
        type: "Get",
        url: "api/employees",
        contentType: "applicaton/json; charset=utf-8"
    })
    .done(function (data)
    {
        html = "";
        $("#ddlEmployees").empty();
        $.each(data, function ()
        {
            html += "<option value=\"" + this["Id"] + "\">" + this["Lastname"] + "</option>";
        });
        $("#ddlEmployees").append(html);
        $("#ddlEmployees").val(emps);
    }).fail(function (jqXHR, testStatus, errorThrown)
    {
        alert("error");
    });
}

function loadTechDll(emps)
{
    $.ajax({
        type: "Get",
        url: "api/allTechs",
        contentType: "applicaton/json; charset=utf-8"
    }).done(function (data)
    {
        html = "";
        $("#ddlTech").empty();
        $.each(data, function ()
        {
            html += "<option value=\"" + this["Id"] + "\">" + this["Lastname"] + "</option>";
        });

        $("#ddlTech").append(html);
        $("#ddlTech").val(emps);
    }).fail(function (jqXHR, testStatus, errorThrown)
    {
        alert("error");
    });
}

function _delete()
{
    emp = new Object();
    emp.Id = localStorage.getItem("Id");

    ajaxCall("Delete", "api/Calls/", emp)
   .done(function (data)
   {
       getAll(data);
       $("#myModal").modal('hide');
   }).fail(function (jqXHR, textStatus, errorThrown)
   {
        errorRoutine(jqXHR);
        $("#myModal").modal('hide');
    });
}

function update()
{
        call = new Object();

        call.EmployeeId = $("#ddlEmployees").val();
        call.ProblemId = $("#ddlProblem").val();
        call.TechId = $("#ddlTech").val();
        call.DateOpened = $("#DateOpened").val();
        call.DateClosed = $("#DateClosed").val();
        call.Notes = $("#Notes").val();
        call.Id = $("#hidden").val();
        call.Id = localStorage.getItem("Id");
        call.Version = localStorage.getItem("Version");
    

        if (!$("#CloseCall").is(":checked"))
        {
            call.DateClosed = null;
            call.OpenStatus = true;
        }
        else
        {
            call.DateClosed = $("#DateClosed").val();
            $("#LabelDateClosed").text(formatDate(call.DateClosed));
            call.OpenStatus = false;
        }

        ajaxCall("Put", "api/Calls/", call).done(function (data)
        {
            $("#myModal").modal('hide');
            getAll(data);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
        
}

function create()
{
    call = new Object();

    call.DateOpened = $("#DateOpened").val();
    call.Notes = $("#Notes").val();
    call.OpenStatus = true;
    call.EmployeeId = $("#ddlEmployees").val();
    call.TechId = $("#ddlTech").val();
    call.ProblemId = $("#ddlProblem").val();
    call.Version = 1;

    

   
    
    ajaxCall("Post", "api/Calls/", call).done(function (call)
    {
       getAll(call);
    })
   .fail(function (jqXHR, textStatus, errorThrown) {
       errorRoutine(jqXHR);
   });
}

function buildTable(data)
{
    $("#main").empty();

    div = $("<div class=\"list-group up-20\"><div>" +
        "<span class=\"col-xs-2 h4\">Date Opened</span>" +
        "<span class=\"col-xs-4 h4\">For</span>" +
        "<span class=\"col-xs-3 h4\">Problem</span>" +
        "</div>");

    div.appendTo($("#main"));

    employees = data;
    btn = $("<button class =\"list-group-item\" id=\"0\" " +
            "data-toggle=\"modal\"data-target=\"#myModal\">" +
            "<span class=\"text-primary\">Add New Call...</span>");

    btn.appendTo(div);

    $.each(data, function (index, call)
    {
        var callId = call.Id;

        btn = $("<button class =\"list-group-item\" id=\"" + call.Id +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html(
            "<span class=\"col-xs-3\" id=\"Date" + callId + "\">" + formatDate(call.DateOpened) + "</span>" +
            "<span class=\"col-xs-4\" id=\"Employee" + callId + "\">" + call.employee_last_name + "</span>" +
            "<span class=\"col-xs-4\" id=\"Problem" + callId + "\">" + call.problem_description + "</span>"
            );

        btn.appendTo(div);
    });
}

function getAll(msg)
{
    $("#LabelStatus").text("Calls Loading...");
    ajaxCall("Get", "api/Calls", "")
    .done(function (data)
    {
        buildTable(data);
        if (msg == "")
            $("#LabelStatus").text("Calls Loaded");
        else
            $("#LabelStatus").text(msg + " - Calls Loaded");
    })
    .fail(function (jqXHR, textStatus, errorThrown)
    {
        errorRoutine(jqXHR);
    });
} 

function formatDate(date)
{
    var d;
    if (date === undefined || date === null)
    {
        d = new Date();
    }
    else
    {
        var d = new Date(Date.parse(date));
    }

    var _day = d.getDate();
    var _month = d.getMonth() + 1;
    var _year = d.getFullYear();
    var _hour = d.getHours();
    var _min = d.getMinutes();
    if (_min < 10) { _min = "0" + _min; }
    return _year + "-" + _month + "-" + _day + " " + _hour + ":" + _min;
}