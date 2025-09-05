using System.Diagnostics;
using Godot;

namespace Game;

using Anim = Player.Anims;

public partial class PlayerStateKick : PlayerState
{
    private Vector2 Aim { get; set; }
    private float Power { get; set; }

    internal void Init(in Vector2 aim, float power)
    {
        Aim = aim;
        Power = power;
    }

    public sealed override void _EnterTree()
    {
        Player.Anim.Play(Anim.Kick);
        Player.Anim.AnimationFinished += OnAnimComplete;

        void OnAnimComplete(StringName _)
        {
            Player.Anim.AnimationFinished -= OnAnimComplete;
            Debug.Assert(_ == Anim.Kick);
            KickBall();
        }
    }

    private void KickBall()
    {
        var velocity = Aim * Power;
        Player.Ball.Kick(velocity);
        SetNextState(Next());

        StateType Next()
            => Player.Controller is Player.ControlType.CPU ? StateType.Recover : StateType.Move;
    }
}
