using AutoMapper;
using Ecommerce.Data.Data;
using Ecommerce.Models.Models;
using Ecommerce.Models.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private APIResponse _resp;


        public ProductController(ApplicationDbContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _resp = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProducts()
        {
            var data=await _db.Products.ToListAsync();
            if (data == null)
            {
                _resp.StatusCode = HttpStatusCode.NotFound;
                _resp.ErrorMessage = "No data is available";
                return NotFound(_resp);
            }

            _resp.IsSuccess = true;
            _resp.StatusCode=HttpStatusCode.OK;
            _resp.Result = data;
            return Ok(_resp);
        }



       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpGet("{id:int}", Name = "GetProduct")]
        public async Task<ActionResult<APIResponse>> GetProduct(int id)
        {
            if (id == 0)
            {
                    _resp.StatusCode = HttpStatusCode.BadRequest;
                    _resp.ErrorMessage = "No data is available";
                    return BadRequest(_resp);   
            }
            var data = await _db.Products.FirstOrDefaultAsync(u => u.Id == id);
            if (data == null)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.ErrorMessage = $"No data is available with id :{id}";
                return BadRequest(_resp);
            }
            _resp.IsSuccess = true;
            _resp.StatusCode=HttpStatusCode.OK;
            _resp.Result = data;
            return Ok(_resp);
        }

        [HttpPost(Name = "CreateProduct")]
        public async Task<ActionResult<APIResponse>> CreateProduct(ProductCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.ErrorMessage = "Please add some record";
                return BadRequest(_resp);
            }
            //looking for the record Name must be unique 
            if (await _db.Products.FirstOrDefaultAsync(u => u.Name.ToLower() == createDTO.Name.ToLower()) != null)
            {
                _resp.StatusCode = HttpStatusCode.BadRequest;
                _resp.ErrorMessage = $"Product {createDTO.Name} already available , Name must be unique";
                return BadRequest(_resp);
            }
            //converting DTO to product schema
            Product model=_mapper.Map<Product>(createDTO);
            await _db.Products.AddAsync(model);
            _db.SaveChanges();

            _resp.IsSuccess = true;
            _resp.StatusCode = HttpStatusCode.Created;
            //It'll show the routing location
            return CreatedAtRoute("GetProduct", new { id = model.Id }, _resp);
        }
    }
}
