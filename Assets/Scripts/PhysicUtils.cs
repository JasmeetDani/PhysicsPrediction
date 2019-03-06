using UnityEngine;

public static class PhysicUtils
{
    public static Vector3 BounceFromPlane(Vector3 InitialVelocity, Vector3 FaceNormal)
    {
        return Vector3.Reflect(InitialVelocity * Constants.Bounce_Damping, FaceNormal);
    }


    public static Vector3 CalculateFloorBouncePoint(Vector3 VFBall, Vector3 FPos)
    {
        float Height = FPos.y;

        float tF = (VFBall.y + Mathf.Sqrt(VFBall.y * VFBall.y + 2 * Constants.G * Height)) / Constants.G;

        float BounceX = FPos.x + VFBall.x * tF;
        float BounceZ = FPos.z + VFBall.z * tF;

        return new Vector3(BounceX, 0, BounceZ);
    }


    public static void LookAheadVelocity_BeforeFloorBounce(ref Vector3 VFBall, ref Vector3 FPos)
    {
        Vector3 PredictorRayEnd = FPos + (VFBall.normalized * Constants.PredictorRayLength);

        RaycastHit hit;
        Physics.Linecast(FPos, PredictorRayEnd, out hit);

        if (hit.normal == Constants.N_CEILING)
        {
            // We may hit the ceiling or alternately, hit any of the walls or land somewhere on the floor

            float tVyZero = VFBall.y / Constants.G;

            float maxH = FPos.y + VFBall.y * tVyZero - Constants.GBy2 * tVyZero * tVyZero;

            if (maxH > Constants.RoomHeight)
            {
                // Ceiling hit is certain

                float tF = (VFBall.y +
                            Mathf.Sqrt(VFBall.y * VFBall.y + 2 * Constants.G * (FPos.y - Constants.RoomHeight)))
                    / Constants.G;

                Vector3 VBallAtCollisionTime = new Vector3(VFBall.x, VFBall.y - Constants.G * tF, VFBall.z);

                VFBall = BounceFromPlane(VBallAtCollisionTime, hit.normal);

                FPos = new Vector3(FPos.x + VFBall.x * tF, Constants.RoomHeight, FPos.z + VFBall.z * tF);

                LookAheadVelocity_BeforeFloorBounce(ref VFBall, ref FPos);
            }
            else
            {
                // We may hit any of the walls or land somewhere on the floor

                if (FindVelocityAfterUnknownWallBounceIfAny(ref VFBall, ref FPos))
                {
                    LookAheadVelocity_BeforeFloorBounce(ref VFBall, ref FPos);
                }
            }
        }
        else
        {
            if (hit.normal != Constants.N_FLOOR)
            {
                // We may hit any of the walls or land somewhere on the floor

                if (FindVelocityAfterKnownWallBounceIfAny(ref VFBall, ref FPos, hit))
                {
                    LookAheadVelocity_BeforeFloorBounce(ref VFBall, ref FPos);
                }
            }
        }

        // We are going to hit the floor next, time to break out of the recursion
    }

    private static bool FindVelocityAfterUnknownWallBounceIfAny(ref Vector3 VFBall, ref Vector3 FPos)
    {
        Vector3 pos3D = CalculateFloorBouncePoint(VFBall, FPos);

        Vector2 pos2D = new Vector2(pos3D.x, pos3D.z);

        if (Constants.RoomRect.Contains(pos2D))
        {
            return false;
        }
        else
        {
            Vector3 VFBallXZ = new Vector3(VFBall.x, 0, VFBall.z);

            Vector3 PredictorRayEnd = FPos + (VFBallXZ.normalized * Constants.PredictorRayLength);

            RaycastHit hit;
            Physics.Linecast(FPos, PredictorRayEnd, out hit);

            FindVelocityAfterWallBounce(ref VFBall, ref FPos, hit);
        }

        return true;
    }

    private static bool FindVelocityAfterKnownWallBounceIfAny(ref Vector3 VFBall, ref Vector3 FPos, RaycastHit hit)
    {
        Vector3 pos3D = CalculateFloorBouncePoint(VFBall, FPos);

        Vector2 pos2D = new Vector2(pos3D.x, pos3D.z);

        if (Constants.RoomRect.Contains(pos2D))
        {
            return false;
        }
        else
        {
            FindVelocityAfterWallBounce(ref VFBall, ref FPos, hit);
        }

        return true;
    }

    private static void FindVelocityAfterWallBounce(ref Vector3 VFBall, ref Vector3 FPos, RaycastHit hit)
    {
        float tF = 0.0f;

        if (hit.normal == Constants.N_FRONTWALL)
        {
            tF = -FPos.z / VFBall.z;

            FPos = new Vector3(FPos.x + VFBall.x * tF,
                               FPos.y + VFBall.y * tF - Constants.GBy2 * tF * tF, 0);
        }
        else if (hit.normal == Constants.N_SIDEWALL1)
        {
            tF = -FPos.x / VFBall.x;

            FPos = new Vector3(0, FPos.y + VFBall.y * tF - Constants.GBy2 * tF * tF, FPos.z + VFBall.z * tF);
        }
        else if (hit.normal == Constants.N_SIDEWALL2)
        {
            tF = (Constants.RoomWidth - FPos.x) / VFBall.x;

            FPos = new Vector3(Constants.RoomWidth,
                               FPos.y + VFBall.y * tF - Constants.GBy2 * tF * tF, FPos.z + VFBall.z * tF);
        }
        else
        {
            tF = (-Constants.RoomLength - FPos.z) / VFBall.z;

            FPos = new Vector3(FPos.x + VFBall.x * tF,
                               FPos.y + VFBall.y * tF - Constants.GBy2 * tF * tF, -Constants.RoomLength);
        }

        Vector3 VBallAtCollisionTime = new Vector3(VFBall.x, VFBall.y - Constants.G * tF, VFBall.z);

        VFBall = BounceFromPlane(VBallAtCollisionTime, hit.normal);
    }
}