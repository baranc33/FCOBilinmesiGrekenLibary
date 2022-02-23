using FluentVal.Models;
using FluentValidation;

namespace FluentVal.FluentValidators
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            // RuleFor ile entity lere ulaşıp
            // lamda generic tipte belirttiğimiz nesnenin prolarına erişiriz
            // daha sonra belli metotlar kullanırız
            // .WithMessage("") metodu ile hata mesajı döndürmesini sağlarız
            // 1 empty ardında birden fazla özellik eklenebilir.
            // boş olamaz
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş olamaz");
            RuleFor(x => x.Name).MinimumLength(4).WithMessage("En Az 4 karakter olmalı");
            RuleFor(x => x.Name).MaximumLength(40).WithMessage("En Fazla 40 karakter olmalı");

            // email için özel kontrol
            RuleFor(x => x.Email).EmailAddress().WithMessage("Yanlış format");
            // aralık sınırlandırma 
            RuleFor(x => x.Age).NotEmpty().WithMessage("Yaş Boş olamaz")
                .InclusiveBetween(18, 60).WithMessage("Yaş sınırlaması 18-60 arası");
        }

    }
}
