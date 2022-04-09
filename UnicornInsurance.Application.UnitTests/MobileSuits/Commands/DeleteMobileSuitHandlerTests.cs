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
using UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.MobileSuits.Commands
{
    class DeleteMobileSuitHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly DeleteMobileSuitHandler _handler;

        public DeleteMobileSuitHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            _handler = new DeleteMobileSuitHandler(_mockUnitOfWork.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task Valid_MobileSuit_Delete(int id)
        {
            await _handler.Handle(new DeleteMobileSuitCommand() { MobileSuitId = id }, CancellationToken.None);

            var mobileSuit = await _mockUnitOfWork.Object.MobileSuitRepository.Get(id);
            mobileSuit.ShouldBeNull();
        }

        [Test]
        [TestCase(23)]
        [TestCase(23246)]
        [TestCase(-2)]
        public async Task DeleteMobileSuit_ShouldThrow_NotFoundException(int id)
        {
            await _handler.Handle(new DeleteMobileSuitCommand() { MobileSuitId = id }, CancellationToken.None)
                          .ShouldThrowAsync<NotFoundException>();
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        public async Task DeleteMobileSuit_ShouldDelete_UserMobileSuits(int id)
        {
            await _handler.Handle(new DeleteMobileSuitCommand() { MobileSuitId = id }, CancellationToken.None);

            var userMobileSuits = await _mockUnitOfWork.Object.UserMobileSuitRepository.GetAll();

            var userMobileSuit = userMobileSuits.Where(i => i.MobileSuitId == id).FirstOrDefault();
            userMobileSuit.ShouldBeNull();
        }
    }
}
