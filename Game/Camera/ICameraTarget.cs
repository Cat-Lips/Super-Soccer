using Godot;

namespace Game;

public interface ICameraTarget
{
    Vector2 TargetPosition { get; }
    float CameraSmoothing { get; }
}
