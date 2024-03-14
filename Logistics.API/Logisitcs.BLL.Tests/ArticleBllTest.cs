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

            result.Should().HaveCount(1887);
            result.ElementAt(0).ArticleGuid.Should().Be("a9abd786-d1a5-4e47-711c-24f7f1416b44");
            result.ElementAt(0).Description.Should().Be("Aluminium box, 80x60x63 cm");
            result.ElementAt(0).Unit.Should().Be("pcs");
        }

        [Test]
        public void Test_Get_All_ArticlesByBoxId()
        {
            IArticleBll articleBll = new ArticleBll(articleAndBoxAssignmentFactory, articleDataFactory);

            IEnumerable<IArticleData> result = articleBll.GetAllArticlesByBoxId("00d9c7f2-fb06-48ce-522e-563381364ba6").Result;

            result.Should().HaveCount(91);
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

            IArticleData result = articleBll.AddArticle(articleData).Result;
            result.Should().BeEquivalentTo(articleData);

            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;

            result.Should().BeEquivalentTo(articleData);

            var deleteResult = articleBll.DeleteArticle(assignmentGuid).Result;
            deleteResult.Should().BeTrue();

            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;

            result.Should().Be(null);
        }

        [Test]
        public void Test_Add_And_Update_And_Delete_ArticleAssignment()
        {
            Guid assignmentGuid = Guid.Parse("00d9c7f2-fb06-0000-522e-563381364ba6");
            DateTime dateTime = new(2025, 12, 31);
            Guid boxGuid = Guid.Parse("00d9c7f2-fb06-48ce-522e-563381364ba6");
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

            IArticleData result = articleBll.AddArticle(articleData).Result;
            result.Should().BeEquivalentTo(articleData);

            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;

            result.Should().BeEquivalentTo(articleData);

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

            bool updateResult = articleBll.UpdateArticle(updateArticleData).Result;
            updateResult.Should().BeTrue();

            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;
            result.Should().BeEquivalentTo(updateArticleData);

            var deleteResult = articleBll.DeleteArticle(assignmentGuid).Result;
            deleteResult.Should().BeTrue();

            result = articleBll.GetArticle(boxGuid.ToString(), assignmentGuid.ToString()).Result;
            result.Should().Be(null);
        }
    }
}