using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour {

    public GameObject facObj;
    //public int facNum;

    public GameObject[] facilities;
    public bool[] isPurchased;
    public float[] facEarn;

	// Use this for initialization
	void Start ()
    {
        //facNum = 0;

        facilities = new GameObject[11];

        isPurchased = new bool[11];
        for (int i = 0; i < 11; i++)
        {
            isPurchased[i] = false;
        }
        isPurchased[0] = true;

        facEarn = new float[11];
        facEarn[0] = 0.1f;
        facEarn[1] = 0.5f;
        facEarn[2] = 4;
        facEarn[3] = 10;
        facEarn[4] = 40;
        facEarn[5] = 100;
        facEarn[6] = 400;
        facEarn[7] = 6000;
        facEarn[8] = 98765;
        facEarn[9] = 1000000;
        facEarn[10] = 100000000;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool[] GetIsPurchased()
    {
        return this.isPurchased;
    }

    public void SetIsPurchased(int position, bool set)
    {
        this.isPurchased[position] = set;
    }

    public void newFacObj(int num)
    {
        facilities[num] = Instantiate(facObj, new Vector3(63.5f + 64 * num, 800, 0), Quaternion.identity);
        facilities[num].transform.SetParent(GameObject.Find("Canvas").transform);
        facilities[num].gameObject.name = "Facility" + (num + 1);
    }
}
