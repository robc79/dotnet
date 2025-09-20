using Shouldly;

namespace Fragment.Domain.Tests;

public class TestFragementTests
{
    public class WhenCreating
    {
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void TextMustBeSupplied(string? invalidText)
        {
            // Arrange

            // Act
            var action = () => { _ = new TextFragment(invalidText); };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void NoTagsAreAssigned()
        {
            // Arrange

            // Act
            var fragment = new TextFragment("some_text");

            // Assert
            fragment.Tags.ShouldBeEmpty();
        }

        [Fact]
        public void CreationTimestampIsRecorded()
        {
            // Arrange
            var now = DateTimeOffset.UtcNow;

            // Act
            var fragment = new TextFragment("some_text");

            // Assert
            fragment.CreatedOn.ShouldBe(now, TimeSpan.FromSeconds(30));
        }
    }

    public class WhenSettingText
    {
        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void ValueMustBeSupplied(string? invalidText)
        {
            // Arrange
            var fragment = new TextFragment("some_text");

            // Act
            var action = () => { fragment.Text = invalidText; };

            // Assert
            action.ShouldThrow<ArgumentException>();
        }
    }
}