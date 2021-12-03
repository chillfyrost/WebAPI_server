using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IMovieRepository Movie { get; }
        Task SaveAsync();
    }
}
