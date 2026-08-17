using Microsoft.EntityFrameworkCore;
using NatilleraBE.Data;
using NatilleraBE.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<clsSocio>();
builder.Services.AddScoped<clsPago>();
builder.Services.AddScoped<clsInteresPago>();
builder.Services.AddScoped<clsPolla>();
builder.Services.AddHttpClient<clsPolla>();
builder.Services.AddScoped<clsPrestamo>();
builder.Services.AddScoped<clsAbonos>();
builder.Services.AddScoped<clsInteresPrestamo>();
builder.Services.AddScoped<clsBanco>();

builder.Services.AddDbContext<NatilleraDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("CadenaSQL")
    ));

builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.UseCors(builder =>
    builder.WithOrigins("http://localhost:5173",
    "https://natillera-fe.vercel.app"
    )
           .AllowAnyHeader()
           .AllowAnyMethod());

app.MapControllers();

app.Run();