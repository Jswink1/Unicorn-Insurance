using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Data.Seeds
{
    public class DeploymentSeed : IEntityTypeConfiguration<Deployment>
    {
        public void Configure(EntityTypeBuilder<Deployment> builder)
        {
            builder.HasData(
                new Deployment
                {
                    Id = 1,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\Collateral.webp",
                    Description = "You got two Mobile Suits with one shot!"
                },
                new Deployment
                {
                    Id = 2,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\ColonyDrop.gif",
                    Description = "Your Mobile Suit got destroyed during the colony drop!"
                },
                new Deployment
                {
                    Id = 3,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\ColonyLaser.webp",
                    Description = "Your Mobile Suit got damaged during the firing of the colony laser!"
                },
                new Deployment
                {
                    Id = 4,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\SwordDuel.webp",
                    Description = "Your Mobile Suit won a sword duel!"
                },
                new Deployment
                {
                    Id = 5,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\DetroyedByBearGuy.webp",
                    Description = "Your Mobile Suit was destroyed by a BearGuy!"
                },
                new Deployment
                {
                    Id = 6,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\EvadedBarrage.gif",
                    Description = "Your Mobile Suit evaded an enemy barrage!"
                },
                new Deployment
                {
                    Id = 7,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\Haro.gif",
                    Description = "You made friends with Haro!"
                },
                new Deployment
                {
                    Id = 8,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\LaunchSuccessful.gif",
                    Description = "Your Mobile Suit launched successfully!"
                },
                new Deployment
                {
                    Id = 9,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\ShotToPieces.gif",
                    Description = "Your Mobile Suit was shot to pieces!"
                },
                new Deployment
                {
                    Id = 10,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\CounteredAttack.gif",
                    Description = "Your Mobile Suit countered an enemy attack!"
                },
                new Deployment
                {
                    Id = 11,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\BlockedAttack.gif",
                    Description = "Your Mobile Suit blocked an enemy attack!"
                },
                new Deployment
                {
                    Id = 12,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\TrippedOnRock.webp",
                    Description = "Your Mobile Suit tripped on a rock!"
                },
                new Deployment
                {
                    Id = 13,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\StompedCoreFighter.webp",
                    Description = "Your Mobile Suit stomped on a Core Fighter!"
                },
                new Deployment
                {
                    Id = 14,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\DestroyedMagellan.gif",
                    Description = "Your Mobile Suit destroyed a Magellan-class Battleship!"
                },
                new Deployment
                {
                    Id = 15,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\ShotByBeamRifle.gif",
                    Description = "Your Mobile Suit was shot by a Beam Rifle!"
                },
                new Deployment
                {
                    Id = 16,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\Sniped.gif",
                    Description = "Your Mobile Suit was sniped!"
                },
                new Deployment
                {
                    Id = 17,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\StabbedByABeamSaber.gif",
                    Description = "Your Mobile Suit was stabbed by a Beam Saber!"
                },
                new Deployment
                {
                    Id = 18,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\ArmChoppedOff.gif",
                    Description = "Your Mobile Suit got its arm chopped off!"
                },
                new Deployment
                {
                    Id = 19,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\PunchedArm.gif",
                    Description = "Your Mobile Suit got punched in the arm!"
                },
                new Deployment
                {
                    Id = 20,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\Ambushed.gif",
                    Description = "Your Mobile Suit was ambushed!"
                },
                new Deployment
                {
                    Id = 21,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\Destroyed.gif",
                    Description = "Your Mobile Suit was destroyed!"
                },
                new Deployment
                {
                    Id = 22,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\NavigatedDebris.gif",
                    Description = "Your Mobile Suit successfully navigated a debris field!"
                },
                new Deployment
                {
                    Id = 23,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\SavedPrincess.webp",
                    Description = "You saved Princess Minerva Zabi from drifting in space!"
                },
                new Deployment
                {
                    Id = 24,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\Punched.webp",
                    Description = "Your Mobile Suit got knocked out!"
                },
                new Deployment
                {
                    Id = 25,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\DestroyedRockets.webp",
                    Description = "Your Mobile Suit destroyed some rockets!"
                },
                new Deployment
                {
                    Id = 26,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\CaughtFire.gif",
                    Description = "Your Mobile Suit caught fire!"
                },
                new Deployment
                {
                    Id = 27,
                    ResultType = "Bad",
                    ImageUrl = "\\images\\deployments\\ChoppedIntoPieces.gif",
                    Description = "Your Mobile Suit was chopped into pieces!"
                },
                new Deployment
                {
                    Id = 28,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\LaunchSuccessful2.gif",
                    Description = "Your Mobile Suit launched successfully!"
                },
                new Deployment
                {
                    Id = 29,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\LaunchSuccessful3.webp",
                    Description = "Your Mobile Suit launched successfully!"
                },
                new Deployment
                {
                    Id = 30,
                    ResultType = "Good",
                    ImageUrl = "\\images\\deployments\\LaunchSuccessful4.gif",
                    Description = "Your Mobile Suit launched successfully!"
                }
            );
        }
    }
}
