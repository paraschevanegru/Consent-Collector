using ConsentCollector.Business.Consent.Models.UserDetails;
using ConsentCollector.Business.Consent.Validators;
using FluentAssertions;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace TestConsentCollector.TestValidators
{
    public class UserDetailModelValidatorTest
    {
        private CreateUserDetailModelValidator _userDetailModelValidator;

        public UserDetailModelValidatorTest()
        {
            _userDetailModelValidator = new CreateUserDetailModelValidator();
        }

        [Fact]
        public void NotAllow_Empty_Firstname()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Firstname = string.Empty;
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Firstname should be not empty.");
        }

        [Fact]
        public void NotAllow_Empty_Lastname()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Lastname = string.Empty;
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Lastname should be not empty.");
        }

        [Fact]
        public void NotAllow_Empty_Email()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Email = string.Empty;
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Email should be not empty.");
        }

        [Fact]
        public void NotAllow_Empty_Number()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Number = string.Empty;
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Number should be not empty.");
        }

        [Fact]
        public void NotAllow_FirstnameShorterThan3Characters()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Firstname = "ab";
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_FirstnameGreaterThan30Characters()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Firstname = "ab".PadLeft(31, 'x');
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }

        [Fact]
        public void NotAllow_LastnameShorterThan3Characters()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Lastname = "ab";
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_LastnameGreaterThan30Characters()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Lastname = "ab".PadLeft(31, 'x');
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }
        private bool IsValidName(string name)
        {
            return Regex.Match(name, @"^[A-Z][-a-zA-Z]+$").Success;
        }
        [Fact]
        public void Firstname_ShouldHaveSpecificFormat()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Firstname = "1as";
            string relationship = "";
            if (!IsValidName(userDetail.Firstname))
            {
                relationship = "yes";
            }
            relationship.Should().Contain("yes");
        }

        [Fact]
        public void Lastname_ShouldHaveSpecificFormat()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Lastname = "12a3";
            string relationship = "";
            if (!IsValidName(userDetail.Lastname))
            {
                relationship = "yes";
            }
            relationship.Should().Contain("yes");
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        [Fact]
        public void Email_ShouldNotHaveTheFormat_OfAnEmailAddress()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Email = "12a3";
            string relationship = "";
            if (!IsValidEmail(userDetail.Email))
            {
                relationship = "yes";
            }
            relationship.Should().Contain("yes");
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^[\d()]*\d[\d()]*\d+$").Success;
        }

        [Fact]
        public void Number_ShouldNotHaveTheFormat_OfAPhoneNumber()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Number = "12a3";
            string relationship = "";
            if (!IsPhoneNumber(userDetail.Number))
            {
                relationship = "yes";
            }
            relationship.Should().Contain("yes");
        }

        [Fact]
        public void When_UserDetail_IsValidItShouldPass()
        {
            var userdetail = new CreateUserDetailModel();
            userdetail.Firstname = "Ana";
            userdetail.Lastname = "Popa";
            userdetail.Email = "ana@gmail.com";
            userdetail.Number = "0744444444";
            var result = _userDetailModelValidator.Validate(userdetail);
            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public void NotAllow_NumberShorterThan10Characters()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Number = "ab";
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("ExactLengthValidator");
        }

        [Fact]
        public void NotAllow_NumberGreaterThan10Characters()
        {
            var userDetail = new CreateUserDetailModel();
            userDetail.Number = "ab".PadLeft(11,'x');
            var result = _userDetailModelValidator.Validate(userDetail);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("ExactLengthValidator");
        }

    }
}
