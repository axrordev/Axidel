using Axidel.Domain.Commons;

namespace Axidel.Domain.Entities.Commons;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}
