using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Components.Controls
{
    public partial class NameForm
    {
        [SupplyParameterFromForm]
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Username { get; set; } = string.Empty;

        [Parameter] public EventCallback<string> OnSubmit { get; set; }

        private async Task HandleValidSubmit()
        {
            await OnSubmit.InvokeAsync(Username);
        }
    }
}
