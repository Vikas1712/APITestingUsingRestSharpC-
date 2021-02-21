using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AssignmentAPITesting.DataEntities;
using AssignmentAPITesting.Utilities;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
namespace AssignmentAPITesting.Steps
{
    [Binding]
    public class GetPublicHolidaySteps : RestapiHelper<Task> 
    {
        public readonly string GetUrl = "https://date.nager.at/api/v2/";
        public static RestClient Client;
        public static RestRequest Request;
        public static RestapiHelper<RootObject> Publicholiday = new RestapiHelper<RootObject>();

        [Given(@"I have the end point of the nager api services")]
        public void GivenIHaveTheEndPointOfTheNagerApiServices()
        {
            Client = new RestClient(GetUrl);
        }


        [When(@"I make a request call with ""(.*)"" and ""(.*)""")]
        public void WhenIMakeARequestCallWithAnd(int year, string countrycode)
        { 
            Request = new RestRequest("PublicHolidays/{Year}/{CountryCode}", Method.GET);
            Request.AddUrlSegment("Year", year).AddUrlSegment("CountryCode", countrycode);
        }

        [Then(@"the ResponseStatus is Completed")]
        public void ThenTheResponseStatusIsCompleted()
        {
            _response = Client.Execute<List<RootObject>>(Request);
            Assert.AreEqual(_response.ResponseStatus.ToString(), ResponseStatus.Completed.ToString());
            Assert.That(_response.StatusCode,Is.EqualTo(HttpStatusCode.OK));
        }

        [Then(@"the response should give me national holiday results")]
        public void ThenTheResponseShouldGiveMeNationalHolidayResults(Table table)
        {
            var output = Publicholiday.GetContentList<PublicHolidays>(_response);
            PublicHolidays holidays =table.CreateInstance<PublicHolidays>();
            foreach (var result in output)
            {
                switch (result.LocalName)
                {
                    case "Nieuwjaarsdag" 
                        when result.Name.Equals("New Year's Day"):
                        Assert.That(result.Name.Equals(holidays.Name));
                        Assert.That(result.LocalName.Equals(holidays.LocalName));
                        Assert.That(result.CountryCode.Equals(holidays.CountryCode));
                        Assert.That(result.Type.Equals(holidays.Type));
                        Assert.That(result.Date.Equals(holidays.Date));
                        Assert.That(result.LaunchYear.Equals(holidays.LaunchYear));
                        break;
                }
            }
        }

        [Then(@"the response should give me ""(.*)"" holiday and type ""(.*)"" results")]
        public void ThenTheResponseShouldGiveMeHolidayAndTypeResults(string holidayName, string type)
        {
            var output = Publicholiday.GetContentList<PublicHolidays>(_response);
            foreach (var item in output)
            {
                if (holidayName != null)
                    switch (item.LocalName)
                    {
                        case "Bevrijdingsdag"
                            when item.Name.Equals("Liberation Day"):
                            Assert.That(item.LaunchYear,Is.EqualTo(1945));
                            Assert.That(item.Type,Is.EqualTo(type));
                            break;
                    }
            }
        }

    }
}
