using System.Diagnostics.CodeAnalysis;

namespace Lingva.BC.Dto
{
    public class TagDto
    {
        [ExcludeFromCodeCoverage]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
