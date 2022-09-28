using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Rest.Api.Mapper;
using Rest.Api.Middleware;
using Rest.Api.Swagger;
using Rest.Domain.App.Interfaces.Repository;
using Rest.Domain.App.Interfaces.Service;
using Rest.Domain.App.Interfaces.Service.Authentication;
using Rest.Infra.CrossCutting;
using Rest.Infra.CrossCutting.Utils;
using Rest.Infra.Data.Context;
using Rest.Infra.Data.Repository;
using Rest.Service;
using Rest.Service.Authentication;
using Rest.Service.Caching;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var primitiveToken = builder.Configuration.GetSection("PrimitiveToken").Get<PrimitiveToken>();
builder.Services.AddSingleton(primitiveToken);

var redisConnection = builder.Configuration.GetSection("Redis").Get<RedisSettings>();

var multiplexer = ConnectionMultiplexer.Connect($"{redisConnection.Endpoint}:{redisConnection.Port}");

builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddDbContext<BaseContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
    options.UseLazyLoadingProxies();
});

#region RepositoriesRegister
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
#endregion

#region ServiceRegister
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<ICustomerCacheService, CustomerCacheService>();
#endregion

builder.Services.AddAutoMapper(config => 
{
    config.ModelMapperConfiguration();
});

builder.Services.AddCors();
builder.Services.AddControllers()
.AddJsonOptions(opt => 
{
    opt.AllowInputFormatterExceptionMessages = true;
    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
    opt.JsonSerializerOptions.WriteIndented = true;
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.AddAuthentication(options => 
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(primitiveToken.BaseSecret)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    // Retorna os headers "api-supported-versions" e "api-deprecated-versions"
    // indicando versões suportadas pela API e o que está como deprecated
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    // Agrupar por número de versão
    options.GroupNameFormat = "'v'VVV";
    // Necessário para o correto funcionamento das rotas
    options.SubstituteApiVersionInUrl = true;
});

//Registra as opções do Swagger para serem construidas em tempo de execução
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

builder.Services.AddSwaggerGen(options =>
{
    // add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseStaticFiles();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            options.SwaggerEndpoint(
                url: $"/swagger/{description.GroupName}/swagger.json", 
                name: description.GroupName.ToUpperInvariant()
            );
        }

        options.DocExpansion(DocExpansion.List);
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
