using HelpdeskDAL;
using HelpdeskViewModels;
using System;
using System.Collections.Generic;

namespace HelpdeskViewModel
{
    public class DepartmentViewModel
    {
        private DepartmentDAO _dao;

        public DepartmentViewModel()
        {
            _dao = new DepartmentDAO();
        }

        public int Version { get; set; }
        public string Id { get; set; }
        public string DepartmentName { get; set; }
        public string ManagerId { get; set; }

        public void Create()
        {
            try
            {
                Department dept = new Department();
                dept.DepartmentName = DepartmentName;
                dept.Version = Version;
                dept.SetManagerIdFromString(ManagerId);
                Id = _dao.Create(dept).GetIdAsString();
            }
            catch(Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "Create");
            }
        }

        public long Delete()
        {
           
            try
            {
                return _dao.Delete(Id);
            }
            catch(Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "Delete()");
                return 0;
            }
            
        }

        public void GetById()
        {
            try
            {
                Department dept = _dao.GetById(Id);
                DepartmentName = dept.DepartmentName;
                Version = dept.Version;
                ManagerId = dept.GetManagerIdAsString();
                Id = dept.GetIdAsString();
               
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "EmployeeViewModel", "GetById");
            }
        }

        public void GetByDepartment()
        {
            try
            {
                Department dept = _dao.GetByDepartment(DepartmentName);
                Id = dept.GetIdAsString();
                dept.Version = Version;
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "GetByDepartment");
            }
        }

        public List<DepartmentViewModel> GetAll()
        {
            List<DepartmentViewModel> viewModels = new List<DepartmentViewModel>();
            try
            {
                List<Department> department = _dao.GetAll();
                foreach (Department dept in department)
                {
                    DepartmentViewModel viewModel = new DepartmentViewModel();
                    viewModel.Id = dept.GetIdAsString();
                    viewModel.DepartmentName = dept.DepartmentName;
                    viewModel.Version = dept.Version;
                    viewModel.ManagerId = dept.GetManagerIdAsString();
                    viewModels.Add(viewModel);
                }

            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "DepartmentViewModel", "GetAll");
            }
            return viewModels;
        }

        public int Update()
        {
            UpdateStatus opStatus;

            try
            {
                Department dept = new Department();
                dept.SetIdFromString(Id);
                dept.DepartmentName = DepartmentName;
                dept.Version = Version;
                ManagerId = dept.GetManagerIdAsString();
                opStatus = _dao.Update(dept);
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "DepartmentViewModel", "Update");
                opStatus = UpdateStatus.Failed;
            }
            return Convert.ToInt16(opStatus);
        }
    }
}