using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicornInsurance.Application.Contracts.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IWeaponRepository WeaponRepository { get; }
        IMobileSuitRepository MobileSuitRepository { get; }
        IMobileSuitCartRepository MobileSuitCartRepository { get; }
        IWeaponCartRepository WeaponCartRepository { get; }
        IOrderHeaderRepository OrderHeaderRepository { get; }
        IMobileSuitPurchaseRepository MobileSuitPurchaseRepository { get; }
        IWeaponPurchaseRepository WeaponPurchaseRepository { get; }
        IUserMobileSuitRepository UserMobileSuitRepository { get; }
        IUserWeaponRepository UserWeaponRepository { get; }
        IDeploymentRepository DeploymentRepository { get; }

        Task Save();
    }
}
