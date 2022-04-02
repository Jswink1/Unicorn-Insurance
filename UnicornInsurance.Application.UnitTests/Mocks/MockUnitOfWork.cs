using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Application.Contracts.Data;

namespace UnicornInsurance.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWork()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockWeaponRepo = MockWeaponRepository.GetWeaponRepository();
            var mockMobileSuitRepo = MockMobileSuitRepository.GetMobileSuitRepository();
            var mockWeaponCartRepo = MockWeaponCartRepository.GetWeaponCartRepository();
            var mockMobileSuitCartRepo = MockMobileSuitCartRepository.GetMobileSuitCartRepository();
            var mockOrderHeaderRepo = MockOrderHeaderRepository.GetOrderHeaderRepository();
            var mockWeaponPurchaseRepo = MockWeaponPurchaseRepository.GetWeaponPurchaseRepository();
            var mockMobileSuitPurchaseRepo = MockMobileSuitPurchaseRepository.GetMobileSuitPurchaseRepository();
            var mockUserWeaponRepo = MockUserWeaponRepository.GetUserWeaponRepository();
            var mockUserMobileSuitRepo = MockUserMobileSuitRepository.GetUserMobileSuitRepository();
            var mockDeploymentRepo = MockDeploymentRepository.GetDeploymentRepository();

            mockUnitOfWork.Setup(r => r.WeaponRepository).Returns(mockWeaponRepo.Object);
            mockUnitOfWork.Setup(r => r.MobileSuitRepository).Returns(mockMobileSuitRepo.Object);
            mockUnitOfWork.Setup(r => r.WeaponCartRepository).Returns(mockWeaponCartRepo.Object);
            mockUnitOfWork.Setup(r => r.MobileSuitCartRepository).Returns(mockMobileSuitCartRepo.Object);
            mockUnitOfWork.Setup(r => r.OrderHeaderRepository).Returns(mockOrderHeaderRepo.Object);
            mockUnitOfWork.Setup(r => r.WeaponPurchaseRepository).Returns(mockWeaponPurchaseRepo.Object);
            mockUnitOfWork.Setup(r => r.MobileSuitPurchaseRepository).Returns(mockMobileSuitPurchaseRepo.Object);
            mockUnitOfWork.Setup(r => r.UserWeaponRepository).Returns(mockUserWeaponRepo.Object);
            mockUnitOfWork.Setup(r => r.UserMobileSuitRepository).Returns(mockUserMobileSuitRepo.Object);
            mockUnitOfWork.Setup(r => r.DeploymentRepository).Returns(mockDeploymentRepo.Object);

            return mockUnitOfWork;
        }
    }
}
