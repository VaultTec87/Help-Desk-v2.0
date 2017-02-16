using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelpdeskViewModels;
using HelpdeskDAL;
using System.Collections.Generic;

namespace Case1UnitTests
{

    [TestClass]
    public class EmployeeViewModelTests
    {
        string employId = "";
        string departId = "";
        
        public EmployeeViewModelTests()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Smartypants";
            vm.GetByLastname();
            employId = vm.Id;
            departId = vm.DepartmentId;
        }
        [TestMethod]
        public void TestGetByLastnameShouldPopulateProps()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Smartypants";
            vm.GetByLastname();
            Assert.IsTrue(vm.DepartmentId.Length == 24);
        }
        [TestMethod]
        public void TestGetByIdShouldPopulateProps()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Id = employId;
            vm.GetById();
            Assert.IsTrue(vm.Id.Length == 24);
        }
        [TestMethod]
        public void TestCreateShouldReturnNewId()// test create
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Title = "Mr.";
            vm.Firstname = "Nick";
            vm.Lastname = "Vail";
            vm.Phoneno = "(555)555-4566";
            vm.Email = "n_vail@fanshaweonline.ca";
            vm.Version = 1;
            vm.DepartmentId = departId;
            vm.Create();
            Assert.IsTrue(vm.Id.Length == 24);
        }
        [TestMethod]
        public void TestUpdateShouldReturnOk()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Id = employId; 
            vm.GetById();
            vm.Phoneno = "(555)555-6547";
            Assert.IsTrue(vm.Update() == (int)UpdateStatus.Ok);
        }
        [TestMethod]
        public void TestUpdateShouldReturnStale()
        {
            EmployeeViewModel vm1 = new EmployeeViewModel();
            EmployeeViewModel vm2 = new EmployeeViewModel();
            vm1.Id = employId;
            vm2.Id = employId;
            vm1.GetById();
            vm2.GetById();
            vm1.Phoneno = "(555)555-3333";
            vm2.Phoneno = "(555)555-3542";
            vm1.Update();
            Assert.IsTrue(vm2.Update() == -2);
        }
        [TestMethod]
        public void TestDeleteShouldReturnOne()
        {
            EmployeeViewModel vm = new EmployeeViewModel();
            vm.Lastname = "Vail";
            vm.GetByLastname();
            Assert.IsTrue(vm.Delete() == 1);
        }
        
    }
}
