using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp1.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private Project _project = new Project();

        [TestMethod]
        public void TestQA()
        {
            var quValues = QualityAttributeView.DefaultData();
            quValues[0].MyValue = 4;
            quValues[1].MyValue = 1;
            quValues[2].MyValue = 2;
            quValues[3].MyValue = 4;
            quValues[4].MyValue = 3;
            quValues[5].MyValue = 4;
            quValues[6].MyValue = 2;
            quValues[7].MyValue = 4;
            quValues[8].MyValue = 4;
            var myQa = quValues.Sum(u => u.MyQu);
            Assert.AreEqual(3.05, myQa, 0.01);
        }

        [TestMethod]
        public void TestSlave()
        {
            var slaves = new List<Slave>();
            slaves.Add(Slave.Create(_project.Id, "Programa", 3000, 80));
            slaves.Add(Slave.Create(_project.Id, "Tiran", 5000, 33));
            slaves.Add(Slave.Create(_project.Id, "Tech", 2500, 45));
            var salary = slaves.Sum(u => u.TotalPayment);
            Assert.AreEqual(24643.35, salary, 0.01);
        }

        [TestMethod]
        public void TestMat()
        {
            var mats = new List<Material>();
            mats.Add(Material.Create(_project.Id, "бумага", 40, 4));
            mats.Add(Material.Create(_project.Id, "чернила", 200, 2));
            var price = mats.Sum(u => u.Total);
            Assert.AreEqual(560, price, 0.01);
        }

        [TestMethod]
        public void TestAmo()
        {
            var equips = new List<Equip>();
            equips.Add(Equip.Create(_project.Id, "Компьютер", 15000, 8, 60, 0.2, 0.4, 0.05));
            equips.Add(Equip.Create(_project.Id, "Ноутбук", 9000, 4, 45, 0.2, 0.2, 0.05));
            var amoKof = equips.Sum(u => u.TotalAmo);
            Assert.AreEqual(162, amoKof, 0.01);
        }

        [TestMethod]
        public void TestEco()
        {
            _project.CapCost = 33414.45;
            _project.ImpCost = 23431.4;
            var otherProject = new Project();
            otherProject.CapCost = 50000;
            otherProject.ImpCost = 34000;
            _project.ProjectTechLevel = 1.6;
            var myOneCost = _project.OneCost;
            var otherOneCost = otherProject.OneCost;
            var ecoEffect = otherOneCost * _project.ProjectTechLevel - myOneCost;
            var ecoTime = otherOneCost / ecoEffect;
            var actualEcoEffect = 1.0 / ecoTime;
            Assert.AreEqual(0.93, actualEcoEffect, 0.01);
        }
    }
}
