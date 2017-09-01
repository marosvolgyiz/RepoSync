using SenseNet.Client;

namespace RepoSync.Converters
{
    public interface IContentConverter
    {
        string ConvertFromContent(Content c);
        Content ParseFromString(string s);
    }
}
