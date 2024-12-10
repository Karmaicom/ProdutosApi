using ProdutosApi.Infra.Data.Interfaces;
using ProdutosApi.Infra.Data.Repositories;

namespace ProdutosApi.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy", policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            #region Configuração de injeção de dependência

            // captura o endereço da connectionString
            var connectionString = builder.Configuration.GetConnectionString("ProdutosApi");

            // injetar no construtor da classe
            builder.Services.AddTransient<IProdutoRepository>(x => new ProdutoRepository(connectionString));

            #endregion

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("DefaultPolicy");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
