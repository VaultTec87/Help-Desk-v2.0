$(function () {
    getAll("");
}); // jQuery default function

$("#main").click(function (e) { // cick on any row

    var probId = e.target.parentNode.id;
    if (probId === "main" || probId === "") {
        probId = e.target.id; // clicked on row somewhere else
    }

    if (probId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(probId);
    }
    else {         // reset fields
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("ProblemId", "new");
        $("#TextBoxProblem").val("");
        $("#TextBoxUpdate").prop("value", "Add");
        $("#TextBoxDelete").hide();
        loadProblemDLL(-1);
    }

});


function getById(probId) {
    ajaxCall("Get", "api/problems/" + probId)
    .done(function (prob) {
        copyInfoToModal(prob);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function copyInfoToModal(prob) {
    $("#TextBoxProblem").val(prob.Description);
    localStorage.setItem("ProblemId", prob.ProblemId);
    localStorage.setItem("Version", prob.Version);
    loadProblemDLL(prob.ProblemId);
}

function loadProblemDLL(prob) {
    $.ajax({
        type: "Get",
        url: "api/problems",
        contentType: "applicaton/json; charset=utf-8"
    })
    .done(function (data) {
        html = "";
        $("#ddlDepts").empty();
        $.each(data, function () {
            html += "<option value=\"" + this["ProblemId"] + "\">" + this["Description"] + "</option>";
        });
        $("#ddlDepts").append(html);
        $("#ddlDepts").val(prob);
    })
    .fail(function (jqXHR, testStatus, errorThrown) {
        alert("error");
    });
}

$("#ButtonDelete").click(function () {
    var deleteprob = confirm("really delete this problem?");
    if (deleteprob) {
        _delete();
        return !deleteprob;
    }
    else
        return deleteprob;
    
});

function _delete() {
    var id = localStorage.getItem("ProblemId");

    ajaxCall("Delete", "api/problems/" + id, "")
   .done(function (data) {
       $("#myModal").modal('hide');
       getAll(data);
   })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
        $("#myModal").modal('hide');
    });
}


$("#ButtonAction").click(function () {
    if ($("#ButtonAction").val() === "Update") {
        $("#ModalStatus").text("Loading...");
        update();
        getAll();
        $("#myModal").modal('hide');
        $("#ModalStatus").text("");
    }
    else {
        create();
        $("#myModal").modal('hide');
    }

    return false;
});



function update() {
    dept = new Object();
    dept.Description = $("#TextBoxProblem").val();
    dept.ProblemId = localStorage.getItem("ProblemId");
    dept.Version = localStorage.getItem("Version");

    ajaxCall("Put", "api/problems/", dept)
   .done(function (data) {
       getAll(data);
       
   })
   .fail(function (jqXHR, textStatus, errorThrown) {
       errorRoutine(jqXHR);
   });
}

function create() {
    prob = new Object();

    prob.Description = $("#TextBoxProblem").val();
    prob.Version = 1;

    ajaxCall("Post", "api/problems/", prob)
   .done(function (data) {
       getAll(data);
       $("#myModal").modal('hide');
   })
   .fail(function (jqXHR, textStatus, errorThrown) {
       errorRoutine(jqXHR);
   });


}

function buildTable(data) {
    $("#main").empty();
    div = $("<div class=\"list-group up-20\"><div>" +
        "<span class=\"col-xs-1 h4\">Problem Name</span>" +
        "</div>");
    div.appendTo($("#main"));
    prob = data;
    btn = $("<button class =\"list-group-item\" id=\"0\" " +
            "data-toggle=\"modal\"data-target=\"#myModal\">" +
            "<span class=\"text-primary\">Add New Problem...</span>");
    btn.appendTo(div);
    $.each(data, function (index, prob) {
        var probId = prob.ProblemId;
        btn = $("<button class =\"list-group-item\" id=\"" + prob.ProblemId +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html(
            "<span class=\"col-xs-3\" id=\"problemname" + prob.ProblemId + "\">" + prob.Description + "</span>");
        btn.appendTo(div);
    });
}





// get all employees
function getAll(msg) {
    $("#LabelStatus").text("Problems Loading...");

    ajaxCall("Get", "api/problems", "")
    .done(function (data) {
        buildTable(data);
        if (msg == "")
            $("#LabelStatus").text("Problems Loaded");
        else
            $("#LabelStatus").text(msg + " - Problems Loaded");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
} // getAll