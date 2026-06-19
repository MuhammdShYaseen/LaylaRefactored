namespace Layla.Domain.DomainServices
{
    public interface ISupportedLanguagePolicy
    {
        bool IsSupported(string code);
    }
}