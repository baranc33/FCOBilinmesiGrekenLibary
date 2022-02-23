using FluentVal.Models;
using FluentValidation;

namespace FluentVal.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        private string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz";
        public CustomerValidator()
        {

            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("En Az 4 karakter olmalı");
            RuleFor(x => x.Name).MaximumLength(40).WithMessage("En Fazla 40 karakter olmalı");


            RuleFor(x => x.Email).EmailAddress().WithMessage("Yanlış format");

            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage)
                .InclusiveBetween(18, 60).WithMessage("Yaş sınırlaması 18-60 arası");


            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage)
                .Must(x =>
                {
                    return DateTime.Now.AddYears(-18) >= x;
                }).WithMessage("18 yaşından küçük olamaz");


          

        }

    }
}
