﻿using UnitOfWork.Core.Interfaces;
using UnitOfWork.Core.Models;
using UnitOfWork.Services.Interfaces;

namespace UnitOfWork.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateProduct(ProductDetails productDetails)
        {
            if (productDetails != null)
            {
                await _unitOfWork.Products.Add(productDetails);

                var result = _unitOfWork.Save();

                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            if (productId > 0)
            {
                var productDetails = await _unitOfWork.Products.GetById(productId);
                if (productDetails != null)
                {
                    _unitOfWork.Products.Delete(productDetails);
                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAll();
            return productDetailsList;
        }

        public async Task<ProductDetails> GetProductById(int productId)
        {
            if (productId > 0)
            {
                var productDetails = await _unitOfWork.Products.GetById(productId);
                if (productDetails != null)
                {
                    return productDetails;
                }
            }
            return null;
        }

        public async Task<bool> UpdateProduct(ProductDetails productDetails)
        {
            if (productDetails != null)
            {
                var product = await _unitOfWork.Products.GetById(productDetails.Id);
                if (product != null)
                {
                    product.ProductName = productDetails.ProductName;
                    product.ProductDescription = productDetails.ProductDescription;
                    product.ProductPrice = productDetails.ProductPrice;
                    product.ProductStock = productDetails.ProductStock;

                    _unitOfWork.Products.Update(product);

                    var result = _unitOfWork.Save();

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}