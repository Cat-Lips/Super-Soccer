using System.Diagnostics;
using F00F;
using Godot;

namespace Game;

using Anim = Player.Anims;

public partial class PlayerStatePass : PlayerState
{
    public sealed override void _EnterTree()
    {
        var dflt = Player.Velocity;
        Player.Velocity = Vector2.Zero;

        Player.Anim.Play(Anim.Kick);
        Player.Anim.AnimationFinished += OnAnimComplete;

        void OnAnimComplete(StringName _)
        {
            Player.Anim.AnimationFinished -= OnAnimComplete;
            Debug.Assert(_ == Anim.Kick);
            PassBall(dflt);
        }

        void PassBall(in Vector2 dflt)
        {
            var targetPlayer = Player.GetClosestTeamPlayerInView();
            var targetPosition = targetPlayer.IsNull()
                ? Player.Ball.Position + DefaultVelocity(dflt)
                : targetPlayer.Position + targetPlayer.Velocity;
            Player.Ball.Pass(targetPosition);

            SetNextState(StateType.Move);

            Vector2 DefaultVelocity(in Vector2 dflt)
            {
                return dflt.IsZero() ? DefaultVelocity() : dflt;

                Vector2 DefaultVelocity()
                    => (Player.IsFlipped ? Vector2.Left : Vector2.Right) * Player.Speed;
            }
        }
    }
}
