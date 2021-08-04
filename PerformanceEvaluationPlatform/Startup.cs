using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PerformanceEvaluationPlatform.DAL;
using PerformanceEvaluationPlatform.DAL.DatabaseContext;
using PerformanceEvaluationPlatform.DAL.Repositories.Deeplinks;
using PerformanceEvaluationPlatform.DAL.Repositories.Examples;
using PerformanceEvaluationPlatform.DAL.Repositories.Fields;
using PerformanceEvaluationPlatform.DAL.Repositories.FormTemplates;
using PerformanceEvaluationPlatform.DAL.Repositories.Surveys;

using PerformanceEvaluationPlatform.DAL.Repositories.Roles;
using PerformanceEvaluationPlatform.DAL.Repositories.Users;
using PerformanceEvaluationPlatform.DAL.Repositories.FormsData;
using PerformanceEvaluationPlatform.Repositories.Document;
using PerformanceEvaluationPlatform.DAL.Repositories.FieldsGroup;
using PerformanceEvaluationPlatform.DAL.Repositories.Teams;
using PerformanceEvaluationPlatform.DAL.Repositories.Projects;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Http;

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
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddSingleton<IDocumentReposotory, MockDataRepository>();

            services.Configure<DatabaseOptions>(Configuration.GetSection("DatabaseOptions"));
            services.AddDbContext<PepDbContext>();
            services.AddTransient<IExamplesRepository, ExamplesRepository>();
            services.AddTransient<IFormTemplatesRepository, FormTemplatesRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<IFieldsRepository, FieldsRepository>();
            services.AddTransient<ISurveysRepository, SurveysRepository>();
            services.AddTransient<IFieldsGroupRepository, FieldsGroupRepository>();
            services.AddTransient<IDeeplinksRepository, DeeplinksRepository>();
            services.AddTransient<ITeamsRepository, TeamsRepository>();
            services.AddTransient<IFormDataRepository, FormDataRepository>();
            services.AddTransient<IProjectsRepository, ProjectsRepository>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie()
                .AddOpenIdConnect("Auth0", options =>
                {
                    options.Authority = $"https://{Configuration["Auth0:Domain"]}";
                    options.ClientId = Configuration["Auth0:ClientId"];
                    options.ClientSecret = Configuration["Auth0:ClientSecret"];

                    options.ResponseType = OpenIdConnectResponseType.Code;

                    options.Scope.Clear();
                    options.Scope.Add("openid");

                    options.CallbackPath = new PathString("/callback");

                    options.ClaimsIssuer = "Auth0";
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
