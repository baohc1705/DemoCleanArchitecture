using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.PublishNews
{
    public class PublishNewsCommandValidator : AbstractValidator<PublishNewsCommand>
    {
        public PublishNewsCommandValidator() 
        {
            RuleFor(x => x.ScheduledAt)
            .GreaterThan(DateTime.UtcNow)
            .When(x => x.ScheduledAt.HasValue)
            .WithMessage("Thời gian hẹn đăng phải ở tương lai.");
        }
    }
}
