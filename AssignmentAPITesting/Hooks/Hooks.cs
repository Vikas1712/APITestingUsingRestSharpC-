using System.Threading.Tasks;
using AssignmentAPITesting.Utilities;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using TechTalk.SpecFlow;

namespace AssignmentAPITesting.Hooks
{
    [Binding]
    public sealed class Hooks :RestapiHelper<Task>
    {
        private static ExtentReports _mExtent;
        private static ExtentTest _mFeatureName;
        private static ExtentTest _mScenario;

        [BeforeScenario]
        [System.Obsolete]
        public void BeforeScenario()
        {
            //Create dynamic scenario name
            _mScenario = _mFeatureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
        }

        [BeforeFeature]
        [System.Obsolete]
        public static void BeforeFeature()
        {
            //Create dynamic feature name
            _mFeatureName = _mExtent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
        }

        [AfterStep]
        [System.Obsolete]
        public void InsertReportingSteps()
        {
            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (ScenarioContext.Current.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        _mScenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "When":
                        _mScenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "Then":
                        _mScenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "And":
                        _mScenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                }
            }
            else if (ScenarioContext.Current.TestError != null)
            {
                switch (stepType)
                {
                    case "Given":
                        _mScenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                        break;
                    case "When":
                        _mScenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.InnerException);
                        break;
                    case "Then":
                        _mScenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message);
                        break;
                }
            }
        }
        [BeforeTestRun]
        public static void InitializeReport()
        {
            var htmlReporter = new ExtentHtmlReporter(@"D:\Learning\AssignmentAPITesting\AssignmentAPITesting\Reports\index.html");
            htmlReporter.Config.Theme = Theme.Dark;
            _mExtent = new ExtentReports();
            _mExtent.AttachReporter(htmlReporter);

        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _mExtent.Flush();
        }
    }
}
