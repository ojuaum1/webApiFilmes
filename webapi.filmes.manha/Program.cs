using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


//app.MapGet("/", () => "Hello World!");
//adiciona  o servico de controllers 
builder.Services.AddControllers();

//Adiciona Serviço de Jwt Bearer (forma de autenticação)
builder.Services.AddAuthentication(options =>
{
    options.DefaultChallengeScheme = "JwtBearer";
    options.DefaultAuthenticateScheme = "JwtBearer";

})

.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //Valida quem esta solicitando  
        ValidateIssuer = true,

        //Valida quem esta recebendo
        ValidateAudience = true,

        //Valida se o tempo de expiração será validado
        ValidateLifetime = true,

        //Forma decriptografia que valida a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filme-chave-autenticacao-webapi-dev")),

        //Valida o tempo de expiração do Token
        ClockSkew = TimeSpan.FromMinutes(5),

        //Nome do issuer (de onde está vindo)
        ValidIssuer = "webapi.filmes.manha",

        //Nome do Audience (para onde está indo)
        ValidAudience = "webapi.filmes.manha",

    };

});

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

    //Usando a autenticaçao no Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

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



//Finaliza a configuraçao do swagger

//adiciona mapeamento dos controllers
app.MapControllers();

// adiciona autenticação
app.UseAuthentication();

app.UseAuthorization();

app.UseHttpsRedirection();

app.Run();