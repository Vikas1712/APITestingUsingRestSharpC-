using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AssignmentAPITesting.DataEntities;
using AssignmentAPITesting.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AssignmentAPITesting.Steps
{
    [Binding]
    public class NewYearsDaySteps : RestapiHelper<Task>
    {
        private static readonly RestapiHelper<RootObject> Publicholiday = new RestapiHelper<RootObject>();

        [Then(@"the ResponseStatus is OK")]
        public void ThenTheResponseStatusIsOk()
        {
            _response = GetPublicHolidaySteps.Client.Execute<List<RootObject>>(GetPublicHolidaySteps.Request);
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"the response should give me New Year's day and validate the ""(.*)"" Localname and Name accordingly")]
        public void ThenTheResponseShouldGiveMeNewYearSDayAndValidateTheLocalnameAndNameAccordingly(string date)
        {
           var output = Publicholiday.GetContentList<PublicHolidays>(_response);
            Assert.That(output[0].Date.Equals(date));
            Assert.That(output[0].Name.Equals("New Year's Day"));
            Assert.That(output[0].LocalName.Equals("Nieuwjaarsdag"));
        }

        [Then(@"the response should give me ""(.*)"" and validate is the weekday name ""(.*)"" accordingly")]
        public void ThenTheResponseShouldGiveMeAndValidateIsTheWeekdayNameAccordingly(string localName, string weekday)
        {
            var output = Publicholiday.GetContentList<PublicHolidays>(_response);
            foreach (var item in output)
            {
                if (localName != null)
                    switch (item.LocalName)
                    {
                        case "Hemelvaartsdag"
                            when item.Name.Equals("Ascension Day"):
                            DateTime dateValue = Convert.ToDateTime(item.Date);
                            Assert.That(dateValue.ToString("dddd"), Is.EqualTo(weekday));
                            break;
                    }
            }
        }

    }
}
