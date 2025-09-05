using Godot;

namespace Game;

public partial class Player
{
    public static class Anims
    {
        public static readonly StringName Run = nameof(Run);
        public static readonly StringName Idle = nameof(Idle);
        public static readonly StringName Kick = nameof(Kick);
        public static readonly StringName Tackle = nameof(Tackle);
        public static readonly StringName PreKick = nameof(PreKick);
        public static readonly StringName Recover = nameof(Recover);
    }
}
