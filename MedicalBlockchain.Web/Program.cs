using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

using MedicalBlockchain.DAL.Data;
using MedicalBlockchain.BLL.Interfaces;
using MedicalBlockchain.BLL.Implementations;
using MedicalBlockchain.BLL.Crypto.Interfaces;
using MedicalBlockchain.BLL.Crypto.Implementations;
using MedicalBlockchain.DAL.Abstractions.Interafces;
using MedicalBlockchain.DAL.Abstractions.Implementations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MSSQL")));

// Dependency injection
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBlockChainService, BlockChainService>();
builder.Services.AddScoped<IPatientService, PatientService>();

// Add Auth
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Auth/Login";
	});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Patient}/{action=Index}/{id?}");

app.Run();
