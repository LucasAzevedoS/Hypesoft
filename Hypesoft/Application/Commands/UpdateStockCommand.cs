using MediatR;

namespace Hypesoft.Application.Commands
{
    public class UpdateStockCommand : IRequest<bool>
    {
        public string ProductId { get; set; } = null!;
        public int NewQuantity { get; set; }
    }
}
