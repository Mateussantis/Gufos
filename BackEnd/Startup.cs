using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


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
            services.AddControllersWithViews().AddNewtonsoftJson(
                opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
