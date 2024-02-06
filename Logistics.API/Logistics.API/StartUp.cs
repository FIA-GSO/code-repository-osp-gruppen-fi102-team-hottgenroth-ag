using Logisitcs.BLL;
using Logisitcs.BLL.Interfaces;
using Logisitcs.DAL;
using Logisitcs.DAL.Interfaces;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Logistics.API
{
   public class Startup
   {
      public IConfiguration Configuration { get; }
      private IWebHostEnvironment CurrentEnvironment { get; }

      public Startup(IConfiguration configuration, IWebHostEnvironment env)
      {
         Configuration = configuration;
         CurrentEnvironment = env;


         Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);//required for the font loading in itext7 (used for pdf generation)
      }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         //Register Mimetypes
         services.AddResponseCompression(options =>
         {
            options.MimeTypes = new[] {
               "application/json"
            };
            options.EnableForHttps = true;
         });

         #region BLL_DAL_DI
         //DAL
         services.AddTransient<IProjectDAL>(provider => new ProjectDAL());
         services.AddTransient<ITransportboxDAL>(provider => new TransportboxDAL());
         services.AddTransient<IPDFDAL>(provider => new PDFDAL());
         services.AddTransient<IArticleDAL>(provider => new ArticleDAL());


         //BLL
         services.AddTransient<IProjectBLL>(provider => new ProjectBLL(provider.GetService<IProjectDAL>()));
         services.AddTransient<ITransportboxBLL>(provider => new TransportboxBLL(provider.GetService<ITransportboxDAL>()));
         services.AddTransient<IPDFBLL>(provider => new PDFBLL(provider.GetService<IPDFDAL>()));
         services.AddTransient<IArticleBLL>(provider => new ArticleBLL(provider.GetService<IArticleDAL>()));

         #endregion

         #region Json

         // Add Newtonsoft JSON
         services.AddControllers().AddNewtonsoftJson(options =>
         {
            // Configure a custom converter
            /*options.SerializerSettings.Converters.Add(new JsonConverter<INTERFACE, OBJECT>());*/

            options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
         });

         #endregion



         services.AddSwaggerGen(opt =>
         {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Logistics.API", Version = "v1" });
            opt.CustomSchemaIds(type => type.ToString());
         });

         services.AddHttpClient();
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {

         app.UseResponseCompression();
         app.UseCors(options =>
            options.WithOrigins("http://localhost:4200", "https://localhost:44349")
            .AllowAnyMethod()
            .AllowAnyHeader());

         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            app.UseSwaggerUI(c =>
            {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "WApp.Server V1");
            });
         }
         else
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseHttpsRedirection();

         app.UseAuthentication();
         app.UseRouting();
         app.UseAuthorization();
         app.UseEndpoints(endpoints =>
         {
            endpoints.MapControllers();
         });
      }
   }
}
