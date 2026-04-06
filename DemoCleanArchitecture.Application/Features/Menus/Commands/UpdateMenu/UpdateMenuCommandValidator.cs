using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.Menus.Commands.UpdateMenu
{
    public class UpdateMenuCommandValidator : AbstractValidator<UpdateMenuCommand>
    {
        public UpdateMenuCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id > 0");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên menu không được để trống.")
                .MaximumLength(150);

            RuleFor(x => x.DisplayOrder)
                .GreaterThanOrEqualTo(0).WithMessage("DisplayOrder nên > 0");

            // Không được set ParentId = chính nó
            RuleFor(x => x)
                .Must(x => x.ParentId != x.Id)
                .WithMessage("Menu không thể là cha của chính nó.");
        }
    }
}
