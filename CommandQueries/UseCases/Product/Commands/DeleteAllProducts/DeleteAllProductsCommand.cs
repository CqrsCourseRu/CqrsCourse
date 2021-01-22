using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQ.CqrsFramework;

namespace CQ.UseCases.Product.Commands.DeleteAllProducts
{
    public class DeleteAllProductsCommand : ICommand
    {
        public DeleteAllDto Dto { get; set; }
    }
}
