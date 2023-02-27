using BackEndAPI.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using BackEndAPI.Services.Contrato;
using BackEndAPI.Services.Implementacion;
using AutoMapper;
using BackEndAPI.DTOs;
using BackEndAPI.Utilidades;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Server = LIFE9\\SQLEXPRESS; DataBase = cobeca; Trusted_Connection = true; TrustServerCertificate = true

builder.Services.AddDbContext<CobecaContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

builder.Services.AddScoped<ICasaService, CasaService>();
builder.Services.AddScoped<IRegistroService, RegistroService>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddCors(options => {
    options.AddPolicy("NuevaPolitica", app =>
    {
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


#region PETICIONES API REST
app.MapGet("/casa/lista", async (
    ICasaService _casaServicio,
    IMapper _mapper
    ) =>
{
    var listaCasa = await _casaServicio.GetList();
    var listaCASADTO = _mapper.Map<List<CasaDTO>>(listaCasa);

    if(listaCASADTO.Count > 0)
        return Results.Ok(listaCASADTO);
    else
        return Results.NotFound();
});

app.MapGet("/registro/lista", async (
    IRegistroService _registroServicio,
    IMapper _mapper
    ) =>
{
    var listaRegistro = await _registroServicio.GetList();
    var listaREGISTRODTO = _mapper.Map<List<RegistroDTO>>(listaRegistro);

    if (listaREGISTRODTO.Count > 0)
        return Results.Ok(listaREGISTRODTO);
    else
        return Results.NotFound();
});

app.MapPost("/registro/guardar", async (
    RegistroDTO modelo,
    IRegistroService _registroServicio,
    IMapper _mapper
    ) => { 
        var _registro = _mapper.Map<Registro>(modelo);
        var _registroCreado = await _registroServicio.Add(_registro);

        if (_registroCreado.IdRegistro != 0)
            return Results.Ok(_mapper.Map<RegistroDTO>(_registroCreado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
app.MapPut("/registro/actualizar/{id_registro}", async (
    int id_registro,
    RegistroDTO modelo,
    IRegistroService _registroServicio,
    IMapper _mapper

    ) => {
        var _encontrado = await _registroServicio.Get(id_registro);

        if(_encontrado is null)return Results.NotFound();

        var _registro = _mapper.Map<Registro>(modelo);

        _encontrado.Nombre = _registro.Nombre;
        _encontrado.Apellido = _registro.Apellido;
        _encontrado.Identificacion = _registro.Identificacion;
        _encontrado.Edad = _registro.Edad;
        _encontrado.RefCasa = _registro.RefCasa;

        var respuesta = await _registroServicio.Update(_encontrado);

        if (respuesta)
            return Results.Ok(_mapper.Map<RegistroDTO>(_encontrado));
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
    });
app.MapDelete("/registro/eliminar/{id_registro}", async (
        int id_registro,
        IRegistroService _registroServicio
    ) => {
        var _encontrado = await _registroServicio.Get(id_registro);

        if(_encontrado is null) return Results.NotFound();

        var respuesta = await _registroServicio.Delete(_encontrado);

        if (respuesta)
            return Results.Ok();
        else
            return Results.StatusCode(StatusCodes.Status500InternalServerError);
});
#endregion

app.UseCors("NuevaPolitica");

app.Run();
