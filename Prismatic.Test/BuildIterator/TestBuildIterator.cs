using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prismatic.Build.Configuration;
using Prismatic.Build.Serialiser;

namespace Prismatic.Test.BuildIterator
{
    using NUnit.Framework;

    [TestFixture]
    public class TestBuildIterator
    {
        [Test]
        public void Construct_DefaultBuildIterator()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var defaultBuildIterator = new DefaultBuildIterator(null);
            //---------------Test Result -----------------------
            Assert.IsNotNull(defaultBuildIterator);
        }

        [Test]
        public void IterateBuildConfig_GivenBuildConfigIterator_ShouldReturnDefaultOrderExecution()
        {
            //---------------Set up test pack-------------------
            var serialiser = new PrismaticJsonSerialiser();
            var document = GetFile(@"build.json");
            var buildConfig = serialiser.Deserialise<DefaultBuildConfig>(document);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var defaultBuildIterator = new DefaultBuildIterator(buildConfig);

            var buildItem = defaultBuildIterator.GetNext();
            var executedList = new List<List<string>>();
            do
            {
                executedList.Add(buildItem);

                buildItem = defaultBuildIterator.GetNext();
            } while (buildItem != null);

            //---------------Test Result -----------------------
            Assert.AreEqual(10, executedList.Count);
            Assert.AreEqual("before_install.ps1", executedList[0][0]);
            Assert.AreEqual("install.ps1", executedList[1][0]);
            Assert.AreEqual("before_script.ps1", executedList[2][0]);
            Assert.AreEqual("after_script.ps1", executedList[9][0]);
        }

        [Test]
        public void GetEnumerator_GivenBuildConfigIterator_ShouldReturnDefaultOrderExecution()
        {
            //---------------Set up test pack-------------------
            var serialiser = new PrismaticJsonSerialiser();
            var document = GetFile(@"build.json");
            var buildConfig = serialiser.Deserialise<DefaultBuildConfig>(document);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var defaultBuildIterator = new DefaultBuildIterator(buildConfig);
            var executedList = new List<List<string>>();

            foreach (var buildItem in defaultBuildIterator)
            {
                executedList.Add(buildItem);
            }


            //---------------Test Result -----------------------
            Assert.AreEqual(10, executedList.Count);
            Assert.AreEqual("before_install.ps1", executedList[0][0]);
            Assert.AreEqual("install.ps1", executedList[1][0]);
            Assert.AreEqual("before_script.ps1", executedList[2][0]);
            Assert.AreEqual("after_script.ps1", executedList[9][0]);
        }

        private static string GetFile(string filePath)
        {
            var stringReader = new StreamReader(filePath);
            return stringReader.ReadToEnd();
        }
    }
}
