using SnakeGameValidator.Model;

namespace SnakeGameValidator.Util
{
    public class GameValidator
    {
        const int OUT_OF_BOUNDS = 418;
        const int FRUIT_NOT_REACHED = 404;
        const int VALID = 200;
        const int INVALID_STATE_TICKS = 400;
        const int INTERNAL_SERVER_ERROR = 500;
        List<List<Tick>> invalidVelocitiesPair = new List<List<Tick>>()
            {
            new List<Tick>() { new Tick() { velX = 1, velY = 0 }, new Tick() { velX = -1, velY = 0 } },
            new List<Tick>() { new Tick() { velX = 0, velY = 1 }, new Tick() { velX = 0, velY = -1} },
            new List<Tick>() { new Tick() { velX = 1, velY = 1 }, new Tick(){ velX = -1, velY = -1 } }
            };

        public int Validate(GameState gs)
        {
            try
            {
                if (!isValidState(gs)) return INVALID_STATE_TICKS;

                var currLocX = gs.gameState.snake.X;
                var currLocY = gs.gameState.snake.Y;
                var currVx = gs.gameState.snake.VelX;
                var currVy = gs.gameState.snake.VelY;

                foreach (var tick in gs.ticks)
                {
                    if (!isValidMove(new Tick() { velX = currVx, velY = currVy }, tick)) return INVALID_STATE_TICKS;

                    if (tick.velX != 0)
                    {
                        currLocX += tick.velX;
                    }
                    else
                    {
                        currLocX += currVx;
                    }

                    if (tick.velY != 0)
                    {
                        currLocY += tick.velY;
                    }
                    else
                    {
                        currLocY += currVy;
                    }
                    currVx = tick.velX;
                    currVy = tick.velY;
                    if (currLocX == gs.gameState.width || currLocY == gs.gameState.height) return OUT_OF_BOUNDS;

                }
                if ((currLocX != gs.gameState.fruit.X) && (currLocY != gs.gameState.fruit.Y)) return FRUIT_NOT_REACHED;

            }
            catch (Exception e)
            {
                //Assuming exception is logged somewhere - OOS
                return INTERNAL_SERVER_ERROR;
            }

            return VALID;
        }

        bool isValidState(GameState gs)
        {
            //1. Board size must be at least 2 - one for snake coord, one for fruit
            if (gs.gameState.width >= 2 || gs.gameState.height >= 2)
            {
                //2. snake must start at pos 0,0
                if (gs.gameState.snake.X == 0 && gs.gameState.snake.Y == 0)
                {
                    //3. The snake velocity 1,0
                    if (gs.gameState.snake.VelX == 1 && gs.gameState.snake.VelY == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        bool isValidMove(Tick t1, Tick t2)
        {
            var move = new List<Tick>() { t1, t2 };
            var invalid = invalidVelocitiesPair.Any(x => x.All(move.Contains));
            return !invalid;
        }
    }
}
