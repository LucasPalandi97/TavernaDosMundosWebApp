using Microsoft.EntityFrameworkCore;
using TdM.Database.Data;
using TdM.Database.Models.Domain;

namespace TdM.Web.Repositories
{
    public class MundoRepository : IMundoRepository
    {
        private readonly TavernaDbContext tavernaDbContext;

        public MundoRepository(TavernaDbContext tavernaDbContext)
        {
            this.tavernaDbContext = tavernaDbContext;
        }
        public async Task<Mundo> AddAsync(Mundo mundo)
        {
            await tavernaDbContext.Mundos.AddAsync(mundo);
            await tavernaDbContext.SaveChangesAsync();
            return mundo;

        }

        public async Task<Mundo?> DeleteAsync(Guid id)
        {
            var existingMundo = await tavernaDbContext.Mundos.FindAsync(id);

                if(existingMundo != null)
            {
                tavernaDbContext.Mundos.Remove(existingMundo);
                await tavernaDbContext.SaveChangesAsync();
                return existingMundo;
            }
            return null;
        }

        public async Task<IEnumerable<Mundo>> GetAllAsync()
        {
            return await tavernaDbContext.Mundos.ToListAsync();
        }

        public Task<Mundo?> GetAsync(Guid id)
        {
            return tavernaDbContext.Mundos.FirstOrDefaultAsync(x => x.MundoId == id);

        }

        public async Task<Mundo?> UpdateAsync(Mundo mundo)
        {
            var existingMundo = await tavernaDbContext.Mundos.FindAsync(mundo.MundoId);

            if(existingMundo != null)
            {
                existingMundo.Nome = mundo.Nome;
                existingMundo.Descricao = mundo.Descricao;
                existingMundo.Autor = mundo.Autor;

                await tavernaDbContext.SaveChangesAsync();

                return existingMundo;
            }

            return null;
        }
    }
}
