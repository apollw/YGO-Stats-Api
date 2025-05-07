using Microsoft.Extensions.DependencyInjection;
using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;
using Supabase.Realtime;
using Supabase.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Carrega as variáveis de ambiente (ou user‐secrets)
var supabaseUrl = builder.Configuration["SUPABASE_URL"]!;
var supabaseKey = builder.Configuration["SUPABASE_KEY"]!;

// 2. Inicializa o client Supabase
var supabase = new Supabase.Client(
    supabaseUrl,
    supabaseKey,
    new Supabase.SupabaseOptions { AutoConnectRealtime = true }
);
await supabase.InitializeAsync();

// 3. Registra no DI
builder.Services.AddSingleton<ISupabaseClient<User, Session, RealtimeSocket, RealtimeChannel, Bucket, FileObject>>(_ => supabase);

var app = builder.Build();

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
