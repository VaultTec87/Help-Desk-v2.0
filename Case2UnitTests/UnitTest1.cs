using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using HelpdeskDAL;
using HelpdeskViewModels;
using System.Collections.Generic;
using HelpdeskViewModel;

namespace Case1UnitTests
{
    [TestClass]
    public class DALUtilTests2
    {
        //[TestMethod]
        //public void TestLoadCollectionsShouldReturnTrue()
        //{
        //    DALUtils util = new DALUtils();
        //    Assert.IsTrue(util.LoadCollections());

        }// TestLoadCollectionsShouldReturnTrue

    }// DALUtilTests

//[TestClass]
//public class EmployeeDAOTests
//{
//    string eid = "";
//    string did = "";

//    public EmployeeDAOTests()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        Employee emp = dao.GetByLastname("Smartypants");
//        eid = emp.GetIdAsString();
//        did = emp.GetDepartmentIdAsString();

//    }// no-arg constructor

//    [TestMethod]
//    public void TestGetByLastnameShouldReturnEmployee()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        Assert.IsInstanceOfType(dao.GetByLastname("Smartypants"), typeof(Employee));

//    }// TestGetByLastnameShouldReturnEmployee

//    [TestMethod]
//    public void TestGetByIdShouldReturnEmployee()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        Assert.IsInstanceOfType(dao.GetById(eid), typeof(Employee));

//    }// TestGetByIdShouldReturnEmployee

//    [TestMethod]
//    public void TestCreateShouldReturnNewId()
//    {
//        Employee emp = new Employee();
//        EmployeeDAO dao = new EmployeeDAO();
//        emp.Title = "Mr.";
//        emp.Firstname = "Evan";
//        emp.Lastname = "Test";
//        emp.Phoneno = "(555)555-5555";
//        emp.Email = "elauersen@fanshaweonline.ca";
//        emp.Version = 1;
//        emp.SetDepartmentIdFromString(did); // 12 byte hex = 24 byte string
//        Assert.IsTrue(dao.Create(emp).GetIdAsString().Length == 24);

//    }// TestCreateShouldReturnNewId

//    [TestMethod]
//    public void TestUpdateShouldReturnOk()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        Employee emp = dao.GetById(eid);
//        emp.Phoneno = "(555)555-9999";
//        Assert.IsTrue(dao.Update(emp) == UpdateStatus.Ok);

//    }// TestUpdateShouldReturOk

//    [TestMethod]
//    public void TestUpdateShouldReturnStale()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        Employee emp1 = dao.GetById(eid);
//        Employee emp2 = dao.GetById(eid);
//        emp1.Phoneno = "(555)555-1111";
//        emp2.Phoneno = "(555)555-2222";
//        UpdateStatus status = dao.Update(emp1);
//        Assert.IsTrue(dao.Update(emp2) == UpdateStatus.Stale);

//    }// TestUpdateShouldReturnStale

//    [TestMethod]
//    public void TestDeleteShouldReturnOne()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        Employee emp = dao.GetByLastname("Test");
//        if (emp == null) // in case this test runs before the Create method test
//        {
//            TestCreateShouldReturnNewId(); // run it for sure
//            emp = dao.GetByLastname("Test");
//        }

//        Assert.IsTrue(dao.Delete(emp.GetIdAsString()) == 1);

//    }// TestShouldReturnOne

//}// EmployeeDAOTests

[TestClass]
public class DepartmentDAOTests
{
    string id_string = "";

    public DepartmentDAOTests()
    {
        DepartmentDAO dao = new DepartmentDAO();
        Department dep = dao.GetByDepartment("Sales");
        id_string = dep.GetIdAsString();

    }// no-arg constructor

    [TestMethod]
    public void TestGetByDepartmentNameShouldReturnDepartment()
    {
        DepartmentDAO dao = new DepartmentDAO();
        Department dep = dao.GetByDepartment("Sales");
        Assert.IsInstanceOfType(dep, typeof(Department));
        Assert.AreEqual(dep.DepartmentName, "Sales");
        Assert.AreEqual(dep.GetIdAsString(), id_string);

    }// TestGetByDepartmentNameShouldReturnDepartment

    [TestMethod]
    public void TestGetByIdShouldReturnDepartment()
    {
        DepartmentDAO dao = new DepartmentDAO();
        Department dep = dao.GetById(id_string);
        Assert.IsInstanceOfType(dep, typeof(Department));
        Assert.AreEqual(dep.DepartmentName, "Sales");
        Assert.AreEqual(dep.GetIdAsString(), id_string);

    }// TestGetByIdShouldReturnDepartment

