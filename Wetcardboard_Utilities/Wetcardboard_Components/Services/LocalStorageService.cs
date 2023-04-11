using Microsoft.JSInterop;
using System.Text.Json;
using Wetcardboard_Components.Services.Interfaces;

namespace Wetcardboard_Components.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        #region Fields & Properties
        #region Constants
        private const string GETITEM = $"localStorage.getItem";
        private const string REMOVEITEM = $"localStorage.removeItem";
        private const string SETITEM = $"localStorage.setItem";
        #endregion \ Constants

        #region Fields
        private readonly IJSRuntime _runtime;
        #endregion \ Fields
        #endregion \ Fields & Properties


        #region Interface Implementations
        #region ILocalStorageService Implementation
        public async Task<T?> GetItem<T>(string key)
        {
            var json = await _runtime.InvokeAsync<string>(GETITEM, key);
            if (string.IsNullOrEmpty(json)) return default;
            return JsonSerializer.Deserialize<T>(json);
        }
        public async Task RemoveItem(string key)
        {
            await _runtime.InvokeVoidAsync(REMOVEITEM, key);
        }
        public async Task SetItem<T>(string key, T value)
        {
            await _runtime.InvokeVoidAsync(SETITEM, key, JsonSerializer.Serialize(value));
        }
        #endregion \ ILocalStorageService Implementation
        #endregion \ Interface Implementations
    }
}
