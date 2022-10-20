using CRUDtest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRUDtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {

        private readonly TodoContext _todoContext;

        public TodoItemsController(TodoContext todoContext)
        {
            _todoContext = todoContext;
        }


        // GET: api/<TodoItemsController>
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return _todoContext.TodoItems;
        }

        // GET api/<TodoItemsController>/5
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(Guid id)
        {
            var result = _todoContext.TodoItems.Find(id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }

            return result;
        }

        // POST api/<TodoItemsController>
        [HttpPost]
        public ActionResult<TodoItem> Post([FromBody] TodoItem value)
        {
            _todoContext.TodoItems.Add(value);
            _todoContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = value.Id }, value); //返回規範中的標頭以及成功新增的資料  標頭例如:成功新增的話回應201  

        }

        // PUT api/<TodoItemsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] TodoItem value)
        {
            if(id != value.Id)
            {
                return BadRequest();
            }

            _todoContext.Entry(value).State = EntityState.Modified;

            try
            {
                _todoContext.SaveChanges();
            }
            catch(DbUpdateException)
            {
                if(_todoContext.TodoItems.Any(e=>e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }

            return NoContent();
        }

        // DELETE api/<TodoItemsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var delete = _todoContext.TodoItems.Find(id);

            if (delete == null)
            {
                return NotFound();
            }

            _todoContext.TodoItems.Remove(delete);
            _todoContext.SaveChanges();

            return NoContent();
        }
    }
}
