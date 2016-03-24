using RestEasyClient.Impl;
using RestEasyClient.Test.Fakes;
using RichardSzalay.MockHttp;
using Should;
using System;
using System.Net.Http;

namespace RestEasyClient.Test
{
    public class CqrsGatewayTests
    {
        private readonly ICqrsGateway<FakeModel> _gateway;
        private readonly HttpClient _httpClient;
        private readonly MockHttpMessageHandler _mockHttp;
        private const string _protocolAndDomain = "http://dummy.com";
        private const string _modelResource = @"/FakeModel/";

        public CqrsGatewayTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_mockHttp);
            _httpClient.BaseAddress = new Uri(_protocolAndDomain);
            _gateway = new CqrsGateway<FakeModel>(_httpClient, new JsonSerializer());
        }

        public void ShouldCallHttpGetWithSpecificRouteWhenFindById()
        {
            // arrange
            var id = 15;
            var expectedGetRoute = @"http://dummy.com/FakeModel/15";
            _mockHttp
                .Expect(expectedGetRoute)
                .Respond("application/json", "");
            // act
            _gateway.FindById(id);
            // assert
            _mockHttp.VerifyNoOutstandingExpectation();
        }

        public void ShouldCallHttpGetWithSpecificRouteWhenSearch()
        {
            // arrange
            var search = new FakeSearch()
            {
                Age = 34,
                Name = "Snake Plisskin",
                SecurityId = Guid.Parse("{239c37a5-9b41-449b-a166-478c28bf6a54}")
            };
            var expectedGetRoute = @"http://dummy.com/FakeModel?Age=34&Name=Snake%20Plisskin&SecurityId=239c37a5-9b41-449b-a166-478c28bf6a54";
            _mockHttp
                .Expect(expectedGetRoute)
                .Respond("application/json", "");
            // act
            _gateway.Search(search);
            // assert
            _mockHttp.VerifyNoOutstandingExpectation();
        }

        public void ShouldSerializeToObjectWhenFindById()
        {
            // arrange
            var id = 15;
            var getRoute = @"http://dummy.com/FakeModel/15";
            _mockHttp
                .When(getRoute)
                .Respond("application/json", "{'Id':15, 'Name':'Rick Grimes', 'Type':'Sheriff'}");
            // act
            var result = _gateway.FindById(id);
            // assert
            result.Id.ShouldEqual(15);
            result.Name.ShouldEqual("Rick Grimes");
            result.Type.ShouldEqual("Sheriff");
        }
    }
}