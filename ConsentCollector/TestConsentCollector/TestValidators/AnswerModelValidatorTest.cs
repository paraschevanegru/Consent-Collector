using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsentCollector.Business.Consent.Models;
using ConsentCollector.Business.Consent.Validators;
using ConsentCollector.Entities.Consent;
using FluentAssertions;
using FluentValidation.TestHelper;
using Xunit;
using Xunit.Abstractions;

namespace TestConsentCollector.TestValidators
{
    public class AnswerModelValidatorTest
    {
        private CreateAnswerModelValidator _answerModelValidator { get; }

        public AnswerModelValidatorTest()
        {
            _answerModelValidator = new CreateAnswerModelValidator();
        }

        [Fact]
        public void NotAllow_Empty_AnswerDate()
        {
            var answer = new CreateAnswerModel();
            _answerModelValidator.TestValidate(answer).ShouldHaveValidationErrorFor(x => x.AnswerDate);

        }

        [Fact]
        public void AnswerDate_NotGreaterThanOrEqual_ToCurrentDate()
        {
            var answer = new CreateAnswerModel();
            answer.AnswerDate = DateTime.Now.Date.AddDays(-1);
            _answerModelValidator.TestValidate(answer).ShouldHaveValidationErrorFor(x => x.AnswerDate);

        }

        [Fact]
        public void When_Answer_IsValidItShouldPass()
        {
            var answer = new CreateAnswerModel();
            answer.AnswerDate = DateTime.Now.Date;
            var result = _answerModelValidator.Validate(answer);
            result.IsValid.Should().BeTrue();
        }
    }
}
