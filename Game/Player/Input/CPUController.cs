using Godot;

namespace Game;

public class CPUController : IPlayerController
{
    public Vector2 Movement() => Vector2.Zero;

    public bool OnPassJustPressed() => false;
    public bool OnPassJustReleased() => false;
    public bool OnShootJustPressed() => false;
    public bool OnShootJustReleased() => false;
}
