using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPayload : Ship
{
    float deathRate;
    float deathCounter = 0;

    private Vector3 TargetLoc;
    private Vector3 TargetDir;
    private Planet TargetPlanet;
    private float TargetValue;

    private GameObject curObject = null;

    private float HomeBaseDistTolerance = 0.5f;
    private bool isMovingTowardsHomeBase = false;

    void Start()
    {
        TargetValue = 0;
        TargetLoc = new Vector3(0, 0, 0);

    }

    public override void Update()
    {
        UpdateAliveText();

        ControlWithArrowKeys();

        float curGravValue = GetGravityValue();

        deathRate = curGravValue * 0.0005f;

        deathCounter += deathRate;

        // If death counter reaches limit, kill someone
        if (deathCounter > 1.0f)
        {
            KillSomeone();
        }

        // If all the people die, then destroy the object
        if (NumPeople <= 0)
        {
            Destroy(this.gameObject);
        }


        if(isMovingTowardsHomeBase)
        {
            if(Vector3.Distance(TargetLoc, this.transform.position) <= HomeBaseDistTolerance)
            {
                UnloadPayload();
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (Vector3.Distance(TargetLoc, this.transform.position) > TargetValue)
            {
                this.transform.position += 10 * TargetDir / GetGravityValue();
            }
        }
    }

    /// <summary>
    /// Used to control the ship with arrow keys. Debug purposes only
    /// </summary>
    /// <returns></returns>
    private void ControlWithArrowKeys()
    {
        // TODO: change speed to be relative to gravity
        float speed = GetGravityValue() * 0.5f;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        transform.position += move * speed * Time.deltaTime;
    }

    private void UnloadPayload()
    {
        Debug.Log("Unloading");
        ShipManager.Instance.collectedObjs[curObject.GetInstanceID()] = true;
        ShipManager.Instance.Home.NumPeople += this.NumPeople;
    }

    private void KillSomeone()
    { 
        NumPeople--;
        deathCounter = 0;
        Debug.Log("Someone died");
	}
    
    public void changeTargetLoc(GameObject obj)
    {
        if (obj.GetInstanceID() == ShipManager.Instance.Home.GetInstanceID())
            isMovingTowardsHomeBase = true;
        else
            isMovingTowardsHomeBase = false;

        curObject = obj;

        TargetLoc = obj.transform.position;
        TargetDir = TargetLoc - this.transform.position;
        TargetDir = TargetDir / TargetDir.magnitude;

        if(!isMovingTowardsHomeBase)
        {
            TargetPlanet = obj.GetComponent<Planet>();
            TargetValue = TargetPlanet.GravityValue / 2;
        }

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

