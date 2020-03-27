using QDetect.Data.Models;
using System;
using System.Collections.Generic;

namespace QDetect.Services.Interfaces
{
    public interface IPeopleService
    {
        Person Add(string name, string link, string uin, string city, List<double> embedding, DateTime quarantine);
        void UpdateQuarantine(int id, DateTime quarantine);
        Person Get(int id);
    }
}
