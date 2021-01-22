using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;

namespace Handlers.UseCases.Product.Commands.DeleteAllProducts
{
    public class DeleteAllProductsCommand : IRequest
    {
        public DeleteAllDto Dto { get; set; }
    }
}
