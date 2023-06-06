using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Teste_MVC.Data;
using Teste_MVC.Repositories.Produtos;
using Teste_MVC.Dto.Clientes;
using Teste_MVC.Models.Clientes;
using Teste_MVC.Dto.Produtos;
using Teste_MVC.Models.Produtos;
using FluentValidation.AspNetCore;
using Teste_MVC.Validation.Clientes;
using FluentValidation;
using Teste_MVC.Validation;
using Teste_MVC.Validation.Produtos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.CreateMap<CreateClienteDto, Cliente>();
    cfg.CreateMap<UpdateClienteDto, Cliente>();
    cfg.CreateMap<ProdutoDto, Produto>();
});

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ProdutoRepository>();
builder.Services.AddScoped<IRepository<Produto>, ProdutoRepository>();
builder.Services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews()
         .AddFluentValidation();

builder.Services.AddControllers().AddFluentValidation(config =>
{
    config.RegisterValidatorsFromAssemblyContaining<ClienteValidation>();
    config.RegisterValidatorsFromAssemblyContaining<ProdutoValidation>();
});
builder.Services.AddTransient<IValidator<Cliente>, ClienteValidation>();
builder.Services.AddTransient<IValidator<Produto>, ProdutoValidation>();


builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
