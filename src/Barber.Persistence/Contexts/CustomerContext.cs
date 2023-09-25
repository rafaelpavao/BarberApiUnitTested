using Barber.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Api.DbContexts;

public class CustomerContext : DbContext{
  public DbSet<Customer> Customers { get; set; } = null!;
  public DbSet<Telephone> Telephones { get; set; } = null!;
  public DbSet<Address> Addresses { get; set; } = null!;

  public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder){
    // Configurando as propriedades dos atributos da entidade "Customer".
    var customer = modelBuilder.Entity<Customer>();

    customer
      .HasMany(customer => customer.Telephones)
      .WithOne(telephone => telephone.Customer)
      .HasForeignKey(telephone => telephone.CustomerId);

    customer.Ignore(c => c.ValidationResult);

    customer
      .HasMany(customer => customer.Addresses)
      .WithOne(address => address.Customer)
      .HasForeignKey(address => address.CustomerId);

    customer
      .Property(customer => customer.Name)
      .HasMaxLength(60)
      .IsRequired();

    customer
      .Property(customer => customer.BirthdayDate)
      .IsRequired();

    customer
      .Property(customer => customer.Gender)
      .IsRequired();

    customer
      .Property(customer => customer.CPF)
      .IsFixedLength()
      .HasMaxLength(14)
      .IsRequired();

    customer
      .Property(customer => customer.Email)
      .HasMaxLength(60)
      .IsRequired();
    
    customer
      .HasData(
        new Customer{
          Id = 1,
          Name = "Guilherme Thomy",
          BirthdayDate = new DateOnly(2023, 8, 22),
          Gender = Gender.Masc,
          CPF = "181.851.057-07",
          Email = "guimasthomy@gmail.com"
        },
        new Customer{
          Id = 2,
          Name = "Guilherme Rosa",
          BirthdayDate = new DateOnly(2023, 8, 22),
          Gender = Gender.Masc,
          CPF = "111.111.111-11",
          Email = "guirosa@gmail.com"
        }
      );

    // Configurando as propriedades dos atributos da entidade "Telphone".
    var telephone =  modelBuilder.Entity<Telephone>();

    telephone
      .Property(telephone => telephone.Type)
      .IsRequired();

    telephone
      .Property(telephone => telephone.Number)
      .HasMaxLength(80)
      .IsRequired();

    telephone
      .HasData(
        new Telephone{
          Id = 1,
          Number = "+55 (47) 99238-1783",
          Type = TelephoneType.Cell,
          CustomerId = 1
        },
        new Telephone{
          Id = 2,
          Number = "+55 (47) 99999-9999",
          Type = TelephoneType.Fix,
          CustomerId = 1
        },
        new Telephone{
          Id = 3,
          Number = "+55 (47) 88888-8888",
          Type = TelephoneType.Cell,
          CustomerId = 2
        },
        new Telephone{
          Id = 4,
          Number = "+55 (47) 77777-7777",
          Type = TelephoneType.Fix,
          CustomerId = 2
        }
      );

    // Configurando as propriedades dos atributos da entidade "Customer".
    var address =  modelBuilder.Entity<Address>();

    address
      .Property(address => address.Street)
      .HasMaxLength(80)
      .IsRequired();

    address
      .Property(address => address.Number)
      .IsRequired();

    address
      .Property(address => address.District)
      .HasMaxLength(60)
      .IsRequired();

    address
      .Property(address => address.City)
      .HasMaxLength(60)
      .IsRequired();

    address
      .Property(address => address.State)
      .IsFixedLength()
      .HasMaxLength(2)
      .IsRequired();

    address
      .Property(address => address.CEP)
      .IsFixedLength()
      .HasMaxLength(9)
      .IsRequired();

    address
      .HasData(
        new Address{
          Id = 1,
          Street = "Agostinho Fernandes Vieira",
          Number = 157,
          District = "District1",
          City = "Itajaí",
          State = "SC",
          CEP = "88301-650",
          CustomerId = 1
        },
        new Address{
          Id = 2,
          Street = "Avenida Presidente Roosevelt",
          Number = 7,
          District = "District2",
          City = "Niterói",
          State = "RJ",
          CEP = "11111-111",
          CustomerId = 1
        },
        new Address{
          Id = 3,
          Street = "Street1",
          Number = 157,
          District = "District3",
          City = "Itajaí",
          State = "SC",
          CEP = "22222-22",
          CustomerId = 2
        },
        new Address{
          Id = 4,
          Street = "Agostinho Fernandes Vieira",
          Number = 157,
          District = "District4",
          City = "Itajaí",
          State = "SC",
          CEP = "33333-33",
          CustomerId = 2
        }
      );

    base.OnModelCreating(modelBuilder);
  }
}