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

 
        public Problem GetById(string probId)
        {

            Problem prob = new Problem();
            try
            {
                prob = repo.GetById<Problem>(probId);
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

        public int Update(Problem prob)
        {
            int status = -1;
            HelpdeskRepository repo = new HelpdeskRepository(new DbContext());
            try
            {
                var filter = Builders<Problem>.Filter.Eq("Id", prob.Id) & Builders<Problem>.Filter.Eq("Version", prob.Version);
                    var update = Builders<Problem>.Update
                    .Set("Description", prob.Description)
                    .Set("Id", prob.Id)
                    .Inc("Version", 1);

                status = Convert.ToInt16(repo.Update(prob.GetIdAsString(), filter, update));
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemDAO", "Update");
            }

            return Convert.ToInt16(status);
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
        public Problem GetByDescription(string desc)
        {
            Problem prob = null;
            var builder = Builders<Problem>.Filter;
            var filter = builder.Eq("Description", desc);

            try
            {
                prob = repo.GetOne<Problem>(filter);
            }
            catch (Exception ex)
            {
                DALUtils.ErrorRoutine(ex, "ProblemViewModel", "GetByDescription");
            }
            return prob;
        }
    }
}
