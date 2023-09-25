using System.Text.RegularExpressions;
using Barber.Api.Models;
using FluentValidation;

namespace Barber.Api.Features.Customers.Commands.CreateNewCustomer;

public class CreateNewCustomerCommandValidator :AbstractValidator<CreateNewCustomerCommand>{
    public CreateNewCustomerCommandValidator() {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name tem que ser preenchido...")
            .MaximumLength(100)
            .WithMessage("Name possui um máximo de 100 caracteres...");

        RuleFor(c => c.CPF)
            .Cascade(CascadeMode.Stop)
            .Length(14)
            .WithMessage("CPF deve ter 14 caracteres...")
            .Must(ValidateCPF)
            .When(c => !string.IsNullOrEmpty(c.CPF))
            .WithMessage("CPF inválido...");
        
        RuleFor(c => c.Email)
          .Cascade(CascadeMode.Stop)
          .NotEmpty()
          .WithMessage("Email tem que ser preenchido...")
          .MaximumLength(60)
          .WithMessage("Email possui um máximo de 60 caracteres...")
          .Must(ValidateEmail)
          .When(c => !string.IsNullOrEmpty(c.Email))
          .WithMessage("Email inválido...");

        RuleFor(c => c.BirthdayDate)
          .NotEmpty()
          .WithMessage("BirthdayDate tem que ser preenchido...");

        RuleFor(c => c.Gender)
          .IsInEnum()
          .WithMessage("Gender inválido...");
        
        RuleFor(c => c.Addresses)
          .NotEmpty()
          .WithMessage("Customer deve possuir ao menos um Address...")
          .ForEach(addressRule => {
              addressRule.SetValidator(new CreateNewCustomerAddressValidator());
          })
          .When(c => c.Addresses != null, ApplyConditionTo.CurrentValidator);

         RuleFor(c => c.Telephones)
          .NotEmpty()
          .WithMessage("Customer deve possuir ao menos um Telephone...")
          .ForEach(telephoneRule => {
              telephoneRule.SetValidator(new CreateNewCustomerTelephoneValidator());
          })
          .When(c => c.Telephones != null, ApplyConditionTo.CurrentValidator);
        
    }

    private bool ValidateCPF(string cpf) 
    {
       cpf = cpf.Replace(".", "").Replace("-", "");
       if (cpf.Length != 11) {
           return false;
       }
       bool allDigitsEqual = true;
       for (int i = 1; i < cpf.Length; i++) {
           if (cpf[i] != cpf[0]) {
               allDigitsEqual = false;
               break;
           }
       }
       if (allDigitsEqual) {
           return false;
       }
       int sum = 0;
       for (int i = 0; i < 9; i++) {
           sum += int.Parse(cpf[i].ToString()) * (10 - i);
       }
       int remainder = sum % 11;
       int verificationDigit1 = remainder < 2 ? 0 : 11 - remainder;
       if (int.Parse(cpf[9].ToString()) != verificationDigit1) {
           return false;
       }
       sum = 0;
       for (int i = 0; i < 10; i++) {
           sum += int.Parse(cpf[i].ToString()) * (11 - i);
       }
       remainder = sum % 11;
       int verificationDigit2 = remainder < 2 ? 0 : 11 - remainder;
       if (int.Parse(cpf[10].ToString()) != verificationDigit2) {
           return false;
       }
       return true;
   }

    private bool ValidateEmail(string email){
      string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        
      return Regex.IsMatch(email, pattern);
    }
}  