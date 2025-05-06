using Infrastructure;
using Microsoft.EntityFrameworkCore;
using PolymorphicResponse_API;
using Services.Implementation;
using Services.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DB_Local");

builder.Services.AddControllers().AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.DefaultIgnoreCondition =
        JsonIgnoreCondition.WhenWritingNull;
}); 

builder.Services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    db.Database.Migrate();
    SeedData.Initialize(db, countPerType: 30);  // tweak how many of each type
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
