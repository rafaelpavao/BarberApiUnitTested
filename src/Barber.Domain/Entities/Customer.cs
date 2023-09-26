using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using FluentValidation.Results;

namespace Barber.Api.Entities;

public enum Gender{ Masc, Fem }


public class Customer 
{
  [NotMapped]
  public ValidationResult ValidationResult { get; protected set; }
  public int Id { get; set;}
  public string Name { get; set; } = string.Empty;
  public DateOnly BirthdayDate { get; set; }
  public Gender Gender { get; set; }
  public string CPF { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public ICollection<Telephone> Telephones { get; set; } = new List<Telephone>();
  public ICollection<Address> Addresses { get; set; } = new List<Address>();


  public bool IsValid()
  {
    ValidationResult = new CustomerValidation().Validate(this);
    return ValidationResult.IsValid;
  }
 
  
  public class CustomerValidation : AbstractValidator<Customer>
  {
    public CustomerValidation()
    {
      RuleFor(c => c.Name)
        .NotEmpty().WithMessage("Por favor, certifique-se de ter inserido o nome")
        .Length(2, 150).WithMessage("O nome deve ter entre 2 e 150 caracteres");

      RuleFor(c => c.Email)
        .NotEmpty().WithMessage("Customer email should not be empty")
        .EmailAddress().WithMessage("Customer email should be valid");

      RuleFor(c => c.BirthdayDate)
        .NotEmpty()
        .Must(HaveMinimumAge)
        .WithMessage("Customer must be of age 18");
      
      RuleFor(c => c.CPF)
        .NotEmpty()
        .Must(IsCpf)
        .WithMessage("Customer CPF must be valid");

      RuleFor(c => c.Id)
        .NotEqual(0);
    }

    public static bool HaveMinimumAge(DateOnly birthDate)
    {
      var today = DateOnly.FromDateTime(DateTime.UtcNow);
      return birthDate <= today.AddYears(-18);
    }
    
    public static bool IsCpf(string CPF)
    {
      int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
      string tempCpf;
      string digito;
      int soma;
      int resto;
      CPF = CPF.Trim();
      CPF = CPF.Replace(".", "").Replace("-", "");
      if (CPF.Length != 11)
        return false;
      tempCpf = CPF.Substring(0, 9);
      soma = 0;

      for(int i=0; i<9; i++)
        soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
      resto = soma % 11;
      if ( resto < 2 )
        resto = 0;
      else
        resto = 11 - resto;
      digito = resto.ToString();
      tempCpf = tempCpf + digito;
      soma = 0;
      for(int i=0; i<10; i++)
        soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
      resto = soma % 11;
      if (resto < 2)
        resto = 0;
      else
        resto = 11 - resto;
      digito = digito + resto.ToString();
      return CPF.EndsWith(digito);
    }
  }
  
  
}