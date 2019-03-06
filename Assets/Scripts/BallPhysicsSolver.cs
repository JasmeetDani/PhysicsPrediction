using UnityEngine;

public class BallPhysicsSolver : MonoBehaviour
{
    private Vector3 VBall = Vector3.zero;

    private bool bActive = true;


    void FixedUpdate()
    {
        if (bActive)
        {
            VBall.y += Time.deltaTime * -Constants.G;


            Vector3 PBall = this.gameObject.transform.position;

            Vector3 dV = VBall * Time.deltaTime;


            RaycastHit hit;

            if (Physics.Linecast(PBall, PBall + dV, out hit))
            {
                hit.collider.gameObject.SendMessage("HandleCollisionWithBall", hit);

                return;
            }

            this.gameObject.transform.position += VBall * Time.deltaTime;
        }
    }


    public Vector3 getBallVelocity() { return VBall; }

    public void setBallVelocity(Vector3 VBall) { this.VBall = VBall; }


    public void Activate() { this.bActive = true; }

    public void DeActivate()
    {
        VBall = Vector3.zero;

        this.bActive = false;
    }
}