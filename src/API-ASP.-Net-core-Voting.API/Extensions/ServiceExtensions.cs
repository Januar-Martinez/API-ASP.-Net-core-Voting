using Microsoft.EntityFrameworkCore;
using API_ASP._Net_core_Voting.API.Data;
using API_ASP._Net_core_Voting.API.Repositories;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;
using API_ASP._Net_core_Voting.API.Services;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new()
                {
                    Title = "VotingAPI",
                    Version = "v1",
                    Description = "API REST para gestión de votaciones, votantes y candidatos."
                });
            });

            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVoterRepository, VoterRepository>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IVoteRepository, VoteRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVoterService, VoterService>();
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IVoteService, VoteService>();

            return services;
        }
    }
}