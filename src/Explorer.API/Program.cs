using Explorer.API.Controllers.ProtoControllers;
using Explorer.API.Startup;
using GrpcServiceTranscoding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureSwagger(builder.Configuration);
const string corsPolicy = "_corsPolicy";
builder.Services.ConfigureCors(corsPolicy);
builder.Services.ConfigureAuth();

builder.Services.RegisterModules();

builder.Services.AddGrpc(); //

//builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddGrpc(options =>
{
    options.EnableDetailedErrors = true; // detaljnije greske u razvojnom okruzenju
}).AddJsonTranscoding();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseCors(corsPolicy);
//app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.MapGrpcService<TourProtoController>();

app.Run();

// Required for automated tests
namespace Explorer.API
{
    public partial class Program { }
}