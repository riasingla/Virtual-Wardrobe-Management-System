using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;
using Virtual_Wardrobe_Management_System.Data_Layer.Repositories;

namespace Virtual_Wardrobe_Management_System.Tests.Data_Layer.Repositories
{
    [TestFixture]
    public class OutfitRepositoryTests
    {
        private Mock<ApplicationDbContext> _mockContext;
        private IOutfitRepository _repository;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<ApplicationDbContext>();
            _repository = new OutfitRepository(_mockContext.Object);
        }

        [Test]
        public void CreateOutfit_ValidOutfit_ShouldAddOutfitToContextAndSaveChanges()
        {
            // Arrange
            var outfit = new Outfit { OutfitId = 1, OutfitName = "Test Outfit" };

            // Act
            _repository.CreateOutfit(outfit);

            // Assert
            _mockContext.Verify(c => c.Outfits.Add(outfit), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void GetOutfits_ShouldReturnAllOutfitsFromContext()
        {
            // Arrange
            var outfits = new List<Outfit>
            {
                new Outfit { OutfitId = 1, OutfitName = "Outfit 1" },
                new Outfit { OutfitId = 2, OutfitName = "Outfit 2" },
                new Outfit { OutfitId = 3, OutfitName = "Outfit 3" }
            };
            var mockOutfits = new Mock<DbSet<Outfit>>();
            mockOutfits.As<IQueryable<Outfit>>().Setup(m => m.Provider).Returns(outfits.AsQueryable().Provider);
            mockOutfits.As<IQueryable<Outfit>>().Setup(m => m.Expression).Returns(outfits.AsQueryable().Expression);
            mockOutfits.As<IQueryable<Outfit>>().Setup(m => m.ElementType).Returns(outfits.AsQueryable().ElementType);
            mockOutfits.As<IQueryable<Outfit>>().Setup(m => m.GetEnumerator()).Returns(outfits.AsQueryable().GetEnumerator());

            _mockContext.Setup(c => c.Outfits).Returns(mockOutfits.Object);

            // Act
            var result = _repository.GetOutfits();

            // Assert
            Assert.That(result, Is.EqualTo(outfits));
        }

        [Test]
        public void GetOutfitById_ExistingId_ShouldReturnMatchingOutfit()
        {
            // Arrange
            var outfitId = 1;
            var outfit = new Outfit { OutfitId = outfitId, OutfitName = "Test Outfit" };
            _mockContext.Setup(c => c.Outfits.FirstOrDefault(It.IsAny<Func<Outfit, bool>>())).Returns(outfit);

            // Act
            var result = _repository.GetOutfitById(outfitId);

            // Assert
            Assert.That(result, Is.EqualTo(outfit));
        }

        [Test]
        public void GetOutfitById_NonExistingId_ShouldThrowArgumentException()
        {
            // Arrange
            var outfitId = 1;
            _mockContext.Setup(c => c.Outfits.FirstOrDefault(It.IsAny<Func<Outfit, bool>>())).Returns((Outfit)null!);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _repository.GetOutfitById(outfitId));
        }

        [Test]
        public void UpdateOutfit_ExistingId_ShouldUpdateOutfitAndSaveChanges()
        {
            // Arrange
            var outfitId = 1;
            var existingOutfit = new Outfit { OutfitId = outfitId, OutfitName = "Existing Outfit" };
            var updatedOutfit = new Outfit { OutfitId = outfitId, OutfitName = "Updated Outfit" };
            _mockContext.Setup(c => c.Outfits.FirstOrDefault(It.IsAny<Func<Outfit, bool>>())).Returns(existingOutfit);

            // Act
            _repository.UpdateOutfit(outfitId, updatedOutfit);
        }
    }
}

