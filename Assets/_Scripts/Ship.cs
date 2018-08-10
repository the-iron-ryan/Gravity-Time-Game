using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int NumPeople = 100;

    float maxNumPeople = 150;
    float birthRate;
    float birthCounter = 0;

    public TextMesh PeopleAliveText;

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
        UpdateAliveText();

        birthRate = GetBirthRate();
        if(!ShipManager.Instance.getPause())
            birthCounter += birthRate;

        if(birthCounter > 1.0f)
        {
            GiveBirthToSomeone();
        }

	}

    /// <summary>
    /// Get birth rate relative to the amount of people alive
    /// </summary>
    /// <returns></returns>
    private float GetBirthRate()
    {
        if (NumPeople < 2)
            return 0;
        else
            return NumPeople * 0.00005f;
    }

    protected void UpdateAliveText()
    {
        PeopleAliveText.text = "People alive: " + NumPeople;
    }

    protected void GiveBirthToSomeone()
    {
        if(NumPeople < maxNumPeople)
            NumPeople++;

        birthCounter = 0;
        Debug.Log("Someone was born");
    }
	
}
