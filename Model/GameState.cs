using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SnakeGameValidator.Model
{
    [Serializable]
    public class GameState
    {
        [JsonPropertyName("state")]
        public State gameState { get; set; }

        [JsonPropertyName("ticks")]
        public required List<Tick> ticks { get; set; }
    }

    [Serializable]
    public struct State
    {
        [JsonPropertyName("gameId")]
        public string gameID { get; set; }

        [JsonPropertyName("width")]
        public int width { get; set; }

        [JsonPropertyName("height")]
        public int height { get; set; }

        [JsonPropertyName("score")]
        public int score { get; set; }

        [JsonPropertyName("fruit")]
        public Fruit fruit { get; set; }

        [JsonPropertyName("snake")]
        public Snake snake { get; set; }
    }

    [Serializable]
    public struct Fruit
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }
    }

    [Serializable]
    public struct Snake
    {
        [JsonPropertyName("x")]
        public int X { get; set; }

        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("velX")]
        public int VelX { get; set; } // X velocity of the snake (one of -1, 0, 1)

        [JsonPropertyName("velY")]
        public int VelY { get; set; } // Y velocity of the snake (one of -1, 0, 1)}
    }

    [Serializable]
    public struct Tick
    {
        [JsonPropertyName("velX")]
        public int velX { get; set; } // X velocity of the snake (one of -1, 0, 1)

        [JsonPropertyName("velY")]
        public int velY { get; set; } // Y velocity of the snake (one of -1, 0, 1)}
    }
}
