using F00F;
using Godot;

namespace Game;

public partial class Player
{
    public enum ControlType { CPU, Player1, Player2, SinglePlayer }

    public IPlayerController Input { get; private set; }

    #region Private

    private static CPUController CPU { get; } = new();
    private static Player1Input Player1 { get; } = new();
    private static Player2Input Player2 { get; } = new();
    private static SinglePlayerInput SinglePlayer { get; } = new();

    private static class Preload
    {
        public static readonly StringName CPU = nameof(CPU);
        public static readonly StringName Player1 = nameof(Player1);
        public static readonly StringName Player2 = nameof(Player2);
    }

    private void OnControllerSet()
    {
        switch (Controller)
        {
            case ControlType.CPU: Input = CPU; SetLabel(Preload.CPU); break;
            case ControlType.Player1: Input = Player1; SetLabel(Preload.Player1); break;
            case ControlType.Player2: Input = Player2; SetLabel(Preload.Player2); break;
            case ControlType.SinglePlayer: Input = SinglePlayer; SetLabel(); break;
        }

        void SetLabel(StringName name = null)
            => ControlLabel.Texture = name.IsNull() ? null : ControlLabels.Load<Texture2D>(name);
    }

    private void InitInput()
    {
        if (Input is null)
            OnControllerSet();
    }

    #endregion
}
