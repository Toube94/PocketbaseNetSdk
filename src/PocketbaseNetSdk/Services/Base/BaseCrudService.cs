using Microsoft.Extensions.Logging;
using PocketbaseNetSdk.Models;
using PocketbaseNetSdk.Models.Base;

namespace PocketbaseNetSdk.Services.Base
{
    public abstract class BaseCrudService<TModel> : IBaseCrudService<TModel>
        where TModel : BaseModel 
    {
        readonly ILogger<BaseCrudService<TModel>>? _logger;

        public PocketbaseClient Client { get; init; }
        public abstract string BasePath(string? path = null);
        public abstract string CollectionName { get; }

        protected BaseCrudService(PocketbaseClient client, ILogger<BaseCrudService<TModel>>? logger)
        {
            Client = client;
            _logger = logger;
        }

        public async Task<Result<TModel>> CreateAsync(TModel model)
        {
            _logger?.LogTrace("CreateAsync called");

            var result = await Client.SendJsonRequestAsync<TModel, TModel>(
                HttpMethod.Post,
                BasePath(),
                model)
                .ConfigureAwait(false);

            if(result.IsSuccess && result.Value == null)
            {
                _logger?.LogError("Result of creation entity is success but value is empty");
                throw new InvalidOperationException("Result of creation entity is success but value is empty");
            }

            return Result<TModel>.Success(result.Value!);
        }

        public async Task<Result<PocketbaseList<TModel>>> GetAsync(int page=1, int perPage=30, string? sort = null, string? filter = null,
            bool skipTotal=false)
        {
            _logger?.LogTrace("GetAsync called");

            var query = new Dictionary<string, object?>
            {
                { "page", page },
                { "perPage", perPage },
                { "skipTotal", skipTotal }
            };

            if (!string.IsNullOrEmpty(sort))
            {
                _logger?.LogTrace("Adding sort parameter: {Sort}", sort);
                query.Add("sort", sort);
            }

            if (!string.IsNullOrEmpty(filter))
            {
                _logger?.LogTrace("Adding filter parameter: {Filter}", filter);
                query.Add("filter", filter);
            }

            var result = await Client.SendJsonRequestAsync<PocketbaseList<TModel>, object>(
                HttpMethod.Get,
                BasePath(),
                null,
                query)
                .ConfigureAwait(false);

            if(result.IsSuccess && result.Value == null)
            {
                _logger?.LogError("Result of getting entities is success but value is empty");
                throw new InvalidOperationException("Result of getting entities is success but value is empty");
            }

            return Result<PocketbaseList<TModel>>.Success(result.Value!);
        }

        public async Task<Result<TModel>> View(string id)
        {
            _logger?.LogTrace("View called for ID: {Id}", id);

            var result = await Client.SendJsonRequestAsync<TModel, object>(
                HttpMethod.Get,
                BasePath(id),
                null)
                .ConfigureAwait(false);

            if(result.IsSuccess && result.Value == null)
            {
                _logger?.LogError("Result of viewing entity is success but value is empty");
                throw new InvalidOperationException("Result of viewing entity is success but value is empty");
            }

            return Result<TModel>.Success(result.Value!);
        }

        public async Task<Result<TModel>> UpdateAsync(TModel model)
        {
            _logger?.LogTrace("UpdateAsync called for ID: {Id}", model.Id);

            var result = await Client.SendJsonRequestAsync<TModel, TModel>(
                HttpMethod.Patch,
                BasePath(model.Id),
                model)
                .ConfigureAwait(false);

            if(result.IsSuccess && result.Value == null)
            {
                _logger?.LogError("Result of updating entity is success but value is empty");
                throw new InvalidOperationException("Result of updating entity is success but value is empty");
            }

            return Result<TModel>.Success(result.Value!);
        }

        public async Task<Result> DeleteAsync(string id)
        {
            _logger?.LogTrace("DeleteAsync called for ID: {Id}", id);

            var result = await Client.SendJsonRequestAsync<object, object>(
                HttpMethod.Delete,
                BasePath(id),
                null)
                .ConfigureAwait(false);

            if(result.IsSuccess)
            {
                return Result.Success();
            }
            else
            {
                return Result.Failure(result.Error ?? "Unknown error during deletion");
            }
        }
    }
}
