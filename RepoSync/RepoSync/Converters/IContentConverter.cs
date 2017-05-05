using SenseNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoSync.Converters
{
    public interface IContentConverter
    {
        string ConvertFromContent(Content c);
        Content ParseFromString(string s);
    }
}
