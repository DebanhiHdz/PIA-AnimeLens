using Backend;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Registra DataAccess con inyección de dependencias
builder.Services.AddScoped<DataAccess>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configurar el middleware de HTTPS
app.UseHttpsRedirection();

// Configurar otros middlewares
app.UseAuthorization();
app.MapControllers();

app.Run();
