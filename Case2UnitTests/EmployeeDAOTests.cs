using HelpdeskDAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Case1UnitTests
{
    [TestClass]
    public class EmployeeDAOTests
    {
        string employeeId = "";
        string departmentId = "";

        public EmployeeDAOTests()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = dao.GetByLastname("Smartypants");
            employeeId = emp.GetIdAsString();
            departmentId = emp.GetDepartmentIdAsString();
        }
        [TestMethod]
        public void TestGetByLastnameShouldReturnEmployee()//Test GetByLastname
        {
            EmployeeDAO dao = new EmployeeDAO();
            Assert.IsInstanceOfType(dao.GetByLastname("Smartypants"), typeof(Employee));
        }
        [TestMethod]
        public void TestGetByIdShouldReturnEmployee()//test GetById
        {
            EmployeeDAO dao = new EmployeeDAO();
            Assert.IsInstanceOfType(dao.GetById(employeeId), typeof(Employee));
        }
        [TestMethod]
        public void TestCreateShouldReturnNewId()// test create
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = new Employee();
            emp.Title = "Mr.";
            emp.Firstname = "Nick";
            emp.Lastname = "Vail";
            emp.Phoneno = "(555)555-4566";
            emp.Email = "n_vail@fanshaweonline.ca";
            emp.Version = 1;
            emp.SetDepartmentIdFromString(departmentId);
            Assert.IsTrue(dao.Create(emp).GetIdAsString().Length == 24);
        }
        [TestMethod]
        public void TestUpdateShouldReturnOk()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = dao.GetById(employeeId);
            emp.Phoneno = "(555)555-9999";
            Assert.IsTrue(dao.Update(emp) == UpdateStatus.Ok);
        }
        [TestMethod]
        public void TestUpdateShouldReturnStale()
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp1 = dao.GetById(employeeId);
            Employee emp2 = dao.GetById(employeeId);
            emp1.Phoneno = "(555)555-1111";
            emp2.Phoneno = "(555)555-2222";
            UpdateStatus status = dao.Update(emp1);
            Assert.IsTrue(dao.Update(emp2) == UpdateStatus.Stale);
        }

        [TestMethod]
        public void TestDeleteShouldReturnOne()// test Delete
        {
            EmployeeDAO dao = new EmployeeDAO();
            Employee emp = dao.GetByLastname("Vail");
            if (emp == null) // did delete run before create?
            {
                TestCreateShouldReturnNewId();
                emp = dao.GetByLastname("Vail");
            }
            Assert.IsTrue(dao.Delete(emp.GetIdAsString()) == 1);

        }

    }
}
