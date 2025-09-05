using F00F;
using Godot;

namespace Game;

using Anim = Player.Anims;

public partial class PlayerStateMove : PlayerState
{
    public sealed override void _PhysicsProcess(double _)
    {
        MovePlayer(out var velocity);
        RotateActionZone(velocity);
        UpdateAnimation(velocity);
        CheckNextState(velocity);

        void MovePlayer(out Vector2 velocity)
        {
            Player.Velocity = Player.Input.Movement() * Player.Speed;
            Player.MoveAndSlide();
            velocity = Player.Velocity;
        }

        void RotateActionZone(in Vector2 velocity)
        {
            if (IsMoving(velocity))
                Player.ActionZone.Rotation = velocity.Angle();
        }

        void UpdateAnimation(in Vector2 velocity)
            => Player.Anim.Play(velocity.LengthSquared() is 0 ? Anim.Idle : Anim.Run);

        void CheckNextState(in Vector2 velocity)
        {
            if (Player.Input.OnShootJustPressed())
            {
                if (Player.HasBall)
                    SetNextState(StateType.PreKick);
                else if (IsMoving(velocity))
                    SetNextState(StateType.Tackle);
            }
            else if (Player.Input.OnPassJustPressed())
            {
                if (Player.HasBall)
                    SetNextState(StateType.Pass);
                //else if (IsMoving(velocity))
                //    SetNextState(StateType.Tackle);
            }
        }

        bool IsMoving(in Vector2 velocity)
            => velocity.NotZero();
    }
}
