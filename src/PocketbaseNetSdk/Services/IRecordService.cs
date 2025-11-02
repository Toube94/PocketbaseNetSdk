using PocketbaseNetSdk.Models;
using PocketbaseNetSdk.Models.Base;
using PocketbaseNetSdk.Services.Base;

namespace PocketbaseNetSdk.Services
{
    public interface IRecordService<TModel> : IBaseCrudService<TModel> where TModel : BaseModel
    {

    }
}
