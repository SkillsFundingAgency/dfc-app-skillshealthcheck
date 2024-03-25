using System.Net.Mime;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;

using FakeItEasy;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Xunit;

namespace DFC.App.SkillsHealthCheck.UnitTests.ControllerTests.SaveMyProgressControllerTests
{
    [Trait("Category", "Save My Progress Unit Tests")]
    public class GetCodeTests : SaveMyProgressControllerTestsBase
    {
        //[Fact]
        //public async Task GetCodeBodyRequestReturnsSuccess()
        //{
        //    using var controller = BuildController(MediaTypeNames.Text.Html);

        //    var result = await controller.GetCodeBody();

        //    result.Should().BeOfType<ViewResult>()
        //         .Which.ViewData.Model.Should().NotBeNull();
        //}

        //[Fact]
        //public async Task GetCodePostRequestReturnsRedirectResultToSmsSent()
        //{
        //    using var controller = BuildController(MediaTypeNames.Text.Html);
        //    A.CallTo(() => GovNotifyService.SendSmsAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
        //        .Returns(new NotifyResponse { IsSuccess = true });

        //    var result = await controller.GetCode(new ReferenceNumberViewModel());

        //    result.Should().BeOfType<RedirectResult>()
        //        .Which.Url.Should().Be("/skills-health-check/save-my-progress/sms");
        //}

        //[Fact]
        //public async Task GetCodeBodyPostRequestReturnsRedirectResultToSmsSent()
        //{
        //    using var controller = BuildController(MediaTypeNames.Text.Html);
        //    A.CallTo(() => GovNotifyService.SendSmsAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
        //        .Returns(new NotifyResponse { IsSuccess = true });

        //    var result = await controller.GetCodeBody(new ReferenceNumberViewModel());

        //    result.Should().BeOfType<RedirectResult>()
        //        .Which.Url.Should().Be("/skills-health-check/save-my-progress/sms");
        //}

        //[Fact]
        //public async Task GetCodePostRequestReturnsRedirectResultToSmsFailed()
        //{
        //    using var controller = BuildController(MediaTypeNames.Text.Html);
        //    A.CallTo(() => GovNotifyService.SendSmsAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
        //        .Returns(new NotifyResponse());

        //    var result = await controller.GetCode(new ReferenceNumberViewModel());

        //    result.Should().BeOfType<RedirectResult>()
        //        .Which.Url.Should().Be("/skills-health-check/save-my-progress/smsfailed");
        //}

        //[Fact]
        //public async Task GetCodeBodyPostRequestReturnsRedirectResultToSmsFailed()
        //{
        //    using var controller = BuildController(MediaTypeNames.Text.Html);
        //    A.CallTo(() => GovNotifyService.SendSmsAsync(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
        //        .Returns(new NotifyResponse());

        //    var result = await controller.GetCodeBody(new ReferenceNumberViewModel());

        //    result.Should().BeOfType<RedirectResult>()
        //        .Which.Url.Should().Be("/skills-health-check/save-my-progress/smsfailed");
        //}
    }
}
