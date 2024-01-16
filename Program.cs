using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using poc.api.sqlserver.Configuration;
using poc.api.sqlserver.EndPoints;
using poc.api.sqlserver.Service;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddConnections();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfig(builder.Configuration);

// Sql Server
builder.Services.AddDbContext<SqlServerDb>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<SqlServerDb>();

// Dapper
builder.Services.AddTransient<IDbConnection>(op => new SqlConnection(builder.Configuration.GetConnectionString("SqlConnection")));

// Service 
builder.Services.AddScoped<IProdutoService, ProdutoService>();

var app = builder.Build();

app.UseHttpsRedirection();

// Configura middlewere
app.UseStatusCodePages(async statusCodeContext => await Results.Problem(statusCode: statusCodeContext.HttpContext.Response.StatusCode).ExecuteAsync(statusCodeContext.HttpContext));

// EndPoints
app.MapGroup("/identity/").MapIdentityApi<IdentityUser>();
app.RegisterProdutosEndpoints();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();