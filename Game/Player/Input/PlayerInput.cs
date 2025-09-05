using F00F;
using Godot;

namespace Game;

public abstract partial class PlayerInput : MyInput, IPlayerController
{
    public Vector2 Movement()
        => GetVector(MoveLeft, MoveRight, MoveUp, MoveDown);

    public bool OnPassJustPressed() => IsActionJustPressed(Pass);
    public bool OnPassJustReleased() => IsActionJustReleased(Pass);

    public bool OnShootJustPressed() => IsActionJustPressed(Shoot);
    public bool OnShootJustReleased() => IsActionJustReleased(Shoot);

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
    public readonly StringName MoveUp;
    public readonly StringName MoveDown;
    public readonly StringName MoveLeft;
    public readonly StringName MoveRight;

    public readonly StringName Pass;
    public readonly StringName Shoot; // Shoot/Tackle
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
}
