using Xunit;

namespace Archysoft.D1.UnitTests.Web.Api.ProfileControllerTests
{
    public class GetProfileMethodShould
    {
        public ProfileControllerSut Sut { get; set; }

        public GetProfileMethodShould()
        {
            Sut = new ProfileControllerSut();
        }

        [Fact]
        public void ReturnsNull()
        {
            // Arrange
            // Action
            var actualResult = Sut.Instance.GetProfile();

            // Assert
            Assert.Null(actualResult);
        }
    }
}
