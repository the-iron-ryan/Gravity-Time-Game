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


    public Dictionary<int, bool> collectedObjs = new Dictionary<int, bool>();
    public Text WinText;


    void Awake()
    {
        collectedObjs.Add(planet11.GetInstanceID(), false);
        collectedObjs.Add(planet12.GetInstanceID(), false);
        collectedObjs.Add(planet21.GetInstanceID(), false);
        collectedObjs.Add(planet22.GetInstanceID(), false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(collectedObjs.Values.All(a => a))
        {
            WinText.gameObject.SetActive(true);
        }
	}
}
