using AutoMapper;
using todoAPI.Comunes.Clases.Contracts;
using todoAPI.Comunes.Clases.Exepciones;
using todoAPI.Comunes.Clases.Helper;
using todoAPI.Dominio.services.General;
using todoAPI.Dominio.services.Login;
using todoAPI.Dominio.services.Todo;
using todoAPI.Dominio.services.Usuarios;
using todoAPI.Infraestructura.General;
using todoAPI.Infraestructura.Todos;
using todoAPI.Infraestructura.Usuarios;
using todoAPI.Mapper;
using todoAPI.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

#region [DataBases]
builder.Services.Configure<TodoDBSettings>(
    builder.Configuration.GetSection("TodoDataBase")
);
#endregion

#region [AutoMapper]
var configMapper = new MapperConfiguration(cfg => cfg.AddProfile(new PerfilAutoMapper()));
var mapper = configMapper.CreateMapper();
builder.Services.AddSingleton(mapper);
#endregion

#region [CORS]
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });
});
#endregion

#region [JSON Web Token]
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [Inyeccion de Dependencias]
builder.Services.AddScoped<ICifradoHelper, CifradoHelper>();
builder.Services.AddScoped<ICrudService<TodoContract>,TodoService>();
builder.Services.AddScoped<ITodoService,TodoService>();
builder.Services.AddScoped<ICrudRepository<TodoEntity>,TodoRepository>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ICrudRepository<UsuarioEntity>, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ICrudService<UsuarioContract>, UsuarioService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ILoginService, LoginService>();
#endregion



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseMiddleware<MiddlewareExcepciones>();

app.UseAuthorization();

app.MapControllers();

app.Run();
