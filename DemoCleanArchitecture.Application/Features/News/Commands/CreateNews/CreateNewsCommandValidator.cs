using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.CreateNews
{
    public class CreateNewsCommandValidator : AbstractValidator<CreateNewsCommand>
    {
        public CreateNewsCommandValidator()
        {
            RuleFor(x => x.MenuId)
                .NotEmpty().WithMessage("MenuId không được để trống.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title không được để trống.")
                .MaximumLength(300).WithMessage("Title tối đa 300 ký tự.");

            RuleFor(x => x.Summary)
                .NotEmpty().WithMessage("Summary không được để trống.")
                .MaximumLength(500).WithMessage("Summary tối đa 500 ký tự.");
                
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content không được để trống.");

            RuleFor(x => x.ThumbnailUrl)
                .NotEmpty().WithMessage("ThumbnailUrl không được để trống.");

            RuleFor(x => x.ScheduledAt)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.ScheduledAt.HasValue)
                .WithMessage("ScheduledAt phải > hiện tại.");
        }
    }
}
