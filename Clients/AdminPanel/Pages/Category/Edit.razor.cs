using System.Net.Http.Json;
using AdminPanel.DTO;
using Microsoft.AspNetCore.Components;

namespace AdminPanel.Pages.Category;

public partial class Edit : ComponentBase
{
    [Parameter] public Guid Id { get; set; }
    [Inject] private HttpClient HttpClient { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;


    private CategoryDto _category = null!;

    protected override async Task OnInitializedAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<CategoryDto?>($"api/v1/Categories/{Id}");

        if (response != null)
        {
            _category = response;
        }
        else
        {
            NavigationManager.NavigateTo("/categories");
        }
    }


    private async Task HandleValidSubmit()
    {
        var response = await HttpClient.PutAsJsonAsync($"api/v1/Categories/{Id}", _category);

        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/categories");
        }
    }

    private void HandleClickCancelButton()
    {
        NavigationManager.NavigateTo("/categories");
    }
}