using BoutiqueManagement.Models;
using FluentValidation;

public class BoutiqueItemValidator : AbstractValidator<BoutiqueItem>
{
    public BoutiqueItemValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.RentalPrice).GreaterThanOrEqualTo(0);
    }
}
