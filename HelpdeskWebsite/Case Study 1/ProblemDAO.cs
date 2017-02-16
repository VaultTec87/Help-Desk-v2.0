using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class ProblemDAO
    {
        private IRepository repo;

        public ProblemDAO()
        {
            repo = new HelpdeskRepository();
        }

 
        public Problem GetById(string probloyId)
        {

            Problem prob = new Problem();
            try
            {
                prob = repo.GetById<Problem>(probloyId);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "GetById");
            }

            return prob;
        }
        public Problem Create(Problem createprob)
        {
            try
            {
                createprob = repo.Create<Problem>(createprob);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Create");
            }

            return createprob;
        }


        public long Delete(string id)
        {

            try
            {
                return repo.Delete<Problem>(id);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Update");
                return 0;
            }

        }

        public UpdateStatus Update(Problem prob)
        {
            UpdateStatus status = UpdateStatus.Failed;
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                var filter = Builders<Problem>.Filter.Eq("Id", prob.Id) & Builders<Problem>.Filter.Eq("Version", prob.Version);
                var update = Builders<Problem>.Update
                    .Set("Description", prob.Description)
                    .Set("Id", prob.Id)
                    .Inc("Version", 1);

                status = repo.Update(prob.GetIdAsString(), filter, update);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Update");
            }

            return status;
        }
        public List<Problem> GetAll()
        {
            List<Problem> prob = new List<Problem>();
            try
            {
                prob = repo.GetAll<Problem>();
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "GetAll");
            }

            return prob;
        }
    }
}
