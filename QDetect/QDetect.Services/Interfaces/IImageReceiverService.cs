using System;
using System.Collections.Generic;
using System.Text;

namespace QDetect.Services.Interfaces
{
    public interface IImageReceiverService
    {
        void Process(string link, List<List<double>> embeddings);
    }
}
