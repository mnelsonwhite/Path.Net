using System;
using Xunit;

namespace SimplePath.Tests
{
    public class SPathTests
    {
        [Fact]
        public void WhenEmptyPathShouldBeEmpty()
        {
            // Arrnage
            var path = new SPath();

            // Act
            var actual = path.ToString("/");

            // Assert
            Assert.Equal(string.Empty, actual);
        }

        [Fact]
        public void WhenRootShouldBeRoot()
        {
            // Arrage
            var path = SPath.Parse(@"\", @"\");

            // Act
            var actual = path.ToString("/");

            // Assert
            Assert.Equal("/", actual);
        }

        [Fact]
        public void WhenEmptyPathSegmentShouldBeIncluded()
        {
            // Arrnage
            var path = SPath.Parse(@"C:\Program Files\", @"\");

            // Act
            var actual = path.ToString("/");

            // Assert
            Assert.Equal("C:/Program Files/", actual);
        }
    }
}
