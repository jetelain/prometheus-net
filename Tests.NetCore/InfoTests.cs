using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Prometheus.Tests
{
    [TestClass]
    public sealed class InfoTests
    {
        [TestMethod]
        public void CreateInfo_Labels()
        {
            var registry = Metrics.NewCustomRegistry();
            registry.SetStaticLabels(new Dictionary<string, string>() { { "static1", "staticvalue" } });
            var factory = Metrics.WithCustomRegistry(registry);

            var info = factory.CreateInfo("name", "help", new[] { "label1", "label2" }, new InfoConfiguration() { LabelNames = new[] { "ignored" } });
            Assert.IsNotNull(info);
            CollectionAssert.AreEqual(new[] { "label1", "label2" }, info.FlattenedLabelNames.ToArray());
        }

        [TestMethod]
        public void CreateInfo_LabelsAndConfiguration()
        {
            var registry = Metrics.NewCustomRegistry();
            registry.SetStaticLabels(new Dictionary<string, string>() { { "static1", "staticvalue" } });
            var factory = Metrics.WithCustomRegistry(registry);

            var info = factory.CreateInfo("name", "help", new[] { "label1", "label2" }, new InfoConfiguration() { LabelNames = new[] { "ignored" } });
            Assert.IsNotNull(info);
            CollectionAssert.AreEqual(new[] { "label1", "label2" }, info.FlattenedLabelNames.ToArray());
        }

        [TestMethod]
        public void CreateInfo_LabelsAndConfiguration_UseStaticLabels()
        {
            var registry = Metrics.NewCustomRegistry();
            registry.SetStaticLabels(new Dictionary<string, string>() { { "static1", "staticvalue" } });
            var factory = Metrics.WithCustomRegistry(registry);

            var info = factory.CreateInfo("name", "help", new[] { "label1", "label2" }, new InfoConfiguration() { LabelNames = new[] { "ignored" }, UseStaticLabels = true });
            Assert.IsNotNull(info);
            CollectionAssert.AreEqual(new[] { "label1", "label2", "static1" }, info.FlattenedLabelNames.ToArray());
        }

        [TestMethod]
        public void CreateInfo_Configuration()
        {
            var registry = Metrics.NewCustomRegistry();
            registry.SetStaticLabels(new Dictionary<string, string>() { { "static1", "staticvalue" } });
            var factory = Metrics.WithCustomRegistry(registry);

            var info = factory.CreateInfo("name", "help", new InfoConfiguration() { LabelNames = new[] { "label1", "label2" } });
            Assert.IsNotNull(info);
            CollectionAssert.AreEqual(new[] { "label1", "label2" }, info.FlattenedLabelNames.ToArray());
        }

        [TestMethod]
        public void CreateInfo_Configuration_UseStaticLabels()
        {
            var registry = Metrics.NewCustomRegistry();
            registry.SetStaticLabels(new Dictionary<string, string>() { { "static1", "staticvalue" } });
            var factory = Metrics.WithCustomRegistry(registry);

            var info = factory.CreateInfo("name", "help", new InfoConfiguration() { LabelNames = new[] { "label1", "label2" }, UseStaticLabels = true });
            Assert.IsNotNull(info);
            CollectionAssert.AreEqual(new[] { "label1", "label2", "static1" }, info.FlattenedLabelNames.ToArray());
        }

        [TestMethod]
        public void CreateChild()
        {
            var registry = Metrics.NewCustomRegistry();
            var factory = Metrics.WithCustomRegistry(registry);

            var info = factory.CreateInfo("name", "help", new InfoConfiguration() { LabelNames = new[] { "label1", "label2" }, UseStaticLabels = true });
            var child = info.WithLabels("value1", "value2");

            Assert.IsNotNull(child);
            CollectionAssert.AreEqual(new[] { "label1", "label2" }, child.FlattenedLabels.Names.ToArray());
            CollectionAssert.AreEqual(new[] { "value1", "value2" }, child.FlattenedLabels.Values.ToArray());
        }
    }
}
