using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using MovieBackendManagementSystem.Api.DbContexts;
using MovieBackendManagementSystem.Api.Services;
using MovieBackendManagementSystem.Api.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserService,UserService>();
builder.Services.AddTransient<UserDbContext>();
builder.Services.AddScoped<IUploadService,UploadService>();
builder.Services.AddScoped<ISearchFilmService,SearchFilmService>();
builder.Services.AddScoped<IDeleteFilmService,DeleteFilmService>();
builder.Services.AddScoped<IGetNewsServices,GetNewsServices>();
builder.Services.AddScoped<IDeleteNewService,DeleteNewService>();
builder.Services.AddScoped<IUpdateNewService,UpdateNewService>();
builder.Services.AddScoped<IGetFilmTypeService,GetFilmTypeService>();
builder.Services.AddCors(c=>c.AddPolicy("any",p=>p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
// 设置Kestrel服务器的请求体大小限制
builder.Services.Configure<KestrelServerOptions>(options =>
    {
        options.Limits.MaxRequestBodySize = null; // 取消限制，允许任意大小的文件
        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5); // 设置5分钟的超时时间
    });
builder.Services.Configure<FormOptions>(options =>
    {
        options.ValueLengthLimit = int.MaxValue; // 设置表单字段的最大长度
        options.MultipartBodyLengthLimit = long.MaxValue; // 设置多部分请求体的最大长度
        options.MultipartHeadersLengthLimit = int.MaxValue; // 设置多部分请求头的最大长度
    });

builder.Services.AddHttpContextAccessor();
// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();

app.UseCors("any");

// 配置静态文件中间件
app.UseStaticFiles();

// 应用启动时打开浏览器
if (app.Environment.IsDevelopment())
{
    var url = "file:///C:/Users/Administrator/Desktop/MovieBackendManagementSystem/MovieBackendManagementSystem.Api/wwwroot/Homepage.html"; 
    ProcessStartInfo startInfo = new ProcessStartInfo(url);
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        startInfo.UseShellExecute = true;
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
    {
        startInfo.FileName = "xdg-open";
        startInfo.Arguments = url;
    }

    Process.Start(startInfo);
}


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
