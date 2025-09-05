using Godot;

namespace Game;

using Anim = Ball.Anims;

public partial class BallStateKicked : BallState
{
    public const int Height = 5;
    public const float Scale = .8f;
    public const float Duration = 1;

    private float time;

    public sealed override void _EnterTree()
    {
        Ball.Height = Height;
        Ball.Anim.Play(Anim.Roll);
        Ball.Sprite.Scale = new Vector2(1, Scale);
    }

    public sealed override void _ExitTree()
        => Ball.Sprite.Scale = Vector2.One;

    public sealed override void _PhysicsProcess(double _delta)
    {
        var delta = (float)_delta;
        if ((time += delta) >= Duration)
            SetNextState(StateType.Free);
        else ApplyMotion(delta);
    }
}
