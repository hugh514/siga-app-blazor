using SigaApp.Components;
using SigaApp.Configs;
using SigaApp.Models.Estudante;
using SigaApp.Models.Professor;
using SigaApp.Models.Turma;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<Conexao>();
builder.Services.AddScoped<ProfessorDAO>();
builder.Services.AddScoped<TurmaDAO>();
builder.Services.AddScoped<EstudanteDAO>();

// ✔ REGISTRA O HTTP CLIENT NECESSÁRIO PARA O SERVIÇO
builder.Services.AddHttpClient<LocalidadeService>();

// ✔ O SEU SERVIÇO
builder.Services.AddScoped<LocalidadeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
