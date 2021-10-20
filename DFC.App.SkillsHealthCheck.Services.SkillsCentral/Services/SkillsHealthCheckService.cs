using System;
using System.Collections.Generic;
using System.ServiceModel;
using AutoMapper;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Extensions;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Mappers;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Messages;
using Level = DFC.App.SkillsHealthCheck.Services.SkillsCentral.Enums.Level;
using SkillsDocumentService;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Services
{
    public class SkillsHealthCheckService : ISkillsHealthCheckService
    {
        private IMapper _autoMapper;
        private ISkillsCentralService _skillsCentralService;

        public SkillsHealthCheckService(IMapper autoMapper, ISkillsCentralService skillsCentralService)
        {
            _skillsCentralService = skillsCentralService;
            _autoMapper = autoMapper;
        }
        /// <summary>
        /// Creates the skills document.
        /// </summary>
        /// <param name="createSkillsDocumentRequest">The create skills document request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public CreateSkillsDocumentResponse CreateSkillsDocument(CreateSkillsDocumentRequest createSkillsDocumentRequest)
        {
            if (createSkillsDocumentRequest == null)
            {
                throw new ArgumentNullException(nameof(createSkillsDocumentRequest));
            }

            var response = new CreateSkillsDocumentResponse();

            try
            {
                var request = createSkillsDocumentRequest.SkillsDocument.GetApiSkillsDocument();

                var apiResult = _skillsCentralService.InsertDocument(request);
                    //ServiceHelper.Instance()
                    //    .Use<ISkillsCentralService, long>(x => x.InsertDocument(request));

                response.DocumentId = apiResult;
                response.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Gets the assessment question.
        /// </summary>
        /// <param name="getAssessmentQuestionRequest">The get assessment question request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public GetAssessmentQuestionResponse GetAssessmentQuestion(GetAssessmentQuestionRequest getAssessmentQuestionRequest)
        {
            if (getAssessmentQuestionRequest == null)
            {
                throw new ArgumentNullException(nameof(getAssessmentQuestionRequest));
            }

            var response = new GetAssessmentQuestionResponse();

            try
            {
                if (getAssessmentQuestionRequest.Level == 0)
                {
                    getAssessmentQuestionRequest.Level = Level.Level1;
                }

                var assessmentType = getAssessmentQuestionRequest.AsessmentType.GetApiAssessmentType();
                var level = _autoMapper.Map<SkillsDocumentService.Level>(getAssessmentQuestionRequest.Level);// getAssessmentQuestionRequest.Level.GetApiEnum<SkillsServiceReference.Level>();
                var accessibility =
                    _autoMapper.Map<SkillsDocumentService.Accessibility>(getAssessmentQuestionRequest.Accessibility); // getAssessmentQuestionRequest.Accessibility.GetApiEnum<SkillsServiceReference.Accessibility>();

                var apiResult = _skillsCentralService.GetSkillsHealthCheckQuestions(assessmentType,
                    getAssessmentQuestionRequest.QuestionNumber, level, accessibility);
                    //ServiceHelper.Instance()
                    //    .Use<ISkillsCentralService, Question>(x => x.GetSkillsHealthCheckQuestions(assessmentType, getAssessmentQuestionRequest.QuestionNumber, level, accessibility));

                response.Question = apiResult.ConvertToModelQuestion();
                response.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Gets the list type fields.
        /// </summary>
        /// <param name="getListTypeFieldsRequest">The get list type fields request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public GetListTypeFieldsResponse GetListTypeFields(GetListTypeFieldsRequest getListTypeFieldsRequest)
        {
            if (getListTypeFieldsRequest == null)
            {
                throw new ArgumentNullException(nameof(getListTypeFieldsRequest));
            }

            var response = new GetListTypeFieldsResponse();
            try
            {
                
                var apiResult = _skillsCentralService.ListTypeFields(getListTypeFieldsRequest.DocumentType);
                //    ServiceHelper.Instance()
                //        .Use<ISkillsCentralService, List<string>>(
                //            x => x.ListTypeFields(getListTypeFieldsRequest.DocumentType));

                if (apiResult != null)
                {
                    response.TypeFields = apiResult;
                }

                response.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Gets the skills document.
        /// </summary>
        /// <param name="getSkillsDocumentRequest">The get skills document request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public GetSkillsDocumentIdResponse GetSkillsDocumentByIdentifier(string Identifier)
        {
            var response = new GetSkillsDocumentIdResponse();
            try
            {
                var apiResult = _skillsCentralService.FindDocumentByKeyValue(Identifier, false);
                //ServiceHelper.Instance()
                //    .Use<ISkillsCentralService, SkillsDocument>(
                //        x => x.FindDocumentByKeyValue(Identifier, false));

                if (apiResult != null)
                {
                    response.DocumentId = apiResult.DocumentId;
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Gets the skills document.
        /// </summary>
        /// <param name="getSkillsDocumentRequest">The get skills document request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public GetSkillsDocumentResponse GetSkillsDocument(GetSkillsDocumentRequest getSkillsDocumentRequest)
        {
            if (getSkillsDocumentRequest == null)
            {
                throw new ArgumentNullException(nameof(getSkillsDocumentRequest));
            }

            var response = new GetSkillsDocumentResponse();

            try
            {
                var apiResult = _skillsCentralService.ReadDocument(getSkillsDocumentRequest.DocumentId);
                    //ServiceHelper.Instance()
                    //    .Use<ISkillsCentralService, SkillsDocument>(
                    //        x => x.ReadDocument(getSkillsDocumentRequest.DocumentId));

                if (apiResult != null)
                {
                    response.SkillsDocument = apiResult.ConvertToModelSkillsDocument();
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Saves the question answer.
        /// </summary>
        /// <param name="saveQuestionAnswerRequest">The save question answer request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public SaveQuestionAnswerResponse SaveQuestionAnswer(SaveQuestionAnswerRequest saveQuestionAnswerRequest)
        {
            if (saveQuestionAnswerRequest == null)
            {
                throw new ArgumentNullException(nameof(saveQuestionAnswerRequest));
            }

            var response = new SaveQuestionAnswerResponse();

            try
            {
                var apiDocument = saveQuestionAnswerRequest.SkillsDocument.GetApiSkillsDocument();
                apiDocument.DocumentId = saveQuestionAnswerRequest.DocumentId;
                _skillsCentralService.UpdateSkillsDocumentDataValues(saveQuestionAnswerRequest.DocumentId, apiDocument);
                //ServiceHelper.Instance()
                //    .Use<ISkillsCentralService>(
                //        x => x.UpdateSkillsDocumentDataValues(saveQuestionAnswerRequest.DocumentId, apiDocument));
                response.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
        /// <summary>
        /// Downloads the document.
        /// </summary>
        /// <param name="downloadDocumentRequest">The download document request.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public DownloadDocumentResponse DownloadDocument(DownloadDocumentRequest downloadDocumentRequest)
        {
            if (downloadDocumentRequest == null) throw new ArgumentNullException(nameof(downloadDocumentRequest));
            var response = new DownloadDocumentResponse();

            try
            {
                response.DocumentBytes =
                    _skillsCentralService.FormatDocumentGetPayload(downloadDocumentRequest.DocumentId,
                        downloadDocumentRequest.Formatter);
                    //ServiceHelper.Instance()
                    // .Use<ISkillsCentralService, byte[]>(
                    //     x => x.FormatDocumentGetPayload(downloadDocumentRequest.DocumentId, downloadDocumentRequest.Formatter));
                response.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }

            return response;
        }


        /// <summary>
        /// Updates the skills document.
        /// </summary>
        /// <param name="updateSkillsDocumentRequest">The update skills document request.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public UpdateSkillsDocumentResponse UpdateSkillsDocument(UpdateSkillsDocumentRequest updateSkillsDocumentRequest)
        {
            if (updateSkillsDocumentRequest == null)
            {
                throw new ArgumentNullException(nameof(updateSkillsDocumentRequest));
            }

            var response = new UpdateSkillsDocumentResponse();

            try
            {
                var apiDocument = updateSkillsDocumentRequest.SkillsDocument.GetApiSkillsDocument();
                apiDocument.DocumentId = updateSkillsDocumentRequest.DocumentId;
                _skillsCentralService.UpdateDocument(apiDocument);
                //ServiceHelper.Instance()
                //    .Use<ISkillsCentralService>(
                //        x => x.UpdateDocument(apiDocument));
                response.Success = true;
            }
            catch (Exception ex)
            {
                if (ex.GetType()
                    .Name.Equals(nameof(CommunicationException), StringComparison.InvariantCultureIgnoreCase))
                {
                    response.ErrorType = ErrorType.CommunicationError;
                }
                response.ErrorMessage = ex.Message;
            }

            return response;
        }
    }
}
