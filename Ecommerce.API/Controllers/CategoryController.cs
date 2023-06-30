using AutoMapper;
using Ecommerce.Data.Data;
using Ecommerce.Models.Models;
using Ecommerce.Models.Models.DTO.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private APIResponse _resp;
        public CategoryController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper=mapper;
            _resp = new();
        }

        [HttpGet("GetCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> GetCategories()
        {
            try 
            {
                var catagories = await _db.Categories.Include(p => p.Products).ToListAsync();
                if (catagories == null)
                {
                    _resp.StatusCode = HttpStatusCode.NotFound;
                    _resp.ErrorMessage = "No Data is available";
                    return NotFound(_resp);
                }
                _resp.StatusCode = HttpStatusCode.OK;
                _resp.IsSuccess = true;
                _resp.Result = catagories;
                return Ok(_resp);
            }
            catch (Exception ex) 
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.ErrorMessage = ex.Message;
                return BadRequest(_resp);
            }
           
        }

        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _db.Categories.FirstOrDefaultAsync(u => u.CategoryId == id);
                if (id == 0 || category == null)
                {
                    _resp.StatusCode = HttpStatusCode.NotFound;
                    _resp.ErrorMessage = $"no record available with id : {id}";
                    return NotFound(_resp);
                }
                _resp.StatusCode = HttpStatusCode.OK;
                _resp.Result = category;
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.ErrorMessage = ex.Message;
                return NotFound(_resp);
            }
        }

        [HttpPost("CreateCategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateCategory([FromBody] CategoryCreateDTO createDTO)
        {
            try 
            {
                //verify whether the same category is available or not
                if (await _db.Categories.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
                {
                    _resp.StatusCode = HttpStatusCode.BadRequest;
                    _resp.ErrorMessage = $"{createDTO.Name}  already exist create a unique category";
                    return BadRequest(_resp);
                }
                var category = _mapper.Map<Category>(createDTO);
                await _db.AddAsync(category);
                await _db.SaveChangesAsync();
                _resp.StatusCode = HttpStatusCode.Created;
                _resp.IsSuccess = true;
                return CreatedAtRoute("GetCategory", new { Id = category.CategoryId }, _resp);
            }
            catch (Exception ex)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.ErrorMessage = ex.Message;
                return BadRequest(_resp);
            }
        }
    }
}
