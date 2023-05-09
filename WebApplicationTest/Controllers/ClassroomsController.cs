﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest.DbContexts;
using WebApplicationTest.Entities;

namespace WebApplicationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassroomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Classrooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classroom>>> GetClassrooms()
        {
            if (_context.Classrooms == null)
            {
                return NotFound();
            }
            return await _context.Classrooms.ToListAsync();
        }

        // GET: api/Classrooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Classroom>> GetClassroom(int id)
        {
            if (_context.Classrooms == null)
            {
                return NotFound();
            }
            var classroom = await _context.Classrooms.FindAsync(id);

            if (classroom == null)
            {
                return NotFound();
            }

            return classroom;
        }

        // PUT: api/Classrooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClassroom(int id, Classroom classroom)
        {
            if (id != classroom.Id)
            {
                return BadRequest();
            }

            _context.Entry(classroom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClassroomExists(id))
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

        // POST: api/Classrooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Classroom>> PostClassroom(Classroom classroom)
        {
            if (_context.Classrooms == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Classrooms'  is null.");
            }
            _context.Classrooms.Add(classroom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClassroom", new { id = classroom.Id }, classroom);
        }

        // DELETE: api/Classrooms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(int id)
        {
            if (_context.Classrooms == null)
            {
                return NotFound();
            }
            var classroom = await _context.Classrooms.FindAsync(id);
            if (classroom == null)
            {
                return NotFound();
            }

            _context.Classrooms.Remove(classroom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("get-all-student/{classroomId}")]
        public IActionResult GetAllStudent(int classroomId)
        {
            var result = _context.StudentsClassrooms
                .Include(sc => sc.Student)
                .ThenInclude(s => s.Hobbies)
                .Include(sc => sc.Classroom)
                .Where(sc => sc.ClassroomId == classroomId) //&& sc.Student.Hobbies.Any(h => h.Name.Contains("Liên Minh"))
                .Select(sc => new
                {
                    sc.Id,
                    Hobbies = sc.Student.Hobbies.Where(h => h.Name.Contains("Liên Minh")).Select(h => h.Name),
                });

            var test = _context.Hobbies
                .Include(h => h.Student)
                //.Where(h => h.Student != null && h.Student.Name == "Nguyễn Văn A")
                .ToList();

            return Ok(result);
        }

        private bool ClassroomExists(int id)
        {
            return (_context.Classrooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
