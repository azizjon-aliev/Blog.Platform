using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace AdminPanel.Components;

public partial class ConfirmationModal : ComponentBase
{
    [CascadingParameter] IModalService Modal { get; set; } = null!;
    [CascadingParameter] BlazoredModalInstance BlazoredModalInstance { get; set; } = null!;

    private async Task HandleClickCancelButton()
    {
        await BlazoredModalInstance.CancelAsync();
    }

    private async Task HandleClickConfirmButton()
    {
        await BlazoredModalInstance.CloseAsync();
    }
}