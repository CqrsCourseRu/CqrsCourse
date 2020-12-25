using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationServices.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Entities;
using Infrastructure.Interfaces;
using Layers.ApplicationServices.Interfaces.Product;
using Microsoft.EntityFrameworkCore;

namespace Layers.ApplicationServices.Implementation
{
    public class ReadOnlyProductService : ReadOnlyEntityService<Product, ProductDto>, IReadOnlyProductService
    {
        public ReadOnlyProductService(IReadOnlyDbContext dbContext, IMapper mapper) 
            : base(dbContext, mapper)
        {
        }

    }
}
