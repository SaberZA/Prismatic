using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Prismatic.Build.Configuration;
using Prismatic.Build.Serialiser;
using YamlDotNet.Serialization;

namespace Prismatic.Test.Serialiser
{
    [TestFixture]
    public class TestBuildSerialiser
    {
        [Test]
        public void Construct_PrismaticSerialiser()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var serialiser = new PrismaticDefaultSerialiser();
            //---------------Test Result -----------------------
            Assert.IsNotNull(serialiser);
        }

        [Test]
        public void Deserialise_GivenJsonFile_ShouldReturnOrderObject()
        {
            //---------------Set up test pack-------------------
            var serialiser = new PrismaticJsonSerialiser();
            var stringReader = new StreamReader(@"test.json");
            var document = stringReader.ReadToEnd();
            //---------------Assert Precondition----------------
            Assert.IsTrue(document.Length > 0);
            //---------------Execute Test ----------------------
            var order = serialiser.Deserialise<Order>(new StringReader(document));
            //---------------Test Result -----------------------
            Assert.IsNotNull(order);
            Assert.AreEqual("Oz-Ware Purchase Invoice", order.Receipt);
            Assert.AreEqual(2, order.Items.Count);
        }

        [Test]
        public void Deserialise_GivenBuildJsonFile_ShouldReturnBuildObject()
        {
            //---------------Set up test pack-------------------
            var serialiser = new PrismaticJsonSerialiser();
            var document = GetFile(@"build.json");
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var buildConfig = serialiser.Deserialise<DefaultBuildConfig>(document);
            //---------------Test Result -----------------------
            Assert.IsNotNull(buildConfig);
            Assert.AreEqual("csharp", buildConfig.Language);
            Assert.AreEqual("Prismatic.sln", buildConfig.Solution);
        }

        private static string GetFile(string filePath)
        {
            var stringReader = new StreamReader(filePath);
            return stringReader.ReadToEnd();
        }

        
    }

    public class Order
    {
        public string Receipt { get; set; }
        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItem> Items { get; set; }

        [YamlMember(Alias = "bill-to")]
        public Address BillTo { get; set; }

        [YamlMember(Alias = "ship-to")]
        public Address ShipTo { get; set; }

        public string SpecialDelivery { get; set; }
    }

    public class Customer
    {
        public string Given { get; set; }
        public string Family { get; set; }
    }

    public class OrderItem
    {
        [YamlMember(Alias = "part_no")]
        public string PartNo { get; set; }
        public string Descrip { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
