using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShipManager : Singleton<ShipManager>
{
    public Planet planet11;
    public Planet planet12;
    public Planet planet21;
    public Planet planet22;
    public Ship Home;
    public ShipPayload payload;
    public GameObject ShipCreation;
    public Text txt;

    private ShipPayload currPayload;
    private GameObject SelectedPayload;
    private GameObject targetPlanet;
    private bool pause;


    public Dictionary<int, bool> collectedObjs = new Dictionary<int, bool>();
    public Text WinText;


    void Awake()
    {
        collectedObjs.Add(planet11.GetInstanceID(), false);
        collectedObjs.Add(planet12.GetInstanceID(), false);
        collectedObjs.Add(planet21.GetInstanceID(), false);
        collectedObjs.Add(planet22.GetInstanceID(), false);
    }

    public bool getPause()
    {
        return pause;
    }
    void Start ()
    {
        pause = false;
        SelectedPayload = null;
        currPayload = null;
	}
    public GameObject getPayload()
    {
        return SelectedPayload;
    }

    public void createShip()
    {
        GameObject s = Instantiate(ShipCreation);
        s.SetActive(true);
        int ppl = int.Parse(txt.text);
        Home.takePeople(ppl);
        s.GetComponent<ShipPayload>().setPPL(ppl);

    }
    public void setTargetPlanet(GameObject tP, bool state)
    {
        targetPlanet = tP;
        if(currPayload != null)
        {

            currPayload.changeTargetLoc(targetPlanet, state);

        }

    }
	public void SelectPayload(GameObject payload)
    {
        if(SelectedPayload != null && payload != SelectedPayload)
        {
            SelectedPayload.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        payload.GetComponent<MeshRenderer>().material.color = Color.cyan;
        SelectedPayload = payload;
        currPayload = SelectedPayload.GetComponent<ShipPayload>();
    }

	// Update is called once per frame
	void Update ()
    {
        if(collectedObjs.Values.All(a => a))
        {
            WinText.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            pause = !pause;

        }
	}
}
