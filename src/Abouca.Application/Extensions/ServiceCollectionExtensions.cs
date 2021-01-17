using System;
using System.Collections.Generic;
using System.Text;
using Abouca.Application.Mappings;
using Abouca.Application.Services;
using Abouca.Application.Validations;
using Abouca.Database;
using Abouca.Database.Context;
using Abouca.Database.Data.Repositories;
using Abouca.Domain;
using Abouca.Domain.Information;
using Abouca.Domain.Interfaces;
using Abouca.Domain.User;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Abouca.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();

            return services
                .AddSingleton(mapper)
                .AddDbContext<MainContext>()

                .AddScoped<AddUserValidator>()
                .AddScoped<ModifyUserValidator>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<UserService>()

                .AddScoped<IInformationRepository, InformationRepository>()
                .AddScoped<DeleteInformationValidator>()
                .AddScoped<ModifyInformationValidator>()
                .AddScoped<InformationService>();
        } 
    }
}
