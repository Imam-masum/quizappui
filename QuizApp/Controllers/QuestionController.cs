using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.Context;
using System.Collections;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        #region
        private readonly QuizDbContext _context;
        #endregion
        #region
        public QuestionController(QuizDbContext context)
        {
            _context = context;
        }
        #endregion
        #region
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestion()
        {
           var randomq= await (_context.Questions.Select(p=>new
           {
               QsId=p.QsId,
               QsInWords=p.QsInWords,
               ImageName=p.ImageName,
               Options=new[] {p.Option1,p.Option2,p.Option3,p.Option4}
           })
                .OrderBy(q=> Guid.NewGuid())
                .Take(5))
                .ToListAsync();
            return Ok(randomq);
        }
        #endregion

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestionById(int id)
        {
            var qestion =  await _context.Questions.FindAsync(id);

            if (qestion == null)
            {
                return BadRequest();
            }
            return qestion;
        }
        #region
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            try
            {
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(question);
        }
        #endregion
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestion(int id,Question question)
        {
            if (id !=question.QsId)
            {
                return BadRequest();
            }
            _context.Entry(question).State= EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }

            catch(DbUpdateConcurrencyException)
            {
                if (!QuestionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }
        [HttpPost]
        [Route("GetAnswer")]
        public async Task<ActionResult<Question>> ReteriveAnswer(int[] qnsId)
        {
            var answer = await(  _context.Questions.Where(w => qnsId.Contains(w.QsId))
                .Select(e => new
                {
                    QsId = e.QsId,
                    QsInWords = e.QsInWords,
                    ImageName = e.ImageName,
                    Options = new string[] { e.Option1, e.Option2, e.Option3, e.Option4 },
                    Answer = e.Answer,
                }).ToListAsync());
                return Ok(answer);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(q=>q.QsId==id);
        }
    }
}
