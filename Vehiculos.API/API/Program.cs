
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos;
using DA;
using DA.Repositorios;
using Flujo;
using Reglas;
using Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;  
using Microsoft.IdentityModel.Tokens;
using System.Text;                                   
using Autorizacion.Middleware;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Leer configuración JWT y registrar autenticación
            var tokenConfig = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenConfig.Issuer,
                        ValidAudience = tokenConfig.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                                                       Encoding.UTF8.GetBytes(tokenConfig.key))
                    };
                });


            // Add services to the container.

            builder.Services.AddControllers();

            // Esto lo resolví junto con Andres!!
            // Para evitar el error de certificado SSL al consumir el mockAPI

            builder.Services.AddHttpClient("ServicioRegistro")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });

            builder.Services.AddHttpClient("ServicioRevision")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpClient();

            builder.Services.AddScoped<IVehiculoFlujo, VehiculoFlujo>();
            builder.Services.AddScoped<IVehiculoDA, VehiculoDA>();
            builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
            builder.Services.AddScoped<IRegistroServicio, RegistroServicio>();
            builder.Services.AddScoped<IRevisionServicio, RevisionServicio>();
            builder.Services.AddScoped<IConfiguracion, Configuracion>();
            builder.Services.AddScoped<IRegistroReglas, RegistroReglas>();
            builder.Services.AddScoped<IRevisionReglas, RevisionReglas>();
            builder.Services.AddScoped<IMarcaDA, MarcaDA>();
            builder.Services.AddScoped<IMarcaFlujo, MarcaFlujo>();
            builder.Services.AddScoped<IModeloFlujo, ModeloFlujo>();
            builder.Services.AddScoped<IModeloDA, ModeloDA>();

            // Registrar servicios del paquete de Autorización
            builder.Services.AddTransient<Autorizacion.Abstracciones.Flujo.IAutorizacionFlujo,
                                           Autorizacion.Flujo.AutorizacionFlujo>();
            builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA,
                                           Autorizacion.DA.SeguridadDA>();
            builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper,
                                           Autorizacion.DA.Repositorios.RepositorioDapper>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.AutorizacionClaims();  // NUEVO — ANTES de UseAuthorization

            app.UseAuthorization(); 

            app.MapControllers();

            app.Run();
        }
    }
}
