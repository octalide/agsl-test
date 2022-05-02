using AGLS.Utilities;
using agsl_test.Helpers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryContext>(options => options.UseSqlite("Data Source=Database/inventory.db"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// attempt to populate the database with csv data from a file if provided as an arg
if (args.Length > 0)
{
    try
    {
        // load the inventory data from the file
        var inventory = Data.LoadInventoryCSV(args[0], skip:true, verbose:true);

        Console.WriteLine($"Inserting {inventory.Count} items into the database...");

        // insert results into the database
        using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<InventoryContext>();
            context.Database.EnsureCreated();
            context.InventoryItems.AddRange(inventory);
            context.SaveChanges();
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Failed to load CSV file: {e.Message}");
    }
}

app.Run();
