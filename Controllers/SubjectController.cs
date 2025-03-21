using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using SubjectApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SubjectApp.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects.ToListAsync();
            return View(subjects);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(string subjectName)
        {
            if (!string.IsNullOrEmpty(subjectName))
            {
                var subject = new Subject{ SubjectName = subjectName };
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubject(int subjectId)
        {
            var subject = await _context.Subjects.FindAsync(subjectId);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubTopics(int subjectId)
        {
            var subTopics = await _context.SubTopics
                .Where(st => st.SubjectId == subjectId)
                .Select(st => new { st.SubTopicId, st.SubTopicName })
                .ToListAsync();

            return Json(subTopics);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubTopic(int subjectId, string subTopicName)
        {
            if (!string.IsNullOrEmpty(subTopicName))
            {
                var subTopic = new SubTopic { SubjectId = subjectId, SubTopicName = subTopicName };
                _context.SubTopics.Add(subTopic);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSubTopic(int subTopicId)
        {
            var subTopic = await _context.SubTopics.FindAsync(subTopicId);
            if (subTopic != null)
            {
                _context.SubTopics.Remove(subTopic);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}
