using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Steppenwolf.Services
{
    public class BlazorHelper
    {
        private const string JsObject = "blazorHelpers";
        private readonly IJSRuntime jsRuntime;

        public BlazorHelper(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task ScrollToFragment(string fragment)
        {
            await this.jsRuntime.InvokeVoidAsync($"{JsObject}.scrollToFragment", fragment);
        }
    }
}