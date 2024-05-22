using System.Net.Http.Json;
using AdminPanel.DTO;
using Microsoft.AspNetCore.Components;


namespace AdminPanel.Pages.Category;

public partial class Index : ComponentBase
{
    [Inject] private HttpClient HttpClient { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

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
        var response = await HttpClient.DeleteAsync($"api/v1/Categories/{id}");

        if (response.IsSuccessStatusCode)
        {
            await OnInitializedAsync();
        }
    }
}