    [TestMethod]
    public void TestCreateShouldReturnNewId()
    {
        Department dep = new Department();
        DepartmentDAO dao = new DepartmentDAO();
        dep.DepartmentName = "Test Department";
        dep.Version = 1;
        Assert.IsTrue(dao.Create(dep).GetIdAsString().Length == 24);

    }// TestCreateShouldReturnNewId

    [TestMethod]
    public void TestUpdateShouldReturnOk()
    {
        DepartmentDAO dao = new DepartmentDAO();
        Department dep = dao.GetById(id_string);
        string name = dep.DepartmentName;
        dep.DepartmentName = "another name";
        Assert.IsTrue(dao.Update(dep) == UpdateStatus.Ok);

        // now revert back to original name!
        dep = dao.GetById(id_string);
        dep.DepartmentName = name;
        Assert.IsTrue(dao.Update(dep) == UpdateStatus.Ok);

    }// TestUpdateShouldReturOk

    [TestMethod]
    public void TestUpdateShouldReturnStale()
    {
        DepartmentDAO dao = new DepartmentDAO();
        Department dep1 = dao.GetById(id_string);
        Department dep2 = dao.GetById(id_string);

        string name = dep1.DepartmentName;

        dep1.DepartmentName = "Test Department";
        dep2.DepartmentName = "name 2";
        UpdateStatus status = dao.Update(dep1);
        Assert.IsTrue(dao.Update(dep2) == UpdateStatus.Stale);

        Department dep = dao.GetById(id_string); // get a new copy of the document with the proper version #
        dep.DepartmentName = name; // now revert back to original name
        Assert.IsTrue(dao.Update(dep) == UpdateStatus.Ok);

    }// TestUpdateShouldReturnStale

    [TestMethod]
    public void TestDeleteShouldReturnOne()
    {
        DepartmentDAO dao = new DepartmentDAO();
        Department dep = dao.GetByDepartment("Test Department");
        if (dep == null) // in case this test runs before the Create method test
        {
            TestCreateShouldReturnNewId(); // run it for sure
            dep = dao.GetByDepartment("Test Department");
        }

        Assert.IsTrue(dao.Delete(dep.GetIdAsString()) == 1);

    }// TestShouldReturnOne

}// DepartmentDAOTests

[TestClass]
public class ProblemDAOTests
{
    string id_string;
    string description = "Washroom stall doors open into Mid-world by accident sometimes";

    bool did_create_test_run = false;

    [TestMethod]
    public void TestCreateShouldReturnNewId()
    {
        if (did_create_test_run)
            return;

        Problem p = new Problem();
        ProblemDAO dao = new ProblemDAO();
        p.Description = description;
        p.Version = 1;
        Assert.IsTrue(dao.Create(p).GetIdAsString().Length == 24);
        id_string = p.GetIdAsString();

        did_create_test_run = true;

    }// TestCreateShouldReturnNewId

    [TestMethod]
    public void TestGetByIdShouldReturnProblem()
    {
        if (!did_create_test_run)
            TestCreateShouldReturnNewId();

        ProblemDAO dao = new ProblemDAO();
        Problem p = dao.GetById(id_string);
        Assert.IsInstanceOfType(p, typeof(Problem));
        Assert.AreEqual(p.Description, description);
        Assert.AreEqual(p.GetIdAsString(), id_string);

    }// TestGetByIdShouldReturnProblem

    [TestMethod]
    public void TestUpdateShouldReturnOk()
    {
        if (!did_create_test_run)
            TestCreateShouldReturnNewId();

        ProblemDAO dao = new ProblemDAO();
        Problem p = dao.GetById(id_string);
        p.Description = "Go now, there are other worlds then these";
        Assert.IsTrue(dao.Update(p) == UpdateStatus.Ok);

        p = dao.GetById(id_string); // get a new copy with the proper version #
        p.Description = description; // now revert back to original description
        Assert.IsTrue(dao.Update(p) == UpdateStatus.Ok);

    }// TestUpdateShouldReturOk

    [TestMethod]
    public void TestUpdateShouldReturnStale()
    {
        if (!did_create_test_run)
            TestCreateShouldReturnNewId();

        ProblemDAO dao = new ProblemDAO();
        Problem p1 = dao.GetById(id_string);
        Problem p2 = dao.GetById(id_string);

        p1.Description = "description 1";
        p1.Description = "description 1";
        UpdateStatus status = dao.Update(p1);
        Assert.IsTrue(dao.Update(p2) == UpdateStatus.Stale);

        p1 = dao.GetById(id_string); // get a new copy with the proper version #
        p1.Description = description; // now revert back to original description
        Assert.IsTrue(dao.Update(p1) == UpdateStatus.Ok);

    }// TestUpdateShouldReturnStale

