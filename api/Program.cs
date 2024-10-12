var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHotelService, HotelService>();
builder.Services.AddControllers();

// Optionally, you can configure Swagger/OpenAPI for testing the API endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// Uncomment the following line if you want to redirect HTTP to HTTPS
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();