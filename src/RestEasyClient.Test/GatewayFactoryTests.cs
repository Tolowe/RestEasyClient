using RestEasyClient.Impl;
using Should;

namespace RestEasyClient.Test
{
    public class GatewayFactoryTests
    {
        private readonly IGatewayFactory _gatewayFactory;

        public GatewayFactoryTests()
        {
            _gatewayFactory = new GatewayFactory("http://dummy.com");
        }

        public void ShouldGetACqrsGateway()
        {
            // arrange
            // act
            var result = _gatewayFactory.GetCqrsGateway<FakeModel>();
            // assert
            result.ShouldNotBeNull();
        }
    }
}