using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prismatic.Build.Runner;

namespace Prismatic.Test.Runner
{
    using NUnit.Framework;

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

        [Test]
        public void RunScript_GivenBeforeInstallScript_ShouldExecuteScript()
        {
            //---------------Set up test pack-------------------
            var powerShellRunner = new PowerShellRunner();
            var folderName = "Runner";
            var scriptName = "testBeforeInstall.ps1";
            var scriptPath = Path.Combine(folderName, scriptName);
            //---------------Assert Precondition----------------
            Assert.IsTrue(File.Exists(scriptPath));
            //---------------Execute Test ----------------------
            var scriptResult = powerShellRunner.RunScript(scriptPath);
            //---------------Test Result -----------------------
            Assert.IsNotNull(scriptResult);
            Assert.AreEqual("Hello World!", scriptResult.ScriptOutput);
            Assert.IsTrue(scriptResult.IsSuccess);
        }
    }
}
