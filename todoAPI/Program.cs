using AutoMapper;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Comunes.Clases.Exepciones;
using todoAPI.Dominio.services.General;
using todoAPI.Dominio.services.Todo;
using todoAPI.Infraestructura.General;
using todoAPI.Infraestructura.Todos;
using todoAPI.Mapper;
using todoAPI.models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<TodoDBSettings>(
    builder.Configuration.GetSection("TodoDataBase")
);

// Container.InyeccionDependencias(builder.Services, builder.Configuration);

var configMapper = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilAutoMapper()));
var mapper = configMapper.CreateMapper();
builder.Services.AddSingleton(mapper);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICrudService<TodoContract>,TodoService>();
builder.Services.AddScoped<ITodoService,TodoService>();
builder.Services.AddScoped<ICrudRepository<TodoEntity>,TodoRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<MiddlewareExcepciones>();

app.UseAuthorization();

app.MapControllers();

app.Run();
