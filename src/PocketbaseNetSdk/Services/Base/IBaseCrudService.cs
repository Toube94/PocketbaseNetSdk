using PocketbaseNetSdk.Models;
using PocketbaseNetSdk.Models.Base;

namespace PocketbaseNetSdk.Services.Base
{
    public interface IBaseCrudService<TModel> where TModel : BaseModel
    {
        /// <summary>
        /// Creates a new model in the Pocketbase collection.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Created TModel</returns>
        public Task<Result<TModel>> CreateAsync(TModel model);
        /// <summary>
        /// Retrieves a paginated list of models from the Pocketbase collection.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="perPage"></param>
        /// <param name="sort"></param>
        /// <param name="filter"></param>
        /// <param name="skipTotal"></param>
        /// <returns></returns>
        public Task<Result<PocketbaseList<TModel>>> GetAsync(int page = 1, int perPage = 30, string? sort = null, string? filter = null,
            bool skipTotal = false);
        /// <summary>
        /// Retrieves a single model by its ID from the Pocketbase collection.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result<TModel>> View(string id);
        /// <summary>
        /// Updates an existing model in the Pocketbase collection.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<Result<TModel>> UpdateAsync(TModel model);
        /// <summary>
        /// Deletes a model by its ID from the Pocketbase collection.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<Result> DeleteAsync(string id);
    }
}
