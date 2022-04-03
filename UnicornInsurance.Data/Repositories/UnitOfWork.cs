using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Constants;
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
        private IOrderHeaderRepository _orderHeaderRepository;
        private IMobileSuitPurchaseRepository _mobileSuitPurchaseRepository;
        private IWeaponPurchaseRepository _weaponPurchaseRepository;        
        private IUserMobileSuitRepository _userMobileSuitRepository;
        private IUserWeaponRepository _userWeaponRepository;
        private IDeploymentRepository _deploymentRepository;

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

        public IOrderHeaderRepository OrderHeaderRepository =>
            _orderHeaderRepository ??= new OrderHeaderRepository(_context);

        public IMobileSuitPurchaseRepository MobileSuitPurchaseRepository =>
            _mobileSuitPurchaseRepository ??= new MobileSuitPurchaseRepository(_context);

        public IWeaponPurchaseRepository WeaponPurchaseRepository =>
            _weaponPurchaseRepository ??= new WeaponPurchaseRepository(_context);

        public IUserMobileSuitRepository UserMobileSuitRepository =>
            _userMobileSuitRepository ??= new UserMobileSuitRepository(_context);

        public IUserWeaponRepository UserWeaponRepository =>
            _userWeaponRepository ??= new UserWeaponRepository(_context);

        public IDeploymentRepository DeploymentRepository =>
            _deploymentRepository ??= new DeploymentRepository(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
