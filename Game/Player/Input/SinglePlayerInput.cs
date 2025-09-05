using Godot;

namespace Game;

public class SinglePlayerInput : PlayerInput
{
    public SinglePlayerInput() => Init();

    private static class Default
    {
        public static readonly Key[] MoveUp = [Key.W, Key.Up];
        public static readonly Key[] MoveDown = [Key.S, Key.Down];
        public static readonly Key[] MoveLeft = [Key.A, Key.Left];
        public static readonly Key[] MoveRight = [Key.D, Key.Right];

        public static readonly Key Pass = Key.Bracketleft;
        public static readonly Key Shoot = Key.Bracketright;
    }
}
