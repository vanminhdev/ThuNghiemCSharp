using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationTest.DbContexts;
using WebApplicationTest.Entities;

namespace WebApplicationTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoDeleteBehaviorController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DemoDeleteBehaviorController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Test xoá principle entity sẽ set null vào khoá ngoại,
        /// </summary>
        [HttpGet("delete-principle")]
        public void DeletePrinciple()
        {
            var principleEntity = _dbContext.EntityPrinciples.Add(new EntityPrinciple
            {
                Name = Faker.Name.FullName(),
            });
            _dbContext.SaveChanges();

            var dependentEntity = _dbContext.EntityDependents.Add(new EntityDependent
            {
                Name = Faker.Name.FullName(),
                EntityPrincipleId = principleEntity.Entity.Id
            });

            var dependentEntity2 = _dbContext.EntityDependent2s.Add(new EntityDependent2
            {
                Name = Faker.Name.FullName(),
                EntityPrincipleId = principleEntity.Entity.Id
            });
            _dbContext.SaveChanges();

            var dependentEntityLevel2 = _dbContext.EntityDependentLevel2s.Add(new EntityDependentLevel2
            {
                Name = Faker.Name.FullName(),
                EntityDependentId = dependentEntity.Entity.Id
            });
            _dbContext.SaveChanges();

            _dbContext.EntityPrinciples.Remove(principleEntity.Entity);
            _dbContext.SaveChanges();

            var test = _dbContext.EntityPrinciples.ToList();
            var test2 = _dbContext.EntityDependents.ToList();
            var test3 = _dbContext.EntityDependent2s.ToList();
            var test4 = _dbContext.EntityDependentLevel2s.ToList();
        }

        [HttpGet("delete-2")]
        public void Delete2()
        {
            _dbContext.EntityPrinciples.Where(e => e.Name != null).ExecuteUpdate(e => e.SetProperty(p => p.Name, Faker.Name.Last()));

            var principleEntity = _dbContext.EntityPrinciples.Add(new EntityPrinciple
            {
                Name = Faker.Name.FullName(),
            });
            _dbContext.SaveChanges();
            var dependentEntity = _dbContext.EntityDependents.Add(new EntityDependent
            {
                Name = Faker.Name.FullName(),
                EntityPrincipleId = principleEntity.Entity.Id
            });
            _dbContext.SaveChanges();
            var dependentEntityLevel2 = _dbContext.EntityDependentLevel2s.Add(new EntityDependentLevel2
            {
                Name = Faker.Name.FullName(),
                EntityDependentId = dependentEntity.Entity.Id
            });
            _dbContext.SaveChanges();

            _dbContext.EntityDependents.Remove(dependentEntity.Entity);
            _dbContext.SaveChanges();

            var test = _dbContext.EntityPrinciples.ToList();
            var test2 = _dbContext.EntityDependents.ToList();
            var test3 = _dbContext.EntityDependent2s.ToList();
            var test4 = _dbContext.EntityDependentLevel2s.ToList();
        }
    }
}
