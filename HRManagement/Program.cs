using HRManagement.Common.Exceptions;
using HRManagement.Data.Contexts;
using HRManagement.Data.Interfaces;
using HRManagement.Data.Repositories;
using HRManagement.Domain.Entities;
using HRManagement.Domain.Interfaces;
using HRManagement.Domain.Services;
using HRManagement.Middlewares;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Add the custom exception filter
    options.Filters.Add<CustomExceptionFilter>();
});



// Register the Exception Filter
builder.Services.AddScoped<CustomExceptionFilter>();

// Register the Department Repositories & Services
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

// Register the Client Repositories & Services
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();

// Register the Employee Repositories & Services
builder.Services.AddScoped< IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped< IEmployeeService, EmployeeService>();

// Register the EmployeeDepartment Repository
builder.Services.AddScoped<IEmployeeDepartmentRepository, EmployeeDepartmentRepository>();





// Register the DbContext
builder.Services.AddDbContext<HRManagementDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HRManagementConnectionString"), sqlServerOptions =>
    {
        sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        );
    });
});



// Learn more about configuring Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();






var app = builder.Build();






// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// Register the Audit middleware
app.UseMiddleware<AuditMiddleware>();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
