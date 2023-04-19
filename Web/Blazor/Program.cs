using Blazor;
using Blazor.Interfaces;
using Blazor.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//instanciamos un objeto de la clase config
Config cadena = new Config(builder.Configuration.GetConnectionString("MySQL"));
builder.Services.AddSingleton(cadena);

//Configurar los servicios
builder.Services.AddScoped<ILoginServicio, LoginServicio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); //Agregar el tipo de autorizacion
builder.Services.AddHttpContextAccessor();
builder.Services.AddResponseCompression();
builder.Services.AddControllers();
builder.Services.AddSweetAlert2(); //Servicio de sweet alert 2

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); //agregar autenticacion
app.UseAuthorization(); //agregar la autorizacion
app.MapControllers(); //Agregar por el uso de controller
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
