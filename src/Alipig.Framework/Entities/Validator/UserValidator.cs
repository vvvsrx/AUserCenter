using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;

namespace Alipig.Framework.Entities.Validator
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotNull().Length(5, 64);
            RuleFor(x => x.Password).NotNull().Length(6,40);
            RuleFor(x => x.PrivateEmail).NotNull().EmailAddress();
            RuleFor(x => x.passwordConfirm).NotNull().Equal(x => x.Password);
        }
    }
}
