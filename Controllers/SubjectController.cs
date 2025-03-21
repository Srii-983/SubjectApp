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
        public JsonResult EditSubject(int subjectId, string newName)
        {
            var subject = _context.Subjects.Find(subjectId);
            if (subject == null)
            {
                return Json(new { success = false, message = "Subject not found!" });
            }

            subject.SubjectName = newName;
            _context.SaveChanges();

            return Json(new { success = true });
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
        [HttpPost]
        public async Task<IActionResult> EditSubTopic(int subTopicId, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                return Json(new { success = false, message = "SubTopic name cannot be empty!" });
            }

            var subTopic = await _context.SubTopics.FindAsync(subTopicId);
            if (subTopic == null)
            {
                return Json(new { success = false, message = "SubTopic not found!" });
            }

            // ✅ Check for duplicate subtopic under the same subject
            bool isDuplicate = await _context.SubTopics
                .AnyAsync(st => st.SubjectId == subTopic.SubjectId && st.SubTopicName == newName);

            if (isDuplicate)
            {
                return Json(new { success = false, message = "SubTopic already exists!" });
            }

            subTopic.SubTopicName = newName;
            await _context.SaveChangesAsync();

            return Json(new { success = true, newName = subTopic.SubTopicName });
        }




    }
}
