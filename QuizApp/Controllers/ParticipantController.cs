using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Data.Context;

namespace QuizApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        #region

        private readonly QuizDbContext _context;
        private Participant _dbPart;
        #endregion
        #region
        public ParticipantController(QuizDbContext context)
        {
            _context = context;
        }
        #endregion
        #region
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participant>>> GetParticipantLsit()
        {
            if (ModelState.IsValid)
            {
                var participant = await _context.Participants.FromSql($"SELECT * from Dbo.Participants").ToListAsync();
                return Ok(participant);
            }
            else
            {
                ModelState.AddModelError("", "No Data Found");
            }
            return Ok();

            //return await _context.Participants.ToListAsync();
        }
        #endregion
        #region
        [HttpGet("{id}")]
        public async Task<ActionResult<Participant>> GetParticipantBYId(int id)
        {

            //var participant = await _context.Participants.FindAsync(id);
            //return participant;





            //if (participant == null)
            //{
            //  return  NotFound("No Participant Available");
            //}
            //using ef core 8 

            FormattableString query = $"Select * from Participants where ParticipantId={id}";
            return await _context.Database.SqlQuery<Participant>(query).FirstOrDefaultAsync();
           

        }
        #endregion
        #region
        [HttpPost]
        public async Task<ActionResult<Participant>> PostParticipant(Participant participant)
        {
            var temP=_context.Participants.Where(x=>x.Name == participant.Name
            && x.Email==participant.Email).FirstOrDefault();
            if (temP==null)
            {
                _context.Participants.Add(participant);
                await _context.SaveChangesAsync();
            }
            else
            {
                participant = temP;
            }
            return Ok(participant);
        }
        #endregion
        #region
        [HttpPut("{id}")]
        public async Task<ActionResult<Participant>> PutParticipant(int id,ParticipantResult participantResult)
        {
            if (id != participantResult.ParticipantId)
            {
                return BadRequest();
            }
            //
            Participant participant=_context.Participants.Find(id);
            participant.Score=participantResult.Score;
            participant.TimeTaken=participantResult.TimeTaken;
            _context.Entry(participant).State = EntityState.Modified;

            try
            {
               await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExist(id))
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
        #endregion
        #region
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteParticipant(int id)
        {
            var participant= await _context.Participants.FindAsync(id);
            if (participant == null)
            {
                 NotFound();
            }
            _context.Participants.Remove(participant);
           await  _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
        #region
        private bool ParticipantExist(int id)
        {
          return   _context.Participants.Any(e => e.ParticipantId==id);
        }
        #endregion
    }
}
