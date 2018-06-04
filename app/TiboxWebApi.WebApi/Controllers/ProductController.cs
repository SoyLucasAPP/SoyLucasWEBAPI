using FluentValidation;
using System.Net;
using System.Web.Http;
using TiboxWebApi.Models;
using TiboxWebApi.UnitOfWork;


namespace TiboxWebApi.WebApi.Controllers
{
    [RoutePrefix("Product")]
    [Authorize]
    public class PoductController : BaseController
    {
        private readonly AbstractValidator<Product> _validator;
        public PoductController(IUnitOfWork unit, AbstractValidator<Product> validator) : base(unit)
        {
            _validator = validator;
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            if (id <= 0) return BadRequest();
            return Ok(_unit.Products.GetEntityById(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            var result = _validator.Validate(product);
            if (!result.IsValid) return Content(HttpStatusCode.BadRequest, result.Errors);
            var id = _unit.Products.Insert(product);
            return Ok(new { id = id });
        }

        [Route("")]
        [HttpPut]
        public IHttpActionResult Put(Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Products.Update(product);
            return Ok(new { status = true });
        }

        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var result = _unit.Products.Delete(new Product { Id = id });
            return Ok(new { detele = true });
        }

        [Route("List")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Products.GetAll());
        }
    }
}
