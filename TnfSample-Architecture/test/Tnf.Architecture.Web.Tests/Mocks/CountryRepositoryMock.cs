using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Tnf.Architecture.EntityFrameworkCore.Entities;
using Tnf.Domain.Repositories;
using System;
using System.Linq.Expressions;

namespace Tnf.Architecture.Web.Tests.Mocks
{
    public class CountryRepositoryMock : IRepository<Country>
    {
        private readonly ConcurrentDictionary<int, Country> _countries = null;

        public CountryRepositoryMock()
        {
            var source = new Dictionary<int, Country>()
            {
                { 1, new Country(1, "Brasil") },
                { 2, new Country(2, "EUA") },
                { 3, new Country(3, "Uruguai") },
                { 4, new Country(4, "Paraguai") },
                { 5, new Country(5, "Venezuela") }
            };

            _countries = new ConcurrentDictionary<int, Country>(source);
        }

        public int Count()
        {
            return _countries.Count;
        }

        public int Count(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            return _countries.Select(s => s.Value).Count(compiled);
        }

        public Task<int> CountAsync()
        {
            return Task.FromResult(_countries.Count);
        }

        public Task<int> CountAsync(Expression<Func<Country, bool>> predicate)
        {
            return Task.FromResult(_countries.Count());
        }

        public void Delete(Country entity)
        {
            _countries.TryRemove(entity.Id, out entity);
        }

        public void Delete(int id)
        {
            _countries.TryRemove(id, out Country entity);
        }

        public void Delete(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            var itemToRemove = _countries.Select(s => s.Value).First(compiled);
            _countries.TryRemove(itemToRemove.Id, out Country country);
        }

        public Task DeleteAsync(Country entity)
        {
            _countries.TryRemove(entity.Id, out Country country);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(int id)
        {
            _countries.TryRemove(id, out Country country);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            var itemToRemove = _countries.Select(s => s.Value).First(compiled);
            _countries.TryRemove(itemToRemove.Id, out Country country);

            return Task.FromResult<object>(null);
        }

        public Country FirstOrDefault(int id)
        {
            var result = _countries.FirstOrDefault(w => w.Key == id);
            return result.Value;
        }

        public Country FirstOrDefault(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            var itemToRemove = _countries.Select(s => s.Value).FirstOrDefault(compiled);
            return itemToRemove;
        }

        public Task<Country> FirstOrDefaultAsync(int id)
        {
            var itemToRemove = _countries.Select(s => s.Value).FirstOrDefault(w => w.Id == id);
            return Task.FromResult(itemToRemove);
        }

        public Task<Country> FirstOrDefaultAsync(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            var itemToRemove = _countries.Select(s => s.Value).FirstOrDefault(compiled);
            return Task.FromResult(itemToRemove);
        }

        public Country Get(int id)
        {
            var itemToRemove = _countries.Select(s => s.Value).First(w => w.Id == id);
            return itemToRemove;
        }

        public IQueryable<Country> GetAll()
        {
            return _countries.Select(s => s.Value).AsQueryable();
        }

        public IQueryable<Country> GetAllIncluding(params Expression<Func<Country, object>>[] propertySelectors)
        {
            throw new NotImplementedException();
        }

        public List<Country> GetAllList()
        {
            return _countries.Select(s => s.Value).ToList();
        }

        public List<Country> GetAllList(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            return _countries.Select(s => s.Value).Where(compiled).ToList();
        }

        public Task<List<Country>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public Task<List<Country>> GetAllListAsync(Expression<Func<Country, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public Task<Country> GetAsync(int id)
        {
            return Task.FromResult(Get(id));
        }

        public Country Insert(Country entity)
        {
            _countries.TryAdd(entity.Id, entity);
            return entity;
        }

        public int InsertAndGetId(Country entity)
        {
            _countries.TryAdd(entity.Id, entity);
            return entity.Id;
        }

        public Task<int> InsertAndGetIdAsync(Country entity)
        {
            return Task.FromResult(InsertAndGetId(entity));
        }

        public Task<Country> InsertAsync(Country entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public Country InsertOrUpdate(Country entity)
        {
            _countries.TryAdd(entity.Id, entity);
            return entity;
        }

        public int InsertOrUpdateAndGetId(Country entity)
        {
            _countries.TryAdd(entity.Id, entity);
            return entity.Id;
        }

        public Task<int> InsertOrUpdateAndGetIdAsync(Country entity)
        {
            return Task.FromResult(InsertOrUpdateAndGetId(entity));
        }

        public Task<Country> InsertOrUpdateAsync(Country entity)
        {
            return Task.FromResult(InsertOrUpdate(entity));
        }

        public Country Load(int id)
        {
            return Get(id);
        }

        public long LongCount()
        {
            return _countries.LongCount();
        }

        public long LongCount(Expression<Func<Country, bool>> predicate)
        {
            var compiled = predicate.Compile();
            return _countries.Select(s => s.Value).LongCount(compiled);
        }

        public Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public Task<long> LongCountAsync(Expression<Func<Country, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        public T Query<T>(Func<IQueryable<Country>, T> queryMethod)
        {
            throw new NotImplementedException();
        }

        public Country Single(Expression<Func<Country, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Country> SingleAsync(Expression<Func<Country, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Country Update(Country entity)
        {
            throw new NotImplementedException();
        }

        public Country Update(int id, Action<Country> updateAction)
        {
            throw new NotImplementedException();
        }

        public Task<Country> UpdateAsync(Country entity)
        {
            throw new NotImplementedException();
        }

        public Task<Country> UpdateAsync(int id, Func<Country, Task> updateAction)
        {
            throw new NotImplementedException();
        }
    }
}
