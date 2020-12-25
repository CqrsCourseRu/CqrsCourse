using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers.UseCases.Common.Commands.DeleteEntity
{
    public abstract class DeleteEntityCommand
    {
        public int Id { get; set; }
    }
}
