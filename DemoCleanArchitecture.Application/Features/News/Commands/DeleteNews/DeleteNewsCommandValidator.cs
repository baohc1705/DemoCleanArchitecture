using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCleanArchitecture.Application.Features.News.Commands.DeleteNews
{
    public class DeleteNewsCommandValidator : AbstractValidator<DeleteNewsCommand>
    {
        public DeleteNewsCommandValidator() => RuleFor(x => x.Id).GreaterThan(0);
    }
}
