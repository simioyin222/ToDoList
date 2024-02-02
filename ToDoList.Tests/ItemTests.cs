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

        [TestMethod]
        public void Save_SavesToDatabase_ItemList()
        {
            Item testItem = new Item("Mow the lawn");
            testItem.Save();
            List<Item> result = Item.GetAll();
            List<Item> testList = new List<Item>{testItem};
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void Find_ReturnsCorrectItemFromDatabase_Item()
        {
            Item newItem = new Item("Mow the lawn");
            newItem.Save();
            Item newItem2 = new Item("Wash dishes");
            newItem2.Save();
            Item foundItem = Item.Find(newItem.Id);
            Assert.AreEqual(newItem, foundItem);
        }
    }
}