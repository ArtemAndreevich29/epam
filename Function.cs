using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using GetAllChats.Models;
using System.Net;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace GetAllChats;

public class Function
{
    private readonly AmazonDynamoDBClient _client;
    private readonly DynamoDBContext _context;

    public Function()
    {
        _client = new AmazonDynamoDBClient();
        _context = new DynamoDBContext(_client);
    }

    public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var userId = request.QueryStringParameters["userId"];

        List<Chat> chats = await GetAllChats(userId);

        var result = new List<GetAllChatsResponseItem>(chats.Count);

		foreach (Chat chat in chats)
        {
            List<User> participants = await GetChatParticipants(chat.ChatId);
            result.Add(new GetAllChatsResponseItem
            {
                Chat = chat,
                Participants = participants
            });
        }

        return new APIGatewayProxyResponse
        {
            StatusCode = 200,
            Body = JsonConvert.SerializeObject(result),
            Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
        };
    }
    catch (Exception ex)
    {
        return new APIGatewayProxyResponse
        {
            StatusCode = 500,
            Body = $"Error: {ex.Message}",
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };
    }
}

private async Task<List<Chat>> GetChatsWithPagination(string userId, int page, int pageSize)

private async Task<List<User>> GetChatParticipants(string chatId)

public class Chat
{
    public string ChatId { get; set; }
}

public class User
{
    public string UserId { get; set; }
}

public class GetAllChatsResponseItem
{
    public Chat Chat { get; set; }
    public List<User> Participants { get; set; }
}
		
        return new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Access-Control-Allow-Origin", "*" }
            },

            Body = JsonSerializer.Serialize(result)
        };
    }

    private async Task<List<Chat>> GetAllChats(string userId)
    {
        var user1 = new QueryOperationConfig()
        {
            IndexName = "user1-updatedDt-index",
            KeyExpression = new Expression()
            {
                ExpressionStatement = "user1 = :user",
                ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>() { { ":user", userId } }
            }
        };
        var user1Results = await _context.FromQueryAsync<Chat>(user1).GetRemainingAsync();

        var user2 = new QueryOperationConfig()
        {
            IndexName = "user2-updatedDt-index",
            KeyExpression = new Expression()
            {
                ExpressionStatement = "user2 = :user",
                ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>() { { ":user", userId } }
            }
        };
        var user2Results = await _context.FromQueryAsync<Chat>(user2).GetRemainingAsync();

        user1Results.AddRange(user2Results);
        return user1Results.OrderBy(x => x.UpdateDt).ToList();
    }
}
