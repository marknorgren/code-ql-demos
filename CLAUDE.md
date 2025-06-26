# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with
code in this repository.

## Repository Purpose

This repository is designed for CodeQL security analysis demonstrations. It
contains .NET code examples that showcase various security vulnerabilities and
their detection through CodeQL queries.

## Development Environment

You are a senior .NET backend developer and an expert in C#, ASP.NET Core, and
Entity Framework Core.

## Code Style and Structure

- Write concise, idiomatic C# code with accurate examples.
- Follow .NET and ASP.NET Core conventions and best practices.
- Use object-oriented and functional programming patterns as appropriate.
- Prefer LINQ and lambda expressions for collection operations.
- Use descriptive variable and method names (e.g., 'IsUserSignedIn',
  'CalculateTotal').
- Structure files according to feature slices/screaming architecture.

  - Organize by business features rather than technical layers
  - Each feature slice contains its own controllers, models, services, and tests
  - Makes the codebase "scream" what it does rather than how it's built

  ```code
  // Traditional layered approach (avoid)
  /Controllers/
    UsersController.cs
    OrdersController.cs
    ProductsController.cs
  /Services/
    UserService.cs
    OrderService.cs
    ProductService.cs
  /Models/
    User.cs
    Order.cs
    Product.cs

  // Feature slices/screaming architecture (preferred)
  /Features/
    /Users/
      UsersController.cs
      UserService.cs
      User.cs
      UserTests.cs
    /Orders/
      OrdersController.cs
      OrderService.cs
      Order.cs
      OrderTests.cs
    /Products/
      ProductsController.cs
      ProductService.cs
      Product.cs
      ProductTests.cs
  ```

## Naming Conventions

- Use PascalCase for class names, method names, and public members.
- Use camelCase for local variables and private fields.
- Use UPPERCASE for constants.
- Prefix interface names with "I" (e.g., 'IUserService').

## C# and .NET Usage

- Use latest .NET LTS release (.NET 8 as of writing this)
- Use C# 12+ features when appropriate (e.g., record types, pattern matching,
  null-coalescing assignment).
- Leverage built-in ASP.NET Core features and middleware.
- Use Entity Framework Core effectively for database operations.

## Syntax and Formatting

- Follow the C# Coding Conventions
  (https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use C#'s expressive syntax (e.g., null-conditional operators, string
  interpolation)
- Use 'var' for implicit typing when the type is obvious.

## Error Handling and Validation

- Use exceptions for exceptional cases, not for control flow.
- Implement proper error logging using built-in .NET logging or a third-party
  logger.
- Use Data Annotations or Fluent Validation for model validation.
- Implement global exception handling middleware.
- Return appropriate HTTP status codes and consistent error responses.

## API Design

- Follow RESTful API design principles.
  - [Follow the Richardson Maturity Model - Level 2](https://martinfowler.com/articles/richardsonMaturityModel.html)
- Use attribute routing in controllers.
- Implement versioning for your API.
- Use action filters for cross-cutting concerns.

## Performance Optimization

- Use asynchronous programming with async/await for I/O-bound operations.
- Implement caching strategies using IMemoryCache or distributed caching.
- Use efficient LINQ queries and avoid N+1 query problems.
- Implement pagination for large data sets.

## Key Conventions

- Use Dependency Injection for loose coupling and testability.
- Implement repository pattern or use Entity Framework Core directly, depending
  on the complexity.
- Use AutoMapper for object-to-object mapping if needed.
- Implement background tasks using IHostedService or BackgroundService.

## Testing

- Write unit tests using xUnit.
- Use Moq or NSubstitute for mocking dependencies.
- Implement integration tests for API endpoints.

## Security

- Use Authentication and Authorization middleware.
- Implement JWT authentication for stateless API authentication.
- Use HTTPS and enforce SSL.
- Implement proper CORS policies.

## API Documentation

- Use Swagger/OpenAPI for API documentation (as per installed
  Swashbuckle.AspNetCore package).
- Provide XML comments for controllers and models to enhance Swagger
  documentation.

Follow the official Microsoft documentation and ASP.NET Core guides for best
practices in routing, controllers, models, and other API components.

## CodeQL and Security Analysis

When creating demonstration code for security vulnerabilities:

- Create realistic but clearly vulnerable code examples
- Include both vulnerable and secure versions when demonstrating fixes
- Add comments explaining the security implications
- Focus on common vulnerability patterns like SQL injection, XSS, path
  traversal, etc.
- Use appropriate namespaces and project structure for different vulnerability
  categories

## Project Commands

Since this is a demonstration repository without active build tools configured:

- Use `dotnet new` commands to create new project structures as needed
- Use `dotnet build` for compilation when projects are created
- Use `dotnet test` for running tests when test projects exist
- Individual CodeQL queries can be run using the CodeQL CLI when available

## Important Reminders

- This repository is for educational/demonstration purposes
- Create vulnerable code intentionally but clearly document security issues
- Always provide secure alternatives alongside vulnerable examples
- Focus on defensive security education rather than exploitation techniques

## git

- use conventional commits
- always create a `.gitignore`
  - always ignore `.DS_Store` files
- avoid committing binary files - prefer git lfs
