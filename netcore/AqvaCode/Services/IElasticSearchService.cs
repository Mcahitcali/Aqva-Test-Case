using AqvaCode.Models;

namespace AqvaCode.Services;

public interface IElasticsearchService
{
    Task ChekIndex(string indexName);
    Task<Data> GetDocument(string indexName, int id);
    Task<List<Data>> GetDocuments(string indexName);
    Task InsertDocument(string indexName, Data data);
    Task<List<Data>> SearchDocuments(string indexName, string title);
}