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
            // Arrange - Make sure the database is cleared before testing
            int result = Item.GetAll().Count;

            // Assert - Check that the database starts empty
            Assert.AreEqual(0, result);
        }
    }
}