using AutoMapper;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.Weapons.Handlers.Commands;
using UnicornInsurance.Application.Features.Weapons.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.Weapons.Commands
{
    public class DeleteWeaponHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteWeaponHandler _handler;

        public DeleteWeaponHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new DeleteWeaponHandler(_mockUnitOfWork.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task Valid_Weapon_Delete(int id)
        {
            await _handler.Handle(new DeleteWeaponCommand() { WeaponId = id }, CancellationToken.None);

            var weapon = await _mockUnitOfWork.Object.WeaponRepository.Get(id);
            weapon.ShouldBeNull();
        }

        [Test]
        [TestCase(23)]
        [TestCase(23246)]
        [TestCase(-2)]
        public async Task DeleteWeapon_ShouldThrow_NotFoundException(int id)
        {
            await _handler.Handle(new DeleteWeaponCommand() { WeaponId = id }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }

        [Test]
        [TestCase(6)]
        public async Task DeleteWeapon_ShouldThrow_DeleteCustomWeaponException(int id)
        {
            await _handler.Handle(new DeleteWeaponCommand() { WeaponId = id }, CancellationToken.None)
                          .ShouldThrowAsync<DeleteCustomWeaponException>();
        }
    }
}
