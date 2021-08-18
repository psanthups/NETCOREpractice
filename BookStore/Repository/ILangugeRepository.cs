using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface ILangugeRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}