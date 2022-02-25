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

        Task Save();
    }
}
