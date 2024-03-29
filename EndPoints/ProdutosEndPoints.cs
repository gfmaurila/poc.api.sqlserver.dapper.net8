﻿using Microsoft.OpenApi.Models;
using poc.api.sqlserver.Model;
using poc.api.sqlserver.Service;

namespace poc.api.sqlserver.EndPoints;
public static class ProdutosEndpoints
{
    public static void RegisterProdutosEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/produto", async (IProdutoService _service) =>
        {
            var produto = await _service.Get();
            if (produto is null)
                return Results.NotFound();

            return TypedResults.Ok(produto);
        })
        .WithName("BuscarProdutos")
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Buscar produtos",
            Description = "Buscar produtos",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Minha Loja" } }
        });

        app.MapGet("/api/produto/{id}", async (int id, IProdutoService _service) =>
        {
            var produto = await _service.Get(id);
            if (produto is null)
                return Results.NotFound();

            return Results.Ok(produto);
        })
        .WithName("BuscarProdutoId")
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Buscar produto pelo id",
            Description = "Buscar produto pelo id",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Minha Loja" } }
        });

        app.MapPost("/api/produto", async (Produto entity, IProdutoService _service) =>
        {
            if (entity is null)
                return Results.NotFound();

            return Results.Created($"{entity.Id}", await _service.Post(entity));
        })
        .WithName("CadastrarProduto")
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Cadastrar produto",
            Description = "Cadastrar produto",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Minha Loja" } }
        });

        app.MapPut("/api/produto", async (Produto entity, IProdutoService _service) =>
        {
            if (entity is null)
                return Results.NotFound();

            return Results.Ok(await _service.Put(entity));
        })
        .WithName("EditarProduto")
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Editar produto",
            Description = "Editar produto",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Minha Loja" } }
        });

        app.MapDelete("/api/produto/{id}", async (int id, IProdutoService _service) =>
        {
            var produto = await _service.Delete(id);
            return Results.Ok($"Produto id={id} deletado");
        })
        .WithName("DeletarProduto")
        .RequireAuthorization()
        .WithOpenApi(x => new OpenApiOperation(x)
        {
            Summary = "Deletar produto",
            Description = "Deletar produto",
            Tags = new List<OpenApiTag> { new OpenApiTag { Name = "Minha Loja" } }
        });
    }
}
