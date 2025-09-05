using Godot;

namespace Game;

public partial class Player : ICameraTarget
{
    private const float Smoothing = 2;

    public Vector2 TargetPosition => Position + Velocity;
    public float CameraSmoothing => Smoothing;
}
