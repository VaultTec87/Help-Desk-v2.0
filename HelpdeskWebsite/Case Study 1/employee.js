$(function () {
    getAll("");
}); // jQuery default function

$("#main").click(function (e) { // cick on any row

    var empId = e.target.parentNode.id;
    if (empId === "main" || empId === "") {
        empId = e.target.id; // clicked on row somewhere else
    }

    if (empId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(empId);
    }
    else {         // reset fields
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        $("#TextBoxUpdate").prop("value", "Add");
        $("#TextBoxDelete").hide();
        loadDepartmentDLL(-1);
    }

}); 


function getById(empId) {
    ajaxCall("Get", "api/employees/" + empId)
    .done(function (emp) {
        copyInfoToModal(emp);
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
}

function copyInfoToModal(emp) {
    $("#TextBoxTitle").val(emp.Title);
    $("#TextBoxFirstname").val(emp.Firstname);
    $("#TextBoxLastname").val(emp.Lastname);
    $("#TextBoxPhone").val(emp.Phoneno);
    $("#TextBoxEmail").val(emp.Email);
    localStorage.setItem("Id", emp.Id);
    localStorage.setItem("Version", emp.Version);
    loadDepartmentDLL(emp.DepartmentId);
} 

function loadDepartmentDLL(empdep) {
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
        $("#ddlDepts").val(empdep);
    })
    .fail(function (jqXHR, testStatus, errorThrown) {
        alert("error");
    });
}

$("#ButtonDelete").click(function () {
    var deleteEmp = confirm("really delete this employee?");
    if (deleteEmp) {
        _delete();
        return !deleteEmp;
    }
    else
        return deleteEmp;
}); 

function _delete() {
    emp = new Object();
    emp.Id = localStorage.getItem("Id");
    ajaxCall("Delete", "api/employees/", emp)
   .done(function (data) {
       getAll(data);
       $("#myModal").modal('hide');
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
    emp = new Object();
    emp.Title = $("#TextBoxTitle").val();
    emp.Firstname = $("#TextBoxFirstname").val();
    emp.Lastname = $("#TextBoxLastname").val();
    emp.Phoneno = $("#TextBoxPhone").val();
    emp.Email = $("#TextBoxEmail").val();
    emp.DepartmentId = $("#ddlDepts").val();
    emp.Id = localStorage.getItem("Id");
    emp.Version = localStorage.getItem("Version");
    
    ajaxCall("Put", "api/employees/", emp)
   .done(function (data) {
       getAll(data);
       $("#myModal").modal('hide');
       //$("#LabelStatus").text(emp)
   })
   .fail(function (jqXHR, textStatus, errorThrown) {
       errorRoutine(jqXHR);
   });


}

function create() {
    emp = new Object();
    emp.Title = $("#TextBoxTitle").val();
    emp.Firstname = $("#TextBoxFirstname").val();
    emp.Lastname = $("#TextBoxLastname").val();
    emp.Phoneno = $("#TextBoxPhone").val();
    emp.Email = $("#TextBoxEmail").val();
    emp.DepartmentId = $("#ddlDepts").val();
    emp.Version = 1;

    ajaxCall("Post", "api/employees/", emp)
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
        "<span class=\"col-xs-1 h4\">Title</span>" +
        "<span class=\"col-xs-5 h4\">First</span>" +
        "<span class=\"col-xs-3 h4\">Last</span>" +
        "</div>");
    div.appendTo($("#main"));
    employees = data; 
    btn = $("<button class =\"list-group-item\" id=\"0\" " +
            "data-toggle=\"modal\"data-target=\"#myModal\">" +
            "<span class=\"text-primary\">Add New Employee...</span>");
    btn.appendTo(div);
    $.each(data, function (index, emp) {
        var empId = emp.Id;
        btn = $("<button class =\"list-group-item\" id=\"" + emp.Id +
            "\" data-toggle=\"modal\" data-target=\"#myModal\">");
        btn.html(
            "<span class=\"col-xs-3\" id=\"employeetitle" + empId + "\">" + emp.Title + "</span>" +
            "<span class=\"col-xs-4\" id=\"employeefname" + empId + "\">" + emp.Firstname + "</span>" +
            "<span class=\"col-xs-4\" id=\"emplastname" + empId + "\">" + emp.Lastname + "</span>" 
            );
        btn.appendTo(div);
    }); 
}





// get all employees
function getAll(msg) {
    $("#LabelStatus").text("Employees Loading...");

    ajaxCall("Get", "api/employees", "")
    .done(function (data) {
        buildTable(data);
        if(msg == "")
            $("#LabelStatus").text("Employees Loaded");
        else
            $("#LabelStatus").text(msg + " - Employees Loaded");
    })
    .fail(function (jqXHR, textStatus, errorThrown) {
        errorRoutine(jqXHR);
    });
} // getAll