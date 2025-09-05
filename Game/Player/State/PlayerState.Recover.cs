using Godot;

namespace Game;

using Anim = Player.Anims;

public partial class PlayerStateRecover : PlayerState
{
    private const int RecoverTime = 500;
    private ulong RecoverEndTime { get; set; }

    public sealed override void _EnterTree()
    {
        Player.Anim.Play(Anim.Recover);
        RecoverEndTime = Time.GetTicksMsec() + RecoverTime;
    }

    public sealed override void _PhysicsProcess(double _)
    {
        MovePlayer();
        CheckNextState();

        void MovePlayer()
        {
            Player.Velocity = Vector2.Zero;
            Player.MoveAndSlide();
        }

        void CheckNextState()
        {
            if (Time.GetTicksMsec() >= RecoverEndTime)
                SetNextState(StateType.Move);
        }
    }
}
