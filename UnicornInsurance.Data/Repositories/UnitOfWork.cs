using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;

namespace UnicornInsurance.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UnicornDataDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IWeaponRepository _weaponRepository;
        private IMobileSuitRepository _mobileSuitRepository;
        private IMobileSuitCartRepository _mobileSuitCartRepository;
        private IWeaponCartRepository _weaponCartRepository;

        public UnitOfWork(UnicornDataDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IWeaponRepository WeaponRepository =>
            _weaponRepository ??= new WeaponRepository(_context);

        public IMobileSuitRepository MobileSuitRepository =>
            _mobileSuitRepository ??= new MobileSuitRepository(_context);

        public IMobileSuitCartRepository MobileSuitCartRepository =>
            _mobileSuitCartRepository ??= new MobileSuitCartRepository(_context);

        public IWeaponCartRepository WeaponCartRepository =>
            _weaponCartRepository ??= new WeaponCartRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            // Pass the username to the DbContext to be audited
            //var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            await _context.SaveChangesAsync();
        }
    }
}
