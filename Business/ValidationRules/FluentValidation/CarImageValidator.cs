﻿using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(p => p.CarID).NotNull();
            RuleFor(p => p.CarID).NotEmpty();
            //RuleFor(p => p.ImagePath).NotEmpty();
        }
    }
}