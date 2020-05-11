using NISSitecore.Foundation.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace NISSitecore.Foundation.Core.Services
{
    public interface IMediatorService
    {
        MediatorResponse<T> GetMediatorResponse<T>(string code, T viewModel = default(T),
            ValidationResult validationResult = null, object parameters = null, string message = null);
    }
}
