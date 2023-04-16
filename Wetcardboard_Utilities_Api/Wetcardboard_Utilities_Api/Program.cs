using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Wetcardboard_Database.Connector;
using Wetcardboard_Shared.Security.Jwt;
using Wetcardboard_Utilities_Api.Services.Implementations;
using Wetcardboard_Utilities_Api.Services.Interfaces;
using Wetcardboard_Utilities_Database.Connector;
using Wetcardboard_Utilities_Models.System;

IJwtFunctions CreateJwtFunctions(ConfigurationManager conf)
{
    var jwtConf = conf.GetSection("Jwt");

    var issuer = jwtConf.GetValue<string>("Issuer");
    var audience = jwtConf.GetValue<string>("Audience");
    var key = jwtConf.GetValue<string>("Key");
    if (string.IsNullOrEmpty(issuer)
        || string.IsNullOrEmpty(audience)
        || string.IsNullOrEmpty(key))
    {
        throw new ArgumentException("Jwt: One or more values in appsetting were not provided");
    }

    var res = new JwtFunctions(issuer, audience, key);
    return res;
}
Wetcardboard_Utilities_System_Props CreateSystemProps(ConfigurationManager conf)
{
    var systemIdentifier = conf.GetValue<string>("SystemIdentifier");

    if (string.IsNullOrEmpty(systemIdentifier))
    {
        throw new ArgumentException("SystemProps: One or more values for in appsetting were not provided");
    }

    var res = new Wetcardboard_Utilities_System_Props(systemIdentifier);
    return res;
}
DbConn_Wetcardboard_Utilities_MySql GetDbConn_WcUtil(ConfigurationManager conf)
{
    var connStr_WcUtil_MySql = conf.GetConnectionString("wetcardboard_utilities_mysql");
    if (string.IsNullOrEmpty(connStr_WcUtil_MySql))
    {
        throw new ArgumentException("One or more values in appsetting were not provided");
    }

    var dbConn_MySql = new DbConn_MySql();
    dbConn_MySql.SetConnectionString(connStr_WcUtil_MySql);
    var dbConn_WcUtil = new DbConn_Wetcardboard_Utilities_MySql(dbConn_MySql);
    return dbConn_WcUtil;
}
void AddAuthentication(WebApplicationBuilder builder, ConfigurationManager conf)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        var jwtConf = conf.GetSection("Jwt");

        var issuer = jwtConf.GetValue<string>("Issuer");
        var audience = jwtConf.GetValue<string>("Audience");
        var key = jwtConf.GetValue<string>("Key");
        if (string.IsNullOrEmpty(issuer)
            || string.IsNullOrEmpty(audience)
            || string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("One or more values in appsetting were not provided");
        }

        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });
}

var builder = WebApplication.CreateBuilder(args);

var conf = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton(CreateJwtFunctions(conf));
builder.Services.AddSingleton(CreateSystemProps(conf));
builder.Services.AddScoped<ITokenService, TokenService>();

AddAuthentication(builder, conf);
builder.Services.AddAuthorization();

// Db Services
builder.Services.AddSingleton<IDbConn_Wetcardboard_Utilities>(GetDbConn_WcUtil(conf));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
