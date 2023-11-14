var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Calling this API will return the initial position of the fruit based on the board size 
// so that its position is always inside the game board
app.MapGet("/startGame", (int boardSize) =>
{
    return generateRandomCoord(boardSize);
})
    .WithName("StartGame")
    .WithOpenApi();

// Calling this API will validate the moves the snake made to get to the fruits
// If the move is valid, the current game score will be incremented
// Assumptions : that the ticks is sent in order
app.MapPost("/validateMoves", (List < Tuple<int, int> > ticks, int score) =>
{
    Console.WriteLine(ticks.Capacity);
    Console.WriteLine(score.ToString());
    return Results.Ok(score++ );
})
    .WithName("validateMoves")
    .WithOpenApi();

app.Run();
Tuple<int,int> generateRandomCoord(int size)
{
    var rand = new Random();
    var x = rand.Next(0, size);
    var y = rand.Next(0, size);
    return Tuple.Create(x, y);
}