    [TestMethod]
    public void TestDeleteShouldReturnOne()
    {
        if (!did_create_test_run)
            TestCreateShouldReturnNewId();

        ProblemDAO dao = new ProblemDAO();
        Problem p = dao.GetById(id_string);
        Assert.IsTrue(dao.Delete(p.GetIdAsString()) == 1);

    }// TestShouldReturnOne

}// ProblemDAOTests

//[TestClass]
//public class EmployeeViewModelTests
//{
//    string eid = "";
//    string did = "";

//    public EmployeeViewModelTests()
//    {
//        EmployeeViewModel vm = new EmployeeViewModel();
//        vm.Lastname = "Smartypants";
//        vm.GetByLastname();
//        eid = vm.Id;
//        did = vm.DepartmentId;

//    }// no-arg constructor

//    [TestMethod]
//    public void TestGetByLastnameShouldPopulateProps()
//    {
//        EmployeeViewModel vm = new EmployeeViewModel();
//        vm.Lastname = "Smartypants";
//        vm.GetByLastname();
//        Assert.IsTrue(vm.DepartmentId.Length == 24); // 12 byte hex = 24 byte string
//        Assert.IsTrue(vm.Title == "Mr.");
//        Assert.IsTrue(vm.Firstname == "Bigshot");

//    }// TestCreateShouldReturnNewId

//    [TestMethod]
//    public void TestCreateShouldReturnNewId()
//    {
//        EmployeeViewModel vm = new EmployeeViewModel();
//        vm.Title = "Mr.";
//        vm.Firstname = "Murilo";
//        vm.Lastname = "Trigo";
//        vm.Phoneno = "(555)-555-5555";
//        vm.Email = "m_trigo@fanshaweonline.ca";
//        vm.Version = 1; // Starting Versions at 1;
//        vm.DepartmentId = did; // Smartypant's department
//        vm.Create();
//        Assert.IsTrue(vm.Id.Length == 24);
//    }

//    [TestMethod]
//    public void TestUpdateShouldReturnOk()
//    {
//        EmployeeViewModel vm = new EmployeeViewModel();
//        vm.Id = eid;
//        vm.GetById();
//        vm.Phoneno = "(555)555-9999";

//        int ver = vm.Version;
//        Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
//        vm.GetById(); // get updated version
//        Assert.IsTrue(vm.Version == ver + 1);
//        Assert.IsTrue(vm.Phoneno == "(555)555-9999");

//    }// TestUpdateShouldReturnOk

//    [TestMethod]
//    public void TestUpdateShouldReturnStale()
//    {
//        EmployeeViewModel vm1 = new EmployeeViewModel();
//        EmployeeViewModel vm2 = new EmployeeViewModel();

//        vm1.Id = vm2.Id = eid;
//        vm1.GetById();
//        vm2.GetById();

//        vm1.Phoneno = "1";
//        vm2.Phoneno = "2";

//        int ver = vm1.Version;

//        Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);
//        Assert.IsTrue(vm2.Update() == (int)UpdateStatus.Stale);

//        vm1.GetById(); // get the update version of the emp
//        Assert.IsTrue(vm1.Phoneno == "1");
//        Assert.IsTrue(vm1.Version == ver + 1);

//    }// TestUpdateShouldReturnStale

//    [TestMethod]
//    public void TestGetByIdShouldPopulateProps()
//    {
//        EmployeeViewModel vm = new EmployeeViewModel();
//        vm.Id = eid;
//        vm.GetById();
//        Assert.IsTrue(vm.DepartmentId.Length == 24);

//    }// TestGetByIdShouldPopulateProps

//    [TestMethod]
//    public void TestGetAllJustCrossFingersAndHopeItWorks()
//    {
//        EmployeeDAO dao = new EmployeeDAO();
//        EmployeeViewModel vm = new EmployeeViewModel();
//        List<Employee> emp_list = dao.GetAll();
//        List<EmployeeViewModel> vm_list = vm.GetAll();

//        Assert.IsTrue(emp_list.Count == vm_list.Count);

