using RestEasyClient.Impl;
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

        public CqrsGatewayTests()
        {
            _mockHttp = new MockHttpMessageHandler();
            _httpClient = new HttpClient(_mockHttp);
            _httpClient.BaseAddress = new Uri(_protocolAndDomain);
            _gateway = new CqrsGateway<FakeModel>(_httpClient);
        }

        public void ShouldCallHttpGetWithSpecificRouteWhenFindById()
        {
            // arrange
            var id = 15;
            var expectedGetRoute = _protocolAndDomain + @"/" + "FakeModel" + @"/" + id;
            _mockHttp
                .Expect(expectedGetRoute)
                .Respond("application/json", "");
            // act
            _gateway.FindById(id);
            // assert
            _mockHttp.VerifyNoOutstandingExpectation();
        }

        public void ShouldSerializeToObjectWhenFindById()
        {
            // arrange
            var id = 15;
            var getRoute = _protocolAndDomain + @"/" + "FakeModel" + @"/" + id;
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