using Godot;

namespace Game;

partial class Ball : ICameraTarget
{
    private const float Smoothing = 8;

    public Vector2 TargetPosition => Carrier?.TargetPosition ?? Position;
    public float CameraSmoothing => Carrier?.CameraSmoothing ?? Smoothing;
}
