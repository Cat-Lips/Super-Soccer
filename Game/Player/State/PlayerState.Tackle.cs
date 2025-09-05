using F00F;
using Godot;

namespace Game;

using Anim = Player.Anims;

public partial class PlayerStateTackle : PlayerState
{
    private const float GroundFriction = 250;
    private const int WaitTime = 200;

    private ulong WaitEndTime { get; set; }
    private bool IsSliding => WaitEndTime is 0;
    private bool IsWaiting => WaitEndTime is not 0;
    private void StartWaitTimer() => WaitEndTime = Time.GetTicksMsec() + WaitTime;

    public sealed override void _EnterTree()
        => Player.Anim.Play(Anim.Tackle);

    public sealed override void _PhysicsProcess(double delta)
    {
        MovePlayer();
        CheckNextState();

        void MovePlayer()
        {
            if (IsSliding)
            {
                Player.Velocity = Player.Velocity.MoveToward(Vector2.Zero, GroundFriction * (float)delta);
                Player.MoveAndSlide();

                if (Player.Velocity.IsZero())
                    StartWaitTimer();
            }
        }

        void CheckNextState()
        {
            if (IsWaiting)
            {
                if (Time.GetTicksMsec() >= WaitEndTime)
                    SetNextState(StateType.Recover);
            }
        }
    }
}
