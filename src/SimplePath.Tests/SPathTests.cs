using System;
using Xunit;

namespace SimplePath.Tests
{
    public class SPathTests
    {
        [Fact]
        public void WhenEmptyPathShouldBeEmpty()
        {
            // Arrange
            var path = new SPath();

            // Act
            var actual = path.ToString("/");

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void WhenRootShouldBeRoot()
        {
            // Arrange
            var path = SPath.Parse(@"\", @"\");

            // Act
            var actual = path.ToString("/");

            // Assert
            Assert.Equal("/", actual);
        }

        [Fact]
        public void WhenEmptyPathSegmentShouldBeIncluded()
        {
            // Arrange
            var path = SPath.Parse(@"C:\Program Files\", @"\");

            // Act
            var actual = path.ToString("/");

            // Assert
            Assert.Equal("C:/Program Files/", actual);
        }

        [Fact]
        public void When_ShouldBe()
        {
            // Arrange
            var path = new SPath("root", "one", "two");
            var expected = new SPath("root", "one");

            // Act
            var actual = path.Parent();

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
