using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour {

    public GameObject facObj;
    public int facNum;

    public bool[] isPurchased;

	// Use this for initialization
	void Start ()
    {
        facNum = 0;

        isPurchased = new bool[7];
        for (int i = 0; i < 7; i++)
        {
            isPurchased[i] = false;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool[] getIsPurchased()
    {
        return this.isPurchased;
    }

    public void setIsPurchased(int position, bool set)
    {
        this.isPurchased[position] = set;
    }

    public void newFacObj()
    {
        Instantiate(facObj, new Vector3(140, 340, 0), Quaternion.identity);
    }
}
