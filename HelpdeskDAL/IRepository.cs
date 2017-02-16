using MongoDB.Driver;
using System.Collections.Generic;

namespace HelpdeskDAL
{
    public interface IRepository
    {
        HelpdeskEntity GetById<HelpdeskEntity>(string id);
        HelpdeskEntity GetOne<HelpdeskEntity>(FilterDefinition<HelpdeskEntity> filter);
        List<HelpdeskEntity> GetMany<HelpdeskEntity>(FilterDefinition<HelpdeskEntity> filter);
        List<HelpdeskEntity> GetAll<HelpdeskEntity>();
        HelpdeskEntity Create<HelpdeskEntity>(HelpdeskEntity item);

        long Delete<HelpdeskEntity>(string id);

        UpdateStatus Update<HelpdeskEntity>(string id, FilterDefinition<HelpdeskEntity> filter, UpdateDefinition<HelpdeskEntity> update);
        bool Exists<HelpdeskEntity>(string id);
    }
}
