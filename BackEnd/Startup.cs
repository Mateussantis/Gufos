using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


// Instalamos o Entity FrameWork
// dotnet tool install --global dotnet-ef

// Baixamos o pacote SQL Server do Entity FrameWork
// dotnet add package Microsoft.EntityFrameWorkCore.SqlServer

// Baixamos o pacote que ira escrever nossos codigos 
// dotnet add package Microsoft.EntityFrameWorkCore.Design 

// Confirmar se foi instalado
// dotnet restore

// Testamos a instalação do EntityFrameWork
// dotnet-ef

// Codigo que criara o nosso Contexto(Contexto da conexão) da Base de Dados e nossos Models
// -o (Cria o Diretorio) || -d (Datanotation nas composiçoes(Tamanhos, tipos, variaveis locais do Banco de Dados))
// dotnet ef dbcontext scaffold "Server=N-1S-DEV-07\SQLEXPRESS; Database=Gufos; User Id=sa; Password=132" Microsoft.EntityFrameWorkCore.SqlServer -o Models -d

// SWAGGER - Documentação
// Instalamos o pacote
// dotnet add BackEnd.csproj package  Swashbuckle.AspNetCore -v 5.0.0-rc4

// JWT - JASON WEB Token
// Adicionamos o pacote jwt
// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 3.0.0

namespace BackEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configuramos como os objetos relacionados apareceram nos reornos
            services.AddControllersWithViews().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Configuramos o Swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo{Title = "API", Version = "V1"});

                // Definimos o caminho e o arquivo temporario de documentação 
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Condiguramos o JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options => {
                    options.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Usamos efetivamente o SWAGGER
            app.UseSwagger();
            // Especificamos o Endpoint na aplicação
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
