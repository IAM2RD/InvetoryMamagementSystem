using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using IMSAPI.Models;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

string origins = "_testoforigins";
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Items") ?? "Data Source=Items.db";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: origins,
        builder =>
        {
            builder.WithOrigins(
                "http://localhost:3000", "https://web.postman.co/workspace/My-Workspace~dbeb135a-e24d-4bd9-8c85-793f2574c1ea/request/create?requestId=9fc1f45c-3d7a-46e7-813e-a9ab28735113"
            );
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSqlite<ItemDb>(connectionString);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory management System",
        Description = "The best API",
        Version = "v1"
    });
});

// Add services to the container.

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaStore API V1");
});



// Adding no cors
app.UseCors(builder => builder.AllowAnyOrigin()
 .AllowAnyMethod()
 .AllowAnyHeader());


app.MapGet("/items", async (ItemDb db) => await db.Items.ToListAsync());
app.MapPost("/items", async ( Item item, ItemDb db) =>
{
    await db.Items.AddAsync(item);
    await db.SaveChangesAsync();
    return Results.Created($"/items/{item.Id}", item);
});
app.MapPut("/items/{id}", async (Item updateItem,ItemDb db, int id) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null) return Results.NotFound();
    item.Name = updateItem.Name;
    item.Description = updateItem.Description;
    item.Category = updateItem.Category;
    item.CurrentUser = updateItem.CurrentUser;  
    item.Type = updateItem.Type;    
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/items/{id}", async (ItemDb db, int id) =>
{
    var item = await db.Items.FindAsync(id);
    if (item is null)
    {
        return Results.NotFound();
    }
    db.Items.Remove(item);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapGet("/users", async (ItemDb db) => await db.Users.ToListAsync());
app.MapGet("/items/{id}", async (ItemDb db, int id) => await db.Items.FindAsync(id));
app.MapPost("/users", async (ItemDb db, User user) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return Results.Created($"/users/{user.Id}", user);
});
app.MapDelete("/users/{id}", async (ItemDb db, int id) =>
{
    var user = await db.Users.FindAsync(id);
    if (user is null)
    {
        return Results.NotFound();

    }
    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Ok();
});
app.MapPut("/users/{id}", async (ItemDb db, User updateUser, int id) =>
{

    var user = await db.Users.FindAsync(id);
    if (user is null) return Results.NotFound();
    user.FirstName = updateUser.FirstName;
    user.LastName = updateUser.LastName;
    user.Items = updateUser.Items;
    await db.SaveChangesAsync();
    return Results.NoContent();
});



app.Run();
