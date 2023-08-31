using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


//app.MapGet("/", () => "Hello World!");
//adiciona  o servico de controllers 
builder.Services.AddControllers();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(options =>
{
    // adiciona informacoes sobre a api no swagger
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "APi para gerenciamento de filmes- introducao a 2 sprint",
        Contact = new OpenApiContact
        {
            Name = "Joao Oliveira",
            Url = new Uri("https://github.com/ojuaum1")
        }
    });

    //configura o swagger para usar o arquivo xml gerado
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
// adiciona mapeamento dos controllers
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
