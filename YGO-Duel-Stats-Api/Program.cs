using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;
using Supabase;
using Supabase.Gotrue;
using Supabase.Interfaces;
using Supabase.Realtime;
using Supabase.Storage;
using System;
using YGO_Duel_Stats_Api.Interfaces;
using YGO_Duel_Stats_Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 1. Carrega as variáveis de ambiente (ou user‐secrets)
var supabaseUrl = builder.Configuration["SUPABASE_URL"]!;
var supabaseKey = builder.Configuration["SUPABASE_KEY"]!;

// 2. Inicializa o client Supabase
var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};
var supabase = new Supabase.Client(supabaseUrl, supabaseKey, options);
await supabase.InitializeAsync();

// 3. Registra no DI
builder.Services.AddSingleton(supabase);

// registra os repositórios
builder.Services.AddSingleton<IDuelistRepository, DuelistRepository>();
builder.Services.AddSingleton<IDeckRepository, DeckRepository>();
builder.Services.AddSingleton<IDuelRepository, DuelRepository>();

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
