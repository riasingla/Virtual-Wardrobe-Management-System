using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace DataAccessLayer.Tests
{
    [TestFixture]
    public class ClothingRepositoryTests
    {
        private ApplicationDbContext _context;
        private IClothingRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new ClothingRepository(_context);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void AddClothingItem_Should_AddNewItem()
        {
            // Arrange
            var item = new ClothingItem
            {
                Id = 1,
                Type = "T-shirt",
                Colour = "Blue",
                Size = "M",
                ImageUrl = "http://example.com/image.jpg"
            };

            // Act
            _repository.AddClothingItem(item);

            // Assert
            Assert.That(_context.ClothingItems.Count(), Is.EqualTo(1));
            var addedItem = _context.ClothingItems.First();
            Assert.That(addedItem.Type, Is.EqualTo(item.Type));
            Assert.That(addedItem.Colour, Is.EqualTo(item.Colour));
            Assert.That(addedItem.Size, Is.EqualTo(item.Size));
            Assert.That(addedItem.ImageUrl, Is.EqualTo(item.ImageUrl));
        }

        [Test]
        public void GetAllClothingItems_Should_ReturnAllItems()
        {
            // Arrange
            _context.ClothingItems.Add(new ClothingItem { Id = 1, Type = "T-shirt", Colour = "Blue", Size = "M", ImageUrl = "http://example.com/image1.jpg" });
            _context.ClothingItems.Add(new ClothingItem { Id = 2, Type = "Jeans", Colour = "Black", Size = "L", ImageUrl = "http://example.com/image2.jpg" });
            _context.SaveChanges();

            // Act
            var items = _repository.GetAllClothingItems().ToList();

            // Assert
            Assert.That(items.Count, Is.EqualTo(2));
        }

        [Test]
        public void DeleteClothingItem_Should_DeleteExistingItem()
        {
            // Arrange
            var item = new ClothingItem { Id = 1, Type = "T-shirt", Colour = "Blue", Size = "M", ImageUrl = "http://example.com/image.jpg" };
            _context.ClothingItems.Add(item);
            _context.SaveChanges();

            // Act
            _repository.DeleteClothingItem(item.Id);

            // Assert
            Assert.That(_context.ClothingItems.Count(), Is.EqualTo(0));
        }

        [Test]
        public void GetById_Should_ReturnItemWithMatchingId()
        {
            // Arrange
            var item = new ClothingItem { Id = 1, Type = "T-shirt", Colour = "Blue", Size = "M", ImageUrl = "http://example.com/image.jpg" };
            _context.ClothingItems.Add(item);
            _context.SaveChanges();

            // Act
            var retrievedItem = _repository.GetById(item.Id);

            // Assert
            Assert.That(retrievedItem, Is.EqualTo(item));
        }

        [Test]
        public void GetById_Should_ThrowException_WhenItemNotFound()
        {
            // Arrange
            //var itemId = 1;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _repository.GetById(123));

        }
    }
}