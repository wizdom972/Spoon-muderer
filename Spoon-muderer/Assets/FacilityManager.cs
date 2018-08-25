using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacilityManager : MonoBehaviour {

    public GameObject facObj;
    public SoundManager soundManager;
    public AudioSource mainBGM;
    //public int facNum;

    public GameObject[] facilities;
    public bool[] isPurchased;
    public float[] facEarn;

	// Use this for initialization
	void Start ()
    {
        //facNum = 0;

        facilities = new GameObject[11];
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        mainBGM = GameObject.Find("Facility1").GetComponent<AudioSource>();
        

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
        float width = (float)UIManager.i_width / 768f;
        float height = (float)UIManager.i_height / 1024f;

        facilities[num] = Instantiate(facObj, new Vector3(), Quaternion.identity);

        facilities[num].transform.SetParent(GameObject.Find("Canvas").transform);
        GameObject.Find("Money Up").transform.SetAsLastSibling();
        facilities[num].gameObject.name = "Facility" + (num + 1);

        facilities[num].transform.localScale = new Vector3(1, 1, 1);
        AudioSource facAud = facilities[num].GetComponentInChildren<AudioSource>();

        switch (num)
        {
            case 0:
                //facilities[num] = Instantiate(facObj, new Vector3(-30 * width, 450 * height, 0), Quaternion.identity);
                //facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/트럼펫_펭귄") as Sprite;
                break;
            case 1:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 - 150 * width, Screen.height / 2 + 390 * height, 0);
                facAud.clip = (soundManager.audio01);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/트럼펫_펭귄") as Sprite;
                break;
            case 2:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 + 150 * width, Screen.height / 2 + 390 * height, 0);
                facAud.clip = (soundManager.audio02);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/드럼_고양이") as Sprite;
                break;
            case 3:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 + 300 * width, Screen.height / 2 + 290 * height, 0);
                facAud.clip = (soundManager.audio03);
                //facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/트럼펫_펭귄") as Sprite;
                break;
            case 4:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 + 300 * width, Screen.height / 2 + 140 * height, 0);
                facAud.clip = (soundManager.audio04);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/발라폰_오징어") as Sprite;
                break;
            case 5:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 + 150 * width, Screen.height / 2 + 90 * height, 0);
                facAud.clip = (soundManager.audio05);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/바이올린_베짱이") as Sprite;
                break;
            case 6:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 - 150 * width, Screen.height / 2 + 90 * height, 0);
                facAud.clip = (soundManager.audio06);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/비올라_베짱이_가을") as Sprite;
                break;
            case 7:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 - 300 * width, Screen.height / 2 + 140 * height, 0);
                facAud.clip = (soundManager.audio07);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/심벌즈_원숭이") as Sprite;
                break;

            case 8:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 - 150 * width, Screen.height / 2 + 240 * height, 0);
                break;
            case 9:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2 + 150 * width, Screen.height / 2 + 240 * height, 0);
                break;
            case 10:
                facilities[num].transform.position = new Vector3(UIManager.i_width / 2, Screen.height / 2 + 210 * height, 0);
                break;
        }
        facAud.timeSamples = (mainBGM.timeSamples);
        facAud.Play();
    }
}
