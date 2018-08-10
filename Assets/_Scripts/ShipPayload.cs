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
    private bool HomeDest;


    private float HomeBaseDistTolerance = 0.5f;

    public void setPPL(int ppl)
    {
        NumPeople = ppl;
        Debug.Log(ppl);
    }

    void Start()
    {
        TargetValue = 0;
        TargetLoc = new Vector3(100, 100, 100);
        TargetDir = new Vector3(0, 0, 0);
        HomeDest = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShipManager.Instance.SelectPayload(this.gameObject);
        }
    }

    private void Update()
    {
        UpdateAliveText();

        ControlWithArrowKeys();

        float curGravValue = GetGravityValue();

        deathRate = curGravValue * 0.0002f;


        if(!ShipManager.Instance.getPause())
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

        if(!ShipManager.Instance.getPause() && Vector3.Distance(TargetLoc, this.transform.position) > TargetValue)
        {

            this.transform.position += 10 * TargetDir / GetGravityValue();
            
        }
        else if(HomeDest)
        {
            UnloadPayload();
           this.gameObject.SetActive(false);
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
        ShipManager.Instance.Home.NumPeople += this.NumPeople;
    }

    private void KillSomeone()
    { 
        NumPeople--;
        deathCounter = 0;
        Debug.Log("Someone died");
	}

    
    public void changeTargetLoc(GameObject obj, bool state)
    {
        TargetLoc = obj.transform.position;
        TargetDir = TargetLoc - this.transform.position;
        TargetDir = TargetDir / TargetDir.magnitude;

        TargetPlanet = obj.GetComponent<Planet>();
        if(TargetPlanet != null)
        {
            TargetValue = TargetPlanet.GravityValue / 2;
        }
        else
        {
            TargetValue = 1.0f;
        }
        

        HomeDest = state;
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

