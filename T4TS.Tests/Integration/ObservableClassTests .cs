using ApprovalTests;
using ApprovalTests.Maintenance;
using ApprovalTests.Reporters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4TS.Tests.Models;
using T4TS.Tests.Utils;

namespace T4TS.Tests.Integration
{
    [TestClass]
    [UseReporter(typeof(DiffReporter))]
    public class ObservableClassOutputAppenderTests
    {

        string Build<T>()
        {
            var solution = DTETransformer.BuildDteSolution(typeof(T));
            return Build(solution);
        }
        string Build<T1,T2>()
        {
            var solution = DTETransformer.BuildDteSolution(typeof(T1), typeof(T2));
            return Build(solution);
        }
        string Build(EnvDTE.Solution solution)
        {
            var settings = new Settings
            {
                GenerateObservables = true,
                DefaultCamelCaseMemberNames = true
            };
            var codeTraverser = new CodeTraverser(solution, settings);
            return T4TS.OutputFormatter.GetOutput(codeTraverser.GetAllInterfaces().ToList(), settings);
        }



        [TestMethod]
        public void CanBuildSimpleObservable()
        {
            Approvals.Verify(Build<LocalModel>());
        }

        [TestMethod]
        public void CanBuildObservableParentChild()
        {
            Approvals.Verify(Build<ParentModel, ChildModel>());
        }




        [TestMethod]
        public void EnsureNoAbandonedFiles()
        {
            ApprovalMaintenance.VerifyNoAbandonedFiles();
        }


    }
}
