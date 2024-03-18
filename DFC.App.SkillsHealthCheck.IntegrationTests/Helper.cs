using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

using DFC.App.SkillsHealthCheck.Models;
using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Models;
using DFC.Compui.Sessionstate;

using FakeItEasy;

namespace DFC.App.SkillsHealthCheck.IntegrationTests
{
    public static class Helper
    {
        public static readonly string CompositeSessionIdHeaderName = "x-dfc-composite-sessionid";

        public static void SetSession(HttpClient client, CustomWebApplicationFactory<Startup> factory)
        {
            client?.DefaultRequestHeaders.Add(CompositeSessionIdHeaderName, Guid.NewGuid().ToString());
            var sessionDataModel = new SessionDataModel
            {
                DocumentId = 1,
                AssessmentQuestionsOverViews = new Dictionary<string, AssessmentQuestionsOverView>()
                {
                    { "AssessmentQuestionOverviewId_SkillAreas", new AssessmentQuestionsOverView() }
                }
            };

            A.CallTo(() => factory.FakeSessionStateService.GetAsync(A<Guid>.Ignored))
                .Returns(new SessionStateModel<SessionDataModel> { State = sessionDataModel });
        }

        public static byte[] GetFileBytes()
        {
            var s = "JVBERi0xLjcKCjQgMCBvYmoKPDwKL0JpdHNQZXJDb21wb25lbnQgOAovQ29sb3JTcGFjZSAvRGV2aWNlUkdCCi9GaWx0ZXIgL0RDVERlY29kZQovSGVpZ2h0IDI5Ci9MZW5ndGgg" +
                "OTc5Ci9TdWJ0eXBlIC9JbWFnZQovVHlwZSAvWE9iamVjdAovV2lkdGggMTA0Cj4+CnN0cmVhbQr/2P/gABBKRklGAAEBAQBgAGAAAP/bAEMADQkKCwoIDQsLCw8ODRAUIRUUEhIUKB0e" +
                "GCEwKjIxLyouLTQ7S0A0OEc5LS5CWUJHTlBUVVQzP11jXFJiS1NUUf/bAEMBDg8PFBEUJxUVJ1E2LjZRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFRUVFR" +
                "UVFRUf/AABEIAB0AaAMBIgACEQEDEQH/xAAfAAABBQEBAQEBAQAAAAAAAAAAAQIDBAUGBwgJCgv/xAC1EAACAQMDAgQDBQUEBAAAAX0BAgMABBEFEiExQQYTUWEHInEUMoGRoQgjQrHB" +
                "FVLR8CQzYnKCCQoWFxgZGiUmJygpKjQ1Njc4OTpDREVGR0hJSlNUVVZXWFlaY2RlZmdoaWpzdHV2d3h5eoOEhYaHiImKkpOUlZaXmJmaoqOkpaanqKmqsrO0tba3uLm6wsPExcbHyMnK" +
                "0tPU1dbX2Nna4eLj5OXm5+jp6vHy8/T19vf4+fr/xAAfAQADAQEBAQEBAQEBAAAAAAAAAQIDBAUGBwgJCgv/xAC1EQACAQIEBAMEBwUEBAABAncAAQIDEQQFITEGEkFRB2FxEyIygQgU" +
                "QpGhscEJIzNS8BVictEKFiQ04SXxFxgZGiYnKCkqNTY3ODk6Q0RFRkdISUpTVFVWV1hZWmNkZWZnaGlqc3R1dnd4eXqCg4SFhoeIiYqSk5SVlpeYmZqio6Slpqeoqaqys7S1tre4ubrC" +
                "w8TFxsfIycrS09TV1tfY2dri4+Tl5ufo6ery8/T19vf4+fr/2gAMAwEAAhEDEQA/APTqKKZMZFhdogGcDIB70APoqml8sjIylRGE3yMf4c9B9alF5blHfzOE5bIII/CgCeio3nijLBnw" +
                "VXefp60sM0c6lo23KDjOKAH0VAl7bSMqrKCW6cHmmSXkRRxFKu9eu4HA5xQBaoqI3EIRnL/KrbTx0PpSLdQtN5Qf58kYwecdeaAJqKrm9twW/efc6kAkD8am8xPMEe75yu7HtQA6iiig" +
                "AooooAoGyfyJ1UKGabzF9CMggGkmtZ7rzndVjZo9iruz3zya0KKAM+S3uZmld0RC0BjADZ5zV2FNkKIeCqgcU+igDKtIpp7O1Taqxo+/fnngnjFTm0k+wywgLvaTd17bs/yq6qqqhVAA" +
                "HYUtAFN7R2vQ4x5JYSMO+4DH+H5VELW5aeJ5eSjklvMPI5xgdBWjRQBSt4pIrY20yIIgCC+7qPpSaWjGIzSHJbCKf9kcD8+TV1lDKVYAg9jS9KACiiigD//ZCmVuZHN0cmVhbQplbmRv" +
                "YmoKNSAwIG9iago8PAovRmlsdGVyIC9GbGF0ZURlY29kZQovTGVuZ3RoIDE2OAo+PgpzdHJlYW0KeJxtkLEOwjAMRHd/hWckTGzXTbKzMHZiRgjaoQXR/x9whCpStZ4uz8r57EDRghcG" +
                "CmEtjtueZSMVVxHvE3yAl8ZWzD1oImtdM2om/akJTIRS8odVeKxxtJbYco7q/G+ywgM8ofMEIolazV6oSalkY/EpqkLRaUo1H2vesCzLjLXPig9wPeCrrBoa4hKk2TuVZOLyRXHfyK91" +
                "uky3/sF4fnvwDr4dA0qcCmVuZHN0cmVhbQplbmRvYmoKNiAwIG9iago8PAovWE9iamVjdCA8PAovSW1hZ2UxIDQgMCBSCj4+Cj4+CmVuZG9iagozIDAgb2JqCjw8Ci9Db250ZW50cyBb" +
                "IDUgMCBSIF0KL0Nyb3BCb3ggWyAwLjAgMC4wIDQxOS41MTk5OSA1OTUuMzIwMDEgXQovTWVkaWFCb3ggWyAwLjAgMC4wIDQxOS41MTk5OSA1OTUuMzIwMDEgXQovUGFyZW50IDIgMCBS" +
                "Ci9SZXNvdXJjZXMgNiAwIFIKL1JvdGF0ZSAwCi9UeXBlIC9QYWdlCj4+CmVuZG9iagoyIDAgb2JqCjw8Ci9Db3VudCAxCi9LaWRzIFsgMyAwIFIgXQovVHlwZSAvUGFnZXMKPj4KZW5k" +
                "b2JqCjEgMCBvYmoKPDwKL1BhZ2VzIDIgMCBSCi9UeXBlIC9DYXRhbG9nCj4+CmVuZG9iago3IDAgb2JqCjw8Ci9BdXRob3IgKHJhdmltKQovQ3JlYXRpb25EYXRlIChEOjIwMjExMTIy" +
                "MTcxNTQwKzAwJzAwJykKL01vZERhdGUgKEQ6MjAyMTExMjIxNzE1NDArMDAnMDAnKQovUHJvZHVjZXIgKE1pY3Jvc29mdDogUHJpbnQgVG8gUERGKQovVGl0bGUgKHNhbXBsZS5QTkcg" +
                "KDEwNNcyOSkpCj4+CmVuZG9iagp4cmVmCjAgOA0KMDAwMDAwMDAwMCA2NTUzNSBmDQowMDAwMDAxNjgwIDAwMDAwIG4NCjAwMDAwMDE2MjEgMDAwMDAgbg0KMDAwMDAwMTQ0NCAwMDAw" +
                "MCBuDQowMDAwMDAwMDA5IDAwMDAwIG4NCjAwMDAwMDExNTQgMDAwMDAgbg0KMDAwMDAwMTM5NCAwMDAwMCBuDQowMDAwMDAxNzI5IDAwMDAwIG4NCnRyYWlsZXIKPDwKL0luZm8gNyAw" +
                "IFIKL1Jvb3QgMSAwIFIKL1NpemUgOAo+PgpzdGFydHhyZWYKMTkwNwolJUVPRgo=";
            return Encoding.Default.GetBytes(s);
        }
    }
}
