using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Vulnerable SQL injection endpoint for CodeQL demonstration
app.MapGet("/user/{id}", (string id) =>
{
    // VULNERABLE: Direct string concatenation leads to SQL injection
    var connectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true;";
    using var connection = new SqlConnection(connectionString);
    var query = $"SELECT * FROM Users WHERE Id = {id}"; // SQL Injection vulnerability
    
    var command = new SqlCommand(query, connection);
    connection.Open();
    var reader = command.ExecuteReader();
    
    var results = new List<object>();
    while (reader.Read())
    {
        results.Add(new { Id = reader["Id"], Name = reader["Name"] });
    }
    
    return Results.Ok(results);
})
.WithName("GetUser")
.WithOpenApi();

// Vulnerable XSS endpoint for CodeQL demonstration
app.MapGet("/search", (string query) =>
{
    // VULNERABLE: Unescaped user input returned in response
    var html = $"<h1>Search Results for: {query}</h1>"; // XSS vulnerability
    return Results.Content(html, "text/html");
})
.WithName("Search")
.WithOpenApi();

// Vulnerable path traversal endpoint for CodeQL demonstration
app.MapGet("/file/{filename}", (string filename) =>
{
    // VULNERABLE: No path validation allows directory traversal
    var basePath = "/app/files/";
    var fullPath = Path.Combine(basePath, filename); // Path traversal vulnerability
    
    if (File.Exists(fullPath))
    {
        return Results.File(fullPath);
    }
    
    return Results.NotFound();
})
.WithName("GetFile")
.WithOpenApi();

// Secure endpoint showing proper parameterized query
app.MapGet("/secure/user/{id}", (int id) =>
{
    // SECURE: Using parameterized query
    var connectionString = "Server=localhost;Database=TestDB;Trusted_Connection=true;";
    using var connection = new SqlConnection(connectionString);
    var query = "SELECT * FROM Users WHERE Id = @Id";
    
    var command = new SqlCommand(query, connection);
    command.Parameters.AddWithValue("@Id", id);
    connection.Open();
    var reader = command.ExecuteReader();
    
    var results = new List<object>();
    while (reader.Read())
    {
        results.Add(new { Id = reader["Id"], Name = reader["Name"] });
    }
    
    return Results.Ok(results);
})
.WithName("GetUserSecure")
.WithOpenApi();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
