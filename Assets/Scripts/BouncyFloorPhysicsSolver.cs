using UnityEngine;

public class BouncyFloorPhysicsSolver : MonoBehaviour
{
    [SerializeField]
    private BallController ballController;


    void HandleCollisionWithBall(RaycastHit hit)
    {
        Vector3 VBall = ballController.Velocity;

        if (VBall.magnitude < Constants.Sleep_Velocity)
        {
            ballController.Sleep(hit);

            return;
        }

        ballController.Bounce(hit);
    }
}