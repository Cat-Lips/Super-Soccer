using Godot;

namespace Game;

public class Player1Input : PlayerInput
{
    public Player1Input() => Init();

    private static class Default
    {
        public static readonly Key MoveUp = Key.Up;
        public static readonly Key MoveDown = Key.Down;
        public static readonly Key MoveLeft = Key.Left;
        public static readonly Key MoveRight = Key.Right;

        public static readonly Key Pass = Key.Bracketleft;
        public static readonly Key Shoot = Key.Bracketright;
    }
}
