QUnit.test("HelpdeskCase1 Tests", function (assert) {
    assert.async(5);

    ajaxCall("Get", "api/employees", "")
    .then(function (data) {
        var num_employees = data.length;
        assert.ok(num_employees > 0, "Found " + num_employees + " Employees");
        console.log("1");
    });
    
    
    var smary_id;
    ajaxCall("Get", "api/employeename/Smartypants", "")
    .then(function (data) {
        assert.ok(data.Firstname === "Bigshot", "Employee " + data.Lastname + " Found");
        smarty_id = data
        return ajaxCall("Get", "api/employees/", data.Id, "");
    })
    .then(function (data){
        assert.ok(smarty_id.Id.length === 24, "Employee " + smarty_id.Lastname + " found " + smarty_id.Id);
        console.log("3");
    });

    ajaxCall("Get", "api/employeename/Smartypants", "")
    .then(function (smarty) {
        emp = new Object();
        emp.DepartmentId = smarty.DepartmentId;
        emp.Title = "Mr.";
        emp.Firstname = "Nick";
        emp.Lastname = "Vail";
        emp.Email = "nick@vail.com";
        emp.Phoneno = "(555)555-5565";
        emp.Version = 1;

        return ajaxCall("Post", "api/employees", emp);
    })
    .then(function (data) {
        assert.equal(data, "Employee Vail created", data);
        return ajaxCall("Get", "api/employeename/Vail", "");
    })
    .then(function (data) {
        assert.ok(data.Firstname === "Nick", "Employee " + data.Lastname + " Retrieved for delete");
        return ajaxCall("Delete", "api/employees", data);
    })
    .then(function (data) {
        assert.ok(data === "Employee Vail Deleted!", data);
    });

    var smarty_copy;
    ajaxCall("Get", "api/employeename/Smartypants", "")
    .then(function (data) {
        smarty_copy = data;
        data.Phoneno = "(555)897-6542";
        return ajaxCall("Put", "api/employees", data);
    })
    .then(function (updmsg) {
        assert.ok(updmsg === "Employee Smartypants updated!", "Employee was updated for the first time");
        return ajaxCall("Put", "api/employees", smarty_copy);
    })
    .then(function (stale) {
        assert.ok(stale === "Data is stale for Smartypants, Employee not updated!", "STALE DATA");
    });


});