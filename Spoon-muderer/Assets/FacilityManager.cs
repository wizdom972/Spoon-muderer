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

        facilities = new GameObject[8];
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        mainBGM = GameObject.Find("Facility1").GetComponent<AudioSource>();
        

        isPurchased = new bool[8];
        for (int i = 0; i < 8; i++)
        {
            isPurchased[i] = false;
        }
        isPurchased[0] = true;

        facEarn = new float[8];
        facEarn[0] = 0.1f;
        facEarn[1] = 0.5f;
        facEarn[2] = 4;
        facEarn[3] = 10;
        facEarn[4] = 40;
        facEarn[5] = 100;
        facEarn[6] = 400;
        facEarn[7] = 6000;
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
        float width = (float)UIManager.iWidth / 768f;
        float height = (float)UIManager.iHeight / 1024f;

        facilities[num] = Instantiate(facObj, new Vector3(), Quaternion.identity);

        facilities[num].transform.SetParent(GameObject.Find("Money Up").transform);
        GameObject.Find("Money Up").transform.SetAsLastSibling();
        facilities[num].gameObject.name = "Facility" + (num + 1);

        facilities[num].transform.localScale = new Vector3(1, 1, 1);
        AudioSource facAud = facilities[num].GetComponentInChildren<AudioSource>();

        switch (num)
        {
            case 0:
                break;
            case 1:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(-100, 130);    
                facAud.clip = (soundManager.audio01);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/트럼펫_펭귄") as Sprite;
                GameObject.Find("gray2").SetActive(false);
                break;
            case 2:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(40, -120);
                facAud.clip = (soundManager.audio02);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/드럼_고양이") as Sprite;
                GameObject.Find("gray3").SetActive(false);
                break;
            case 3:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(100, 130);
                facAud.clip = (soundManager.audio03);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/트럼본_북극곰") as Sprite;
                GameObject.Find("gray4").SetActive(false);
                break;
            case 4:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(-120, -30);
                facAud.clip = (soundManager.audio04);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/발라폰_오징어") as Sprite;
                GameObject.Find("gray5").SetActive(false);
                break;
            case 5:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(260, 0);
                facAud.clip = (soundManager.audio05);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/바이올린_베짱이") as Sprite;
                GameObject.Find("gray6").SetActive(false);
                break;
            case 6:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(-260, 0);
                facAud.clip = (soundManager.audio06);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/비올라_베짱이_가을") as Sprite;
                GameObject.Find("gray7").SetActive(false);
                break;
            case 7:
                facilities[num].GetComponent<RectTransform>().anchoredPosition = new Vector2(120, 30);
                facAud.clip = (soundManager.audio07);
                facilities[num].GetComponent<Image>().sprite = Resources.Load<Sprite>("Facility/심벌즈_원숭이") as Sprite;
                GameObject.Find("gray8").SetActive(false);
                break;
        }
        facAud.timeSamples = (mainBGM.timeSamples);
        facAud.Play();
    }
}
