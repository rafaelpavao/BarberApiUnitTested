using Barber.Api.DbContexts;
using Barber.Api.Extensions;
using Barber.Api.Features.Addresses.Commands.CreateAddress;
using Barber.Api.Features.Addresses.Commands.CreateTelephone;
using Barber.Api.Features.Addresses.Commands.UpdateAddress;
using Barber.Api.Features.Addresses.Commands.UpdateTelephone;
using Barber.Api.Features.Customers.Commands.CreateNewCustomer;
using Barber.Api.Features.Customers.Commands.UpdateCustomer;
using Barber.Api.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel( //Para colocar o host sempre em 5000
    options => {
        options.ListenLocalhost(5000);
    }
);

builder.Services.AddControllers();

builder.Services.AddDbContext<CustomerContext>( //Para usar o Banco de Dados
    options => {
        options.UseNpgsql("Host=localhost;Database=BarberShop;Username=postgres;Password=123");
    }
);

builder.Services.AddApplicationServices();

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.ResetDatabaseAsync();

app.Run();