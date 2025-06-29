﻿using CatalogService.Domain.Entities;
using CatalogService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CatalogService.DataAccess.Data
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly CatalogDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(CatalogDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> ListAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task AddOutboxEventAsync(string eventType, object payload)
        {
            string payloadJson = JsonSerializer.Serialize(payload);

            var outboxRecord = new Outbox
            {
                EventType = eventType,
                Payload = payloadJson,
                IsProcessed = false,
                CreatedAt = DateTime.UtcNow
            };

            await _context.Set<Outbox>().AddAsync(outboxRecord);
        }
    }
}
