using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Alipig.Framework.Entities.Validator
{
    public class SiteValidator : AbstractValidator<Site>
    {
        public SiteValidator()
        {
            RuleFor(x => x.SiteName).NotNull().Length(1, 50);
        }
    }
}
