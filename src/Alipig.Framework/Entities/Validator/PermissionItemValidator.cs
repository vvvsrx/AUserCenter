using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Alipig.Framework.Entities.Validator
{
    public class PermissionItemValidator : AbstractValidator<PermissionItem>
    {
        public PermissionItemValidator()
        {
            RuleFor(x => x.Code).NotEmpty().Matches("^[A-Za-z][A-Za-z0-9_]{0,49}$").WithMessage("权限项代码只能以字母开头，由1-50个字母、数字或下划线组成！");
            RuleFor(x => x.DisplayName).NotEmpty();
            
        }
    }
}
