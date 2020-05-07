using System;
using System.Linq;
using WordsTeacher.Data;
using WordsTeacher.Data.Entities;

namespace WordsTeacher.Services
{
    public class Repository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Insert(T entity)
        {
            entity.CreateDateUtc = DateTime.UtcNow;
            entity.UpdateTimeUtc = DateTime.UtcNow;
            _dbContext.Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            entity.UpdateTimeUtc = DateTime.UtcNow;
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public void Delete(T entity)
        {
            entity.DeleteTimeUtc = DateTime.UtcNow;
            _dbContext.Update(entity);
            _dbContext.SaveChanges();
        }

        public IQueryable<T> Table => _dbContext.Set<T>().AsQueryable().Where(a => a.DeleteTimeUtc == null);
    }
}