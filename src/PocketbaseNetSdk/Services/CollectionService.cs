using Microsoft.Extensions.Logging;
using PocketbaseNetSdk.Models.Base;

namespace PocketbaseNetSdk.Services
{
    public class CollectionService(PocketbaseClient client, ILogger<CollectionService>? logger = null) : ICollectionService
    {
        public IRecordService<TModel> Collection<TModel>(string collectionName) where TModel : BaseModel, new()
        {
            return new RecordService<TModel>(client, collectionName, logger as ILogger<RecordService<TModel>>);
        }
    }
}
