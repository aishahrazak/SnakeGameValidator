using SnakeGameValidator.Model;
using SnakeGameValidator.Util;
using System.ComponentModel.DataAnnotations;

namespace SnakeGameValidator
{
    public static class Endpoints
    {
        public static void Map(WebApplication app)
        {
            var validator = new GameValidator();
            const int INVALID_METHOD = 405;


            // Calling this API will return the initial position of the fruit based on the board size 
            // so that its position is always inside the game board
            app.MapGet("/new", (int w, int h) =>
            {
                if (w == 0 && h == 0) return Results.BadRequest();

                try
                {
                    var fruitPosition = generateRandomCoord(w, h);
                    return Results.Ok(new Fruit
                    {
                        X = fruitPosition.Item1,
                        Y = fruitPosition.Item2
                    });
                }
                catch (Exception e)
                {
                    //Assuming exception is logged somewhere
                    return Results.StatusCode(500);
                }
            })
            .WithName("new")
            .WithOpenApi();

            // Calling this API will validate the moves the snake made to get to the fruits
            // If the move is valid, the current game score will be incremented
            // Assumptions : that the ticks is sent in order
            app.MapPost("/validate", (GameState gs) =>
            {
                var statusCode = validator.Validate(gs);
                if (statusCode.Equals(200)) //TODO: create constant/reuse available ones
                {
                    var newState = new State();
                    newState.score = gs.gameState.score + 1;
                    newState.width = gs.gameState.width;
                    newState.height = gs.gameState.height;
                    var newFruitPos = generateRandomCoord(gs.gameState.width, gs.gameState.height);
                    newState.fruit = new Fruit { X = newFruitPos.Item1, Y = newFruitPos.Item2 };
                    newState.snake = gs.gameState.snake;
                    return Results.Ok(newState);
                }
                else
                {
                    return Results.StatusCode(statusCode);
                }
            })
                .WithName("validate")
                .WithOpenApi();

            app.Map("{**catch-all}", () =>
            {
                return Results.StatusCode(INVALID_METHOD);
            }).WithOpenApi();
        }

        static Tuple<int, int> generateRandomCoord(int w, int h)
        {
            var rand = new Random();
            var x = rand.Next(0, w);
            var y = rand.Next(0, h);

            //If generated coord is the origin, change the x coord
            if (x == 0 && y == 0)
                x = rand.Next(1, h);
            return Tuple.Create(x, y);
        }
    }
}
