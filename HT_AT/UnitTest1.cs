using HT_AT.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json.Nodes;

namespace HT_AT
{
    public class Tests
    {
        protected RestRequest restRequest;
        protected RestClient restClient;
        protected RestResponse restResponse;
 

        [SetUp]
        public async Task Setup()
        {
            restClient = new RestClient(@"https://jsonplaceholder.typicode.com/");
            restRequest = new RestRequest("users",Method.Get);
            restResponse = await restClient.ExecuteAsync(restRequest);
        }


        [Test]
        public void statusCodeTest()
        {
            var expectedResult = restResponse.StatusCode;
            var actualResult = HttpStatusCode.OK;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void responseHeaderTest()
        {
            var contentHeaders = restResponse.ContentHeaders.ToList();
            var expectedResult = contentHeaders.First()?.Value.ToString();
            var actualResult = "application/json; charset=utf-8";

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void responseBodyTest()
        {
            var jsonobject = JsonConvert.DeserializeObject<List<User>>(restResponse.Content);
            var expectedResult = 10;
            var actualResult = jsonobject.Count();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

    }
}