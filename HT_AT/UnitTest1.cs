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
        public void StatusCodeTest()
        {
            var expectedResult = HttpStatusCode.OK;
            var actualResult = restResponse.StatusCode;

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ResponseHeaderTest()
        {
            var contentHeaders = restResponse.ContentHeaders.ToList();
            var expectedResult = contentHeaders.First()?.Value.ToString();
            var actualResult = "application/json; charset=utf-8";

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ResponseBodyTest()
        {
            var jsonobject = JsonConvert.DeserializeObject<List<User>>(restResponse.Content);
            var expectedResult = 10;
            var actualResult = jsonobject.Count();

            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

    }
}
/*Goals of API Automation Testing
 * Ensuring that various APIs of a software system are working correctly.
Examining the data returned by an API call. For example, the HTTP response code, and HTTP response body to verify the correctness of the API under test against a variety of inputs.
Modifying HTTP request headers, and parameters and asserting results.
Supplying valid and invalid values as parameters of post HTTP requests and validating results.
Calling various HTTP methods like GET/PUT/POST/DELETE on a given API URL.
Verifying that an API is responding correctly on edge cases.
Running tests often and quickly, either manually or through continuous integration triggers (CI) pipelines (Jenkins, Azure DevOps) to ensure product quality.
Reducing debugging effort by finding issues closer to the root cause.
Testing the system earlier in development, even when the UI is not available.
Generate API documentation automatically from API specification.
After understanding the basic goals of API testing, we are in a better position to understand why Selenium should not be used for API testing.*/