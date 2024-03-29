﻿using Codecool.CodecoolShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Codecool.CodecoolShop.Daos
{
    public interface IUserDao : IDao<(CheckoutModel, List<CartItemModel>)>
    {
        void AddUser(CheckoutModel user);
        CheckoutModel GetUserByCredentials(string email, string password);
        void UpdateUser(CheckoutModel user);
    }
}
