using MediatR;

namespace Hypesoft.Application.Commands
{
    public class CreateCategoryCommand : IRequest<string>

    {
        public string Name { get; set; }
    }
}
