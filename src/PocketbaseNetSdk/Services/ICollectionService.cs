
namespace PocketbaseNetSdk.Services
{
    public interface ICollectionService
    {
        public IRecordService<TModel> Collection<TModel>(string collectionName)
            where TModel : Models.Base.BaseModel, new();
    }
}
