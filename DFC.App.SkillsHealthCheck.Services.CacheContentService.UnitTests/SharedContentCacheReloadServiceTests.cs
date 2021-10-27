using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using DFC.App.SkillsHealthCheck.Data.Helpers;
using DFC.App.SkillsHealthCheck.Data.Models.CmsApiModels;
using DFC.App.SkillsHealthCheck.Data.Models.ContentModels;
using DFC.Compui.Cosmos.Contracts;
using DFC.Content.Pkg.Netcore.Data.Contracts;
using DFC.Content.Pkg.Netcore.Data.Models.ClientOptions;

using FakeItEasy;

using Microsoft.Extensions.Logging;

using Xunit;

namespace DFC.App.SkillsHealthCheck.Services.CacheContentService.UnitTests
{
    public class SharedContentCacheReloadServiceTests
    {
        private readonly IMapper fakeMapper = A.Fake<IMapper>();
        private readonly IDocumentService<SharedContentItemModel> fakeSharedContentItemDocumentService = A.Fake<IDocumentService<SharedContentItemModel>>();
        private readonly ICmsApiService fakeCmsApiService = A.Fake<ICmsApiService>();
        private readonly SharedContentCacheReloadService sharedContentCacheReloadService;

        //private readonly CmsApiClientOptions cmsApiClientOptions;

        public SharedContentCacheReloadServiceTests()
        {
            var cmsApiClientOptions = new CmsApiClientOptions { ContentIds = Guid.NewGuid().ToString() };
            sharedContentCacheReloadService = new SharedContentCacheReloadService(A.Fake<ILogger<SharedContentCacheReloadService>>(), fakeMapper, fakeSharedContentItemDocumentService, cmsApiClientOptions, fakeCmsApiService);
        }

        [Fact]
        public async Task SharedContentCacheReloadServiceReloadAllCancellationRequestedCancels()
        {
            //Arrange
            var cancellationToken = new CancellationToken(true);

            //Act
            await sharedContentCacheReloadService.Reload(cancellationToken);

            //Assert
            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => fakeSharedContentItemDocumentService.UpsertAsync(A<SharedContentItemModel>.Ignored)).MustNotHaveHappened();
        }

        [Fact]
        public async Task SharedContentCacheReloadServiceReloadAllReloadsItems()
        {
            //Arrange
            var dummyContentItem = A.Dummy<SharedContentItemApiDataModel>();

            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).Returns(dummyContentItem);
            //var sharedContentCacheReloadService = new SharedContentCacheReloadService(A.Fake<ILogger<SharedContentCacheReloadService>>(), fakeMapper, fakeSharedContentItemDocumentService, fakeCmsApiService);

            //Act
            await sharedContentCacheReloadService.Reload(CancellationToken.None);

            //Assert
            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).MustHaveHappened(SharedContentKeyHelper.GetSharedContentKeys().Count(), Times.Exactly);
            A.CallTo(() => fakeSharedContentItemDocumentService.UpsertAsync(A<SharedContentItemModel>.Ignored)).MustHaveHappened(SharedContentKeyHelper.GetSharedContentKeys().Count(), Times.Exactly);
        }

        [Fact]
        public async Task SharedContentCacheReloadServiceReloadSharedContentSuccessful()
        {
            //Arrange
            var dummyContentItem = A.Dummy<SharedContentItemApiDataModel>();

            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).Returns(dummyContentItem);
            //var sharedContentCacheReloadService = new SharedContentCacheReloadService(A.Fake<ILogger<SharedContentCacheReloadService>>(), fakeMapper, fakeSharedContentItemDocumentService, fakeCmsApiService);

            //Act
            await sharedContentCacheReloadService.ReloadSharedContent(CancellationToken.None);

            //Assert
            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).MustHaveHappened(SharedContentKeyHelper.GetSharedContentKeys().Count(), Times.Exactly);
            A.CallTo(() => fakeSharedContentItemDocumentService.UpsertAsync(A<SharedContentItemModel>.Ignored)).MustHaveHappened(SharedContentKeyHelper.GetSharedContentKeys().Count(), Times.Exactly);
        }

        [Fact]
        public async Task SharedContentCacheReloadServiceReloadSharedContentNullApiResponse()
        {
            //Arrange
            SharedContentItemApiDataModel? nullContentItem = null;

            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).Returns(nullContentItem);
            //var sharedContentCacheReloadService = new SharedContentCacheReloadService(A.Fake<ILogger<SharedContentCacheReloadService>>(), fakeMapper, fakeSharedContentItemDocumentService, fakeCmsApiService);

            //Act
            await sharedContentCacheReloadService.ReloadSharedContent(CancellationToken.None);

            //Assert
            A.CallTo(() => fakeCmsApiService.GetItemAsync<SharedContentItemApiDataModel>(A<string>.Ignored, A<Guid>.Ignored)).MustHaveHappened(SharedContentKeyHelper.GetSharedContentKeys().Count(), Times.Exactly);
            A.CallTo(() => fakeSharedContentItemDocumentService.UpsertAsync(A<SharedContentItemModel>.Ignored)).MustNotHaveHappened();
        }
    }
}
