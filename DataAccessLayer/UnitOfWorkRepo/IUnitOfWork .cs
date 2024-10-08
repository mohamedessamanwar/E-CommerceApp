﻿using DataAccessLayer.Data.Context;
using DataAccessLayer.Repositories.AddessRepo;
using DataAccessLayer.Repositories.OrderItemRepo;
using DataAccessLayer.Repositories.OrderRepo;
using DataAccessLayer.Repositories.ProductRepo;
using DataAccessLayer.Repositories.ReviewRepo;
using DataAccessLayer.Repositories.ShoppingCartRepo;
using DataAccessLayer.Repositories.UserTokens;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace DataAccessLayer.UnitOfWorkRepo
{
    public interface IUnitOfWork
    {
      
        // Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.Serializable);
        Task<IDbContextTransaction> BeginTransactionAsync(System.Data.IsolationLevel isolationLevel);
        void Commit();
        void Rollback();
        int Complete();
        IProductRepository productRepository { get; }
        IAddressRepo addressRepo { get; }
        IShoppiingCartRepo shoppiingCartRepo { get; }
        IOrderRepo orderRepo { get; }
        IOrderItemRepo orderItemRepo { get; }
        IReviewRepo reviewRepo { get; }

        IUserTokenRepo userToken { get; }


    }
}