//        for (int i = 0; i < emp_list.Count; i++)
//        {
//            Assert.IsTrue(emp_list[i].Title == vm_list[i].Title);
//            Assert.IsTrue(emp_list[i].Firstname == vm_list[i].Firstname);
//            Assert.IsTrue(emp_list[i].Lastname == vm_list[i].Lastname);
//            Assert.IsTrue(emp_list[i].Phoneno == vm_list[i].Phoneno);
//            Assert.IsTrue(emp_list[i].Email == vm_list[i].Email);
//            Assert.IsTrue(emp_list[i].GetIdAsString() == vm_list[i].Id);
//            Assert.IsTrue(emp_list[i].GetDepartmentIdAsString() == vm_list[i].DepartmentId);
//            Assert.IsTrue(emp_list[i].Version == vm_list[i].Version);
//        }

//    }// TestGetAllJustCrossFingersAndHopeItWorks

//    [TestMethod]
//    public void TestDeleteShouldReturnOne()
//    {
//        EmployeeViewModel vm = new EmployeeViewModel();
//        vm.Lastname = "Trigo";
//        vm.GetByLastname();
//        Assert.IsTrue(vm.Delete() == 1);

//    }// TestDeleteShouldReturnOne

//}// EmployeeViewModel TestClass

[TestClass]
    public class DepartmentViewModelTests
    {
        string id_string = "";
        string dep_name = "";
        string new_dep_name = "";

        public DepartmentViewModelTests()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            dep_name = "Lab";
            new_dep_name = "Test Dep Name";
            vm.DepartmentName = dep_name;
            vm.GetByDepartment();
            id_string = vm.Id;

        }// no-arg constructor

        [TestMethod]
        public void TestGetByDepartmentNameShouldPopulateProps()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.DepartmentName = dep_name;
            vm.GetByDepartment();

            DepartmentViewModel vmd = new DepartmentViewModel();
            vmd.Id = id_string;
            vmd.GetById();

            Assert.IsTrue(vm.Id.Length == 24); // 12 byte hex = 24 byte string
            Assert.IsTrue(vm.Id == vmd.Id);
            Assert.IsTrue(vm.DepartmentName == dep_name);

        }// TestCreateShouldReturnNewId

        [TestMethod]
        public void TestCreateShouldReturnNewId()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.DepartmentName = new_dep_name;
            vm.Version = 1; // Starting Versions at 1;
            vm.Create();
            Assert.IsTrue(vm.Id.Length == 24);
        }

        [TestMethod]
        public void TestUpdateShouldReturnOk()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.Id = id_string;
            vm.GetById();
            vm.DepartmentName = "dep name changed";

            int ver = vm.Version;
            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
            vm.GetById(); // get updated version
            Assert.IsTrue(vm.Version == ver + 1);
            Assert.IsTrue(vm.DepartmentName == "dep name changed");

        // change it back
        vm.DepartmentName = dep_name;
            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
            vm.GetById();
            Assert.IsTrue(vm.Version == ver + 2);
            Assert.IsTrue(vm.DepartmentName == dep_name);

        }// TestUpdateShouldReturnOk

        [TestMethod]
        public void TestUpdateShouldReturnStale()
        {
            DepartmentViewModel vm1 = new DepartmentViewModel();
            DepartmentViewModel vm2 = new DepartmentViewModel();

            vm1.Id = vm2.Id = id_string;
            vm1.GetById();
            vm2.GetById();

            string name = vm1.DepartmentName;
            vm1.DepartmentName = "1";
            vm2.DepartmentName = "2";

            int ver = vm1.Version;

            Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);
            Assert.IsTrue(vm2.Update() == (int)UpdateStatus.Stale);

            vm1.GetById(); // get the update version of the emp
            Assert.IsTrue(vm1.DepartmentName == "1");
            Assert.IsTrue(vm1.Version == ver + 1);

            // change it back
            vm1.GetById();
            vm1.DepartmentName = name;
            Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);

        }// TestUpdateShouldReturnStale

        [TestMethod]
        public void TestGetByIdShouldPopulateProps()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.Id = id_string;
            vm.GetById();
            Assert.IsTrue(vm.DepartmentName == dep_name);

        }// TestGetByIdShouldPopulateProps

        [TestMethod]
        public void TestGetAllJustCrossFingersAndHopeItWorks()
        {
            DepartmentDAO dao = new DepartmentDAO();
            DepartmentViewModel vm = new DepartmentViewModel();
            List<Department> emp_list = dao.GetAll();
            List<DepartmentViewModel> vm_list = vm.GetAll();

            Assert.IsTrue(emp_list.Count == vm_list.Count);

            for (int i = 0; i < emp_list.Count; i++)
            {
                Assert.IsTrue(emp_list[i].GetIdAsString() == vm_list[i].Id);
                Assert.IsTrue(emp_list[i].DepartmentName == vm_list[i].DepartmentName);
                Assert.IsTrue(emp_list[i].Version == vm_list[i].Version);
            }

        }// TestGetAllJustCrossFingersAndHopeItWorks

        [TestMethod]
        public void TestDeleteShouldReturnOne()
        {
            DepartmentViewModel vm = new DepartmentViewModel();
            vm.Id = id_string;
            vm.GetById();

            Assert.IsTrue(vm.Delete() == 1);

        }// TestDeleteShouldReturnOne

    }// DepartmentViewModelTests

