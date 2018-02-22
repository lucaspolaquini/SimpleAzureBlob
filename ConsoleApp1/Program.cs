using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connection String
            CloudStorageAccount account = CloudStorageAccount.Parse("");
            var blobClient = account.CreateCloudBlobClient();
            
            // Selecionando Container
            var container =  blobClient.GetContainerReference("xpto");
            container.CreateIfNotExists();

            //Criar o arquivo
            var blob = container.GetBlockBlobReference("NameOfFile");
            blob.UploadText("1 2 3 testando");
            blob.Metadata.Add("autor", "lucas");

            //Shared Access Signature
            var sas = container.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddMinutes(10),
                SharedAccessStartTime = DateTime.UtcNow
            });

            //Listagem
            foreach (var item in container.ListBlobs())
            {
                Console.WriteLine(item.Uri);
            }

            //Apagando container
            container.Delete();
            Console.Read();

        }
    }
}
