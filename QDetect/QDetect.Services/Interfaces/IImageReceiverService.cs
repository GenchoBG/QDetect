using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QDetect.Services.Interfaces
{
    public interface IImageReceiverService
    {
        Task ProcessAsync(string link, IList<IList<double>> embeddings);
    }
}
