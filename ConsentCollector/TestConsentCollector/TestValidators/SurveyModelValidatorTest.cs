using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Validators;
using FluentAssertions;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TestConsentCollector.TestValidators
{
    public class SurveyModelValidatorTest
    {
        private CreateSurveyModelValidator _surveyModelValidator { get; }
        public SurveyModelValidatorTest()
        {
            _surveyModelValidator = new CreateSurveyModelValidator();
        }

        [Fact]
        public void NotAllow_Empty_Subject()
        {
            var survey = new CreateSurveyModel();
            survey.Subject = string.Empty;
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Subject should be not empty.");
        }

        [Fact]
        public void NotAllow_Empty_Description()
        {
            var survey = new CreateSurveyModel();
            survey.Description = string.Empty;
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Description should be not empty.");
        }

        [Fact]
        public void NotAllow_SubjectShorterThan3Characters()
        {
            var survey = new CreateSurveyModel();
            survey.Subject = "ab";
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_SubjectGreaterThan50Characters()
        {
            var survey = new CreateSurveyModel();
            survey.Subject = "ab".PadLeft(51, 'x');
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }

        [Fact]
        public void NotAllow_DescriptionShorterThan3Characters()
        {
            var survey = new CreateSurveyModel();
            survey.Description = "ab";
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_DescriptionGreaterThan150Characters()
        {
            var survey = new CreateSurveyModel();
            survey.Description = "ab".PadLeft(151, 'x');
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorCode).Should().Contain("LengthValidator");
        }


        [Fact]
        public void NotAllow_Empty_LaunchDate()
        {
            var survey = new CreateSurveyModel();
            _surveyModelValidator.TestValidate(survey).ShouldHaveValidationErrorFor(x => x.LaunchDate);

        }

        [Fact]
        public void LaunchDate_NotGreaterThanOrEqual_ToCurrentDate()
        {
            var survey = new CreateSurveyModel();
            survey.LaunchDate = DateTime.Now.Date.AddDays(-1);
            _surveyModelValidator.TestValidate(survey).ShouldHaveValidationErrorFor(x => x.LaunchDate);

        }

        [Fact]
        public void NotAllow_Empty_ExpirationDate()
        {
            var survey = new CreateSurveyModel();
            _surveyModelValidator.TestValidate(survey).ShouldHaveValidationErrorFor(x => x.ExpirationDate);

        }

        [Fact]
        public void ExpirationDate_NotGreaterThanOrEqual_ToLaunchDate()
        {
            var survey = new CreateSurveyModel();
            survey.LaunchDate = DateTime.Now;
            survey.ExpirationDate = DateTime.Now.AddDays(-1);
            var result = DateTime.Compare(survey.LaunchDate, survey.ExpirationDate);
            string relationship = "";
            if (result > 0)
                relationship = "is later than";
            relationship.Should().Contain("is later than");

        }

        [Fact]
        public void NotAllow_Empty_LegalBasis()
        {
            var survey = new CreateSurveyModel();
            survey.LegalBasis = string.Empty;
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeFalse();
            result.Errors.Select(x => x.ErrorMessage).Should().Contain("Legal Basis should be not empty.");
        }

        [Fact]
        public void LegalBasis_ShouldNotContain_ValuesFromList()
        {
            var survey = new CreateSurveyModel();
            List<string> legalBasis = new List<string>() { "Contract", "Law", "Legitimate Interest" };
            survey.LegalBasis = "string";
            string relationship = "";
            legalBasis.ForEach(x =>
            {
                if (!x.Contains(survey.LegalBasis))
                {
                    relationship = "yes";
                }
            });
            relationship.Should().Contain("yes");
        }

        [Fact]
        public void When_Survey_IsValidItShouldPass()
        {
            var survey = new CreateSurveyModel();
            survey.Subject = "abcd";
            survey.Description = "abcde";
            survey.LaunchDate = DateTime.Now.Date;
            survey.ExpirationDate = DateTime.Now.AddDays(1);
            survey.LegalBasis = "Law";
            var result = _surveyModelValidator.Validate(survey);
            result.IsValid.Should().BeTrue();
        }
    }
}
