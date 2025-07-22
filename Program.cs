using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DulcesChurrascosAPI.Data;
using DulcesChurrascosAPI.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar DbContext con SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Agregar Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient("OpenRouterClient", client =>
{
    client.BaseAddress = new Uri("https://openrouter.ai/");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
});

builder.Services.AddCors(o => o.AddPolicy("AllowReactApp", p =>
{
  p.WithOrigins("http://localhost:5173")
   .AllowAnyMethod()
   .AllowAnyHeader()
   .AllowCredentials();
}));

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// —— PRODUCTOS ——
app.MapGet("/api/productos", async (AppDbContext db) =>
    await db.Productos.ToListAsync());

app.MapGet("/api/productos/{id:int}", async (int id, AppDbContext db) =>
    await db.Productos.FindAsync(id) is Producto prod
        ? Results.Ok(prod)
        : Results.NotFound());

app.MapPost("/api/productos", async (Producto producto, AppDbContext db) =>
{
    db.Productos.Add(producto);
    await db.SaveChangesAsync();
    return Results.Created($"/api/productos/{producto.Id}", producto);
});

app.MapPut("/api/productos/{id:int}", async (int id, Producto input, AppDbContext db) =>
{
    var prod = await db.Productos.FindAsync(id);
    if (prod is null) return Results.NotFound();

    prod.Nombre = input.Nombre;
    prod.Tipo = input.Tipo;
    prod.Modalidad = input.Modalidad;
    prod.TerminoCoccion = input.TerminoCoccion;
    prod.GuarnicionesDisponibles = input.GuarnicionesDisponibles;
    prod.CantidadPorCaja = input.CantidadPorCaja;
    prod.PrecioUnitario = input.PrecioUnitario;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/productos/{id:int}", async (int id, AppDbContext db) =>
{
    var prod = await db.Productos.FindAsync(id);
    if (prod is null) return Results.NotFound();

    db.Productos.Remove(prod);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// —— INVENTARIOS ——
app.MapGet("/api/inventarios", async (AppDbContext db) =>
    await db.Inventarios.Include(i => i.Producto).ToListAsync());

app.MapGet("/api/inventarios/{id:int}", async (int id, AppDbContext db) =>
    await db.Inventarios.Include(i => i.Producto).FirstOrDefaultAsync(i => i.Id == id) is Inventario inv
        ? Results.Ok(inv)
        : Results.NotFound());

app.MapPost("/api/inventarios", async (Inventario inv, AppDbContext db) =>
{
    db.Inventarios.Add(inv);
    await db.SaveChangesAsync();
    return Results.Created($"/api/inventarios/{inv.Id}", inv);
});

app.MapPut("/api/inventarios/{id:int}", async (int id, Inventario input, AppDbContext db) =>
{
    var inv = await db.Inventarios.FindAsync(id);
    if (inv is null) return Results.NotFound();

    inv.ProductoId = input.ProductoId;
    inv.Cantidad = input.Cantidad;
    inv.UnidadMedida = input.UnidadMedida;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/inventarios/{id:int}", async (int id, AppDbContext db) =>
{
    var inv = await db.Inventarios.FindAsync(id);
    if (inv is null) return Results.NotFound();

    db.Inventarios.Remove(inv);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// —— COMBOS ——
app.MapGet("/api/combos", async (AppDbContext db) =>
    await db.Combos.ToListAsync());

app.MapGet("/api/combos/{id:int}", async (int id, AppDbContext db) =>
    await db.Combos.FindAsync(id) is Combo combo
        ? Results.Ok(combo)
        : Results.NotFound());

app.MapPost("/api/combos", async (Combo combo, AppDbContext db) =>
{
    db.Combos.Add(combo);
    await db.SaveChangesAsync();
    return Results.Created($"/api/combos/{combo.Id}", combo);
});

app.MapPut("/api/combos/{id:int}", async (int id, Combo input, AppDbContext db) =>
{
    var combo = await db.Combos.FindAsync(id);
    if (combo is null) return Results.NotFound();

    combo.Nombre = input.Nombre;
    combo.Descripcion = input.Descripcion;
    combo.ProductosIncluidos = input.ProductosIncluidos;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/combos/{id:int}", async (int id, AppDbContext db) =>
{
    var combo = await db.Combos.FindAsync(id);
    if (combo is null) return Results.NotFound();

    db.Combos.Remove(combo);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// —— VENTAS ——
app.MapGet("/api/ventas", async (AppDbContext db) =>
    await db.Ventas.ToListAsync());

app.MapGet("/api/ventas/{id:int}", async (int id, AppDbContext db) =>
    await db.Ventas.FindAsync(id) is Venta venta
        ? Results.Ok(venta)
        : Results.NotFound());

app.MapPost("/api/ventas", async (Venta venta, AppDbContext db) =>
{
    db.Ventas.Add(venta);
    await db.SaveChangesAsync();
    return Results.Created($"/api/ventas/{venta.Id}", venta);
});

app.MapPut("/api/ventas/{id:int}", async (int id, Venta input, AppDbContext db) =>
{
    var venta = await db.Ventas.FindAsync(id);
    if (venta is null) return Results.NotFound();

    venta.Fecha = input.Fecha;
    venta.Detalle = input.Detalle;
    venta.Total = input.Total;

    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/ventas/{id:int}", async (int id, AppDbContext db) =>
{
    var venta = await db.Ventas.FindAsync(id);
    if (venta is null) return Results.NotFound();

    db.Ventas.Remove(venta);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// —— CHURRASCOS ——

app.MapGet("/api/churrascos", async (AppDbContext db) =>
    await db.Churrascos.ToListAsync());

// Obtener por id
app.MapGet("/api/churrascos/{id:int}", async (int id, AppDbContext db) =>
    await db.Churrascos.FindAsync(id) is Churrasco ch
        ? Results.Ok(ch)
        : Results.NotFound());

// Crear con validaciones
app.MapPost("/api/churrascos", async (Churrasco ch, AppDbContext db) =>
{
    // Validar guarniciones
    var guas = ch.Guarniciones
                 .Split(',', StringSplitOptions.RemoveEmptyEntries);
    if (guas.Length != 2)
        return Results.BadRequest("Se requieren exactamente 2 guarniciones.");

    // Asignar modalidad
    ch.Modalidad = ch.Porciones switch
    {
        1 => ModalidadChurrasco.Individual,
        3 => ModalidadChurrasco.Familiar3,
        5 => ModalidadChurrasco.Familiar5,
        _ => throw new ArgumentOutOfRangeException()
    };

    db.Churrascos.Add(ch);
    await db.SaveChangesAsync();
    return Results.Created($"/api/churrascos/{ch.Id}", ch);
});

// Actualizar
app.MapPut("/api/churrascos/{id:int}", async (int id, Churrasco input, AppDbContext db) =>
{
    var ch = await db.Churrascos.FindAsync(id);
    if (ch is null) return Results.NotFound();

    ch.TipoCarne      = input.TipoCarne;
    ch.TerminoCoccion = input.TerminoCoccion;
    ch.Guarniciones   = input.Guarniciones;
    ch.Porciones      = input.Porciones;
    ch.PorcionesExtra = input.PorcionesExtra;
    // (si quieres, recalcula Modalidad aquí también)

    await db.SaveChangesAsync();
    return Results.NoContent();
});

// Eliminar
app.MapDelete("/api/churrascos/{id:int}", async (int id, AppDbContext db) =>
{
    var ch = await db.Churrascos.FindAsync(id);
    if (ch is null) return Results.NotFound();

    db.Churrascos.Remove(ch);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// —— DULCES ——
app.MapPost("/api/dulces", async (Dulce d, AppDbContext db) =>
{
    if (!d.PorUnidad)
    {
        if (d.CantidadPorCaja is not (6 or 12 or 24))
            return Results.BadRequest("Si no es por unidad, la caja debe tener 6, 12 o 24 unidades.");
    }
    db.Dulces.Add(d);
    await db.SaveChangesAsync();
    return Results.Created($"/api/dulces/{d.Id}", d);
});

app.MapGet("/api/dulces/{id:int}", async (int id, AppDbContext db) =>
    await db.Dulces.FindAsync(id) is Dulce d
        ? Results.Ok(d)
        : Results.NotFound());

app.MapPut("/api/dulces/{id:int}", async (int id, Dulce input, AppDbContext db) =>
{
    var d = await db.Dulces.FindAsync(id);
    if (d is null) return Results.NotFound();
    d.TipoDulce = input.TipoDulce;
    d.PorUnidad = input.PorUnidad;
    d.CantidadPorCaja = input.CantidadPorCaja;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/dulces/{id:int}", async (int id, AppDbContext db) =>
{
    var d = await db.Dulces.FindAsync(id);
    if (d is null) return Results.NotFound();
    db.Dulces.Remove(d);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPost("/api/chat", async (
    AppDbContext db,
    IHttpClientFactory httpClientFactory,
    ChatRequest req,
    ILogger<Program> logger) =>
{
    try
    {
        // 1. Obtener datos reales del sistema
        var productos = await db.Productos
            .Select(p => p.Nombre)
            .ToListAsync();

        var combos = await db.Combos
            .Select(c => $"{c.Nombre}: {c.Descripcion}")
            .ToListAsync();

        // 2. Crear contexto con productos y combos
        var contexto = $"Productos disponibles: {string.Join(", ", productos)}. " +
                       $"Combos disponibles: {string.Join(" | ", combos)}.";

        // 3. Incluir el contexto como mensaje 'system'
        var mensajesConContexto = new List<ChatMessage>
        {
            new ChatMessage
            {
                role = "system",
                content = $"Eres un asistente del restaurante Dulces Churrascos. Tu función es ayudar al usuario con base en este contexto del menú actual: {contexto}"
            }
        };

        // 4. Agregar mensajes originales del usuario
        mensajesConContexto.AddRange(req.messages);

        // 5. Construir el JSON final para OpenRouter
        var jsonRequest = JsonSerializer.Serialize(new
        {
            model = "qwen/qwen3-235b-a22b-07-25:free",
            messages = mensajesConContexto,
            stream = false
        });

        var body = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        var client = httpClientFactory.CreateClient("OpenRouterClient");
        var response = await client.PostAsync("/api/v1/chat/completions", body);

        var json = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("❌ Error de OpenRouter: {Error}", json);
            return Results.Problem($"OpenRouter error: {json}");
        }

        return Results.Content(json, "application/json");
    }
    catch (Exception ex)
    {
        logger.LogError("❗ Error en /api/chat: {Message}", ex.Message);
        return Results.Problem("Error interno en el servidor.");
    }
});

app.Run();

public class ChatRequest
{
    public List<ChatMessage> messages { get; set; } = new();
}

public class ChatMessage
{
    public string role { get; set; }
    public string content { get; set; }
}
