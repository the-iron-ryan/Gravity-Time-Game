using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPayload : MonoBehaviour {

    public int NumPeople;

	void Start ()
    {
		
	}
	
	void Update ()
    {
         Debug.Log(GetGravityValue());
        //Debug.Log(GetGravityValue());
	}
    
    public float GetGravityValue()
    {
        // Current position of the ship
        Vector3 pos = this.transform.position;
        float x = pos.x;
        float y = pos.z;

        // Coordinates of planets
        float x1 = ShipManager.Instance.planet11.transform.position.x;
        float y1 = ShipManager.Instance.planet12.transform.position.z;
        float x2 = ShipManager.Instance.planet22.transform.position.x;
        float y2 = ShipManager.Instance.planet22.transform.position.z;

        //Debug.Log(x1);

        // Calculating value of gravity
        float xy1_val = ShipManager.Instance.planet11.GravityValue * (x2 - x) / (x2 - x1) + ShipManager.Instance.planet21.GravityValue * (x - x1) / (x2 - x1);
        float xy2_val = ShipManager.Instance.planet12.GravityValue * (x2 - x) / (x2 - x1) + ShipManager.Instance.planet22.GravityValue * (x - x1) / (x2 - x1);
        //Debug.Log(xy1_val);
        //Debug.Log(xy2_val);
        float gravity_val = xy1_val * (y2 - y) / (y2 - y1) + xy2_val * (y - y1) / (y2 - y1);

        return gravity_val;
    }
}

