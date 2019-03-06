using UnityEngine;

public static class Constants
{
    public const float G = 9.81f;
    public const float GBy2 = G * 0.5f;

    public const float Bounce_Damping = 0.6f;

    public const float Sleep_Velocity = 0.5f;

    public const float RoomLength = 9.754f;
    public const float RoomWidth = 6.402f;
    public const float RoomHeight = 7.25f;

    public static readonly Rect RoomRect = Rect.MinMaxRect(0, -RoomLength, RoomWidth, 0);

    public static readonly Vector3 N_SIDEWALL1 = Vector3.right;
    public static readonly Vector3 N_SIDEWALL2 = Vector3.left;
    public static readonly Vector3 N_FLOOR = Vector3.up;
    public static readonly Vector3 N_CEILING = Vector3.down;
    public static readonly Vector3 N_BACKWALL = Vector3.forward;
    public static readonly Vector3 N_FRONTWALL = Vector3.back;

    public const float PredictorRayLength = 200.0f;
}