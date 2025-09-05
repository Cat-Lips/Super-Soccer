using F00F;
using Godot;

namespace Game;

using Anim = Ball.Anims;

public partial class BallStateCarried : BallState
{
    private const float DribbleFrequency = 10;
    private const float DribbleIntensity = 3;

    private float DribbleTime { get; set; }

    public sealed override void _PhysicsProcess(double _delta)
    {
        var delta = (float)_delta;
        ApplyGravity(delta);
        MoveBall(delta);

        void MoveBall(float delta)
        {
            DribbleTime += delta;

            var velocity = Ball.Carrier.Velocity;
            var isMoving = velocity.NotZero();
            var isMovingX = velocity.X.NotZero();
            var isFlipped = Ball.Carrier.IsFlipped;
            var ballPoint = Ball.Carrier.GetBallPoint();

            var dribble = isMovingX ? Dribble() : 0;

            Ball.Velocity = velocity;
            Ball.Position = ballPoint + new Vector2(isFlipped ? -dribble : dribble, 0);
            Ball.Anim.Play(isMoving ? Anim.Roll : Anim.Idle);

            float Dribble()
                => Mathf.Cos(DribbleTime * DribbleFrequency) * DribbleIntensity;
        }
    }
}
