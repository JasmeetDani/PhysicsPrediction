using UnityEngine;

public class Simulation : MonoBehaviour
{
    [SerializeField]
    private BallController ballController = null;

    [SerializeField]
    private BouncePointController bouncePointController;


    private bool bPerformRacquetHit = false;

    private Vector3 VelocitySetInResponseToRacquetHit = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bPerformRacquetHit = true;

            UpdateSimulation();
        }
    }

    void FixedUpdate()
    {
        if (bPerformRacquetHit)
        {
            bPerformRacquetHit = false;

            ballController.Fire(VelocitySetInResponseToRacquetHit);
        }
    }


    private void UpdateSimulation()
    {
        VelocitySetInResponseToRacquetHit = ballController.Velocity;

        VelocitySetInResponseToRacquetHit.x += Time.deltaTime * 400;
        VelocitySetInResponseToRacquetHit.y += Time.deltaTime * 400;
        VelocitySetInResponseToRacquetHit.z += Time.deltaTime * 1000;
        
        bouncePointController.UpdatePosition(ballController.Pos, VelocitySetInResponseToRacquetHit);
    }
}