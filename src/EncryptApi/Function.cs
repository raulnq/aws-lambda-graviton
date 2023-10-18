using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using System.Security.Cryptography;
using System.Text;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PIApi;

public class Function
{
    public async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest input, ILambdaContext context)
    {
        var text = new List<string>();
        for (int i = 0; i < 50; i++)
        {
            text.Add(Guid.NewGuid().ToString());
        }

        var byteConverter = new UnicodeEncoding();
        using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
        {
            Parallel.ForEach(text, t => Encryption(byteConverter.GetBytes(t), RSA.ExportParameters(false), false));
        }

        return new APIGatewayHttpApiV2ProxyResponse
        {
            StatusCode = 200,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }

    static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
    {
        try
        {
            byte[] encryptedData;
            using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
            {
                RSA.ImportParameters(RSAKey);
                encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
            }
            return encryptedData;
        }
        catch (CryptographicException)
        {
            return Array.Empty<byte>();
        }
    }
}
