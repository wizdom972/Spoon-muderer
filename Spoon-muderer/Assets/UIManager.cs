using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private float money;     // 재화
    Text MoneyText;          // 현재 재화 텍스트
    private int facNum = 11; // 재화 생산 시설 개수

    private float[] fac;     // 재화 생산 시설 업그레이드 비용

    GameObject FacScroll, UpgScroll; // 스크롤뷰 오브젝트

    FacilityManager fm;

    // Use this for initialization
    void Start()
    {
        money = 0;

        fac = new float[facNum];
        fac[0] = 1;
        fac[1] = 10;
        fac[2] = 10;
        fac[3] = 10;
        fac[4] = 10;
        fac[5] = 10;
        fac[6] = 10;
        fac[7] = 10;
        fac[8] = 10;
        fac[9] = 10;
        fac[10] = 10;

        MoneyText = GameObject.Find("current money").GetComponent<Text>();
        FacScroll = GameObject.Find("Facility Scroll");
        UpgScroll = GameObject.Find("Upgrade Scroll");

        UpgScroll.SetActive(false); // 업그레이드 스크롤뷰 비활성화

        fm = GameObject.Find("FacilityManager").GetComponent<FacilityManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (MoneyText != null)
        {
            MoneyText.text = "Current: " + getMoney();
        }
	}

    public float getMoney()   // 재화 반환
    {
        return money;
    }
    
    public void setMoney(float extra)  // 재화 수정
    {
        money = getMoney() + extra;
    }

    public void onClickButtonMoneyUP()
    {
        // 클릭 시 재화 증가
        setMoney(10);
        Debug.Log("money + 10, now: " + getMoney());
    }

    public void onClickButtonFacility(Button b) 
    {
        int num;
        // 재화 생산시설 구매 버튼
        if (b.name.Length == 7)
        {
            num = b.name[6] - '1';
        }
        else
        {
            num = b.name[7] - '1' + 10;
        }
        //- '1';
        if (getMoney() >= fac[num])
        {
            // 구매 여부 확인
            if (!fm.getIsPurchased()[num])
            {
                fm.newFacObj(num);
                fm.setIsPurchased(num, true);
            }
            // 재화 소모
            setMoney((-1) * fac[num]);
            // 구매 가격 증가
            switch (num)
            {
                case 0:
                    fac[num] += 1;
                    break;
                case 1:
                    fac[num] += 10;
                    break;
                case 2:
                    fac[num] += 10;
                    break;
                case 3:
                    fac[num] += 10;
                    break;
                case 4:
                    fac[num] += 10;
                    break;
                case 5:
                    fac[num] += 10;
                    break;
                case 6:
                    fac[num] += 10;
                    break;
                case 7:
                    fac[num] += 10;
                    break;
                case 8:
                    fac[num] += 10;
                    break;
                case 9:
                    fac[num] += 10;
                    break;
                case 10:
                    fac[num] += 10;
                    break;
            }
            b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac[num];
        }
    }
    public void onClickButtonMenu(Button b)
    {
        // 하단 메뉴 버튼
        if (b.name == "Facilities Button")
        {
            // 재화생산시설 스크롤뷰 활성화
            UpgScroll.SetActive(false);
            FacScroll.SetActive(true);
        }
        else
        {
            // 업그레이드 스크롤뷰 활성화
            UpgScroll.SetActive(true);
            FacScroll.SetActive(false);
        }
    }
}
