using System.Collections.Generic;
using System.IO;
using AssignmentAPITesting.DataEntities;
using RestSharp;
using RestSharp.Serialization.Json;

namespace AssignmentAPITesting.Utilities
{
    public class RestapiHelper<T>
    {
        public IRestResponse<List<RootObject>> _response;
        public string baseUrl = "https://date.nager.at/api/v2/";

        public RestClient SetUrl(string URL, string endpoint)
        {
            this.baseUrl = URL;
            var url = Path.Combine(baseUrl, endpoint);
            var restClient = new RestClient(url);
            return restClient;
        }

        public IRestResponse GetResponse(RestClient client, RestRequest request)
        {
            return client.Execute(request);
        }

        public List<TPublicHolidays> GetContentList<TPublicHolidays>(IRestResponse response)
        {

            var deserialize = new JsonDeserializer();
            var output= deserialize.Deserialize<List<TPublicHolidays>>(response);
            return output;
        }
    }
}
