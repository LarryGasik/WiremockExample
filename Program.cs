using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

Console.WriteLine("Let's do Hello world");
var server = WireMockServer.Start(port: 5001); // http://localhost:5001
Console.WriteLine("WireMock running at {0}", server.Urls[0]);

server
    .Given(
        Request.Create()
            .WithPath("/api/hello")
            .UsingGet()
    )
    .RespondWith(
        Response.Create()
            .WithStatusCode(200)
            .WithHeader("Content-Type", "application/json")
            .WithBody("{ \"message\": \"Diesel wants to be fed\" }")
    );

server
    .Given(
        Request.Create()
            .WithPath("/api/ThrowError")
            .UsingPost()
    )
    .RespondWith(
        Response.Create()
            .WithStatusCode(500)
            .WithHeader("Content-Type", "application/json")
            .WithBody("{ \"error\": \"Something went wrong on the server.\" }")
    );
server
    .Given(
        Request.Create()
            .WithPath("/api/TakeYourTime")
            .UsingPut()
    )
    .RespondWith(
        Response.Create()
            .WithStatusCode(504)
            .WithDelay(TimeSpan.FromSeconds(3)) 
            .WithHeader("Content-Type", "application/json")
            .WithBody("{ \"error\": \"Gateway timeout. Server is taking too long to respond.\" }")
    );

Console.WriteLine("Execute a get on http://localhost:5001/api/hello");
Console.WriteLine("Execute a post on http://localhost:5001/api/ThrowError");
Console.WriteLine("Execute a put on http://localhost:5001/api/TakeYourTime");
Console.ReadKey();

server.Stop();