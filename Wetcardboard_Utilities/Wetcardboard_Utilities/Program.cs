using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Wetcardboard_Authentication.Authenticator;
using Wetcardboard_Authentication.Authenticator.Azure_AD_OAuth2;
using Wetcardboard_Database.Connector;
using Wetcardboard_Shared.Http;
using Wetcardboard_Shared.Logging;
using Wetcardboard_Shared.Security.Jwt;
using Wetcardboard_Utilities.Database.Implementations;
using Wetcardboard_Utilities.Database.Interfaces;
using Wetcardboard_Utilities_Api_Services.Implementations;
using Wetcardboard_Utilities_Api_Services.Interfaces;
using Wetcardboard_Utilities_Database.Connector;
using Wetcardboard_Utilities_Models.Front_End;
using Wetcardboard_Utilities_Models.System;

Auth_DtModelProps_Azure_AD_OAuth2_Auth GetAzureAdAuthProps(ConfigurationManager conf)
{
    var azureAdSett = conf.GetSection("Azure").GetSection("Ad_Login");
    var azureAdAuthSett = azureAdSett.GetSection("Authorization");

    var tenant = azureAdSett.GetValue<string>("tenant");
    var clientId = azureAdSett.GetValue<string>("client_id");
    var clientSecret_timeReg = azureAdSett.GetValue<string>("client_secret_timeReg");

    var codeChallenge = azureAdAuthSett.GetValue<string>("code_challenge");
    var codeChallengeMode = azureAdAuthSett.GetValue<string>("code_challenge_method");
    var codeVerifier = azureAdAuthSett.GetValue<string>("code_verifier");
    var grantType = azureAdAuthSett.GetValue<string>("grant_type");
    var noncePool = azureAdAuthSett.GetValue<string>("nonce_pool");
    var redirectUri = string.Empty; // azureAdAuthSett.GetValue<string>("redirect_uri");
    var responseMode = azureAdAuthSett.GetValue<string>("response_mode");
    var responseType = azureAdAuthSett.GetValue<string>("response_type");
    var scope = azureAdAuthSett.GetValue<string>("scope");
    var state = azureAdAuthSett.GetValue<string>("state");
    var urlAuthCodePart1 = azureAdAuthSett.GetValue<string>("url_authCode_part1");
    var urlAuthCodePart2 = azureAdAuthSett.GetValue<string>("url_authCode_part2");
    var urlAuthCodeRedirectUri = azureAdAuthSett.GetValue<string>("url_authCode_redirectUri");
    var urlGraphApi = azureAdAuthSett.GetValue<string>("url_graphApi");
    var urlIdTokenPart1 = azureAdAuthSett.GetValue<string>("url_idToken_part1");
    var urlIdTokenPart2 = azureAdAuthSett.GetValue<string>("url_idToken_part2");
    var urlIdTokenRedirectUri = azureAdAuthSett.GetValue<string>("url_idToken_redirectUri");
    var urlSignOutPart1 = azureAdAuthSett.GetValue<string>("url_signOut_part1");
    var urlSignOutPart2 = azureAdAuthSett.GetValue<string>("url_signOut_part2");
    var urlSignOutPostLogoutRedirectUri = azureAdAuthSett.GetValue<string>("url_signOut_postLogoutRedirectUri");

    if (string.IsNullOrEmpty(tenant) || string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret_timeReg)
        || string.IsNullOrEmpty(responseType) /*|| string.IsNullOrEmpty(redirectUri)*/ || string.IsNullOrEmpty(responseMode)
        || string.IsNullOrEmpty(scope) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(codeChallenge)
        || string.IsNullOrEmpty(codeChallengeMode) || string.IsNullOrEmpty(codeVerifier) || string.IsNullOrEmpty(grantType)
        || string.IsNullOrEmpty(noncePool) || string.IsNullOrEmpty(urlAuthCodePart1) || string.IsNullOrEmpty(urlAuthCodePart2)
        || string.IsNullOrEmpty(urlAuthCodeRedirectUri) || string.IsNullOrEmpty(urlGraphApi) || string.IsNullOrEmpty(urlIdTokenPart1)
        || string.IsNullOrEmpty(urlIdTokenPart2) || string.IsNullOrEmpty(urlIdTokenRedirectUri)
        || string.IsNullOrEmpty(urlSignOutPart1) || string.IsNullOrEmpty(urlSignOutPart2)
        || string.IsNullOrEmpty(urlSignOutPostLogoutRedirectUri))
    {
        throw new ArgumentException("One or more values in appsetting were not provided");
    }

    var props = new Auth_DtModelProps_Azure_AD_OAuth2_Auth(
        tenant: tenant,
        clientId: clientId,
        clientSecret: clientSecret_timeReg,
        grantType: grantType,
        nonce_pool: noncePool,
        responseType: responseType,
        redirectUri: redirectUri,
        responseMode: responseMode,
        scope: scope,
        codeVerifier: codeVerifier,
        state: state,
        codeChallenge: codeChallenge,
        codeCallengeMethod: codeChallengeMode,
        url_AuthCode_Part1: urlAuthCodePart1,
        url_AuthCode_Part2: urlAuthCodePart2,
        url_AuthCode_RedirectUri: urlAuthCodeRedirectUri,
        url_GraphApi: urlGraphApi,
        url_IdToken_Part1: urlIdTokenPart1,
        url_IdToken_Part2: urlIdTokenPart2,
        url_IdToken_RedirectUri: urlIdTokenRedirectUri,
        url_SignOut_Part1: urlSignOutPart1,
        url_SignOut_Part2: urlSignOutPart2,
        url_SignOut_PostLogoutRedirectUri: urlSignOutPostLogoutRedirectUri
        );
    return props;
}
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
        throw new ArgumentException("One or more values in appsetting were not provided");
    }

    var res = new JwtFunctions(issuer, audience, key);
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
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
        .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        });
}
Wetcardboard_Utilities_Fe_Appsettings CreateFeAppsettings(ConfigurationManager conf)
{
    var apiService_BasePath = conf.GetValue<string>("ApiService_BasePath");
    if (string.IsNullOrEmpty(apiService_BasePath))
    {
        throw new ArgumentException("One or more values in appsetting were not provided");
    }

    var res = new Wetcardboard_Utilities_Fe_Appsettings
    {
        ApiBasePath = apiService_BasePath
    };
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


var builder = WebApplication.CreateBuilder(args);

var conf = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient().AddHeaderPropagation(options =>
{
    options.Headers.Add("Cookie");
});
builder.Services.AddSingleton(GetAzureAdAuthProps(conf));
builder.Services.AddScoped<IAuthenticator, Auth_Azure_AD_OAuth2>();
builder.Services.AddSingleton(CreateJwtFunctions(conf));
builder.Services.AddSingleton(CreateFeAppsettings(conf));
builder.Services.AddSingleton(CreateSystemProps(conf));
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IHttpFunctions, HttpFunctions>();
builder.Services.AddSingleton<IWtCbLogger>();
builder.Services.AddBlazoredSessionStorage();

// Db Services
builder.Services.AddSingleton<IDbConn_Wetcardboard_Utilities>(GetDbConn_WcUtil(conf));
builder.Services.AddSingleton<IDbConn_Wetcardboard_Utilities_Fe, DbConn_Wetcardboard_Utilities_Fe>();
builder.Services.AddSingleton<IWetcardboard_Utilities_ApiService_TokenService, Wetcardboard_Utilities_ApiService_TokenService>();

builder.Services.AddControllers();

AddAuthentication(builder, conf);
builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();
