using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;

namespace Avangardum.AvangardumUnityUtilityLib.Tests
{
    [TestFixture]
    public class CoroutineExtensionsTests
    {
        private int _testInt;

        [Test]
        public async Task ToTaskConvertsCoroutineToTask()
        {
            var task = CoroutineHelper.StartCoroutine(TestCoroutine()).ToTask();
            Assert.That(_testInt, Is.EqualTo(1));
            await task;
            Assert.That(_testInt, Is.EqualTo(2));
        }
        
        private IEnumerator TestCoroutine()
        {
            _testInt = 1;
            yield return new WaitForEndOfFrame();
            _testInt = 2;
        }
    }
}