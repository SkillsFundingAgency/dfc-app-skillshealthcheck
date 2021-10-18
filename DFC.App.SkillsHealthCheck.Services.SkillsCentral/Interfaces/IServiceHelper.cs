using System;

namespace DFC.App.SkillsHealthCheck.Services.SkillsCentral.Interfaces
{
    public interface IServiceHelper
    {
        void Use<TService>(Action<TService> action);

        TReturn Use<TService, TReturn>(Func<TService, TReturn> action);
    }
}
