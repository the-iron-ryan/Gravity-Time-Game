using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPayload : Ship
{

    private Vector3 TargetLoc;
    private Vector3 TargetDir;
    private Planet TargetPlanet;
    private float TargetValue;



	void Start ()
    {
        TargetValue = 0;
        TargetLoc = new Vector3(0, 0, 0);
	}
	
	void Update ()
    {

        if(Vector3.Distance(TargetLoc, this.transform.position) > TargetValue)
        {
            this.transform.position += 10 * TargetDir / GetGravityValue();

        }
	}
    
    public void changeTargetLoc(GameObject obj)
    {
        TargetLoc = obj.transform.position;
        TargetDir = TargetLoc - this.transform.position;
        TargetDir = TargetDir / TargetDir.magnitude;
        TargetPlanet = obj.GetComponent<Planet>();
        TargetValue = TargetPlanet.GravityValue/2;

    }
    public float GetGravityValue()
    {
        // Current position of the ship
        Vector3 pos = this.transform.position;
        float x = pos.x;
        float z = pos.z;


        // Coordinates of planets
        float x1 = ShipManager.Instance.planet11.transform.position.x;
        float z1 = ShipManager.Instance.planet11.transform.position.z;
        float x2 = ShipManager.Instance.planet22.transform.position.x;
        float z2 = ShipManager.Instance.planet22.transform.position.z;


        // Calculating value of gravity
        float xz1_val = ShipManager.Instance.planet11.GravityValue * (x2 - x) / (x2 - x1) + ShipManager.Instance.planet21.GravityValue * (x - x1) / (x2 - x1);
        float xy2_val = ShipManager.Instance.planet12.GravityValue * (x2 - x) / (x2 - x1) + ShipManager.Instance.planet22.GravityValue * (x - x1) / (x2 - x1);

        float gravity_val = xz1_val * (z2 - z) / (z2 - z1) + xy2_val * (z - z1) / (z2 - z1);

        return gravity_val;
    }
}

