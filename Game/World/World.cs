using F00F;
using Godot;

namespace Game;

[Tool]
public partial class World : Node
{
    #region Private

    private Node BG => field ??= GetNode(nameof(BG));
    private Camera Camera => field ??= (Camera)GetNode(nameof(Camera));

    private StaticBody2D TopWall => field ??= (StaticBody2D)GetNode($"%{nameof(TopWall)}/Body");
    private StaticBody2D LeftWall => field ??= (StaticBody2D)GetNode($"%{nameof(LeftWall)}/Body");
    private StaticBody2D RightWall => field ??= (StaticBody2D)GetNode($"%{nameof(RightWall)}/Body");
    private StaticBody2D BottomWall => field ??= (StaticBody2D)GetNode($"%{nameof(BottomWall)}/Body");

    #endregion

    #region Godot

    public sealed override void _Ready()
    {
        InitWalls();
        InitCamera();

        void InitWalls()
        {
            foreach (var wall in New.Array(TopWall, LeftWall, RightWall, BottomWall))
            {
                wall.SetCollisionLayer(Layer.Walls);
                wall.SetCollisionMask(Layer.Player, Layer.Ball);

            }
        }

        void InitCamera()
            => Camera.SetLimits(BG);
    }

    #endregion
}
