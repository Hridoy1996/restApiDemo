using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using restApiDemo.Data;
using restApiDemo.model;
namespace restApiDemo.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : Controller
    {
        private QuotesDbContext _quotesDbContext;
        
        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }
       
        [HttpGet]

        // GET: QuotesController
        public IActionResult Get(string ord)
        {
            IQueryable<Quote> quotes;

            if (ord == "asc")
            {
                quotes =  _quotesDbContext.Quotes.OrderBy(q => q.CreatedAt);
            }
            else if (ord == "desc")
            {
                quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
            }
            else
            {
                quotes = _quotesDbContext.Quotes;
            }
            return Ok(quotes);
        }
        [HttpGet("[action]")]
        // GET: QuotesController
        public IActionResult PagingQuote(int pageNumber=1, int pageSize=1)
        {
           return Ok(_quotesDbContext.Quotes.Skip((pageNumber-1)*pageSize).Take(pageSize));
           
        }
        // GET: QuotesController/Details/5
        [HttpGet("{id}", Name = "Get")]
        public Quote Get(int id)
        {
            return _quotesDbContext.Quotes.Find(id);
        }
        // GET: Quotes/Quotes/Test/5
        [HttpGet("[action]/{id}")]
        public int Test(int id)
        {
            return id;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}", Name = "Put")]
        public void Put(int id, [FromBody] Quote quote)
        {
            var record = _quotesDbContext.Quotes.Find(id);
            record.Author = quote.Author;
            record.Description = quote.Description;
            record.Title = quote.Title;
            _quotesDbContext.SaveChanges();
        }
        [HttpDelete("{id}", Name = "Delete")]
        public void Delete(int id)
        {
            var record = _quotesDbContext.Quotes.Find(id);
            _quotesDbContext.Quotes.Remove(record);
            _quotesDbContext.SaveChanges();
        }



    }
}
