using Microsoft.VisualStudio.TestTools.UnitTesting;
using ToDoList.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;

namespace ToDoList.Tests
{
    [TestClass]
    public class ItemTests : IDisposable
    {
        public IConfigurationRoot Configuration { get; set; }

        public ItemTests()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            DBConfiguration.ConnectionString = Configuration["ConnectionStrings:TestConnection"];
        }

        public void Dispose()
        {
            Item.ClearAll();
        }

        [TestMethod]
        public void GetAll_DatabaseEmptyAtFirst_0()
        {
            int result = Item.GetAll().Count;
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueIfDescriptionsAreTheSame_Item()
        {
            Item firstItem = new Item("Mow the lawn", 1);
            Item secondItem = new Item("Mow the lawn", 1);
            Assert.AreEqual(firstItem, secondItem);
        }
    }
}