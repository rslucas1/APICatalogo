﻿using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        protected AppDbContext _context;

        public Repository(AppDbContext contexto)
        {
            _context = contexto;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefault(predicate);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
