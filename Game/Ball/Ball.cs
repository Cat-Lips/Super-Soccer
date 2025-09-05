using System;
using F00F;
using Godot;

namespace Game;

using static BallState;

[Tool]
public partial class Ball : AnimatableBody2D
{
    #region Private

    private Action<StateType> SetState;

    internal Sprite2D Sprite => field ??= (Sprite2D)GetNode(nameof(Sprite));
    internal AnimationPlayer Anim => field ??= (AnimationPlayer)GetNode(nameof(Anim));
    internal Area2D ContactZone => field ??= (Area2D)GetNode(nameof(ContactZone));

    #endregion

    #region Export

    [Export(PropertyHint.Range, "0,1")]
    public float Bounciness { get; set; } = .8f;

    [Export] public float FrictionAir { get; set; } = 35;
    [Export] public float FrictionGround { get; set; } = 250;
    [Export] public float HighPassThreshold { get; set; } = 130;

    #endregion

    public bool IsMoving => Velocity.NotZero();
    public bool IsFlipped => Sprite.IsFlipped();

    public float Height { get; internal set; }
    public Player Carrier { get; internal set; }
    public Vector2 Velocity { get; internal set; }
    public float HeightVelocity { get; internal set; }

    public void Kick(in Vector2 velocity)
    {
        Carrier = null;
        Velocity = velocity;
        SetState(StateType.Kicked);
    }

    public void Pass(in Vector2 to)
    {
        Carrier = null;
        (Velocity, HeightVelocity) = Position.GetVelocityTo(to, FrictionGround, HighPassThreshold, .2f);
        SetState(StateType.Free);
    }

    public void Stop()
        => Velocity = Vector2.Zero;

    #region Godot

    public sealed override void _Ready()
    {
        SyncToPhysics = false;

        this.SetCollisionLayer(Layer.Ball);
        this.SetCollisionMask(Layer.Walls);

        ContactZone.SetCollisionLayer();
        ContactZone.SetCollisionMask(Layer.Player);

        Editor.Disable(this);
        if (Editor.IsEditor) return;

        InitState(this, out SetState);
    }

    public sealed override void _PhysicsProcess(double _)
    {
        Sprite.Flip(Velocity);
        Sprite.Position = Vector2.Up * Height;
    }

    #endregion
}
