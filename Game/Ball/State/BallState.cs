using System;
using F00F;
using Godot;

namespace Game;

using StateType = BallState.StateType;

public abstract partial class BallState : SceneState<Ball, StateType>
{
    protected Ball Ball => Context;

    public enum StateType { Free, Kicked, Carried }

    public static void InitState(Ball context, out Action<StateType> SetState)
    {
        var _SetState = Init(context, StateType.Free, type => type switch
        {
            StateType.Free => new BallStateFree { Context = context },
            StateType.Kicked => new BallStateKicked { Context = context },
            StateType.Carried => new BallStateCarried { Context = context },
            _ => throw new NotImplementedException(),
        });

        SetState = state => _SetState(state, null);
    }

    #region Shared

    protected void ApplyBounce(float delta) => ApplyGravity(delta, true);
    protected void ApplyGravity(float delta, bool bounce = false)
    {
        if (Ball.Height is 0 && Ball.HeightVelocity is 0)
            return;

        var descent = Const.Gravity * delta;
        Ball.HeightVelocity -= descent;
        Ball.Height += Ball.HeightVelocity;

        if (Ball.Height < 0) Ball.Height = 0;
        if (Ball.Height is 0) ApplyBounce();

        void ApplyBounce()
            => Ball.HeightVelocity = CanBounce() ? -Ball.HeightVelocity * Ball.Bounciness : 0;

        bool CanBounce()
            => bounce && -Ball.HeightVelocity > descent * Ball.Bounciness;
    }

    protected void ApplyMotion(float delta)
    {
        ApplyRebound(Ball.MoveAndCollide(Ball.Velocity * delta));

        void ApplyRebound(KinematicCollision2D x)
        {
            if (x is null) return;
            Ball.Velocity = Ball.Velocity.Bounce(x.GetNormal()) * Ball.Bounciness;
            SetNextState(StateType.Free);
        }
    }

    #endregion
}
