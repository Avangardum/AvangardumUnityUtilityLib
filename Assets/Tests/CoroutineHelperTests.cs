using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Avangardum.AvangardumUnityUtilityLib.Tests
{
    [TestFixture]
    public class CoroutineHelperTests
    {
        private int _testInt;

        [SetUp]
        public void Setup()
        {
            var coroutineRunner = Object.FindObjectOfType<CoroutineHelper.CoroutineRunner>();
            if (coroutineRunner != null) coroutineRunner.StopAllCoroutines();
            
            _testInt = 0;
        }

        [UnityTest]
        public IEnumerator TestCoroutineRuns()
        {
            CoroutineHelper.StartCoroutine(TestCoroutine());
            Assert.That(_testInt, Is.EqualTo(1));
            yield return new WaitForEndOfFrame();
            Assert.That(_testInt, Is.EqualTo(2));
        }

        [UnityTest]
        public IEnumerator TestCoroutineStops()
        {
            var coroutine = CoroutineHelper.StartCoroutine(TestCoroutine());
            Assert.That(_testInt, Is.EqualTo(1));
            CoroutineHelper.StopCoroutine(coroutine);
            yield return new WaitForEndOfFrame();
            Assert.That(_testInt, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator InvokeRunsAfterDelay()
        {
            CoroutineHelper.Invoke(() => _testInt = 1, 0.1f);
            Assert.That(_testInt, Is.EqualTo(0));
            yield return new WaitForSeconds(0.12f);
            Assert.That(_testInt, Is.EqualTo(1));
        }

        [UnityTest]
        public IEnumerator InvokeRepeatingRepeats()
        {
            CoroutineHelper.InvokeRepeating(() => _testInt++, 0.1f, 0.1f);
            Assert.That(_testInt, Is.EqualTo(0));
            yield return new WaitForSeconds(0.24f);
            Assert.That(_testInt, Is.EqualTo(2));
        }

        [UnityTest]
        public IEnumerator InvokeStops()
        {
            var testInvoke = CoroutineHelper.Invoke(() => _testInt++, 0.1f);
            var testInvokeRepeating = CoroutineHelper.InvokeRepeating(() => _testInt++, 0.1f, 0.1f);
            Assert.That(_testInt, Is.EqualTo(0));
            CoroutineHelper.StopCoroutine(testInvoke);
            CoroutineHelper.StopCoroutine(testInvokeRepeating);
            yield return new WaitForSeconds(0.24f);
            Assert.That(_testInt, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator WorksAfterSceneReload()
        {
            yield return TestCoroutineRuns();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            yield return TestCoroutineRuns();
        }

        private IEnumerator TestCoroutine()
        {
            _testInt = 1;
            yield return new WaitForEndOfFrame();
            _testInt = 2;
        }
    }
}
