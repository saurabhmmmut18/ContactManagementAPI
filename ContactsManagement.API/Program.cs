using ContactsManagement.Core.Interfaces;
using ContactsManagement.API.Middleware; // Import the middleware

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowContactsManagementUI",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular app's URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Register services for JSON file-based storage
builder.Services.AddScoped<IContactRepository, ContactRepository>(); // Repository for handling JSON data
builder.Services.AddScoped<IContactService, ContactService>(); // Service for business logic

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors("AllowContactsManagementUI");

// Add Global Error Handling Middleware
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseAuthorization();
app.MapControllers();
app.Run();
