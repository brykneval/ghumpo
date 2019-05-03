using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using ghumpo.api;
using ghumpo.common;
using ghumpo.data.Infrastructure;
using ghumpo.data.Repository;
using ghumpo.service;
using ghumpo.service.Query;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using WebActivatorEx;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof (NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof (NinjectWebCommon), "Stop")]

namespace ghumpo.api
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof (OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof (NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IDbConnection>().To<SqlConnection>().WithConstructorArgument("connectionString",
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            //kernel.Bind<ILuceneSearch>().To<LuceneSearch>();
            kernel.Bind<IBlobCore>().To<BlobCore>();

            kernel.Bind<IRestaurantCommandRepository>().To<RestaurantCommandRepository>();
            kernel.Bind<IRestaurantCommandService>().To<RestaurantCommandService>();
            kernel.Bind<IRestaurantListingQueryService>().To<RestaurantListingQueryService>();
            kernel.Bind<IGeoCoordinatesCommandRepository>().To<GeoCoordinatesCommandRepository>();
            kernel.Bind<IGeoCoordinatesCommandService>().To<GeoCoordinatesCommandService>();
            kernel.Bind<IImageRatingCommandRepository>().To<ImageRatingCommandRepository>();
            kernel.Bind<IImageRatingQueryRepository>().To<ImageRatingQueryRepository>();
            kernel.Bind<IImageRatingCommandService>().To<ImageRatingCommandService>();
            kernel.Bind<IImageListingMapperCommandRepository>().To<ImageListingMapperCommandRepository>();
            kernel.Bind<IImageCommandRepository>().To<ImageCommandRepository>();
            kernel.Bind<IImageCommandService>().To<ImageCommandService>();
            kernel.Bind<IImageListingQueryService>().To<ImageListingQueryService>();
            kernel.Bind<ILocalBusinessCommandRepository>().To<LocalBusinessCommandRepository>();
            kernel.Bind<ILocalBusinessCommandService>().To<LocalBusinessCommandService>();
            kernel.Bind<ISearchListingMapperQueryRepository>().To<SearchListingMapperQueryRepository>();
            kernel.Bind<ISearchListingMapperCommandRepository>().To<SearchListingMapperCommandRepository>();
            kernel.Bind<ISearchListingMapperCommandService>().To<SearchListingMapperCommandService>();
            kernel.Bind<ISearchListingMapperQueryService>().To<SearchListingMapperQueryService>();
            kernel.Bind<IProfileCommandRepository>().To<ProfileCommandRepository>();
            kernel.Bind<IProfileCommandService>().To<ProfileCommandService>();
            kernel.Bind<IProfileQueryRepository>().To<ProfileQueryRepository>();
            kernel.Bind<ISuggestionsQueryService>().To<SuggestionsQueryService>();
            kernel.Bind<IProfileStatsQueryService>().To<ProfileStatsQueryService>();
            kernel.Bind<IProfileStatsCommandRepository>().To<ProfileStatsCommandRepository>();
            kernel.Bind<IImageReportCommandRepository>().To<ImageReportCommandRepository>();
            kernel.Bind<IImageReportCommandService>().To<ImageReportCommandService>();
            kernel.Bind<IMobileStatsCommandService>().To<MobileStatsCommandService>();
            kernel.Bind<IFeedbackCommandService>().To<FeedbackCommandService>();
            kernel.Bind<IImageRestaurantCommandService>().To<ImageRestaurantCommandService>();
            kernel.Bind<IBusinessGroupQueryService>().To<BusinessGroupQueryService>();
        }
    }
}