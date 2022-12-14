var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAuthentication().AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:690080255204-884gbfoohcgdvv7e9aknlotmli2uggme.apps.googleusercontent.com"];
    options.ClientSecret = builder.Configuration["Authentication:Google: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"];
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
