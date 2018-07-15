using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private float money;     // 재화
    Text MoneyText;          // 현재 재화 텍스트
    Text EarnText;           // 현재 생산 텍스트
    private int facNum = 11; // 재화 생산 시설 개수

    private float[] initFac; // 초기 재화 생산 시설 구매 비용
    private float[] fac;     // 재화 생산 시설 업그레이드 비용
    private int[] facLevel;

    GameObject FacScroll, UpgScroll; // 스크롤뷰 오브젝트

    FacilityManager fm;

    float timeSpan, checkTime;

    // Use this for initialization
    void Start()
    {
        money = 0;

        fac = new float[facNum];
        fac[0] = 1;
        fac[1] = 100;
        fac[2] = 500;
        fac[3] = 3000;
        fac[4] = 10000;
        fac[5] = 40000;
        fac[6] = 200000;
        fac[7] = 1700000;
        fac[8] = 123456789;
        fac[9] = 4000000000;
        fac[10] = 75000000000;

        initFac = new float[facNum];
        for (int i = 0; i < facNum; i++)
        {
            initFac[i] = fac[i];
        }

        facLevel = new int[facNum];
        for (int j = 0; j < facNum; j++)
        {
            facLevel[j] = 0;
        }
        facLevel[0] = 1;

        MoneyText = GameObject.Find("current money").GetComponent<Text>();
        EarnText  = GameObject.Find("earning money").GetComponent<Text>();
        FacScroll = GameObject.Find("Facility Scroll");
        UpgScroll = GameObject.Find("Upgrade Scroll");

        UpgScroll.SetActive(false); // 업그레이드 스크롤뷰 비활성화

        fm = GameObject.Find("FacilityManager").GetComponent<FacilityManager>();

        timeSpan = 0.0f;
        checkTime = 1.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (MoneyText != null)
        {
            MoneyText.text = "Current: " + GetMoney();
        }
        if (EarnText != null)
        {
            EarnText.text = "Earning: " + fm.getCurrentEarn();
        }
        timeSpan += Time.deltaTime;
        if (timeSpan > checkTime)
        {
            AutoEarn();
            timeSpan = 0;
        }
	}

    public float GetMoney()   // 재화 반환
    {
        return money;
    }
    
    public void SetMoney(float extra)  // 재화 수정
    {
        money = GetMoney() + extra;
    }

    public void onClickButtonMoneyUP()  // 클릭 시 재화 증가
    {
        SetMoney(100);
        //Debug.Log("money + 10, now: " + GetMoney());
    }

    public void onClickButtonFacility(Button b)  // 재화 생산시설 구매 버튼
    {
        int num;
        if (b.name.Length == 7)
        {
            num = b.name[6] - '1';
        }
        else
        {
            num = b.name[7] - '1' + 10;
        }
        if (GetMoney() >= fac[num])
        {
            Text facText = GameObject.Find("facility" + (num + 1)).GetComponentInChildren<Text>();
            // 구매 여부 확인
            if (!fm.getIsPurchased()[num])
            {
                fm.newFacObj(num);
                fm.setIsPurchased(num, true);
                facText.text = facText.text + "\nLv.1";
            }
            // 재화 소모
            SetMoney((-1) * fac[num]);
            // 구매 가격 증가
            fac[num] = initFac[num] * Mathf.Pow(1.13f, facLevel[num]++);

            // 텍스트 업데이트
            b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac[num]; // 버튼
            facText.text = facText.text.Remove(facText.text.LastIndexOf(".") + 1) + facLevel[num]; // 레벨
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

    public void AutoEarn()
    {
        SetMoney(fm.getCurrentEarn());
    }
}
