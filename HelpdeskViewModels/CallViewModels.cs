using HelpdeskDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskViewModels
{
    public class CallViewModel
    {
        private CallDAO _dao;

        public CallViewModel()
        {
            _dao = new CallDAO();
        }

        public string EmployeeId { get; set; }
        public string ProblemId { get; set; }
        public string employee_last_name { get; set; }
        public string problem_description { get; set; }
        public string tech_name { get; set; }
        public string TechId { get; set; }
        public System.DateTime DateOpened { get; set; }
        public System.DateTime? DateClosed { get; set; }
        public bool OpenStatus { get; set; }
        public string Notes { get; set; }
        public string Id { get; set; }
        public int Version { get; set; }


        public void Create()
        {
            try
            {
                Call call = new Call();
                call.SetTechIdFromString(TechId);
                call.SetEmployeeIdFromString(EmployeeId);
                call.SetProblemIdFromString(ProblemId);
                call.DateClosed = DateClosed;
                call.DateOpened = DateOpened;
                call.OpenStatus = OpenStatus;
                call.Notes = Notes;
                call.Version = Version;
                Id = (_dao.Create(call)).GetIdAsString();

            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "CallViewModel", "Create");
            }

        }

        public long Delete()
        {
            long deleted = 0;

            try
            {
                deleted = _dao.Delete(Id);
                return deleted;
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "CallViewModel", "Delete");
                return deleted;
            }
            
        }
        public void GetById()
        {
            try
            {
                Call call = _dao.GetById(Id);
                TechId = call.GetTechIdAsString();
                EmployeeId = call.GetEmployeeIdAsString();
                ProblemId = call.GetProblemIdAsString();
                OpenStatus = call.OpenStatus;
                DateClosed = call.DateClosed;
                DateOpened = call.DateOpened;
                Notes = call.Notes;
                Version = call.Version;
                
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "CallViewModel", "GetById");
            }
        }

        public int Update()
        {
            UpdateStatus opStatus = UpdateStatus.Failed;

            try
            {
                Call call = new Call();
                call.SetEmployeeIdFromString(EmployeeId);
                call.SetIdFromString(Id);
                call.SetProblemIdFromString(ProblemId);
                call.SetTechIdFromString(TechId);
                call.Notes = Notes;
                call.OpenStatus = OpenStatus;
                call.DateClosed = DateClosed;
                call.DateOpened = DateOpened;
                call.Version = Version;

                opStatus = _dao.Update(call);
            }
            catch (Exception ex)
            {
                ViewModelUtils.ErrorRoutine(ex, "ViewModel", "Update");
            }

            return Convert.ToInt16(opStatus);
        }

        public List<CallViewModel> GetAll()
        {
            List<CallViewModel> viewModels = new List<CallViewModel>();
            try
            {
                List<Call> Calls = _dao.GetAll();
                foreach (Call c in Calls)
                {
                    CallViewModel viewModel = new CallViewModel();
                    viewModel.DateClosed = c.DateClosed;
                    viewModel.DateOpened = c.DateOpened;
                    viewModel.TechId = c.GetTechIdAsString();
                    viewModel.ProblemId = c.GetProblemIdAsString();
                    viewModel.EmployeeId = c.GetEmployeeIdAsString();
                    viewModel.Id = c.GetIdAsString();
                    viewModel.OpenStatus = c.OpenStatus;
                    viewModel.Notes = c.Notes;
                    viewModel.Version = c.Version;
                    viewModel.GetEmpInfo();
                    viewModels.Add(viewModel);
                }

            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "CallViewModel", "GetAll");
            }
            return viewModels;
        }
        public void GetEmpInfo()
        {
            //get employee last name
            EmployeeViewModel emp = new EmployeeViewModel();
            emp.Id = this.EmployeeId;
            emp.GetById();
            this.employee_last_name = emp.Lastname;

            //get technician info
            emp.Id = this.TechId;
            emp.GetById();
            this.tech_name = emp.Lastname;

            //get problem by name
            ProblemViewModel prob = new ProblemViewModel();
            prob.Id = this.ProblemId;
            prob.GetById();
            this.problem_description = prob.Description;

        }
    }
}
