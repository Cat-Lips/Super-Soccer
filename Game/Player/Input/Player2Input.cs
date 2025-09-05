using Godot;

namespace Game;

public class Player2Input : PlayerInput
{
    public Player2Input() => Init();

    private static class Default
    {
        public static readonly Key MoveUp = Key.W;
        public static readonly Key MoveDown = Key.S;
        public static readonly Key MoveLeft = Key.A;
        public static readonly Key MoveRight = Key.D;

        public static readonly Key Pass = Key.Quoteleft;
        public static readonly Key Shoot = Key.Key1;
    }
}
