using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Alipig.Framework.Entities.Validator
{
    public class RoleValidator : AbstractValidator<Role>
    {
        public RoleValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(1, 50);
            RuleFor(x => x.SiteId).NotNull().NotEmpty();
        }
    }
}
