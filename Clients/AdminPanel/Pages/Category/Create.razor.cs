using System.Net.Http.Json;
using AdminPanel.DTO;
using Microsoft.AspNetCore.Components;

namespace AdminPanel.Pages.Category;

public partial class Create : ComponentBase
{
    readonly CategoryDto _category = new();

    [Inject] private HttpClient HttpClient { get; set; } = null!;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;


    private async Task SaveNewCategory()
    {
        var response = await HttpClient.PostAsJsonAsync("api/v1/categories", _category);

        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}