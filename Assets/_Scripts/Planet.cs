using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Planet : MonoBehaviour
{
    public float GravityValue = 1.0f;

	void Start () {
		
	}
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShipManager.Instance.payload.changeTargetLoc(this.gameObject);
        }
    }
    void Update ()
    {
        
        this.transform.localScale = Vector3.one * GravityValue;
	}
}
