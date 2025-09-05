using System;
using F00F;

namespace Game;

using StateType = PlayerState.StateType;

public abstract partial class PlayerState : SceneState<Player, StateType>
{
    protected Player Player => Context;

    public enum StateType { Move, Pass, Kick, Tackle, Header, Recover, PreKick, VolleyKick, BicycleKick }

    public static void Init(Player context)
    {
        Init(context, StateType.Move, type => type switch
        {
            StateType.Move => new PlayerStateMove { Context = context },
            StateType.Pass => new PlayerStatePass { Context = context },
            StateType.Kick => new PlayerStateKick { Context = context },
            StateType.Tackle => new PlayerStateTackle { Context = context },
            StateType.Header => new PlayerStateHeader { Context = context },
            StateType.Recover => new PlayerStateRecover { Context = context },
            StateType.PreKick => new PlayerStatePreKick { Context = context },
            StateType.VolleyKick => new PlayerStateVolleyKick { Context = context },
            StateType.BicycleKick => new PlayerStateBicycleKick { Context = context },
            _ => throw new NotImplementedException(),
        });
    }
}
