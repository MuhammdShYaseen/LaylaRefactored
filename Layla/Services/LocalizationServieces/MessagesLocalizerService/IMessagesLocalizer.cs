using Microsoft.Extensions.Localization;

namespace Layla.Services.LocalizationServieces.MessagesLocalizerService
{
    public interface IMessagesLocalizer
    {
        IStringLocalizer Get(string moduleName, string group = "");
        string Localize(string key, params object?[]? args);
    }
}
