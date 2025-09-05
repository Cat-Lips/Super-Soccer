using F00F;
using Godot;

namespace Game;

[Tool]
public partial class Player : CharacterBody2D
{
    #region Private

    private Sprite2D Sprite => field ??= (Sprite2D)GetNode(nameof(Sprite));
    internal AnimationPlayer Anim => field ??= (AnimationPlayer)GetNode(nameof(Anim));
    private Sprite2D ControlLabel => field ??= (Sprite2D)GetNode($"%{nameof(ControlLabel)}");
    private ResourcePreloader ControlLabels => field ??= (ResourcePreloader)GetNode($"%{nameof(ControlLabels)}");

    internal Area2D ActionZone => field ??= (Area2D)GetNode(nameof(ActionZone));
    internal Area2D ContactZone => field ??= (Area2D)GetNode(nameof(ContactZone));

    private Marker2D BallPoint => field ??= (Marker2D)Sprite.GetNode(nameof(BallPoint));
    private Marker2D FlipPoint => field ??= (Marker2D)Sprite.GetNode(nameof(FlipPoint));
    internal Vector2 GetBallPoint() => (Sprite.IsFlipped() ? FlipPoint : BallPoint).GlobalPosition;

    internal Ball Ball => field ??= (Ball)this.GetSibling(nameof(Ball));

    #endregion

    #region Export

    [Export] public float Power { get; set; } = 150;
    [Export] public float Speed { get; set; } = 80;

    [Export] public ControlType Controller { get; private set => this.Set(ref field, value, OnControllerSet); }

    #endregion

    public bool IsFlipped => Sprite.IsFlipped();
    public bool HasBall => Ball.Carrier == this;

    #region Godot

    public sealed override void _Ready()
    {
        this.SetCollisionLayer(Layer.Player);
        this.SetCollisionMask(Layer.Walls, Layer.GoalLine);

        ActionZone.SetCollisionLayer();
        ActionZone.SetCollisionMask(Layer.Player);

        ContactZone.SetCollisionLayer();
        ContactZone.SetCollisionMask(Layer.Ball);

        Editor.Disable(this);
        if (Editor.IsEditor) return;

        InitInput();
        PlayerState.Init(this);
    }

    public sealed override void _Process(double _)
    {
        ControlLabel.Visible =
            HasBall && Controller is ControlType.CPU ||
            Controller is ControlType.Player1 or ControlType.Player2;
    }

    public sealed override void _PhysicsProcess(double _)
        => Sprite.Flip(Velocity);

    #endregion
}
