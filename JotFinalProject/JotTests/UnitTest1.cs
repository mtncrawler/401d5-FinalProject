using JotFinalProject.Data;
using JotFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace JotTests
{
    public class UnitTest1
    {
        /// <summary>
        /// Test to get a category name
        /// </summary>
        [Fact]
        public void TestToGetCategoryName()
        {
            Category category = new Category();
            category.Name = "Test Category";

            Assert.Equal("Test Category", category.Name);
        }

        /// <summary>
        /// Test to set a category title
        /// </summary>
        [Fact]
        public void TestToSetCategoryName()
        {
            Category category = new Category();
            category.Name = "Test Cateogry";

            category.Name = "Test Category 2";

            Assert.Equal("Test Category 2", category.Name);
        }

        /// <summary>
        /// Test to get a category id
        /// </summary>
        [Fact]
        public void TestToGetCategoryID()
        {
            Category category = new Category();
            category.ID = 1;

            Assert.Equal(1, category.ID);
        }

        /// <summary>
        /// Test to set a category id
        /// </summary>
        [Fact]
        public void TestToSetCategoryID()
        {
            Category category = new Category();
            category.ID = 1;

            category.ID = 2;

            Assert.Equal(2, category.ID);
        }

        /// <summary>
        /// Test to get an image url
        /// </summary>
        [Fact]
        public void TestToGetImageUrl()
        {
            ImageUploaded image = new ImageUploaded();
            image.ImageUrl = "testimage.png";

            Assert.Equal("testimage.png", image.ImageUrl);
        }

        /// <summary>
        /// Test to set an image url
        /// </summary>
        [Fact]
        public void TestToSetImageUrl()
        {
            ImageUploaded image = new ImageUploaded();
            image.ImageUrl = "testimage.png";

            image.ImageUrl = "testimage2.png";

            Assert.Equal("testimage2.png", image.ImageUrl);
        }

        /// <summary>
        /// Test to get note id
        /// </summary>
        [Fact]
        public void TestToGetNoteID()
        {
            Note note = new Note();
            note.ID = 1;

            Assert.Equal(1, note.ID);
        }

        /// <summary>
        /// Test to set note id
        /// </summary>
        [Fact]
        public void TestToSetNoteID()
        {
            Note note = new Note();
            note.ID = 1;

            note.ID = 2;

            Assert.Equal(2, note.ID);
        }

        /// <summary>
        /// Test to get note text
        /// </summary>
        [Fact]
        public void TestToGetNoteText()
        {
            Note note = new Note();
            note.Text = "Test text string";

            Assert.Equal("Test text string", note.Text);
        }

        /// <summary>
        /// Test to set note text
        /// </summary>
        [Fact]
        public void TestToSetNoteText()
        {
            Note note = new Note();
            note.Text = "Test text string";

            note.Text = "Second text string";

            Assert.Equal("Second text string", note.Text);
        }

        /// <summary>
        /// Test to get note userid
        /// </summary>
        [Fact]
        public void TestToGetNoteUserID()
        {
            Note note = new Note();
            note.UserID = "12";

            Assert.Equal("12", note.UserID);
        }

        /// <summary>
        /// Test to set note userid
        /// </summary>
        [Fact]
        public void TestToSetNoteUserID()
        {
            Note note = new Note();
            note.UserID = "12";

            note.UserID = "22";

            Assert.Equal("22", note.UserID);
        }


        ///// <summary>
        ///// Test to create and read category
        ///// </summary>
        //[Fact]
        //public async void TestCreateCategory()
        //{
        //    DbContextOptions<JotDbContext> options = new DbContextOptionsBuilder<JotDbContext>().UseInMemoryDatabase("AddCategory").Options;

        //    using (JotDbContext context = new JotDbContext(options))
        //    {
        //        Category category = new Category();
        //        category.Name = "TestCategory";

        //        context.Category.Add(category);
        //        context.SaveChanges();

        //        var categoryName = await context.Category.FirstOrDefaultAsync(cat => cat.Name == category.Name);

        //        Assert.Equal("TestCategory", category.Name);
        //    }
        //}

        ///// <summary>
        ///// Test to update category
        ///// </summary>
        //[Fact]
        //public async void TestUpdateCategory()
        //{
        //    DbContextOptions<JotDbContext> options = new DbContextOptionsBuilder<JotDbContext>().UseInMemoryDatabase("GetCategory").Options;

        //    using (JotDbContext context = new JotDbContext(options))
        //    {
        //        Category category = new Category();
        //        category.Name = "Test";
        //        category.Name = "Test2";

        //        context.Update(category);
        //        context.SaveChanges();

        //        var categoryName = await context.Category.FirstOrDefaultAsync(cat => cat.Name == category.Name);

        //        Assert.Equal("Test2", category.Name);
        //    }
        //}



    }
}




