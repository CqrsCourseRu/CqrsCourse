using CQ.CqrsFramework;

namespace Handlers.UseCases.Order.Commands.UpdateOrder
{
    public abstract class UpdateEntityCommand<TDto> : ICommand
    {
        public int Id { get; set; }
        public TDto Dto { get; set; }
    }
}
