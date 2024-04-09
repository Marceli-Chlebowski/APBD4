﻿using WebApplication1.Models;

namespace WebApplication1.Endpoints;

public static class AnimalEndpoints
{
    public static void MapAnimalEndpoints(this WebApplication app)
    {
        app.MapGet("/animals", () =>
        {
            var animals = StaticData.Animals;
            return Results.Ok();
        });

        app.MapGet("/animals/{id}", (int id) =>
        {
            return Results.Ok(id);
        });

        app.MapPost("/animals", () =>
        {
            return Results.Created(); 
        });
    }
}