using System.Collections.Generic;
using System.Threading.Tasks;

namespace QDetect.Services.Interfaces
{
    public interface IImageReceiverService
    {
        Task ProcessAsync(string link, IList<IList<double>> embeddings);
    }
}
