using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using DFC.App.SkillsHealthCheck.Extensions;
using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using DFC.App.SkillsHealthCheck.ViewModels.SaveMyProgress;
using DFC.Compui.Sessionstate;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DFC.App.SkillsHealthCheck.Controllers
{
    [ExcludeFromCodeCoverage]
    public class SaveMyProgressController : BaseController<SaveMyProgressController>
    {
        public const string PageTitle = "Save My Progress";

        private readonly ILogger<SaveMyProgressController> logger;
        private readonly ISkillsHealthCheckService skillsHealthCheckService;

        public SaveMyProgressController(
            ILogger<SaveMyProgressController> logger,
            ISessionStateService<SessionDataModel> sessionStateService,
            ISkillsHealthCheckService skillsHealthCheckService) : base(logger, sessionStateService)
        {
            this.logger = logger;
            this.skillsHealthCheckService = skillsHealthCheckService;
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/htmlhead")]
        [Route("skills-health-check/save-my-progress/getcode/htmlhead")]
        [Route("skills-health-check/save-my-progress/sms/htmlhead")]
        [Route("skills-health-check/save-my-progress/email/htmlhead")]
        [Route("skills-health-check/save-my-progress/emailsent/htmlhead")]
        public IActionResult HtmlHead()
        {
            TempData.Keep();
            var viewModel = GetHtmlHeadViewModel(PageTitle);

            logger.LogInformation($"{nameof(HtmlHead)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

        [Route("skills-health-check/save-my-progress/breadcrumb")]
        [Route("skills-health-check/save-my-progress/getcode/breadcrumb")]
        [Route("skills-health-check/save-my-progress/sms/breadcrumb")]
        [Route("skills-health-check/save-my-progress/email/breadcrumb")]
        [Route("skills-health-check/save-my-progress/emailsent/breadcrumb")]
        public IActionResult Breadcrumb()
        {
            TempData.Keep();
            var viewModel = BuildBreadcrumb();

            logger.LogInformation($"{nameof(Breadcrumb)} has returned content");

            return this.NegotiateContentResult(viewModel);
        }

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
        public async Task<IActionResult> Document(SaveMyProgressViewModel model)
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
        public IActionResult Body([FromQuery] string? type)
        {
            var model = GetSaveMyProgressViewModel(type);
            return this.NegotiateContentResult(model);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/body")]
        public async Task<IActionResult> Body(SaveMyProgressViewModel model)
        {
            var type = await GetAssessmentTypeAsync();
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

            var viewModel = GetSaveMyProgressViewModel(type);
            return this.NegotiateContentResult(viewModel);
        }

        #endregion

        #region GetCode

        [HttpGet]
        [Route("skills-health-check/save-my-progress/getcode/body")]
        public async Task<IActionResult> GetCodeBody()
        {
            var type = await GetAssessmentTypeAsync();
            TempData.Keep();
            var (link, text) = GetBackLinkAndText(type);
            var viewModel = new ReferenceNumberViewModel() { ReturnLink = link, ReturnLinkText = text, Document = new Document() };
            await AddDocumentDetailsAsync(viewModel.Document);
            return this.NegotiateContentResult(viewModel);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/getcode/body")]
        public async Task<IActionResult> GetCodeBody(ReferenceNumberViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: send a text message
                TempData["PhoneNumber"] = model?.PhoneNumber;
                TempData.Keep();
                return Redirect($"/skills-health-check/save-my-progress/sms");
            }

            return await GetCodeBody();
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/getcode")]
        [Route("skills-health-check/save-my-progress/getcode/document")]
        public async Task<IActionResult> GetCode()
        {
            var type = await GetAssessmentTypeAsync();
            TempData.Keep();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var (link, text) = GetBackLinkAndText(type);
            var referenceViewModel = new ReferenceNumberViewModel() { ReturnLink = link, ReturnLinkText = text, Document = new Document() };
            await AddDocumentDetailsAsync(referenceViewModel.Document);

            return this.NegotiateContentResult(new GetCodeViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = referenceViewModel,
            });
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/getcode")]
        public async Task<IActionResult> GetCode(ReferenceNumberViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: send a text message
                TempData["PhoneNumber"] = model?.PhoneNumber;
                TempData.Keep();
                return Redirect($"/skills-health-check/save-my-progress/sms");
            }

            return await GetCode();
        }

        #endregion

        #region CheckYourPhone

        [HttpGet]
        [Route("skills-health-check/save-my-progress/sms/body")]
        public async Task<IActionResult> CheckYourPhoneBody()
        {
            var type = await GetAssessmentTypeAsync();
            var (link, text) = GetBackLinkAndText(type);
            var viewModel = new ReferenceNumberViewModel() { ReturnLink = link, ReturnLinkText = text, PhoneNumber = TempData["PhoneNumber"]?.ToString() ?? string.Empty };
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/sms")]
        [Route("skills-health-check/save-my-progress/sms/document")]
        public async Task<IActionResult> CheckYourPhone()
        {
            var type = await GetAssessmentTypeAsync();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var (link, text) = GetBackLinkAndText(type);
            var referenceViewModel = new ReferenceNumberViewModel() { ReturnLink = link, ReturnLinkText = text, PhoneNumber = TempData["PhoneNumber"]?.ToString() ?? string.Empty };

            logger.LogInformation($"{nameof(GetCode)} has returned content");

            return this.NegotiateContentResult(new GetCodeViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = referenceViewModel,
            });
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
            return this.NegotiateContentResult(viewModel);
        }

        [HttpPost]
        [Route("skills-health-check/save-my-progress/email/body")]
        public async Task<IActionResult> EmailBody(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: send an email
                TempData["Email"] = model?.EmailAddress;
                TempData.Keep();
                return Redirect($"/skills-health-check/save-my-progress/emailsent");
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
        public async Task<IActionResult> Email(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                // TODO: send an email
                TempData["Email"] = model?.EmailAddress;
                TempData.Keep();
                return Redirect($"/skills-health-check/save-my-progress/emailsent");
            }

            return await Email();
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/emailsent/body")]
        public async Task<IActionResult> CheckYourEmailBody()
        {
            var type = await GetAssessmentTypeAsync();
            var (link, text) = GetBackLinkAndText(type);
            var viewModel = new EmailViewModel() { ReturnLink = link, ReturnLinkText = text, EmailAddress = TempData["Email"]?.ToString() ?? string.Empty };
            return this.NegotiateContentResult(viewModel);
        }

        [HttpGet]
        [Route("skills-health-check/save-my-progress/emailsent")]
        [Route("skills-health-check/save-my-progress/emailsent/document")]
        public async Task<IActionResult> CheckYourEmail()
        {
            var type = await GetAssessmentTypeAsync();
            var htmlHeadViewModel = GetHtmlHeadViewModel(PageTitle);
            var breadcrumbViewModel = BuildBreadcrumb();
            var (link, text) = GetBackLinkAndText(type);
            var emailViewModel = new EmailViewModel() { ReturnLink = link, ReturnLinkText = text, EmailAddress = TempData["Email"]?.ToString() ?? string.Empty };

            logger.LogInformation($"{nameof(GetCode)} has returned content");

            return this.NegotiateContentResult(new EmailDocumentViewModel
            {
                HtmlHeadViewModel = htmlHeadViewModel,
                BreadcrumbViewModel = breadcrumbViewModel,
                BodyViewModel = emailViewModel,
            });
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
    }
}
