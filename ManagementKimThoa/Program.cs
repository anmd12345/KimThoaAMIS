using ManagementKimThoa.Contexts;
using ManagementKimThoa.Repositories;
using ManagementKimThoa.Repositories.Interfaces;
using ManagementKimThoa.Services;
using ManagementKimThoa.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ManagementKimThoa.Attributes.AuthorizeAttribute>();
})
.AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    );
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();

builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IBranchService, BranchService>();


builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IFileService, FileService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<INoteRepository, NoteRepository>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseStatusCodePagesWithReExecute("/");

app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine("================================");
    Console.WriteLine($"Time      : {DateTime.Now}");
    Console.WriteLine($"ProcessId : {Environment.ProcessId}");

    await context.Session.LoadAsync();

    Console.WriteLine($"SessionId : {context.Session.Id}");

    var user = context.Session.GetString("CurrentUser");

    Console.WriteLine($"CurrentUser Exists: {!string.IsNullOrEmpty(user)}");

    await next();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();