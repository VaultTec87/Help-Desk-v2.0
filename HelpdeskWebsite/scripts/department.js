$(function () {
    getAll("");

$("#DepartmentModalForm").validate({
    rules: {
        TextBoxDepartment: { maxlength: 25, required: true },
        ddlManager: {required: true}
    },
    ignore: ".ignore, :hidden",
    errorElement: "div",
    wrapper: "div",
    messages: {
        TextBoxDepartment: {
            required: "required 1-25 chars.", maxlength: "required 1-25 chars."
        },
        ddlManager: {
            required: "Select a Manager."
        }
            
    }
});

$("#main").click(function (e) {

    var deptId = e.target.parentNode.id;
    if (deptId === "main" || deptId === "") {
        deptId = e.target.id;
    }

    if (deptId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(deptId);
    }
    else {
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxDepartment").val("");
        $("#TextBoxUpdate").prop("value", "Add");
        $("#TextBoxDelete").hide();
        loadDepartmentDLL(-1);
        loadManagerDDL(-1);
    }

});

$("#ButtonAction").click(function () {
    if ($("#DepartmentModalForm").valid()) {
        if ($("#ButtonAction").val() === "Update") {
            update();
            $("#myModal").modal('hide');
        }
        else {
            create();
            $("#myModal").modal('hide');
        }
    }
        return false;
    
});

$("#ButtonDelete").click(function () {
    var deleteEmp = confirm("really delete this Department?");
    if (deleteEmp) {
        _delete();
        return !deleteEmp;
    }
    else
        return deleteEmp;
});

});

function getById(deptId) {
    ajaxCall("Get", "api/departments/" + deptId)
    .done(function (dept) {
        copyInfoToModal(dept);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function copyInfoToModal(dept) {
    var validator = $('#DepartmentModalForm').validate();
    validator.resetForm();
    $("#TextBoxDepartment").val(dept.DepartmentName);
    localStorage.setItem("Id", dept.Id);
    localStorage.setItem("Version", dept.Version);
    loadDepartmentDLL(dept.Id);
    loadManagerDDL(dept.ManagerId);
}

function loadDepartmentDLL(dept) {
    $.ajax({
        type: "Get",
        url: "api/departments",
        contentType: "applicaton/json; charset=utf-8"
    })
    .done(function (data) {
        html = "";
        $("#ddlDepts").empty();
        $.each(data, function () {
            html += "<option value=\"" + this["Id"] + "\">" + this["DepartmentName"] + "</option>";
        });
        $("#ddlDepts").append(html);
        $("#ddlDepts").val(dept);
    })
    .fail(function (jqXHR, testStatus, errorThrown) {
        alert("error");
    });
}

function loadManagerDDL(manager)
{
    $.ajax({
        type: "Get",
        url: "api/employees",
        contentType: "applicaton/json; charset=utf-8"
    })
    .done(function (data) {
        html = "";
        $("#ddlManager").empty();
        $.each(data, function () {
            html += "<option value=\"" + this["Id"] + "\">" + this["Lastname"] + "</option>";
        });
        $("#ddlManager").append(html);
        $("#ddlManager").val(manager);
    })
    .fail(function (jqXHR, testStatus, errorThrown) {
        alert("error");
    });
}

function _delete() {
    dept = new Object();
    dept.Id = localStorage.getItem("Id");
    ajaxCall("Delete", "api/departments/", dept)
   .done(function (data) {
       getAll("1 - Department was DELETED! ");
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
    dept.DepartmentName = $("#TextBoxDepartment").val();
    dept.ManagerId = $("#ddlManager").val();
    dept.Id = localStorage.getItem("Id");
    dept.Version = localStorage.getItem("Version");

    ajaxCall("Put", "api/departments/", dept)
   .done(function (data) {
       getAll(data);
   })
   .fail(function (jqXHR, textStatus, errorThrown) {
       errorRoutine(jqXHR);
   });
}

function create() {
    dept = new Object();
    
    dept.DepartmentName = $("#TextBoxDepartment").val();
    dept.ManagerId = $("#ddlManager").val();
    dept.Version = 1;

    ajaxCall("Post", "api/departments/", dept)
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
        "<span class=\"col-xs-1 h4\">Department Name</span>" +
        "</div>");
    div.appendTo($("#main"));
    dept = data;
    btn = $("<button class =\"list-group-item\" id=\"0\" " +
            "data-toggle=\"modal\"data-target=\"#myModal\">" +
            "<span class=\"text-primary\">Add New Department...</span>");
    btn.appendTo(div);
    $.each(data, function (index, dept) {
        var deptId = dept.Id;
        btn = $("<button class =\"list-group-item\" id=\"" + dept.Id +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html(
            "<span class=\"col-xs-3\" id=\"departmentname" + dept.Id + "\">" + dept.DepartmentName + "</span>");
        btn.appendTo(div);
    });
}

function getAll(msg) {
    $("#LabelStatus").text("Departments Loading...");

    ajaxCall("Get", "api/departments", "")
    .done(function (data) {
        buildTable(data);
        if (msg == "")
            $("#LabelStatus").text("Departments Loaded");
        else
            $("#LabelStatus").text(msg + " - Departments Loaded");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
} 