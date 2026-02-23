using Infrastructure.Configurations; 
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerSecurity();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Badminton Booking API v1");
});

app.UseStaticFiles(); // serve wwwroot (avatars, ...)
app.UseCors("AllowAll");
app.UseHttpsRedirection();

// Xác thực & Phân quyền
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();