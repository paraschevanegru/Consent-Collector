using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Validators;
using FluentAssertions;
using System.Linq;
using Xunit;

namespace TestConsentCollector.TestValidators
{
    public class NotificationModelValidatorTest
    {
        private CreateNotificationModelValidator _notificationModelValidator { get; }

        public NotificationModelValidatorTest()
        {
            _notificationModelValidator = new CreateNotificationModelValidator();
        }

        [Fact]
        public void NotAllow_Empty_Title()
        {
            var notification = new NotificationModel();
            notification.Title = string.Empty;
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Title should be not empty.");
        }

        [Fact]
        public void NotAllow_TitleShorterThan3Characters()
        {
            var notification = new NotificationModel();
            notification.Title = "ab";
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_TitleGreaterThan50Characters()
        {
            var notification = new NotificationModel();
            notification.Title = "ab".PadLeft(51,'x');
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void When_Notification_IsValidItShouldPass()
        {
            var notification = new NotificationModel();
            notification.Title = "abcd";
            notification.Description = "abcde";
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeTrue();
        }


        [Fact]
        public void NotAllow_Empty_Description()
        {
            var notification = new NotificationModel();
            notification.Description = string.Empty;
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Description should be not empty.");
        }

        [Fact]
        public void NotAllow_DescriptionShorterThan3Characters()
        {
            var notification = new NotificationModel();
            notification.Description = "ab";
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_DescriptionGreaterThan150Characters()
        {
            var notification = new NotificationModel();
            notification.Description = "ab".PadLeft(151, 'x');
            var result = _notificationModelValidator.Validate(notification);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }

    }
}
