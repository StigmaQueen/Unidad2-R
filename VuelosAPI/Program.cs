using Microsoft.EntityFrameworkCore;
using VuelosAPI.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<sistem21_vuelosContext>(optionsBuilder =>

    optionsBuilder.UseMySql("server=sistemas19.com;database=sistem21_vuelos;user=sistem21_cecy;password=impresiones16", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.5.17-mariadb")),
    ServiceLifetime.Transient
    );
var app = builder.Build();

app.UseRouting();
app.UseFileServer();
app.UseEndpoints(x => x.MapControllers());
app.Run();