using Godot;

namespace Game;

using Anim = Ball.Anims;

public partial class BallStateFree : BallState
{
    public sealed override void _EnterTree()
        => Ball.ContactZone.BodyEntered += OnBodyEntered;

    public sealed override void _ExitTree()
        => Ball.ContactZone.BodyEntered -= OnBodyEntered;

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            Ball.Carrier = player;
            SetNextState(StateType.Carried);
        }
    }

    public sealed override void _PhysicsProcess(double _delta)
    {
        var delta = (float)_delta;
        ApplyBounce(delta);
        MoveBall(delta);

        void MoveBall(float delta)
        {
            if (Ball.IsMoving)
            {
                var friction = Ball.Height is 0 ? Ball.FrictionGround : Ball.FrictionAir;
                Ball.Velocity = Ball.Velocity.MoveToward(Vector2.Zero, friction * delta);
                Ball.Anim.Play(Anim.Roll);
                ApplyMotion(delta);
                return;
            }

            Ball.Anim.Play(Anim.Idle);
        }
    }
}
