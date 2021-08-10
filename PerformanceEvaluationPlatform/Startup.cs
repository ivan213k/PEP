using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PerformanceEvaluationPlatform.DAL;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks;
using PerformanceEvaluationPlatform.DAL.Repositories.Document;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using PerformanceEvaluationPlatform.DAL.Repositories.FieldsGroup;
using PerformanceEvaluationPlatform.DAL.Repositories.FormsData;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.DAL.Repositories.Projects;
using PerformanceEvaluationPlatform.DAL.Repositories.Roles;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;
using PerformanceEvaluationPlatform.DAL.Repositories.Teams;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
using PerformanceEvaluationPlatform.Models.Document.Validator;
using PerformanceEvaluationPlatform.Application.Services.Example;
using PerformanceEvaluationPlatform.Models.User.Auth0;

namespace PerformanceEvaluationPlatform
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddControllers();
            services.AddSwaggerGen();

            services.AddTransient<IAuth0ClientFactory, Auth0ClientFactory>();

            services.AddMemoryCache();

            services.Configure<Auth0Configur>(options => Configuration.GetSection("Auth0Configure").Bind(options));

            services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.AddDbContext<PepDbContext>();
            services.AddTransient<IExamplesRepository, ExamplesRepository>();
            services.AddTransient<IFormTemplatesRepository, FormTemplatesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IFieldsRepository, FieldsRepository>();
            services.AddTransient<ISurveysRepository, SurveysRepository>();

            services.AddTransient<IDocumentReposotory, DocumentRepository>();
            services.AddTransient<IDocumentValidator, DocumentRequestModelsValidator>();
            services.AddTransient<IFieldsGroupRepository, FieldsGroupRepository>();
            services.AddTransient<IDeeplinksRepository, DeeplinksRepository>();
            services.AddTransient<ITeamsRepository, TeamsRepository>();
            services.AddTransient<IFormDataRepository, FormDataRepository>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();


            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=> 
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}";
                options.Audience = Configuration["Auth0:Audience"];
                options.TokenValidationParameters = tokenValidationParameters;
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "PerformanceEvaluationPlatform");
            });

            app.UseRouting();

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
