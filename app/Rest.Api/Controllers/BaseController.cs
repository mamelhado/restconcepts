using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;

namespace Rest.Api.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController<TEntity, TCreateModel, TUpdateModel, TOutputModel> : ControllerBase 
        where TEntity : class
        where TCreateModel : class
        where TUpdateModel : class
        where TOutputModel : class
    {
        private readonly IBaseService<TEntity> _baseService;
        private readonly ILogger<BaseController<TEntity, TCreateModel, TUpdateModel, TOutputModel>> _logger;

        public BaseController(ILogger<BaseController<TEntity, TCreateModel, TUpdateModel, TOutputModel>> logger, IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
            _logger = logger;
        }

        /// <summary>
        /// Get a specific Item.
        /// </summary>
        /// <param name="id"></param> 
        /// <response code="200">Returns the item specified by your id</response>
        /// <response code="404">If the item is not found </response>  
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<ActionResult<TOutputModel>> GetByIdAsync(int id, CancellationToken token) 
        {
            var entity = await _baseService.GetByIdAsync(id, token);
            if (entity == null)
            {
                _logger.LogWarning($"Object not found");
                return NotFound();
            }

            return Ok(entity);
        }


        /// <summary>
        /// Get a collection of Itens.
        /// </summary>
        /// <response code="200">Returns the collection</response>
        /// <response code="204">If the collection is empty </response>
        /// <response code="404">If the collection is not found </response>  
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual ActionResult<IQueryable<TOutputModel>> GetAll() 
        {
            var entities =  _baseService.GetAll();
            if (entities == null)
            {
                _logger.LogTrace($"The collection not found!");
                return NotFound();
            }

            if (entities != null && !entities.Any()) 
            {
                _logger.LogTrace($"The collection is empty!");
                return NoContent();
            }

            return Ok(entities);
        }

        //[HttpGet()]
        //public async Task<IQueryable<TOutputModel>> GetOData() 
        //{
        //    return await _baseService.GetOData();
        //}

        /// <summary>
        /// Create a new Item.
        /// </summary>
        /// <param name="model"></param> 
        /// <response code="201">Returns de item created</response>
        /// <response code="400">If the request has errors then not create Item </response>
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<ActionResult<TOutputModel>> InsertAsync([FromBody] TEntity model, CancellationToken token) 
        {
            var entity = await _baseService.AddAsync(model);
            _logger.LogTrace($"The object {JsonConvert.SerializeObject(entity)} was created");
            return Created(nameof(GetByIdAsync),entity);
        }


        /// <summary>
        /// Update a specific Item.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content for request processed with success</response>
        /// <response code="400">If the request has errors then not update Item </response>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> UpdateAsync(int id, [FromBody] TEntity model, CancellationToken token)
        {
            await _baseService.UpdateAsync(model, token);
            _logger.LogTrace($"The object {JsonConvert.SerializeObject(model)} was updated");
            return NoContent();
        }

        /// <summary>
        /// Deletes a specific Item.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="204">Returns no content for request processed with success</response>
        /// <response code="404">If the collection is not found </response>  
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> DeleteAsync(int id, CancellationToken token)
        {
            var entity = await _baseService.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            await _baseService.DeleteAsync(id, token);
            _logger.LogTrace($"The object {JsonConvert.SerializeObject(entity)} was deleted");
            return NoContent();
        }
    }
}