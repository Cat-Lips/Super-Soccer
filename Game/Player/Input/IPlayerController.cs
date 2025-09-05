using Godot;

namespace Game;

public interface IPlayerController
{
    Vector2 Movement();

    bool OnPassJustPressed();
    bool OnPassJustReleased();
    bool OnShootJustPressed();
    bool OnShootJustReleased();
}
