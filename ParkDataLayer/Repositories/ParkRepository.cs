using Microsoft.EntityFrameworkCore;
using ParkDataLayer.Model;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ParkRepository
    {
        private readonly DBContext ctx;

        public ParkRepository(DBContext ctx)
        {
            this.ctx = ctx;
        }

        public ParkEF GetOrCreatePark(string parkId)
        {
            ParkEF existingPark = ctx.Parken
                .Include(p => p._huis)
                .FirstOrDefault(p => p.Id == parkId);

            if (existingPark != null)
            {
                return existingPark;
            }
            else
            {
                return null;
            }
        }
    }
}
