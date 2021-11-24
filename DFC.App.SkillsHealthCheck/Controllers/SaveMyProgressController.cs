using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Filters;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.GovNotify;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;
using DFC.Compui.Sessionstate;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    [ServiceFilter(typeof(SessionStateFilter))]
    public class SaveMyProgressController : BaseController<SaveMyProgressController>
    {
        public const string PageTitle = "Save My Progress";

        private readonly ILogger<SaveMyProgressController> logger;
        private readonly IGovNotifyService govNotifyService;
        private readonly IConfiguration configuration;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;

        public SaveMyProgressController(
            ILogger<SaveMyProgressController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            IOptions<SessionStateOptions> sessionStateOptions,
            IGovNotifyService govNotifyService,
            IConfiguration configuration,
            ISkillsHealthCheckService skillsHealthCheckService) : base(logger, sessionStateService, sessionStateOptions)
        {
            this.logger = logger;
            this.govNotifyService = govNotifyService;
            this.configuration = configuration;
            this.skillsHealthCheckService = skillsHealthCheckService;
        }

        #region HtmlHead

        [HttpGet]
        [Route("skills-health-check/save-my-progress/htmlhead")]
        [Route("skills-health-check/save-my-progress/getcode/htmlhead")]
        [Route("skills-health-check/save-my-progress/sms/htmlhead")]
        [Route("skills-health-check/save-my-progress/smsfailed/htmlhead")]
        [Route("skills-health-check/save-my-progress/email/htmlhead")]
        [Route("skills-health-check/save-my-progress/emailsent/htmlhead")]
        [Route("skills-health-check/save-my-progress/emailfailed/htmlhead")]
        public IActionResult HtmlHead()
        {
            TempData.Keep();
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        #endregion

        #region Breadcrumb

        [Route("skills-health-check/save-my-progress/breadcrumb")]
        [Route("skills-health-check/save-my-progress/getcode/breadcrumb")]
        [Route("skills-health-check/save-my-progress/sms/breadcrumb")]
        [Route("skills-health-check/save-my-progress/smsfailed/breadcrumb")]
        [Route("skills-health-check/save-my-progress/email/breadcrumb")]
        [Route("skills-health-check/save-my-progress/emailsent/breadcrumb")]
        [Route("skills-health-check/save-my-progress/emailfailed/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            TempData.Keep();
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        #endregion

        #region SaveMyProgress

        [HttpGet]
        [Route("skills-health-check/save-my-progress/")]
        [Route("skills-health-check/save-my-progress/document")]
        public async Task<IActionResult> Document([FromQuery] string? type)
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            await SetAssessmentTypeAsync(type);
            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                SaveMyProgressViewModel = GetSaveMyProgressViewModel(type),
            });
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/")]
        [Route("skills-health-check/save-my-progress/document")]
        public async Task<IActionResult> Document([FromBody] SaveMyProgressViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model?.SelectedOption)
                {
                    case Enums.SaveMyProgressOption.Email:
                        return Redirect("/skills-health-check/save-my-progress/email");

                    case Enums.SaveMyProgressOption.ReferenceCode:
                        return Redirect("/skills-health-check/save-my-progress/getcode");

                    default:
                        break;
                }

                ModelState.AddModelError("SelectedOption", SaveMyProgressViewModel.SelectedOptionValidationError);
            }

            var type = await GetAssessmentTypeAsync();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            return this.NegotiateContentResult(new DocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                SaveMyProgressViewModel = GetSaveMyProgressViewModel(type),
            });
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/body")]
        public async Task<IActionResult> Body([FromQuery] string? type)
        {
            await SetAssessmentTypeAsync(type);
            var model = GetSaveMyProgressViewModel(type);

            logger.LogInformation($"{nameof(Body)} has returned content");
            return this.NegotiateContentResult(model);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/body")]
        public async Task<IActionResult> Body([FromBody] SaveMyProgressViewModel model)
        {
            if (ModelState.IsValid)
            {
                switch (model?.SelectedOption)
                {
                    case Enums.SaveMyProgressOption.Email:
                        return Redirect("/skills-health-check/save-my-progress/email");

                    case Enums.SaveMyProgressOption.ReferenceCode:
                        return Redirect("/skills-health-check/save-my-progress/getcode");

                    default:
                        break;
                }

                ModelState.AddModelError("SelectedOption", SaveMyProgressViewModel.SelectedOptionValidationError);
            }

            var type = await GetAssessmentTypeAsync();
            var viewModel = GetSaveMyProgressViewModel(type);
            return this.NegotiateContentResult(viewModel);
        }

        #endregion

        #region GetCode

        [HttpGet]
        [Route("skills-health-check/save-my-progress/getcode/body")]
        public async Task<IActionResult> GetCodeBody()
        {
            TempData.Keep();
            var referenceViewModel = await GetReferenceNumberViewModelAsync();
            await AddDocumentDetailsAsync(referenceViewModel.Document!);

            logger.LogInformation($"{nameof(GetCodeBody)} has returned content");
            return this.NegotiateContentResult(referenceViewModel);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/getcode/body")]
        public async Task<IActionResult> GetCodeBody([FromBody] ReferenceNumberViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await ProcessSmsRequestAsync(model);
            }

            return await GetCodeBody();
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/getcode")]
        [Route("skills-health-check/save-my-progress/getcode/document")]
        public async Task<IActionResult> GetCode()
        {
            TempData.Keep();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            var referenceViewModel = await GetReferenceNumberViewModelAsync();
            await AddDocumentDetailsAsync(referenceViewModel.Document!);

            return this.NegotiateContentResult(new GetCodeViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = referenceViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/getcode")]
        public async Task<IActionResult> GetCode([FromBody] ReferenceNumberViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await ProcessSmsRequestAsync(model);
            }

            return await GetCode();
        }

        #endregion

        #region SMS

        [HttpGet]
        [Route("skills-health-check/save-my-progress/sms/body")]
        public async Task<IActionResult> CheckYourPhoneBody()
        {
            var viewModel = await GetReferenceNumberViewModelAsync();

            logger.LogInformation($"{nameof(CheckYourPhoneBody)} has returned content");
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/sms")]
        [Route("skills-health-check/save-my-progress/sms/document")]
        public async Task<IActionResult> CheckYourPhone()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(GetCode)} has returned content");

            var viewModel = new GetCodeViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = await GetReferenceNumberViewModelAsync(),
            };
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/smsfailed/body")]
        public async Task<IActionResult> SmsFailedBody()
        {
            var viewModel = await GetErrorViewModelAsync(TempData["PhoneNumber"]?.ToString());

            logger.LogInformation($"{nameof(SmsFailedBody)} has returned content");
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/smsfailed")]
        [Route("skills-health-check/save-my-progress/smsfailed/document")]
        public async Task<IActionResult> SmsFailed()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(GetCode)} has returned content");

            var viewModel = new ErrorDocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = await GetErrorViewModelAsync(TempData["PhoneNumber"]?.ToString()),
            };
            return this.NegotiateContentResult(viewModel);
        }

        #endregion

        #region Email

        [HttpGet]
        [Route("skills-health-check/save-my-progress/email/body")]
        public async Task<IActionResult> EmailBody()
        {
            var type = await GetAssessmentTypeAsync();
            TempData.Keep();
            var (link, text) = GetBackLinkAndText(type);
            var viewModel = new EmailViewModel() { ReturnLink = link, ReturnLinkText = text };

            logger.LogInformation($"{nameof(EmailBody)} has returned content");
            return this.NegotiateContentResult(viewModel);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/email/body")]
        public async Task<IActionResult> EmailBody([FromBody]EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await ProcessEmailRequestAsync(model);
            }

            return await EmailBody();
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/email")]
        [Route("skills-health-check/save-my-progress/email/document")]
        public async Task<IActionResult> Email()
        {
            var type = await GetAssessmentTypeAsync();
            TempData.Keep();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var (link, text) = GetBackLinkAndText(type);
            var emailViewModel = new EmailViewModel() { ReturnLink = link, ReturnLinkText = text };

            logger.LogInformation($"{nameof(GetCode)} has returned content");

            return this.NegotiateContentResult(new EmailDocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = emailViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/email")]
        public async Task<IActionResult> Email([FromBody]EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                return await ProcessEmailRequestAsync(model);
            }

            return await Email();
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/emailsent/body")]
        public async Task<IActionResult> CheckYourEmailBody()
        {
            var viewModel = await GetEmailViewModelAsync();

            logger.LogInformation($"{nameof(CheckYourEmailBody)} has returned content");
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/emailsent")]
        [Route("skills-health-check/save-my-progress/emailsent/document")]
        public async Task<IActionResult> CheckYourEmail()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            logger.LogInformation($"{nameof(GetCode)} has returned content");

            var viewModel = new EmailDocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = await GetEmailViewModelAsync(),
            };
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/emailfailed/body")]
        public async Task<IActionResult> EmailFailedBody()
        {
            var viewModel = await GetErrorViewModelAsync(TempData["Email"]?.ToString());

            logger.LogInformation($"{nameof(EmailFailedBody)} has returned content");
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/emailfailed")]
        [Route("skills-health-check/save-my-progress/emailfailed/document")]
        public async Task<IActionResult> EmailFailed()
        {
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(GetCode)} has returned content");

            var viewModel = new ErrorDocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = await GetErrorViewModelAsync(TempData["Email"]?.ToString()),
            };
            return this.NegotiateContentResult(viewModel);
        }

        #endregion

        private static SaveMyProgressViewModel GetSaveMyProgressViewModel(string? type)
        {
            var (link, text) = GetBackLinkAndText(type);
            return new SaveMyProgressViewModel { ReturnLink = link, ReturnLinkText = text };
        }

        private static (string, string) GetBackLinkAndText(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return ("/skills-health-check/your-assessments", "Return to your skills health check");
            }
            else
            {
                return ($"/skills-health-check/question?assessmentType={type}", "Return to your skills health check assessment");
            }
        }

        private async Task<ErrorViewModel> GetErrorViewModelAsync(string? sendTo)
        {
            var type = await GetAssessmentTypeAsync();
            var (link, text) = GetBackLinkAndText(type);
            var viewModel = new ErrorViewModel()
            {
                ReturnLink = link,
                ReturnLinkText = text,
                SendTo = sendTo,
                Code = await GetDocumentCodeAsync(),
            };
            return viewModel;
        }

        private async Task<EmailViewModel> GetEmailViewModelAsync()
        {
            var type = await GetAssessmentTypeAsync();
            var (link, text) = GetBackLinkAndText(type);
            var viewModel = new EmailViewModel()
            {
                ReturnLink = link,
                ReturnLinkText = text,
                Document = new Document(),
                EmailAddress = TempData["Email"]?.ToString() ?? string.Empty,
            };
            await AddDocumentDetailsAsync(viewModel.Document);
            return viewModel;
        }

        private async Task<ReferenceNumberViewModel> GetReferenceNumberViewModelAsync()
        {
            var type = await GetAssessmentTypeAsync();
            var (link, text) = GetBackLinkAndText(type);
            var referenceViewModel = new ReferenceNumberViewModel()
            {
                ReturnLink = link,
                ReturnLinkText = text,
                PhoneNumber = TempData["PhoneNumber"]?.ToString(),
                Document = new Document(),
            };
            return referenceViewModel;
        }

        private async Task<string?> GetDocumentCodeAsync()
        {
            var documentId = await GetDocumentId();
            var response = skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest() { DocumentId = documentId });
            var code = response?.SkillsDocument?.SkillsDocumentIdentifiers?
                .FirstOrDefault(d => d.ServiceName == Constants.SkillsHealthCheck.DocumentSystemIdentifierName)?
                .Value;

            return code?.ToUpper(System.Globalization.CultureInfo.CurrentCulture) ?? throw new Exception("No document found.");
        }

        private async Task AddDocumentDetailsAsync(Document document)
        {
            var documentId = await GetDocumentId();
            var response = skillsHealthCheckService.GetSkillsDocument(new GetSkillsDocumentRequest() { DocumentId = documentId });
            if (response.Success)
            {
                document.DateAssessmentsCreated = TimeZoneInfo.ConvertTimeFromUtc(response.SkillsDocument.CreatedAt, TimeZoneInfo.FindSystemTimeZoneById("GMT Standard Time"));
                document.Code = response.SkillsDocument.SkillsDocumentIdentifiers.FirstOrDefault(d => d.ServiceName == Constants.SkillsHealthCheck.DocumentSystemIdentifierName)?.Value;
                if (string.IsNullOrWhiteSpace(document.Code))
                {
                    logger.LogError($"Document guid not found for document Id [{documentId}]");
                    Response.Redirect("/alerts/500?errorcode=saveProgressRef", true);
                }
            }
            else
            {
                Response.Redirect("/alerts/500?errorcode=saveProgressResponse", true);
            }

            document.Code = document.Code?.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
        }

        private string GetDomainUrl()
        {
            return $"{configuration["CompositeBaseUrl"]}skills-health-check/home";
        }

        private async Task<IActionResult> ProcessSmsRequestAsync(ReferenceNumberViewModel model)
        {
            var document = new Document();
            await AddDocumentDetailsAsync(document);
            var response = await govNotifyService.SendSmsAsync(GetDomainUrl(), model.PhoneNumber!, document.Code!);
            TempData["PhoneNumber"] = model?.PhoneNumber;
            TempData.Keep();
            if (response.IsSuccess)
            {
                return Redirect("/skills-health-check/save-my-progress/sms");
            }

            logger.LogError($"Failed to send SMS. {response.Message}");
            return Redirect("/skills-health-check/save-my-progress/smsfailed");
        }

        private async Task<IActionResult> ProcessEmailRequestAsync(EmailViewModel model)
        {
            var document = new Document();
            await AddDocumentDetailsAsync(document);
            var response = await govNotifyService.SendEmailAsync(GetDomainUrl(), model.EmailAddress!, document.Code!);
            TempData["Email"] = model?.EmailAddress;
            TempData.Keep();
            if (response.IsSuccess)
            {
                return Redirect("/skills-health-check/save-my-progress/emailsent");
            }

            logger.LogError($"Failed to send Email. {response.Message}");
            return Redirect("/skills-health-check/save-my-progress/emailfailed");
        }
    }
}
