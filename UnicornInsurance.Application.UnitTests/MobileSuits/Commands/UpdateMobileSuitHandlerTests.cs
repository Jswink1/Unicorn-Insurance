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
using UnicornInsurance.Application.DTOs.MobileSuit;
using UnicornInsurance.Application.DTOs.Weapon;
using UnicornInsurance.Application.Exceptions;
using UnicornInsurance.Application.Features.MobileSuits.Handlers.Commands;
using UnicornInsurance.Application.Features.MobileSuits.Requests.Commands;
using UnicornInsurance.Application.Responses;
using UnicornInsurance.Application.UnitTests.Mocks;

namespace UnicornInsurance.Application.UnitTests.MobileSuits.Commands
{
    public class UpdateMobileSuitHandlerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateMobileSuitHandler _handler;

        public UpdateMobileSuitHandlerTests()
        {
            _mockUnitOfWork = MockUnitOfWork.GetUnitOfWork();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();

            _handler = new UpdateMobileSuitHandler(_mockUnitOfWork.Object, _mapper);
        }

        private static IEnumerable<List<FullMobileSuitDTO>> MobileSuitValues()
        {
            List<FullMobileSuitDTO> mobileSuits = new()
            {
                new FullMobileSuitDTO()
                {
                    Id = 4,
                    Name = "Mediocre Barbatos",
                    Description = "A downgrade from the original",
                    Price = 27m
                },
                new FullMobileSuitDTO()
                {
                    Id = 5,
                    Name = "Sengoku Ashtray Gundam",
                    Description = "A mobile suit you can use as an ashtray",
                    Price = 555555m
                },
            };

            return new[] { mobileSuits };
        }

        private static IEnumerable<List<FullMobileSuitDTO>> MobileSuitWithCustomWeaponValues()
        {
            List<FullMobileSuitDTO> mobileSuits = new()
            {
                new FullMobileSuitDTO()
                {
                    Id = 1,
                    Name = "Pheonix Gundam",
                    Description = "Better than a unicorn",
                    Price = 2m,
                    CustomWeapon = new WeaponDTO
                    {
                        Id = 6,
                        Name = "Super New-Type Destroyer System",
                        Description = "Better than a regular one",
                        Price = 5500m
                    }
                },
                new FullMobileSuitDTO()
                {
                    Id = 2,
                    Name = "Random Gundam",
                    Description = "Very Random",
                    Price = 14m,
                    CustomWeapon = new WeaponDTO
                    {
                        Id = 6,
                        Name = "Random Custom Weapon",
                        Description = "Why",
                        Price = 6000m
                    }
                },
            };

            return new[] { mobileSuits };
        }

        [Test]
        [Order(1)]
        [TestCaseSource(nameof(MobileSuitValues))]
        [TestCaseSource(nameof(MobileSuitWithCustomWeaponValues))]
        public async Task Valid_MobileSuit_Updated(List<FullMobileSuitDTO> mobileSuits)
        {
            foreach (var mobileSuit in mobileSuits)
            {
                var result = await _handler.Handle(new UpdateMobileSuitCommand() { MobileSuitDTO = mobileSuit }, CancellationToken.None);
                result.ShouldBeOfType<BaseCommandResponse>();
                result.Success.ShouldBeTrue();
            }
        }

        [Test]
        [Order(2)]
        [TestCaseSource(nameof(MobileSuitValues))]
        [TestCaseSource(nameof(MobileSuitWithCustomWeaponValues))]
        public async Task UpdateMobileSuit_ShouldThrow_NotFoundException(List<FullMobileSuitDTO> mobileSuits)
        {
            Random rnd = new();

            foreach (var mobileSuit in mobileSuits)
            {
                mobileSuit.Id = rnd.Next(100, 1000000);

                await _handler.Handle(new UpdateMobileSuitCommand() { MobileSuitDTO = mobileSuit }, CancellationToken.None)
                              .ShouldThrowAsync<NotFoundException>();
            }
        }

        [Test]
        [Order(3)]
        [TestCaseSource(nameof(MobileSuitWithCustomWeaponValues))]
        public async Task UpdateMobileSuit_WithNoCustomWeapon_ToHaveCustomWeapon(List<FullMobileSuitDTO> mobileSuits)
        {
            int[] MobileSuitIdWithNoCustomWeapon = new int[] { 4, 5, };
            var count = 0;

            foreach (var mobileSuit in mobileSuits)
            {
                mobileSuit.Id = MobileSuitIdWithNoCustomWeapon[count];
                count++;

                var result = await _handler.Handle(new UpdateMobileSuitCommand() { MobileSuitDTO = mobileSuit }, CancellationToken.None);
                result.ShouldBeOfType<BaseCommandResponse>();
                result.Success.ShouldBeTrue();

                var mobileSuitDetails = await _mockUnitOfWork.Object.MobileSuitRepository.GetFullMobileSuitDetails(mobileSuit.Id);
                mobileSuitDetails.CustomWeapon.ShouldNotBeNull();
                mobileSuitDetails.CustomWeapon.IsCustomWeapon.ShouldBeTrue();
            }
        }

        [Test]
        [Order(4)]
        [TestCaseSource(nameof(MobileSuitValues))]
        public async Task UpdateMobileSuit_WithCustomWeapon_ToHaveNoCustomWeapon(List<FullMobileSuitDTO> mobileSuits)
        {
            int[] MobileSuitIdWithCustomWeapon = new int[] { 1, 2, };
            var count = 0;

            foreach (var mobileSuit in mobileSuits)
            {
                mobileSuit.Id = MobileSuitIdWithCustomWeapon[count];
                count++;

                var result = await _handler.Handle(new UpdateMobileSuitCommand() { MobileSuitDTO = mobileSuit }, CancellationToken.None);
                result.ShouldBeOfType<BaseCommandResponse>();
                result.Success.ShouldBeTrue();

                var mobileSuitDetails = await _mockUnitOfWork.Object.MobileSuitRepository.GetFullMobileSuitDetails(mobileSuit.Id);
                mobileSuitDetails.CustomWeapon.ShouldBeNull();
            }
        }
    }
}
