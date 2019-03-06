using UnityEngine;

public class BouncePointController : MonoBehaviour
{
    public void UpdatePosition(Vector3 hitPos, Vector3 returnVelocity)
    {
        Vector3 VFBall = returnVelocity;

        Vector3 FPos = hitPos;


        PhysicUtils.LookAheadVelocity_BeforeFloorBounce(ref VFBall, ref FPos);


        float Height = FPos.y;

        float tF = (VFBall.y + Mathf.Sqrt(VFBall.y * VFBall.y + 2 * Constants.G * Height)) / Constants.G;

        float BounceX = FPos.x + VFBall.x * tF;
        float BounceZ = FPos.z + VFBall.z * tF;

        transform.position = new Vector3(BounceX, 0.1f, BounceZ);
    }
}