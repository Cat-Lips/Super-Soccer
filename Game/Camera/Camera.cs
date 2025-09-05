using F00F;
using Godot;

namespace Game;

using Camera2D = Godot.Camera2D;

[Tool]
public partial class Camera : Camera2D
{
    private ICameraTarget target;
    [Export] public Node2D Target { get; set => this.Set(ref field, value, () => target = Target as ICameraTarget); }

    public sealed override void _Ready()
    {
        FollowTarget();
        ResetSmoothing();
    }

    public sealed override void _Process(double _)
        => FollowTarget();

    private void FollowTarget()
    {
        Position = target?.TargetPosition ?? Target?.Position ?? default;
        PositionSmoothingSpeed = target?.CameraSmoothing ?? default;
    }
}
