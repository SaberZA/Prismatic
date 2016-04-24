using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prismatic.Build.Configuration;
using Prismatic.Build.Runner;
using Prismatic.Build.Serialiser;

namespace Prismatic.Test.Runner
{
    [TestFixture]
    public class TestBuildRunner
    {
        // ReSharper disable InconsistentNaming
        [Test]
        public void Construct_PowerShellRunner()
        {
            //---------------Set up test pack-------------------

            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var powerShellRunner = new PowerShellRunner();
            //---------------Test Result -----------------------
            Assert.IsNotNull(powerShellRunner);
        }

        [Ignore("Still have to configure PowerShell DSC for Linux")] //TODO  24 Apr 2016: Ignored Test - Still have to configure PowerShell DSC for Linux
        [Test]
        public void RunScript_GivenBeforeInstallScript_ShouldExecuteScript()
        {
            //---------------Set up test pack-------------------
            var powerShellRunner = new PowerShellRunner();
            var folderName = "Runner";
            var scriptName = "before_install.ps1";
            var scriptPath = Path.Combine(folderName, scriptName);
            //---------------Assert Precondition----------------
            Assert.IsTrue(File.Exists(scriptPath));
            //---------------Execute Test ----------------------
            var scriptResult = powerShellRunner.RunScript(scriptPath);
            //---------------Test Result -----------------------
            Assert.IsNotNull(scriptResult);
            Assert.AreEqual("Hello World In before_install!", scriptResult.ScriptOutput);
            Assert.IsTrue(scriptResult.IsSuccess);
        }

        [Ignore("Still have to configure PowerShell DSC for Linux")] //TODO  24 Apr 2016: Ignored Test - Still have to configure PowerShell DSC for Linux
        [Test]
        public void RunScript_GivenGivenBuildConfig_ShouldExecuteAllScriptsInDefaultOrder()
        {
            //---------------Set up test pack-------------------
            var powerShellRunner = new PowerShellRunner(@"Runner");
            
            var serialiser = new PrismaticJsonSerialiser();
            var document = GetFile(@"build.json");
            var buildConfig = serialiser.Deserialise<DefaultBuildConfig>(document);
            var defaultBuildIterator = new DefaultBuildIterator(buildConfig);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var scriptResults = powerShellRunner.RunScripts(defaultBuildIterator);
            //---------------Test Result -----------------------
            Assert.IsNotNull(scriptResults);
            Assert.IsTrue(scriptResults.Any());
            Assert.AreEqual("Hello World In before_install!", scriptResults[0].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In install!", scriptResults[1].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In before_script!", scriptResults[2].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In script!", scriptResults[3].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In after_success!", scriptResults[4].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In after_failure!", scriptResults[5].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In before_deploy!", scriptResults[6].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In deploy!", scriptResults[7].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In after_deploy!", scriptResults[8].ScriptOutput.Replace(Environment.NewLine, ""));
            Assert.AreEqual("Hello World In after_script!", scriptResults[9].ScriptOutput.Replace(Environment.NewLine, ""));
        }

        private static string GetFile(string filePath)
        {
            var stringReader = new StreamReader(filePath);
            return stringReader.ReadToEnd();
        }
    }
}
