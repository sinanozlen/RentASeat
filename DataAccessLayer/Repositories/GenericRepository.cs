using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        private readonly RenASeatContext _renASeatContext;

        public GenericRepository(RenASeatContext renASeatContext)
        {
            _renASeatContext = renASeatContext;
        }

        public void Add(T entity)
        {
            _renASeatContext.Add(entity);
            _renASeatContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            _renASeatContext.Remove(entity);
            _renASeatContext.SaveChanges();
        }


        public T GetbyID(int ID)
        {
            var values= _renASeatContext.Set<T>().Find(ID);
            return values;
        }

        public List<T> GetListAll()
        {
           var values= _renASeatContext.Set<T>().ToList();
            return values;
        }

        public void Update(T entity)
        {
            _renASeatContext.Update(entity);
            _renASeatContext.SaveChanges();
        }
    }
}
