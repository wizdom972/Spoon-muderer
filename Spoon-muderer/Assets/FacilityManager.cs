using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour {

    public GameObject facObj;
    public int facNum;

    public GameObject[] facilities;
    public bool[] isPurchased;

	// Use this for initialization
	void Start ()
    {
        facNum = 0;

        facilities = new GameObject[11];

        isPurchased = new bool[11];
        for (int i = 0; i < 11; i++)
        {
            isPurchased[i] = false;
        }
        isPurchased[0] = true;
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

    public void newFacObj(int num)
    {
        facilities[num] = Instantiate(facObj, new Vector3((float) 63.5 + 64 * num, 800, 0), Quaternion.identity);
        facilities[num].transform.SetParent(GameObject.Find("Canvas").transform);
        facilities[num].gameObject.name = "Facility" + (num + 1);
    }
}
