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
    public class TransportboxBllTest
    {
        private ITransportBoxDataFactory transportBoxDataFactory;
        private ITransportboxFactory transportboxFactory;

        [SetUp]
        public void Setup()
        {
            transportBoxDataFactory = new TransportBoxDataFactory();
            transportboxFactory = new TransportboxFactory();
        }

        [Test]
        public void TestGetAllTransportBoxesByPrjGuid()
        {
            string projectGuid = "18500286-ad03-4240-8ec8-ffe1d3a4e77d";
            ITransportboxBll transportboxBll = new TransportboxBll(transportBoxDataFactory, transportboxFactory);

            IEnumerable<ITransportBoxData> transportBoxDatas = transportboxBll.GetAllTransportBoxesByProjectGuid(projectGuid).Result;

            //Prüfen ob die erwartete Anzahl von Transportboxen an dem Projekt angehangen ist
            transportBoxDatas.Should().HaveCount(40);
            //Probeweise erstes Element auf Richtigkeit prüfen
            transportBoxDatas.ElementAt(0).ProjectGuid.Should().Be(projectGuid);
            transportBoxDatas.ElementAt(0).LocationTransport.Should().Be("Transporter, hinten Links");
            transportBoxDatas.ElementAt(0).LocationHome.Should().Be("Zuhause");
            transportBoxDatas.ElementAt(0).Description.Should().Be("Office Equipment 1");
            transportBoxDatas.ElementAt(0).BoxGuid.Should().Be("0d32d002-ee68-45ae-5e1e-70bed56f7c8e");
            transportBoxDatas.ElementAt(0).Number.Should().Be(1);
            transportBoxDatas.ElementAt(0).BoxCategory.Should().Be("Office");
        }

        [Test]
        public void TestGetAllTransportBoxesWithoutPrjGuid()
        {
            Guid guid = Guid.Empty;
            ITransportboxBll transportboxBll = new TransportboxBll(transportBoxDataFactory, transportboxFactory);

            IEnumerable<ITransportBoxData> transportBoxDatas = transportboxBll.GetAllTransportBoxesWithoutPrjGuid().Result;

            //Prüfen ob die erwartete Anzahl von Transportboxen keinem Projekt zugeordnet ist
            transportBoxDatas.Should().HaveCount(102);
            //Probeweise erstes Element auf Richtigkeit prüfen
            transportBoxDatas.ElementAt(0).ProjectGuid.Should().Be(guid);
            transportBoxDatas.ElementAt(0).BoxGuid.Should().Be("ba15fea3-64cf-4f76-5bd4-d1b3f1fafb42");
            transportBoxDatas.ElementAt(0).LocationTransport.Should().Be(null);
            transportBoxDatas.ElementAt(0).LocationHome.Should().Be(null);
            transportBoxDatas.ElementAt(0).LocationDeployment.Should().Be(null);
            transportBoxDatas.ElementAt(0).Description.Should().Be("Tables");
            transportBoxDatas.ElementAt(0).Number.Should().Be(42);
            transportBoxDatas.ElementAt(0).BoxCategory.Should().Be("Compound");
        }

        [Test]
        public void TestGetTransportbox()
        {
            //Guid der erwarteten Transportbox
            Guid transportboxGuid = Guid.Parse("9a6311bd-266c-4454-8c92-b5bde35e3e44");

            ITransportboxBll transportboxBll = new TransportboxBll(transportBoxDataFactory, transportboxFactory);

            ITransportBoxData transportBoxDatas = transportboxBll.GetTransportbox(transportboxGuid).Result;

            //Element auf Richtigkeit prüfen
            transportBoxDatas.ProjectGuid.Should().Be("18500286-ad03-4240-8ec8-ffe1d3a4e77d");
            transportBoxDatas.BoxGuid.Should().Be("9a6311bd-266c-4454-8c92-b5bde35e3e44");
            transportBoxDatas.LocationTransport.Should().Be(null);
            transportBoxDatas.LocationHome.Should().Be(null);
            transportBoxDatas.LocationDeployment.Should().Be(null);
            transportBoxDatas.Description.Should().Be("SG 300, outer tent, registration & assessment/waiting");
            transportBoxDatas.Number.Should().Be(25);
            transportBoxDatas.BoxCategory.Should().Be("Compound");
        }

        [Test]
        public void TestAddAndDeleteTransportbox()
        {
            Guid projectGuid = Guid.Parse("18500286-ad03-4240-8ec8-ffe1d3a4e77d");
            Guid boxGuid = Guid.NewGuid();
            ITransportBoxData expectedTransportBoxData = new TransportBoxData
            {
                ProjectGuid = projectGuid,
                BoxGuid = boxGuid,
                Number = 201,
                Description = "Test",
                LocationDeployment = null,
                LocationHome = null,
                LocationTransport = null,
                BoxCategory = "Compound"
            };

            ITransportboxBll transportboxBll = new TransportboxBll(transportBoxDataFactory, transportboxFactory);
            //TransportBox hinzufügen
            ITransportBoxData transportBoxData = transportboxBll.AddTransportbox(expectedTransportBoxData).Result;
            transportBoxData.Should().BeEquivalentTo(expectedTransportBoxData);
            //TransportBox nachschauen ob die Transportbox in der DB ist
            ITransportBoxData dbTransportbox = transportboxBll.GetTransportbox(boxGuid).Result;
            dbTransportbox.Should().BeEquivalentTo(expectedTransportBoxData);
            //Löschen der angelegten Transportbox
            bool deleteResult = transportboxBll.DeleteTransportbox(boxGuid).Result;
            //Prüfen ob löschen erfolgreich war
            deleteResult.Should().BeTrue();
            //Schauen ob die Transportbox noch da ist, sollte null sein
            dbTransportbox = transportboxBll.GetTransportbox(boxGuid).Result;
            dbTransportbox.Should().Be(null);
        }

        [Test]
        public void TestUpdateAndDeleteTransportbox()
        {
            Guid projectGuid = Guid.Parse("18500286-ad03-4240-8ec8-ffe1d3a4e77d");
            Guid boxGuid = Guid.NewGuid();
            ITransportBoxData addedTransportBoxData = new TransportBoxData
            {
                ProjectGuid = projectGuid,
                BoxGuid = boxGuid,
                Number = 201,
                Description = "Test",
                LocationDeployment = null,
                LocationHome = null,
                LocationTransport = null,
                BoxCategory = "Compound"
            };

            ITransportboxBll transportboxBll = new TransportboxBll(transportBoxDataFactory, transportboxFactory);
            //TransportBox hinzufügen
            ITransportBoxData transportBoxData = transportboxBll.AddTransportbox(addedTransportBoxData).Result;
            transportBoxData.Should().BeEquivalentTo(addedTransportBoxData);
            //TransportBox nachschauen ob die Transportbox in der DB ist
            ITransportBoxData dbTransportbox = transportboxBll.GetTransportbox(boxGuid).Result;
            dbTransportbox.Should().BeEquivalentTo(addedTransportBoxData);

            ITransportBoxData updatedTransportBoxData = new TransportBoxData
            {
                ProjectGuid = projectGuid,
                BoxGuid = boxGuid,
                Number = 201,
                Description = "TestUpdate",
                LocationDeployment = null,
                LocationHome = null,
                LocationTransport = null,
                BoxCategory = "Compound"
            };
            //TransportBox updaten
            bool updateResult = transportboxBll.UpdateTransportbox(updatedTransportBoxData).Result;
            //Prüfen ob updaten erfolgreich war
            updateResult.Should().BeTrue();
            //Nachschauen ob die Transportbox in der DB upgedatet wurde
            dbTransportbox = transportboxBll.GetTransportbox(boxGuid).Result;
            dbTransportbox.Should().BeEquivalentTo(updatedTransportBoxData);
            //Löschen der angelegten Transportbox
            bool deleteResult = transportboxBll.DeleteTransportbox(boxGuid).Result;
            //Prüfen ob löschen erfolgreich war
            deleteResult.Should().BeTrue();
        }

        [Test]
        public void TestDeleteTransportboxShouldBeFalse()
        {
            Guid guid = Guid.Parse("18500286-ad03-4240-8ec8-ffe1d3a4e77d");
            ITransportboxBll transportboxBll = new TransportboxBll(transportBoxDataFactory, transportboxFactory);

            //Versuchen Transportbox zu löschen die nicht in der DB ist
            bool deleteResult = transportboxBll.DeleteTransportbox(guid).Result;
            //Prüfen ob löschen nicht erfolgreich war
            deleteResult.Should().BeFalse();
        }
    }
}