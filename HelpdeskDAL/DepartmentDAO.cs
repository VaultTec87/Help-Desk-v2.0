using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class DepartmentDAO
    {
        private IRepository repo;

        public DepartmentDAO()
        {
            repo = new HelpdeskRepository();
        }

        public Department GetByDepartment(string deptName)
        {
            Department dept = new Department();
            var builder = Builders<Department>.Filter;
            var filter = builder.Eq("DepartmentName", deptName);

            try
            {
                dept = repo.GetOne<Department>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetByLastname");
            }

            return dept;
        }
        public Department GetById(string deptId)
        {

            Department dept = new Department();
            try
            {
                dept = repo.GetById<Department>(deptId);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetById");
            }

            return dept;
        }
        public Department Create(Department createdept)
        {
            try
            {
                createdept = repo.Create<Department>(createdept);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Create");
            }

            return createdept;
        }


        public long Delete(string id)
        {

            try
            {
                repo.Delete<Department>(id);
                return 1;
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Update");
                return 0;
            }

        }

        public UpdateStatus Update(Department dep)
        {
            UpdateStatus status = UpdateStatus.Failed;
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                var filter = Builders<Department>.Filter.Eq("Id", dep.Id) & Builders<Department>.Filter.Eq("Version", dep.Version);
                var update = Builders<Department>.Update
                    .Set("ManagerId", dep.ManagerId)
                    .Set("DepartmentName", dep.DepartmentName)
                    .Set("Id", dep.Id)
                    .Inc("Version", 1);

                status = repo.Update(dep.GetIdAsString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "Update");
            }

            return status;
        }
        public List<Department> GetAll()
        {
            List<Department> dept = new List<Department>();
            try
            {
                dept = repo.GetAll<Department>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentDAO", "GetAll");
            }

            return dept;
        }
    }
}
