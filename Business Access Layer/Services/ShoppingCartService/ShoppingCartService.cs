using AutoMapper;
using BusinessAccessLayer.DTOS.ShoppingCart;
using BusinessAccessLayer.DTOS;
using DataAccessLayer.Data.Models;
using DataAccessLayer.UnitOfWorkRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using DataAccessLayer.Data.Context;
using Microsoft.AspNetCore.DataProtection;

namespace BusinessAccessLayer.Services.ShoppingCartService
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly Locke locke;
        private readonly IDataProtector _protector;
        public ShoppingCartService(IUnitOfWork unitOfWork, IMapper mapper, Locke locke, IDataProtectionProvider provider)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.locke = locke;
            _protector = provider.CreateProtector("Sensitive.Data.Protection");

        }

        #region Last version
        //public async Task<CreateStatus<ShoppingCartCreateView>> CreateShoppingCart(ShoppingCartCreateView shoppingCartCreateView)
        //{
        //    int maxRetryAttempts = 3;
        //    int retryCount = 0;

        //    while (retryCount < maxRetryAttempts)
        //    {
        //        try
        //        {
        //            using (var context = new ECommerceContext())
        //            {
        //                // Set the command timeout to 120 seconds
        //                //context.Database.SetCommandTimeout(120);
        //                using (var transaction = await unitOfWork.BeginTransactionAsync(System.Data.IsolationLevel.Serializable))
        //                {
        //                    // Get the product with a lock
        //                    var product = await unitOfWork.productRepository.GetProductWithLock(shoppingCartCreateView.ProductId);

        //                    Console.WriteLine(product.Count);

        //                    // Check if product is null or if there is not enough stock
        //                    if (product == null || product.Count < shoppingCartCreateView.Count)
        //                    {
        //                        return new CreateStatus<ShoppingCartCreateView>()
        //                        {
        //                            Value = shoppingCartCreateView,
        //                            Massage = "Not enough product in stock",
        //                            Status = false
        //                        };
        //                    }

        //                    // Deduct the product count
        //                    product.Count -= shoppingCartCreateView.Count;
        //                    unitOfWork.productRepository.Update(product, nameof(product.Count));
        //                    unitOfWork.Complete();
        //                    await Task.Delay(3000);

        //                    var carts = await unitOfWork.shoppiingCartRepo.GetShoppingCartsByUserCart(shoppingCartCreateView.UserId);
        //                    var cart = carts?.FirstOrDefault(s => s.ProductId == shoppingCartCreateView.ProductId);

        //                    if (carts == null || cart == null)
        //                    {
        //                        // Map and add new cart item
        //                        var newCart = mapper.Map<ShoppingCart>(shoppingCartCreateView);
        //                        await unitOfWork.shoppiingCartRepo.AddAsync(newCart);
        //                    }
        //                    else
        //                    {
        //                        // Update existing cart item
        //                        cart.Count += shoppingCartCreateView.Count;
        //                        unitOfWork.shoppiingCartRepo.Update(cart, nameof(cart.Count));
        //                    }

        //                    int result = unitOfWork.Complete();

        //                    if (result == 0)
        //                    {
        //                        transaction.Rollback();
        //                        return new CreateStatus<ShoppingCartCreateView>()
        //                        {
        //                            Value = shoppingCartCreateView,
        //                            Massage = "Try again",
        //                            Status = false
        //                        };
        //                    }

        //                    transaction.Commit();
        //                    return new CreateStatus<ShoppingCartCreateView>()
        //                    {
        //                        Value = shoppingCartCreateView,
        //                        Massage = "added",
        //                        Status = true
        //                    };
        //                }
        //            }
        //        }
        //        catch (System.Exception ex)
        //        {
        //            if (IsTransientError(ex))
        //            {
        //                Console.WriteLine("mmmmmmm");
        //                retryCount++;
        //                if (retryCount >= maxRetryAttempts)
        //                {
        //                    return new CreateStatus<ShoppingCartCreateView>()
        //                    {
        //                        Value = shoppingCartCreateView,
        //                        Massage = "Max retry attempts exceeded. Please try again later.",
        //                        Status = false
        //                    };
        //                }
        //                await Task.Delay(3000); // Optional: Add a delay before retrying
        //            }
        //            else
        //            {
        //                return new CreateStatus<ShoppingCartCreateView>()
        //                {
        //                    Value = shoppingCartCreateView,
        //                    Massage = ex.Message,
        //                    Status = false
        //                };
        //            }
        //        }
        //    }

        //    return new CreateStatus<ShoppingCartCreateView>()
        //    {
        //        Value = shoppingCartCreateView,
        //        Massage = "An unexpected error occurred.",
        //        Status = false
        //    };
        //}

        //private bool IsTransientError(System.Exception ex)
        //{
        //    if (ex is DbUpdateException dbUpdateEx && dbUpdateEx.InnerException is SqlException sqlEx)
        //    {
        //        // Check if the error number corresponds to a deadlock
        //        return sqlEx.Number == 1205;
        //    }
        //    return true;
        //}
        #endregion

        #region lock version 
        //public async Task<CreateStatus<ShoppingCartCreateView>> CreateShoppingCart(ShoppingCartCreateView shoppingCartCreateView)
        //{
        //    lock (locke)
        //    {
        //        // get product . 
        //        var product =  unitOfWork.productRepository.gitid(shoppingCartCreateView.ProductId);
        //        Console.WriteLine(product.Count);
        //        // chaeck count .
        //        if (product?.Count < shoppingCartCreateView.Count)
        //        {
        //            return new CreateStatus<ShoppingCartCreateView>()
        //            {
        //                Value = shoppingCartCreateView,
        //                Massage = "Try again",
        //                Status = false
        //            };

        //        }
        //        else
        //        {
        //            product.Count -= shoppingCartCreateView.Count;
        //            unitOfWork.Complete();
        //        }

        //    }


        //    var carts = await unitOfWork.shoppiingCartRepo.GetShoppingCartsByUserCart(shoppingCartCreateView.UserId);
        //    var cart = carts?.FirstOrDefault(s => s.ProductId == shoppingCartCreateView.ProductId);
        //    if (carts == null || cart == null)
        //    {

        //        //map .
        //        var Cart = mapper.Map<ShoppingCart>(shoppingCartCreateView);
        //        //add
        //        await unitOfWork.shoppiingCartRepo.AddAsync(Cart);
        //        int result = unitOfWork.Complete();
        //        //cheack result 
        //        if (result == 0)
        //        {
        //            return new CreateStatus<ShoppingCartCreateView>()
        //            {
        //                Value = shoppingCartCreateView,
        //                Massage = "Try again",
        //                Status = false
        //            };

        //        }
        //        return new CreateStatus<ShoppingCartCreateView>()
        //        {
        //            Value = shoppingCartCreateView,
        //            Massage = "added",
        //            Status = true
        //        };

        //    }

        //    else
        //    {
        //        cart.Count += shoppingCartCreateView.Count;
        //        int result = unitOfWork.Complete();
        //        //cheack result 
        //        if (result == 0)
        //        {
        //            return new CreateStatus<ShoppingCartCreateView>()
        //            {
        //                Value = shoppingCartCreateView,
        //                Massage = "Try again",
        //                Status = false
        //            };

        //        }
        //        return new CreateStatus<ShoppingCartCreateView>()
        //        {
        //            Value = shoppingCartCreateView,
        //            Massage = "added",
        //            Status = true
        //        };

        //    }
        //} 
        #endregion

        #region add V2 
        public async Task<CreateStatus<ShoppingCartCreateView>> CreateShoppingCart(ShoppingCartCreateView shoppingCartCreateView)
        {

            var product = await unitOfWork.productRepository.GetProductWithLock(shoppingCartCreateView.ProductId);
            if (product == null || product.Count < shoppingCartCreateView.Count)
            {
                return new CreateStatus<ShoppingCartCreateView>()
                {
                    Value = shoppingCartCreateView,
                    Massage = "Not enough product in stock",
                    Status = false
                };
            }
            var carts = await unitOfWork.shoppiingCartRepo.GetShoppingCartsByUserCart(shoppingCartCreateView.UserId);
            var cart = carts?.FirstOrDefault(s => s.ProductId == shoppingCartCreateView.ProductId);

            if (carts == null || cart == null)
            {
                // Map and add new cart item
                var newCart = mapper.Map<ShoppingCart>(shoppingCartCreateView);
                await unitOfWork.shoppiingCartRepo.AddAsync(newCart);
            }
            else
            {
                // Update existing cart item
                cart.Count = shoppingCartCreateView.Count;
                unitOfWork.shoppiingCartRepo.Update(cart, nameof(cart.Count));
            }

            int result = unitOfWork.Complete();

            if (result == 0)
            {

                return new CreateStatus<ShoppingCartCreateView>()
                {
                    Value = shoppingCartCreateView,
                    Massage = "Try again",
                    Status = false
                };
            }


            return new CreateStatus<ShoppingCartCreateView>()
            {
                Value = shoppingCartCreateView,
                Massage = "added",
                Status = true
            };
        }
        #endregion

        public async Task<CreateStatus<ShoppingCartCreateView>> IncreaseCartCountByOne( int id )
        {
            var cart = await unitOfWork.shoppiingCartRepo.GetByIdAsync(id);
            var product = await unitOfWork.productRepository.GetByIdAsync(cart.ProductId);
            if (product.Count < cart.Count + 1) {
                return new CreateStatus<ShoppingCartCreateView>()
                {                 
                    Massage = "Item count is not enough",
                    Status = false
                };
            }
            cart.Count++;
            unitOfWork.shoppiingCartRepo.Update(cart, nameof(cart.Count));
           //unitOfWork.Complete();
            int result = unitOfWork.Complete();

            if (result == 0)
            {
                return new CreateStatus<ShoppingCartCreateView>()
                {
                    Massage = "Try again",
                    Status = false
                };
            }

            return new CreateStatus<ShoppingCartCreateView>()
            {
                Massage = "increased  by 1",
                Status = true
            };
        }

        public async Task<CreateStatus<ShoppingCartCreateView>> decresedCartCountByOne( int id)
        {
            var cart = await unitOfWork.shoppiingCartRepo.GetByIdAsync(id);
            var product = await unitOfWork.productRepository.GetByIdAsync(cart.ProductId);
            if (cart.Count == 1 )
            {
                return new CreateStatus<ShoppingCartCreateView>()
                {
                    Massage = "must have one item ",
                    Status = false
                };
            }
            cart.Count--;
            unitOfWork.shoppiingCartRepo.Update(cart, nameof(cart.Count));
          //  unitOfWork.Complete();
            int result = unitOfWork.Complete();

            if (result == 0)
            {
                return new CreateStatus<ShoppingCartCreateView>()
                {
                    Massage = "Try again",
                    Status = false
                };
            }

            return new CreateStatus<ShoppingCartCreateView>()
            {
                Massage = "decreased  by 1",
                Status = true
            };
        }

        public async Task<CreateStatus<ShoppingCartCreateView>> Remove( int id)
        {
            var cart = await unitOfWork.shoppiingCartRepo.GetByIdAsync(id);
            unitOfWork.shoppiingCartRepo.Delete(cart);
            int result = unitOfWork.Complete();

            if (result == 0)
            {
                return new CreateStatus<ShoppingCartCreateView>()
                {
                    Massage = "Try again",
                    Status = false
                };
            }
            return new CreateStatus<ShoppingCartCreateView>()
            {
                Massage = "deleted",
                Status = true
            };
        }


        public async Task<int> ClearCartAsync(string userId)
        {
            var carts = await unitOfWork.shoppiingCartRepo.GetShoppingCartsByUserCart(userId);
            unitOfWork.shoppiingCartRepo.DeleteBulk(carts);
            return unitOfWork.Complete();

        }





















        public async Task<ShoppingCartView?> ViewShoppingCart(string userId)
        {
            var Carts = await unitOfWork.shoppiingCartRepo.GetShoppingWithProductView(userId);
            if(Carts == null)
            {
                return null; 
            }
            ShoppingCartView shoppingCartView = new ShoppingCartView();
            foreach (var cart in Carts) {
                shoppingCartView.SubTotal += cart.Product.CurrentPrice * cart.Count;
                shoppingCartView.ItemCount += cart.Count;
                shoppingCartView.productShoppingCartViews.Add(new ProductShoppingCartView()
                {
                    ShoppingCartId = _protector.Protect(cart.Id.ToString()) ,
                    Price = cart.Product.Price,
                    CurrentPrice = cart.Product.CurrentPrice,
                    Urls = cart.Product.Images?.Select(x => x.Url).ToList(),
                    Discount = cart.Product.Discount,
                    Description = cart.Product.Description,
                    Model = cart.Product.Model,
                    ProductId = cart.ProductId,
                    Name = cart.Product.Name,
                    ItemCount = cart.Count 
                });  
            }
            return shoppingCartView;
        }

    }
}

