using System;
using F00F;
using Godot;

namespace Game;

public partial class Goal : Node2D
{
    #region Private

    private Area2D BackNet => field ??= (Area2D)GetNode(nameof(BackNet));
    private Area2D ScoreZone => field ??= (Area2D)GetNode(nameof(ScoreZone));
    private StaticBody2D FrontLine => field ??= (StaticBody2D)GetNode(nameof(FrontLine));

    #endregion

    public event Action Score;

    #region Godot

    public sealed override void _Ready()
    {
        BackNet.SetCollisionLayer();
        BackNet.SetCollisionMask(Layer.Ball);
        BackNet.BodyEntered += OnBackNetHit;

        ScoreZone.SetCollisionLayer();
        ScoreZone.SetCollisionMask(Layer.Ball);
        ScoreZone.BodyEntered += OnScoreZoneHit;

        FrontLine.SetCollisionLayer(Layer.GoalLine);
        FrontLine.SetCollisionMask(Layer.Player);

        void OnBackNetHit(Node2D body)
        {
            if (body is Ball ball)
                ball.Stop();
        }

        void OnScoreZoneHit(Node2D body)
        {
            if (body is Ball ball)
                Score?.Invoke();
        }
    }

    #endregion
}
