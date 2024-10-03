namespace Clothing.Application.Common.Interface
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }

    }
}
