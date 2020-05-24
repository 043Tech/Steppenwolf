using System.Threading.Tasks;
using Microsoft.JSInterop;
using Steppenwolf.Shared;

namespace Steppenwolf.Services
{
    public class BlazorHelper
    {
        private const string BlazorHelpers = "blazorHelpers";
        private const string CkEditorInterop = "CKEditorInterop";
        private readonly IJSRuntime jsRuntime;

        public BlazorHelper(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task ScrollToFragment(string fragment)
        {
            await this.jsRuntime.InvokeVoidAsync($"{BlazorHelpers}.scrollToFragment", fragment);
        }

        public async Task InitCkEditor<T>(string id, DotNetObjectReference<T> objectReference) where T : class
        {
            await this.jsRuntime.InvokeVoidAsync($"{CkEditorInterop}.init", id, objectReference);
        }
    }
}