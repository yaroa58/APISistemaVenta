using SistemaVenta.IOC;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.inyectarDependencias(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    { //para permitir entrada de cualquier origen 
        app.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// activar entrada cualquier origen

app.UseCors("NuevaPlitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
