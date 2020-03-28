using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace QDetect.Services.Interfaces
{
    public interface IImageReceiverService
    {
        Task ProcessAsync(IFormFile link, IList<IList<double>> embeddings);
    }
}
