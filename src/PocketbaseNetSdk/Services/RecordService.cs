using Microsoft.Extensions.Logging;
using PocketbaseNetSdk.Models.Base;
using PocketbaseNetSdk.Services.Base;

namespace PocketbaseNetSdk.Services
{
    public class RecordService<TModel>(PocketbaseClient client, string collectionName, ILogger<BaseCrudService<TModel>>? logger) 
        : BaseCrudService<TModel>(client, logger), IRecordService<TModel>
        where TModel : BaseModel, new()
    {
        public override string CollectionName { get; } = collectionName;

        public override string BasePath(string? path = null)
        {
            if (path != null)
            {
                return $"/api/collections/{CollectionName}/records/{path}";
            }

            return $"/api/collections/{CollectionName}/records";
        }
    }
}
