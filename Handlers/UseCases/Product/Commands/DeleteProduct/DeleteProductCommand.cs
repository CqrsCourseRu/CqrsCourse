using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers.CqrsFramework;
using Handlers.UseCases.Common.Commands.DeleteEntity;

namespace Handlers.UseCases.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : DeleteEntityCommand, IRequest
    {
    }
}
