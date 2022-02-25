using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Models;

namespace UnicornInsurance.Data.Repositories
{
    public class WeaponRepository : GenericRepository<Weapon>, IWeaponRepository
    {
        private readonly UnicornDataDBContext _dbContext;

        public WeaponRepository(UnicornDataDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Weapon>> GetStandardWeaponsList()
        {
            var standardWeapons = await _dbContext.Weapons.Where(w => w.IsCustomWeapon == false).ToListAsync();

            return standardWeapons;
        }
    }
}
