namespace Game;

using F00F;
using Godot;
using Anim = Player.Anims;

public partial class PlayerStatePreKick : PlayerState
{
    private const float RewardPeriod = 1; // seconds
    private const float RewardCurve = 2; // 0 constant, 1 linear, 0..1 ease in, 1..x ease out

    private float AimTime { get; set; }
    private Vector2 AimVector { get; set; }

    public sealed override void _EnterTree()
    {
        Player.Anim.Play(Anim.PreKick);
        Player.Velocity = Vector2.Zero;
    }

    public sealed override void _PhysicsProcess(double _delta)
    {
        var delta = (float)_delta;

        AimTime += delta;
        AimVector += Player.Input.Movement() * delta;

        if (Player.Input.OnShootJustReleased())
        {
            var bonus = Mathf.Ease(AimTime.ClampMax(RewardPeriod) / RewardPeriod, RewardCurve);
            var power = Player.Power * (1 + bonus);
            var aim = AimVector.IsZero() ? DefaultAim() : AimVector.Normalized();

            SetNextState<PlayerStateKick>(StateType.Kick,
                next => next.Init(aim, power));

            Vector2 DefaultAim()
                => Player.IsFlipped ? Vector2.Left : Vector2.Right;
        }
    }
}
