using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;
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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _mockContext = new Mock<ApplicationDbContext>(options);
            _repository = new OutfitRepository(_mockContext.Object);
        }

        [Test]
        public void CreateOutfit_ValidOutfit_ShouldAddOutfitToContextAndSaveChanges()
        {
            // Arrange
            var outfit = new Outfit { OutfitId = 1000, OutfitName = "Test Outfit" };
            var outfitsMock = new Mock<DbSet<Outfit>>();

            _mockContext.Setup(c => c.Outfits).Returns(outfitsMock.Object);

            // Act
            _repository.CreateOutfit(outfit);

            // Assert
            outfitsMock.Verify(o => o.Add(outfit), Times.Once);
            _mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Test]
        public void GetOutfits_ShouldReturnAllOutfitsFromContext()
        {
            // Arrange
            int userId = 123; // Replace with the desired user ID
            var outfits = new List<Outfit>
    {
        new Outfit { OutfitId = 1, OutfitName = "Outfit 1", UserId = userId },
        new Outfit { OutfitId = 2, OutfitName = "Outfit 2", UserId = userId },
        new Outfit { OutfitId = 3, OutfitName = "Outfit 3", UserId = userId }
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

            _mockContext.Setup(c => c.Outfits.FirstOrDefault(It.IsAny<Func<Outfit, bool>>()))
                .Returns<Func<Outfit, bool>>(predicate => new List<Outfit> { outfit }.FirstOrDefault(predicate));

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
    
        
    }
}

