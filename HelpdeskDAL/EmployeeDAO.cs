using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    public class EmployeeDAO
    {
        private IRepository repo;

        public EmployeeDAO()
        {
            repo = new HelpdeskRepository();
        }

        public Employee GetByLastname(string lname)
        {
            Employee emp = new Employee();
            var builder = Builders<Employee>.Filter;
            var filter = builder.Eq("Lastname", lname);

            try
            {
                emp = repo.GetOne<Employee>(filter); 
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetByLastname");
            }

            return emp;
        }
        public Employee GetById(string employId)
        {

            Employee emp = new Employee();
            try
            {
                emp = repo.GetById<Employee>(employId);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetById");
            }

            return emp;
        }
        public Employee Create(Employee createEmp)
        {
            try
            {
                createEmp = repo.Create<Employee>(createEmp);
            }
            catch(Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Create");
            }

            return createEmp;
        }


        public long Delete(string id)
        {
           
            try
            {
                repo.Delete<Employee>(id);
                return 1;
            }
            catch(Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Update");
                return 0;
            }
            
        }

        public UpdateStatus Update(Employee emp)
        {
            UpdateStatus status;
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            if(!(repo.Exists<Employee>(emp.GetIdAsString())))
            {
               return status = UpdateStatus.Failed;
            }
            try
            {
                var filter = Builders<Employee>.Filter.Eq("Id", emp.Id) & Builders<Employee>.Filter.Eq("Version", emp.Version);
                var update = Builders<Employee>.Update
                    .Set("DepartmentId", emp.DepartmentId)
                    .Set("Email", emp.Email)
                    .Set("Firstname", emp.Firstname)
                    .Set("Lastname", emp.Lastname)
                    .Set("Phoneno", emp.Phoneno)
                    .Set("IsTech", emp.IsTech)
                    .Set("StaffPicture64", emp.StaffPicture64)
                    .Set("Title", emp.Title)
                    .Inc("Version", 1);

                status = repo.Update(emp.GetIdAsString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "Update");
            }

            return status = UpdateStatus.Ok;
        }
        public List<Employee> GetAll()
        {
            List<Employee> emp = new List<Employee>();
            try
            {
                emp = repo.GetAll<Employee>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "EmployeeDAO", "GetAll");
            }

            return emp;
        }
    }
}
