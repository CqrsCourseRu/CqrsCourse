using CQ.CqrsFramework;

namespace Handlers.UseCases.Common.Queries.GetEntityById
{
    public abstract class GetEntityByIdQuery<TDto> : IQuery<TDto>
    {
        public int Id { get; set; }
    }
}
