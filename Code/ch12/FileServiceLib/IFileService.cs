using System;
using System.ServiceModel;

namespace FileServiceLib
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        string[] GetSubDirectories(string directory);
        
        [OperationContract]
        string[] GetFiles(string directory);
        
        [OperationContract]
        int RetrieveFile(string filename, int amountToRead, out byte[] bytes);
    }
}
