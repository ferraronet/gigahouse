using GigaHouse.Core.Common.Logging;
using MongoDB.Bson;

namespace GigaHouse.Infrastructure.Interfaces.Services
{
    public interface IRequestLogService
    {
        Task<RequestLog?> GetRequestByIdAsync(string id);

        Task SaveRequestAsync(string id, BsonDocument jsonBody);

        Task<bool> UpdateRequestAsync(string id, BsonDocument updatedJsonBody);

        Task<bool> DeleteRequestAsync(string id);
    }
}
