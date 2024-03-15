using FluentAssertions;
using Logisitcs.BLL.Factories;
using Logisitcs.BLL.Interfaces;
using Logisitcs.BLL.Interfaces.Factories;
using Logisitcs.BLL.Interfaces.ModelInterfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logisitcs.BLL.Tests
{
    [TestFixture]
    public class ArticleBllTest
    {
        private IArticleAndBoxAssignmentFactory articleAndBoxAssignmentFactory;
        private IArticleDataFactory articleDataFactory;

        [SetUp]
        public void Setup()
        {
            articleAndBoxAssignmentFactory = new ArticleAndBoxAssignmentFactory();
            articleDataFactory = new ArticleDataFactory();
        }

        [Test]
        public void Test_Get_All_Articles()
        {
            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);

            IEnumerable<IArticleData> result = articleBll.GetAllArticles().Result;

            // 1887 is the number of articles in the database
            result.Should().HaveCount(1887);
            // Check if the first article is correct in the list
            result.ElementAt(0).ArticleGuid.Should().Be("a9abd786-d1a5-4e47-711c-24f7f1416b44");
            result.ElementAt(0).Description.Should().Be("Aluminium box, 80x60x63 cm");
            result.ElementAt(0).Unit.Should().Be("pcs");
        }

        [Test]
        public void Test_Get_All_ArticlesByBoxId()
        {
            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);

            IEnumerable<IArticleData> result = articleBll.GetAllArticlesByBoxId("00d9c7f2-fb06-48ce-522e-563381364ba6").Result;

            // 91 is the number of articles in the database that belong to the box with the given id
            result.Should().HaveCount(91);
            // Check if the first article is the expected article in the list
            result.ElementAt(0).ArticleGuid.Should().Be("a9abd786-d1a5-4e47-711c-24f7f1416b44");
            result.ElementAt(0).ArticleBoxAssignment.Should().Be("16e4e52a-9e4d-45c4-bc4d-4748eee65f2b");
            result.ElementAt(0).BoxGuid.Should().Be("00d9c7f2-fb06-48ce-522e-563381364ba6");
            result.ElementAt(0).Position.Should().Be(1.0);
            result.ElementAt(0).Status.Should().Be("None");
            result.ElementAt(0).Quantity.Should().Be(1);
            result.ElementAt(0).ExpiryDate.Should().Be(null);
        }

        [Test]
        public void Test_Get_Article()
        {
            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);

            IArticleData result = articleBll.GetArticle("00d9c7f2-fb06-48ce-522e-563381364ba6", "16e4e52a-9e4d-45c4-bc4d-4748eee65f2b").Result;
            // Check if the article is the expected article
            result.ArticleGuid.Should().Be("a9abd786-d1a5-4e47-711c-24f7f1416b44");
            result.ArticleBoxAssignment.Should().Be("16e4e52a-9e4d-45c4-bc4d-4748eee65f2b");
            result.BoxGuid.Should().Be("00d9c7f2-fb06-48ce-522e-563381364ba6");
            result.Position.Should().Be(1.0);
            result.Status.Should().Be("None");
            result.Quantity.Should().Be(1);
            result.ExpiryDate.Should().Be(null);
        }

        [Test]
        public void Test_Add_And_Delete_ArticleAssignment()
        {
            Guid assignmentGuid = Guid.Parse("00d9c7f2-fb06-0000-522e-563381364ba6");
            DateTime dateTime = new(2025, 12, 31);
            Guid boxGuid = Guid.Parse("00d9c7f2-fb06-48ce-522e-563381364ba6");
            // Create an article
            IArticleData articleData = new ArticleData()
            {
                ArticleGuid = "a9abd786-d1a5-4e47-711c-24f7f1416b44",
                ArticleName = null,
                Description = "Aluminium box, 80x60x63 cm",
                Gtin = null,
                Unit = "pcs",
                Position = 1.0,
                Status = "Defect",
                Quantity = 1,
                BoxGuid = Guid.Parse("00d9c7f2-fb06-48ce-522e-563381364ba6"),
                ArticleBoxAssignment = assignmentGuid,
                ExpiryDate = dateTime,
            };

            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);

            // Add the article
            IArticleData result = articleBll.AddArticle(articleData).Result;
            result.Should().BeEquivalentTo(articleData);
            // Check if the article was added
            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;
            result.Should().BeEquivalentTo(articleData);
            // Delete the article and check if delete was successful
            var deleteResult = articleBll.DeleteArticle(assignmentGuid).Result;
            deleteResult.Should().BeTrue();
            // Check if the article was deleted
            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;

            result.Should().Be(null);
        }

        [Test]
        public void Test_Add_And_Update_And_Delete_ArticleAssignment()
        {
            Guid assignmentGuid = Guid.Parse("00d9c7f2-fb06-0000-522e-563381364ba6");
            DateTime dateTime = new(2025, 12, 31);
            Guid boxGuid = Guid.Parse("00d9c7f2-fb06-48ce-522e-563381364ba6");
            // Create an article
            IArticleData articleData = new ArticleData()
            {
                ArticleGuid = "a9abd786-d1a5-4e47-711c-24f7f1416b44",
                ArticleName = null,
                Description = "Aluminium box, 80x60x63 cm",
                Gtin = null,
                Unit = "pcs",
                Position = 1.0,
                Status = "Defect",
                Quantity = 1,
                BoxGuid = Guid.Parse("00d9c7f2-fb06-48ce-522e-563381364ba6"),
                ArticleBoxAssignment = assignmentGuid,
                ExpiryDate = dateTime,
            };

            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);
            // Add the article
            IArticleData result = articleBll.AddArticle(articleData).Result;
            result.Should().BeEquivalentTo(articleData);
            // Check if the article was added
            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;
            result.Should().BeEquivalentTo(articleData);
            // Create an updated article
            IArticleData updateArticleData = new ArticleData()
            {
                ArticleGuid = "a9abd786-d1a5-4e47-711c-24f7f1416b44",
                ArticleName = null,
                Description = "Aluminium box, 80x60x63 cm",
                Gtin = null,
                Unit = "pcs",
                Position = 1.0,
                Status = "Defect",
                Quantity = 1,
                BoxGuid = Guid.Parse("00d9c7f2-fb06-48ce-522e-563381364ba6"),
                ArticleBoxAssignment = assignmentGuid,
                ExpiryDate = dateTime,
            };
            // Update the article and check if update was successful
            bool updateResult = articleBll.UpdateArticle(updateArticleData).Result;
            updateResult.Should().BeTrue();
            // Check if the article was updated
            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;
            result.Should().BeEquivalentTo(updateArticleData);
            // Delete the article and check if delete was successful
            var deleteResult = articleBll.DeleteArticle(assignmentGuid).Result;
            deleteResult.Should().BeTrue();
            // Check if the article was deleted
            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;
            result.Should().Be(null);
        }
        // Test if the delete method returns false if the article does not exist
        [Test]
        public void Test_Delete_Article_Should_Be_False()
        {
            Guid assignmentGuid = Guid.Parse("00d9c7f2-fb06-0000-0000-563381364ba6");

            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);

            var deleteResult = articleBll.DeleteArticle(assignmentGuid).Result;
            deleteResult.Should().BeFalse();
        }
    }
}