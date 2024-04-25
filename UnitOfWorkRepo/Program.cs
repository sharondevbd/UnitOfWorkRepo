using UnitOfWork.Services.Interfaces;
using UnitOfWork.Services;
using UnitOfWork.Infrastructure.ServiceExtension;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
object value = builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddScoped<IProductService, ProductService>();

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers().AddNewtonsoftJson(x =>
//{
//    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
//    x.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DbContextClass>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddCors(options =>
{
    options.AddPolicy("Restaurent",
                      policy =>
                      {
                          policy.WithOrigins("*");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();

                      });
});
var app = builder.Build();
app.UseCors("Restaurent");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
