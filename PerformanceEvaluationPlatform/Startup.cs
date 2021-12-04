using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PerformanceEvaluationPlatform.Application.Interfaces.Documents;
using PerformanceEvaluationPlatform.Application.Interfaces.Projects;
using PerformanceEvaluationPlatform.Application.Interfaces.Surveys;
using PerformanceEvaluationPlatform.Application.Services.Deeplinks;
using PerformanceEvaluationPlatform.Application.Services.Document;
using PerformanceEvaluationPlatform.Application.Services.Example;
using PerformanceEvaluationPlatform.Application.Services.Excel;
using PerformanceEvaluationPlatform.Application.Services.Field;
using PerformanceEvaluationPlatform.Application.Services.FormsData;
using PerformanceEvaluationPlatform.Application.Services.FormTemplates;
using PerformanceEvaluationPlatform.Application.Services.Projects;
using PerformanceEvaluationPlatform.Application.Services.Role;
using PerformanceEvaluationPlatform.Application.Services.Surveys;
using PerformanceEvaluationPlatform.Application.Services.Users;
using PerformanceEvaluationPlatform.DAL;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using PerformanceEvaluationPlatform.DAL.Repositories.FieldsGroup;
using PerformanceEvaluationPlatform.DAL.Repositories.Teams;
using PerformanceEvaluationPlatform.Infrastructure.Document;
//using PerformanceEvaluationPlatform.Models.User.Auth0;
using PerformanceEvaluationPlatform.Models.User.Policies;
using PerformanceEvaluationPlatform.Persistence.Repositories.Documents;
using PerformanceEvaluationPlatform.Persistence.Repositories.Projects;
using PerformanceEvaluationPlatform.Persistence.Repositories.Surveys;
using PerformanceEvaluationPlatform.Application.Services.Users.Auth0;
using PerformanceEvaluationPlatform.Application.Interfaces.Users.Auth0;
using PerformanceEvaluationPlatform.Application.Model.Users;
using PerformanceEvaluationPlatform.Application.Interfaces.Roles;
using PerformanceEvaluationPlatform.Persistence.Repositories.Roles;

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
            services.AddCors(options => options.AddPolicy("default", builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            }));
            services.AddControllers();
            services.AddSwaggerGen();


            services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.AddDbContext<PepDbContext>();

            services.Configure<PerformanceEvaluationPlatform.Persistence.DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.AddDbContext<PerformanceEvaluationPlatform.Persistence.DatabaseContext.PepDbContext>();

            services.AddTransient<PerformanceEvaluationPlatform.Application.Interfaces.Examples.IExamplesRepository, PerformanceEvaluationPlatform.Persistence.Repositories.Examples.ExamplesRepository>();
            services.AddTransient<IExamplesService, ExamplesService>();

            services.AddTransient<PerformanceEvaluationPlatform.Application.Interfaces.Fields.IFieldsRepository, PerformanceEvaluationPlatform.Persistence.Repositories.Fields.FieldsRepository>();
            services.AddTransient<IFieldService, FieldService>();
            services.AddTransient<IExcelService, ExcelService>();

            services.AddTransient<PerformanceEvaluationPlatform.Application.Interfaces.FormTemplates.IFormTemplatesRepository, PerformanceEvaluationPlatform.Persistence.Repositories.FormTemplates.FormTemplatesRepository>();
            services.AddTransient<IFormTemplatesService, FormTemplatesService>();

            services.AddTransient<PerformanceEvaluationPlatform.Application.Interfaces.Users.IUserRepository, PerformanceEvaluationPlatform.Persistence.Repositories.Users.UserRepository>();
            services.AddTransient<IUserService, UserService>();

            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<IFieldsRepository, FieldsRepository>();

            services.AddTransient<ISurveysRepository, SurveysRepository>();
            services.AddTransient<ISurveyService, SurveyService>();

            services.AddTransient<IDocumentReposotory, DocumentRepository>();
            services.AddTransient<IDocumentService, DocumentService>();
            services.AddTransient<IFieldsGroupRepository, FieldsGroupRepository>();

            services.AddTransient<PerformanceEvaluationPlatform.Application.Interfaces.Deeplinks.IDeeplinksRepository,PerformanceEvaluationPlatform.Persistence.Repositories.Deeplinks.DeeplinksRepository>();
            services.AddTransient<IDeeplinksService, DeeplinksService>();
            
            services.AddTransient<ITeamsRepository, TeamsRepository>();
            services.AddTransient<PerformanceEvaluationPlatform.Application.Interfaces.FormsData.IFormDataRepository, PerformanceEvaluationPlatform.Persistence.Repositories.FormsData.FormDataRepository>();
            services.AddTransient<IFormDataService, FormDataService>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();
            services.AddTransient<IProjectsService, ProjectsService>();

            services.AddTransient<IAuth0ClientFactory, Auth0ClientFactory>();

            services.AddMemoryCache();

            services.Configure<Auth0Configur>(options => Configuration.GetSection("Auth0Configur").Bind(options));
            var DocumentStorage = new DocumentStorageConfigur();
            Configuration.GetSection("DocumentStorageConfigur").Bind(DocumentStorage);
            services.AddSingleton(x => DocumentStorage);

            services.AddSingleton<IDocumentStorage, AzureBlobDocumentStorage>();

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
                options.Authority = $"https://{Configuration["Auth0Configur:Domain"]}";
                options.Audience = Configuration["Auth0Configur:Audience"];
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization(options => {
                options.AddPolicy(Policy.User, policy =>
                 {
                     policy.RequireRole("User");
                     policy.RequireAuthenticatedUser();
                 });
            });

            services.AddAuthorization(options => {
                options.AddPolicy(Policy.Admin, policy =>
                {
                    policy.RequireRole("Admin");
                    policy.RequireAuthenticatedUser();
                });
            });

            services.AddAuthorization(options => {
                options.AddPolicy(Policy.ProjectCoordinator, policy =>
                {
                    policy.RequireRole("ProjectCoordinator");
                    policy.RequireAuthenticatedUser();
                });
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
            app.UseCors("default");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
