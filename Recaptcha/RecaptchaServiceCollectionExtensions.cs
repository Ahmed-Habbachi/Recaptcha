﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace CaseTunisia.Recaptcha
{
    public static class RecaptchaServiceCollectionExtensions
    {
        public static void AddRecaptcha(this IServiceCollection services, RecaptchaOptions configureOptions)
        {
            configureOptions.CheckArgumentNull(nameof(configureOptions));

            services.TryAddSingleton(Options.Create(configureOptions));
            services.TryAddSingleton<RecaptchaService>();
            services.TryAddSingleton<IRecaptchaValidationService>((sp) => sp.GetRequiredService<RecaptchaService>());
            services.TryAddSingleton<IRecaptchaConfigurationService>((sp) => sp.GetRequiredService<RecaptchaService>());
            services.TryAddSingleton<ValidateRecaptchaFilter>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddRecaptcha(this IServiceCollection services, Action<RecaptchaOptions> configuration)
        {
            configuration.CheckArgumentNull(nameof(configuration));

            var configureOptions = new RecaptchaOptions();

            configuration(configureOptions);

            AddRecaptcha(services, configureOptions);
        }
    }
}
