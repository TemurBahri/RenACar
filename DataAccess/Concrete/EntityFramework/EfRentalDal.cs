﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfRepositoryBase<Rental, RenACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetAllRentalDetails(Expression<Func<Rental, bool>> filter = null)
        {
            using (RenACarContext context = new RenACarContext())
            {
                var result =
                    from rental in filter == null ? context.Rentals : context.Rentals.Where(filter)
                    join customer in context.Customers
                        on rental.CustomerId equals customer.Id
                    join user in context.Users
                         on customer.UserId equals user.Id
                    join car in context.Cars
                         on rental.CarId equals car.Id
                    join brand in context.Brands
                         on car.BrandId equals brand.Id
                    join color in context.Colors
                         on car.ColorId equals color.Id
                    select new RentalDetailDto
                    {
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate,
                        RentalId = rental.Id,
                        BrandName = brand.BrandName,
                        CarDesctiption = car.Description,
                        ColorName = color.ColorName,
                        CompanyName = customer.CompanyName,
                        DailyPrice = car.DailyPrice,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        ModelYear = car.ModelYear
                    };

                return result.ToList();
            }
        }
    }
}