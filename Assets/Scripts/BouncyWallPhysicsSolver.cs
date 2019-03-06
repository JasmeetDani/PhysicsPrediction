using UnityEngine;

public class BouncyWallPhysicsSolver : MonoBehaviour
{
    [SerializeField]
    public BallController ballController;


    void HandleCollisionWithBall(RaycastHit hit)
    {
        ballController.Bounce(hit);
    }
}