using NUnit.Framework;
using UnityEngine;

namespace Avangardum.AvangardumUnityUtilityLib.Tests
{
    [TestFixture]
    public class TransformExtensionTests
    {
        [Test]
        public void ChildrenReturnsAllChildren()
        {
            var parent = new GameObject("Parent").transform;
            var child1 = new GameObject("Child1").transform;
            child1.parent = parent;
            var child2 = new GameObject("Child2").transform;
            child2.parent = parent;
            Assert.That(parent.GetChildren().Count, Is.EqualTo(2));
        }
    }
}