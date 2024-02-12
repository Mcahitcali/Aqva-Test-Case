using AqvaCode.Models;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Transport;


namespace AqvaCode.Services;

public class ElasticsearchService : IElasticsearchService
{
    private readonly IConfiguration _configuration;
    private readonly ElasticsearchClient _client;

    public ElasticsearchService(IConfiguration configuration)
    {
        _configuration = configuration;
        _client = CreateInstance();
    }

    private ElasticsearchClient CreateInstance()
    {
        string cloudId = _configuration.GetSection("ElasticsearchServer:CloudId").Value;
        string apiKey = _configuration.GetSection("ElasticsearchServer:ApiKey").Value;


        return new ElasticsearchClient(cloudId, new ApiKey(apiKey));
    }

    public async Task ChekIndex(string indexName)
    {
        var anyIndex = await _client.Indices.ExistsAsync(indexName);

        if (anyIndex.Exists)
        {
            return;
        }

        var response = await _client.Indices.CreateAsync(indexName,
        ci => ci
        .Index(indexName)
        .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
        );

        return;
    }

    public async Task<Data> GetDocument(string indexName, int id)
    {
        var response = await _client.GetAsync<Data>(id, q => q.Index(indexName));

        return response.Source;
    }

    public async Task<List<Data>> GetDocuments(string indexName)
    {
        var response = await _client.SearchAsync<Data>(s => s
        .Index(indexName)
        .From(0)
        .Size(100)
        );

        return response.Documents.ToList();
    }

    public async Task InsertDocument(string indexName, Data data)
    {
        var response = await _client.CreateAsync(data, idx => idx.Index(indexName));

        if (response.ApiCallDetails.HttpStatusCode == 409)
        {
            await _client.UpdateAsync<Data, Data>(indexName, 1, u => u.Doc(data));
        }
    }

    public async Task<List<Data>> SearchDocuments(string indexName, string searchText)
    {
        var response = await _client.SearchAsync<Data>(s => s
            .Index(indexName)
            .From(0)
            .Size(100)
            // .Query(q => q.MatchPhrasePrefix(m => m.Field(f => f.Title).Query(title)))
            .Query(q => q.Bool(
                b => b.Should(
                    mt => mt.MatchPhrasePrefix(m => m.Field(f => f.Title).Query(searchText)),
                    tx => tx.MatchPhrasePrefix(m => m.Field(f => f.Url).Query(searchText))
                    )
                )
            )
        );

        return response.Documents.ToList();
    }
}