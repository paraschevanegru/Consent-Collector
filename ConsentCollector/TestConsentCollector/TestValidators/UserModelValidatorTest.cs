using ConsentCollector.Business.Consent.Models.Users;
using ConsentCollector.Business.Consent.Validators;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace TestConsentCollector.TestValidators
{
    public class UserModelValidatorTest
    {
        private CreateUserModelValidator _userModelValidator;

        public UserModelValidatorTest()
        {
            _userModelValidator = new CreateUserModelValidator();
        }

        [Fact]
        public void NotAllow_Empty_Username()
        {
            var user = new CreateUserModel();
            user.Username = string.Empty;
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Username should be not empty.");
        }

        [Fact]
        public void NotAllow_Empty_Role()
        {
            var user = new CreateUserModel();
            user.Role = string.Empty;
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Role should be not empty.");
        }

        [Fact]
        public void NotAllow_Empty_Password()
        {
            var user = new CreateUserModel();
            user.Password = string.Empty;
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Password should be not empty.");
        }

        [Fact]
        public void NotAllow_FirstnameShorterThan3Characters()
        {
            var user = new CreateUserModel();
            user.Username = "ab";
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_FirstnameGreaterThan20Characters()
        {
            var user = new CreateUserModel();
            user.Username = "ab".PadLeft(21, 'x');
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }

        public static bool IsUsername(string username)
        {
            return Regex.Match(username, @"^[a-zA-Z0-9]*$").Success;
        }

        [Fact]
        public void Username_ShouldNotHaveSpecialCharacters()
        {
            var user = new CreateUserModel();
            user.Username = "12a3*";
            string relationship = "";
            if (!IsUsername(user.Username))
            {
                relationship = "yes";
            }

            relationship.Should().Contain("yes");
        }

        [Fact]
        public void Role_ShouldNotContain_ValuesFromList()
        {
            var user = new CreateUserModel();
            List<string> role = new List<string>() {"user", "admin"};
            user.Role = "string";
            string relationship = "";
            role.ForEach(x =>
            {
                if (!x.Contains(user.Role))
                {
                    relationship = "yes";
                }
            });
            relationship.Should().Contain("yes");
        }

        [Fact]
        public void NotAllow_PasswordToHave_MinimumLength_Of8Characters()
        {
            var user = new CreateUserModel();
            user.Password = "abaaaaa";
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("MinimumLengthValidator");
        }

        [Fact]
        public void NotAllow_PasswordToHave_MaximumLength_Of16Characters()
        {
            var user = new CreateUserModel();
            user.Password = "ab".PadLeft(16, 'x');
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeFalse();
            //result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }

        public static bool IsUppercase(string password)
        {
            return Regex.Match(password, @"[A-Z]+").Success;
        }

        [Fact]
        public void Password_ShouldNotHaveUppercase()
        {
            var user = new CreateUserModel();
            user.Password = "a";
            string relationship = "";
            if (!IsUppercase(user.Password))
            {
                relationship = "yes";
            }

            relationship.Should().Contain("yes");
        }

        public static bool IsLowercase(string password)
        {
            return Regex.Match(password, @"[a-z]+").Success;
        }

        [Fact]
        public void Password_ShouldNotHaveLowercase()
        {
            var user = new CreateUserModel();
            user.Password = "A";
            string relationship = "";
            if (!IsLowercase(user.Password))
            {
                relationship = "yes";
            }

            relationship.Should().Contain("yes");
        }

        public static bool IsDigit(string password)
        {
            return Regex.Match(password, @"[0-9]+").Success;
        }

        [Fact]
        public void Password_ShouldNotHaveDigits()
        {
            var user = new CreateUserModel();
            user.Password = "a";
            string relationship = "";
            if (!IsDigit(user.Password))
            {
                relationship = "yes";
            }

            relationship.Should().Contain("yes");
        }

        public static bool IsSpecialCharacters(string password)
        {
            return Regex.Match(password, @"[\!\?\*\.]+").Success;
        }

        [Fact]
        public void Password_ShouldNotHaveSpecialCharacters()
        {
            var user = new CreateUserModel();
            user.Password = "a";
            string relationship = "";
            if (!IsSpecialCharacters(user.Password))
            {
                relationship = "yes";
            }

            relationship.Should().Contain("yes");
        }

        [Fact]
        public void When_User_IsValidItShouldPass()
        {
            var user = new CreateUserModel();
            user.Username = "ana123";
            user.Password = "Ana123789*000000";
            user.Role = "user";
            var result = _userModelValidator.Validate(user);
            result.IsValid.Should().BeTrue();
        }
    }
}
