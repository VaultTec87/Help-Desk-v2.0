$(function () {
    getAll("");

$("#EmployeeModalForm").validate({
    rules: {
        TextBoxTitle: { maxlength: 4, required: true, validTitle: true },
        TextBoxFirstname: { maxlength: 25, required: true },
        TextBoxLastname: { maxlength: 25, required: true },
        TextBoxEmail: { maxlength: 40, required: true, email: true },
        TextBoxPhone: { maxlength: 15, required: true },
        ddlDepts: { required: true }
    },
    ignore: ".ignore, :hidden",
    errorElement: "div",
    wrapper: "div",
    messages: {
        TextBoxTitle: {
            required: "required 1-4 chars.", maxlength: "required 1-4 chars.", validTitle: "Mr. Ms. Mrs. Dr. or Miss"
        },
        TextBoxFirstname: {
            required: "required 1-25 chars.", maxlength: "required 1-25 chars."
        },
        TextBoxLastname: {
            required: "required 1-25 chars.", maxlength: "required 1-25 chars."
        },
        TextBoxPhone: {
            required: "required 1-15 chars.", maxlength: "required 1-15 chars."
        },
        TextBoxEmail: {
            required: "required 1-40 chars.", maxlength: "required 1-40 chars.", email: "invaild email format"
        },
        ddlDepts: {
            required : "Select a department"
        }

    }
});


$("#main").click(function (e) {
    var empId = e.target.parentNode.id;
    if (empId === "main" || empId === "") {
        empId = e.target.id;
    }

    if (empId !== "0") {
        $("#ButtonAction").prop("value", "Update");
        $("#ButtonDelete").show();
        getById(empId);
    }
    else {
        $("#ButtonDelete").hide();
        $("#ButtonAction").prop("value", "Add");
        localStorage.setItem("Id", "new");
        $("#TextBoxTitle").val("");
        $("#TextBoxFirstname").val("");
        $("#TextBoxLastname").val("");
        $("#TextBoxPhone").val("");
        $("#TextBoxEmail").val("");
        $("#IsTech").val("");
        $("#TextBoxUpdate").prop("value", "Add");
        $("#TextBoxDelete").hide();
        $('#ImageHolder').html("");
        $("#HideStaffPic").hide();
        $('#fileUpload').val("");
        loadDepartmentDLL(-1);
    }
        
});

$("#ButtonAction").click(function () {
    var reader = new FileReader();
    var file = $('#fileUpload')[0].files[0];

    reader.onload = function (readerEvt) 
    { 
        var binaryString = readerEvt.target.result;
        localStorage.setItem("StaffPicture64", btoa(binaryString));
        if ($("#ButtonAction").val() === "Update")
        {
            update();
        }
        else 
        {
            create();
        }
    }
});

$("#ButtonAction").click(function () {
    var validator = $('#EmployeeModalForm').validate();
    validator.resetForm();

    if ($("#EmployeeModalForm").valid()) {
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

$.validator.addMethod("validTitle", function (value, element) {
    return this.optional(element) || (value === "Mr." || value === "Ms." || value === "Mrs." || value === "Dr." || value === "Miss.");
}, "");

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
        var validator = $('#EmployeeModalForm').validate();
        validator.resetForm();
        $("#TextBoxTitle").val(emp.Title);
        $("#TextBoxFirstname").val(emp.Firstname);
        $("#TextBoxLastname").val(emp.Lastname);
        $("#TextBoxPhone").val(emp.Phoneno);
        $("#TextBoxEmail").val(emp.Email);
        $('#ImageHolder').html('<img id="StaffPicture" height="120" width="110" src="data:image/png;base64,' + emp.StaffPicture64 + '" />');
        $("#IsTech").is(":checked");
        localStorage.setItem("Id", emp.Id);
        localStorage.setItem("Version", emp.Version);
        localStorage.setItem("StaffPicture64", emp.StaffPicture64);
        loadDepartmentDLL(emp.DepartmentId);
        $("#HideStaffPic").show();
    
        if (emp.StaffPicture64 === "Picture not selected")
        {
            $('#ImageHolder').html("");
        }
        else
        {
            $('#ImageHolder').html('<img id="StaffPicture" height="120" width="110" src="data:image/png;base64,' + emp.StaffPicture64 + '" />');
        }
        
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

function update() {

    var reader = new FileReader();
    var file = $('#fileUpload')[0].files[0];
    if (file) {
        reader.readAsBinaryString(file);
        reader.onload = function (readerEvt) {

            var binaryString = reader.result;
            var encodedString = btoa(binaryString);


            emp = new Object();
            emp.Title = $("#TextBoxTitle").val();
            emp.Firstname = $("#TextBoxFirstname").val();
            emp.Lastname = $("#TextBoxLastname").val();
            emp.Phoneno = $("#TextBoxPhone").val();
            emp.Email = $("#TextBoxEmail").val();
            emp.DepartmentId = $("#ddlDepts").val();
            emp.StaffPicture64 = encodedString;
            emp.Id = localStorage.getItem("Id");
            emp.Version = localStorage.getItem("Version");

             emp.IsTech = document.getElementById("IsTech").checked
            

            ajaxCall("Put", "api/employees/", emp)
           .done(function (emp) {
               getAll(emp);
           })
           .fail(function (jqXHR, textStatus, errorThrown) {
               errorRoutine(jqXHR);
           });
        }
    }
    else {
        emp = new Object();
        emp.Title = $("#TextBoxTitle").val();
        emp.Firstname = $("#TextBoxFirstname").val();
        emp.Lastname = $("#TextBoxLastname").val();
        emp.Phoneno = $("#TextBoxPhone").val();
        emp.Email = $("#TextBoxEmail").val();
        emp.DepartmentId = $("#ddlDepts").val();
        emp.StaffPicture64 = localStorage.getItem("StaffPicture64");
        emp.Id = localStorage.getItem("Id");
        emp.Version = localStorage.getItem("Version");

        if (document.getElementById("IsTech").checked)
        {
            emp.IsTech = true;
        }
        else
        {
            emp.IsTech = false;
        }

        ajaxCall("Put", "api/employees/", emp)
        .done(function (emp) {
            getAll(emp);
        })
        .fail(function (jqXHR, textStatus, errorThrown) {
            errorRoutine(jqXHR);
        });
    }
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
    emp.IsTech = document.getElementById("IsTech").checked;

    var reader = new FileReader();
    var file = $('#fileUpload')[0].files[0];
    if (file)
    {
        reader.readAsBinaryString(file);
        reader.onload = function (readerEvt)
        {
            var binaryString = reader.result;
            var encodedString = btoa(binaryString);
            emp.StaffPicture64 = encodedString;

            ajaxCall("Post", "api/employees/", emp)
           .done(function (emp) {
               getAll(emp);
           })
           .fail(function (jqXHR, textStatus, errorThrown) {
               errorRoutine(jqXHR);
           });
        }
    }
    else {
        emp.StaffPicture64 = "Picture not selected";

        ajaxCall("Post", "api/employees/", emp)
       .done(function (emp) {
           getAll(emp);
       })
       .fail(function (jqXHR, textStatus, errorThrown) {
           errorRoutine(jqXHR);
       });
    }

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
} 