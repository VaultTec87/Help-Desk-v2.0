$(function () {
    getAll("");

$("#ProblemModalForm").validate({
    rules: {
        TextBoxProblem: { maxlength: 25, required: true },
    },
    ignore: ".ignore, :hidden",
    errorElement: "div",
    wrapper: "div",
    messages: {
        TextBoxProblem: {
            required: "required 1-25 chars.", maxlength: "required 1-25 chars."
        },

    }
});

$("#main").click(function (e) { 

    var probId = e.target.parentNode.id;
    if (probId === "main" || probId === "") {
        probId = e.target.id; 
    }

    if (probId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(probId);
    }
    else {        
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxProblem").val("");
        $("#TextBoxUpdate").prop("value", "Add");
        $("#TextBoxDelete").hide();
       
    }

});
$("#ButtonAction").click(function () {
    if ($("#ProblemModalForm").valid()) {
        if ($("#ButtonAction").val() === "Update") {
            update();
            $("#myModal").modal('hide');
        }
        else {
            create();
            $("#myModal").modal('hide');
        }

        return false;
    }
});

$("#ButtonDelete").click(function () {
    var deleteEmp = confirm("really delete this employee?");
    if (deleteEmp) {
        _delete();
        return !deleteEmp;
    }
    else
        return deleteEmp;
});

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
    var validator = $('#ProblemModalForm').validate();
    validator.resetForm();

    $("#TextBoxProblem").val(prob.Description);
    localStorage.setItem("Id", prob.Id);
    localStorage.setItem("Version", prob.Version);
}

function _delete() {
    var id = localStorage.getItem("Id");

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

function update() {
    dept = new Object();
    dept.Description = $("#TextBoxProblem").val();
    dept.Id = localStorage.getItem("Id");
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
        var probId = prob.Id;
        btn = $("<button class =\"list-group-item\" id=\"" + prob.Id +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html(
            "<span class=\"col-xs-3\" id=\"problemname" + prob.Id + "\">" + prob.Description + "</span>");
        btn.appendTo(div);
    });
}

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
} 