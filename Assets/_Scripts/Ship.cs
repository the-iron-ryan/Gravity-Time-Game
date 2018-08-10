using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int NumPeople;

    void Start ()
    {
		
	}
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShipManager.Instance.setTargetPlanet(this.gameObject, true);
        }
    }

    public void takePeople(int pop)
    {
        NumPeople -= pop;
    }
    void Update ()
    {

	}
}
