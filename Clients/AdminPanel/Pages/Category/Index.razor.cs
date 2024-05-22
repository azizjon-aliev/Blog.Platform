using System.Net.Http.Json;
using AdminPanel.DTO;
using Microsoft.AspNetCore.Components;
using Blazored.Modal;
using Blazored.Modal.Services;
using AdminPanel.Components;


namespace AdminPanel.Pages.Category;

public partial class Index : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    [CascadingParameter] IModalService Modal { get; set; } = default!;

    private List<CategoryDto>? _categories;

    protected override async Task OnInitializedAsync()
    {
        _categories = await HttpClient.GetFromJsonAsync<List<CategoryDto>>("api/v1/Categories");
    }

    private void HandleClickAddItemButton()
    {
        NavigationManager.NavigateTo("categories/create");
    }

    private void HandleClickEditItemButton(Guid id)
    {
        NavigationManager.NavigateTo($"categories/edit/{id}");
    }

    private async Task HandleClickRemoveItemButton(Guid id)
    {
        var parameters = new ModalParameters();
        var options = new ModalOptions
        {
            UseCustomLayout = true
        };

        var modal = Modal.Show<ConfirmationModal>("Подвердите действия", parameters, options);
        var result = await modal.Result;

        if (!result.Cancelled)
        {
            var response = await HttpClient.DeleteAsync($"api/v1/Categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                await OnInitializedAsync();
            }
        }
    }
}