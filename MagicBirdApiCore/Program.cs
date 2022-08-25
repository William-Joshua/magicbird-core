using Microsoft.OpenApi.Models;
using MagicBirdApiCore.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//EF Core 小坑：DbContextPool 会引起数据库连接池连接耗尽 - 博客园 at https://www.cnblogs.com/dudu/p/10398225.html
builder.Services.AddDbContextPool<MTSLoggerCenterContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MagicBirdConnection"));
}, poolSize: 64);
builder.Services.AddScoped<DbContext, MTSLoggerCenterContext>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MagicBird Api",
        Description = "MagicBird Api Summary",
        TermsOfService = new Uri("https://github.com/William-Joshua")
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
