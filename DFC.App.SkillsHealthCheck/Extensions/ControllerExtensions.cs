﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Mime;
using DFC.App.SkillsHealthCheck.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace DFC.App.SkillsHealthCheck.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ControllerExtensions
    {
        public static IActionResult NegotiateContentResult(this Controller controller, object? viewModel, object? dataModel = null)
        {
            if (controller == null)
            {
                throw new ArgumentNullException(nameof(controller));
            }

            if (controller.Request.Headers.Keys.Contains(HeaderNames.Accept))
            {
                var acceptHeaders = controller.Request.Headers[HeaderNames.Accept].ToString().Split(';');

                foreach (var acceptHeader in acceptHeaders)
                {
                    var items = acceptHeader.Split(',');

                    if (items.Contains(MediaTypeNames.Application.Json, StringComparer.OrdinalIgnoreCase))
                    {
                        return controller.Ok(dataModel ?? viewModel);
                    }

                    if (items.Contains(MediaTypeNames.Text.Html, StringComparer.OrdinalIgnoreCase) || items.Contains("*/*"))
                    {
                        return viewModel switch
                        {
                            IBodyPostback _ => controller.View("Body", viewModel),
                            IDocumentPostback _ => controller.View("Document", viewModel),
                            ViewModels.SaveMyProgress.ErrorViewModel _ => controller.View("ErrorBody", viewModel),
                            ViewModels.SaveMyProgress.ErrorDocumentViewModel _ => controller.View("Error", viewModel),
                            _ => controller.View(viewModel),
                        };
                    }
                }
            }

            return controller.StatusCode((int)HttpStatusCode.NotAcceptable);
        }
    }
}
