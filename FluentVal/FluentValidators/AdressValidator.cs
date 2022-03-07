using FluentVal.Models;
using FluentValidation;

namespace FluentVal.FluentValidators
{
    public class AdressValidator : AbstractValidator<Adress>
    {
        private string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz";
        public AdressValidator()
        {


            RuleFor(x=>x.Content).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Province).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.PostCode).NotEmpty().WithMessage(NotEmptyMessage)
                .MaximumLength(5).WithMessage("{PropertyName} alanı En Fazla ={MaxLenght} Kadar olmalıdır");
        }
    }
}
