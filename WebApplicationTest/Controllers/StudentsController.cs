using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplicationTest.DbContexts;
using WebApplicationTest.Dtos.Student;
using WebApplicationTest.Entities;

namespace WebApplicationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        private const int TestId = 1;
        private readonly ILogger _logger;

        public StudentsController(ApplicationDbContext context, IDbContextFactory<ApplicationDbContext> contextFactory,
            ILogger<StudentsController> logger)
        {
            _context = context;
            _contextFactory = contextFactory;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        [Benchmark]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            //_context.Students.Remove(_context.Students.FirstOrDefault(s => s.Id == 1)!);
            //_context.SaveChanges();
            if (_context.Students == null)
            {
                return NotFound();
            }

            int id = TestId;


            var test = _context.Students.FirstOrDefault(s => s.Id == id);

            //var test2 = _context.Database.SqlQuery<int>($"select count(Id) from Students").FirstOrDefault();

            _logger.LogInformation($"{nameof(GetStudents)}: threadId = {Environment.CurrentManagedThreadId}");

            Thread thread = new Thread(() =>
            {
                _logger.LogInformation($"run thread: threadId = {Environment.CurrentManagedThreadId}");


                var dbContext = _contextFactory.CreateDbContext();
                var test2 = dbContext.Students.ToList();
            });
            thread.Start();

            var task = Task.Run(async () =>
            {
                _logger.LogInformation($"run task: threadId = {Environment.CurrentManagedThreadId}");

                var dbContext = _contextFactory.CreateDbContext();
                var test2 = await dbContext.Students.ToListAsync();
            });
            //task.RunSynchronously();
            await Task.WhenAll(task);


            var result = await _context.Students.ToListAsync();
            return result;
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        [HttpDelete("remove-all")]
        public void RemoveAll()
        {
            //có tác dụng transaction
            var transaction = _context.Database.BeginTransaction();
            _context.Students.ExecuteDelete();

            //_context.SaveChanges();
            transaction.Commit();
        }

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
            }
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return (_context.Students?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
