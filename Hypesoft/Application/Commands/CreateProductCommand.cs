using MediatR;

namespace Hypesoft.Application.Commands
{
    public class CreateProductCommand : IRequest<string>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string CategoryId { get; set; } = null!;
        public int StockQuantity { get; set; }
    }
}
