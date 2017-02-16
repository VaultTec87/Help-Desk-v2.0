using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpdeskDAL
{
    public class DbContext
    {
        public IMongoDatabase Db;

        public DbContext()
        {
            MongoClient client = new MongoClient();
            Db = client.GetDatabase("HelpdeskDB2");

        }

        public IMongoCollection<HelpdeskEntity>GetCollection<HelpdeskEntity>()
        {
            return Db.GetCollection<HelpdeskEntity>(typeof(HelpdeskEntity).Name.ToLower() + "s");
        }
    }
}
