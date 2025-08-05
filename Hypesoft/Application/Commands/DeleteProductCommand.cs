using MediatR;

namespace Hypesoft.Application.Commands
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public string Id { get; }

        public DeleteProductCommand(string id)
        {
            Id = id;
        }
    }
}