[TestClass]
public class ProblemViewModelTests
{
    string id_string = "";
    string descrip = "";
    bool did_create_run = false;

    [TestMethod]
    public void TestCreateShouldReturnNewId()
    {
        if (did_create_run)
            return;

        ProblemViewModel vm = new ProblemViewModel();
        descrip = "The man in black fled across the desert, and the gunslinger followed";
        vm.Description = descrip;
        vm.Version = 1; // Starting Versions at 1;
        vm.Create();
        Assert.IsTrue(vm.ProblemId.Length == 24);
        id_string = vm.ProblemId;
        did_create_run = true;

    }// TestCreateShouldReturnNewId

    [TestMethod]
    public void TestUpdateShouldReturnOk()
    {
        if (!did_create_run)
            TestCreateShouldReturnNewId();

        ProblemViewModel vm = new ProblemViewModel();
        vm.ProblemId = id_string;
        vm.GetById();
        vm.Description = "1";

        int ver = vm.Version;
        Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
        vm.GetById(); // get updated version
        Assert.IsTrue(vm.Version == ver + 1);
        Assert.IsTrue(vm.Description == "1");

        // change it back
        vm.Description = descrip;
        Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
        vm.GetById();
        Assert.IsTrue(vm.Version == ver + 2);
        Assert.IsTrue(vm.Description == descrip);

    }// TestUpdateShouldReturnOk

    [TestMethod]
    public void TestUpdateShouldReturnStale()
    {
        if (!did_create_run)
            TestCreateShouldReturnNewId();

        ProblemViewModel vm1 = new ProblemViewModel();
        ProblemViewModel vm2 = new ProblemViewModel();

        vm1.ProblemId = vm2.ProblemId = id_string;
        vm1.GetById();
        vm2.GetById();

        vm1.Description = "11";
        vm2.Description = "22";

        int ver = vm1.Version;

        Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);
        Assert.IsTrue(vm2.Update() == (int)UpdateStatus.Stale);

        vm1.GetById(); // get the update version of the emp
        Assert.IsTrue(vm1.Description == "11");
        Assert.IsTrue(vm1.Version == ver + 1);

        // change it back
        vm1.GetById();
        vm1.Description = descrip;
        Assert.IsTrue(vm1.Update() == (int)UpdateStatus.Ok);

    }// TestUpdateShouldReturnStale

    [TestMethod]
    public void TestGetByIdShouldPopulateProps()
    {
        if (!did_create_run)
            TestCreateShouldReturnNewId();

        ProblemViewModel vm = new ProblemViewModel();
        vm.ProblemId = id_string;
        vm.GetById();
        Assert.IsTrue(vm.Description == descrip);

    }// TestGetByIdShouldPopulateProps

    [TestMethod]
    public void TestGetAllJustCrossFingersAndHopeItWorks()
    {
        if (!did_create_run)
            TestCreateShouldReturnNewId();

        ProblemDAO dao = new ProblemDAO();
        ProblemViewModel vm = new ProblemViewModel();
        List<Problem> emp_list = dao.GetAll();
        List<ProblemViewModel> vm_list = vm.GetAll();

        Assert.IsTrue(emp_list.Count == vm_list.Count);

        for (int i = 0; i < emp_list.Count; i++)
        {
            Assert.IsTrue(emp_list[i].GetIdAsString() == vm_list[i].ProblemId);
            Assert.IsTrue(emp_list[i].Description == vm_list[i].Description);
            Assert.IsTrue(emp_list[i].Version == vm_list[i].Version);
        }

    }// TestGetAllJustCrossFingersAndHopeItWorks

    [TestMethod]
    public void TestDeleteShouldReturnOne()
    {
        if (!did_create_run)
            TestCreateShouldReturnNewId();

        ProblemViewModel vm = new ProblemViewModel();
        vm.ProblemId = id_string;
        vm.GetById();

        Assert.IsTrue(vm.Delete() == 1);

    }// TestDeleteShouldReturnOne

}// ProblemViewModelTests

//}// Case1UnitTests
