using Microsoft.EntityFrameworkCore;
using VuelosAPI.Models;

namespace VuelosAPI.Repositories
{
    public class Repository<T> where T : class
    {
        private readonly sistem21_vuelosContext context;

        public Repository(sistem21_vuelosContext context)
        {
            this.context = context;
        }

        public DbSet<T> Get()
        {
            return context.Set<T>();
        }

        public T? Get(object id)
        {
            return context.Find<T>(id);
        }
        public void Insert(T entidad)
        {
            context.Add(entidad);
            context.SaveChanges();
        }

        public void Update(T entidad)
        {
            context.Update(entidad);
            context.SaveChanges();
        }

        public void Delete(T entidad)
        {
            context.Remove(entidad);
            context.SaveChanges();
        }
    }
}
