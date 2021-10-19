//using System;
//using System.ServiceModel;
//using DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces;

//namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Helpers
//{
//    public class ServiceHelper : IServiceHelper
//    {
//        public void Use<TService>(Action<TService> action)
//        {
//            var factory = new ChannelFactory<TService>(typeof(TService).Name);
//            var proxy = (IClientChannel)factory.CreateChannel();

//            var success = false;
//            try
//            {
//                action((TService)proxy);
//                proxy.Close();
//                success = true;
//            }
//            finally
//            {
//                if (!success)
//                {
//                    proxy.Abort();
//                }
//            }
//        }

//        public TReturn Use<TService, TReturn>(Func<TService, TReturn> action)
//        {
//            var name = typeof(TService).Name;
//            var binding = new BasicHttpBinding();
//            var endPointAddress = new EndpointAddress("");
//            var factory = new ChannelFactory<TService>(binding, endPointAddress);
//            var proxy = (IClientChannel)factory.CreateChannel();

//            bool success = false;
//            try
//            {
//                var result = action((TService) proxy);
//                proxy.Close();
//                success = true;
//                return result;
//            }
//            catch (Exception ex)
//            {
//                var error = ex.Message;
//                proxy.Abort();
//                throw;
//            }
//            finally
//            {
//                if (!success)
//                {
//                    proxy.Abort();
//                }
//            }
//        }

//        private static readonly Lazy<IServiceHelper> HelperInstance = new Lazy<IServiceHelper>(() => new ServiceHelper());
//        public static Func<IServiceHelper> Instance = () => HelperInstance.Value;
//    }
//}
