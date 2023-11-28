package handler;

import com.amazonaws.services.lambda.runtime.Context;
import com.amazonaws.services.lambda.runtime.RequestHandler;
import com.amazonaws.services.lambda.runtime.events.APIGatewayProxyRequestEvent;
import com.amazonaws.services.lambda.runtime.events.APIGatewayProxyResponseEvent;
import layer.service.APIGatewayService;
import layer.service.APIGatewayServiceImpl;
import layer.service.DynamoDBService;
import layer.service.DynamoDBServiceImpl;


public class UpdateUserFunction implements RequestHandler<APIGatewayProxyRequestEvent, APIGatewayProxyResponseEvent> {
    private static final DynamoDBService dynamoDBService = new DynamoDBServiceImpl();
    private static final APIGatewayService apiGatewayService = new APIGatewayServiceImpl();

    public APIGatewayProxyResponseEvent handleRequest(final APIGatewayProxyRequestEvent input,
                                                      final Context context) {
        try {
            Map<String, String> pathParameters = input.getPathParameters();
            String inputBody = input.getBody();

            
            String resultMessage = updateUser(pathParameters, inputBody);

            
            return new APIGatewayProxyResponseEvent()
                    .withStatusCode(200)
                    .withBody(resultMessage);
        } catch (Exception e) {
            
            return new APIGatewayProxyResponseEvent()
                    .withStatusCode(503)
                    .withBody("Error updating record: " + e.getMessage());
        }
    }

    private String updateUser(Map<String, String> pathParameters, String inputBody) {
        
        try {
            
            
            
            return "Record updated successfully";
        } catch (Exception e) {
            
            throw new RuntimeException("Error updating record: " + e.getMessage());
        }
    }   
}

