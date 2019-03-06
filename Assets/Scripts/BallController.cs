using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector3 Pos
    {
        get { return transform.position; }

        private set {}
    }

    public Vector3 Velocity
    {
        get { return solverScript.getBallVelocity(); }

        private set { }
    }


    private BallPhysicsSolver solverScript = null;
    
    void Awake()
    {
        solverScript = GetComponent<BallPhysicsSolver>();
    }


    public void Fire(Vector3 V)
    {
        solverScript.Activate();

        solverScript.setBallVelocity(V);
    }

    public void Sleep(RaycastHit hit)
    {
        transform.position = hit.point;

        solverScript.DeActivate();

        return;
    }

    public void Bounce(RaycastHit hit)
    {
        Vector3 VFBall = PhysicUtils.BounceFromPlane(Velocity, hit.normal);

        transform.position = hit.point;

        solverScript.setBallVelocity(VFBall);
    }
}