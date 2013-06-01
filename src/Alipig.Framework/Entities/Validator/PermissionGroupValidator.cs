using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Alipig.Framework.Entities.Validator
{
    public class PermissionGroupValidator : AbstractValidator<PermissionGroup>
    {
        public PermissionGroupValidator()
        {
            RuleFor(x => x.Name).NotNull().Length(1,50);
        }
    }
}
