namespace DDD.Application.Common.Interfaces.Services;
public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}