using System.Text.Json;
using System.Text.Json.Serialization;
using AqvaCode.Models;
using AqvaCode.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AqvaCode.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IElasticsearchService _elasticsearchService;
    public List<Data> data { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IElasticsearchService elasticsearchService)
    {
        _logger = logger;
        _elasticsearchService = elasticsearchService;
    }

    public async Task OnGetAsync()
    {
        await _elasticsearchService.ChekIndex("sozcu-urls");
        await Task.Delay(500);

        data = await _elasticsearchService.GetDocuments("sozcu-urls");
    }

    public async Task<PartialViewResult> OnGetListTable(string searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText)) {
            data = await _elasticsearchService.GetDocuments("sozcu-urls");
        } else {
            data = await _elasticsearchService.SearchDocuments("sozcu-urls", searchText);
        }
        

        return Partial("_listTable", data);
    }
}
