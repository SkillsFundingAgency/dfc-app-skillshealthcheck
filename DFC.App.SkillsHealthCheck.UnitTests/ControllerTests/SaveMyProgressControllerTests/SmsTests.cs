using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FakeItEasy;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class SmsTests : SaveMyProgressControllerTestsBase
    {
        private const string PhoneNumber = "123";
        private const string Code = "Code";

        [Fact]
        public async Task CheckYourPhoneBodyRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", PhoneNumber } });

            var result = await controller.CheckYourPhoneBody();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<ReferenceNumberViewModel>()
                .Which.PhoneNumber.Should().Be(PhoneNumber);
        }

        [Fact]
        public async Task CheckYourPhoneRequestReturnsSuccess()
        {
            using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", PhoneNumber } });

            var result = await controller.CheckYourPhone();

            result.Should().NotBeNull()
                .And.BeOfType<ViewResult>()
                .Which.ViewData.Model.Should().NotBeNull()
                .And.BeOfType<GetCodeViewModel>()
                .Which.BodyViewModel?.PhoneNumber.Should().Be(PhoneNumber);
        }

        //[Fact]
        //public async Task SmsFailedBodyGetRequestReturnsSuccess()
        //{
        //    var skillsDocumentIdentifier = new SkillsDocumentIdentifier
        //    {
        //        ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
        //        Value = Code,
        //    };

        //    A.CallTo(() => SkillsHealthCheckService.GetSkillsDocument(A<int>.Ignored))
        //        .Returns(new SkillsCentral.Api.Domain.Models.SkillsDocument { Id = 1 });
        //    using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", PhoneNumber } });

        //    var result = await controller.SmsFailedBody();

        //    var model = result.Should().NotBeNull()
        //        .And.BeOfType<ViewResult>()
        //        .Which.ViewData.Model.Should().NotBeNull()
        //        .And.BeOfType<ErrorViewModel>()
        //        .Which;
        //    model.SendTo.Should().Be(PhoneNumber);
        //    model.Code.Should().Be(Code.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        //}

        //[Fact]
        //public async Task SmsFailedGetRequestReturnsSuccess()
        //{
        //    var skillsDocumentIdentifier = new SkillsDocumentIdentifier
        //    {
        //        ServiceName = Constants.SkillsHealthCheck.DocumentSystemIdentifierName,
        //        Value = Code,
        //    };

        //    A.CallTo(() => SkillsHealthCheckService.GetSkillsDocument(A<int>.Ignored))
        //       .Returns(new SkillsCentral.Api.Domain.Models.SkillsDocument { Id = 1 });

        //    using var controller = BuildController(MediaTypeNames.Text.Html, new Dictionary<string, object> { { "PhoneNumber", PhoneNumber } });

        //    var result = await controller.SmsFailed();

        //    var model = result.Should().NotBeNull()
        //        .And.BeOfType<ViewResult>()
        //        .Which.ViewData.Model.Should().NotBeNull()
        //        .And.BeOfType<ErrorDocumentViewModel>()
        //        .Which.BodyViewModel;

        //    model.Should().NotBeNull();
        //    model!.SendTo.Should().Be(PhoneNumber);
        //    model.Code.Should().Be(Code.ToUpper(System.Globalization.CultureInfo.CurrentCulture));
        //}
    }
}
