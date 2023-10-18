using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace PIApi;

public class Function
{
    public async Task<APIGatewayHttpApiV2ProxyResponse> FunctionHandler(APIGatewayHttpApiV2ProxyRequest input, ILambdaContext context)
    {
        var value = CalculatePI(10000000);

        return new APIGatewayHttpApiV2ProxyResponse
        {
            Body = $@"{{""Value"":{value}}}",
            StatusCode = 200,
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }

    private double CalculatePI(double iterations)
    {
        double numerator = 4;
        double denominator = 1;
        double pi = 0;
        int @operator = 1;
        for (var x=0; x < iterations; x++)
        {
            pi += @operator * (numerator / denominator);
            denominator += 2;
            @operator *= -1;
        }
        return pi;
    }
} 
