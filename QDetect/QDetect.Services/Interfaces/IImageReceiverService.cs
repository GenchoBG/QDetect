using System.Collections.Generic;
using System.Threading.Tasks;

namespace QDetect.Services.Interfaces
{
    public interface IImageReceiverService
    {
        Task Process(string link, List<List<double>> embeddings);
    }
}
