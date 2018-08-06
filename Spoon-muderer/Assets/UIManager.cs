using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private Money _m;
    private float _money;     // 재화
    private float _click;     // 클릭 당 얻는 재화
    Text moneyText;           // 현재 재화 텍스트
    Text earnText;            // 현재 생산 텍스트
    private int _facNum = 11; // 재화 생산 시설 개수

    private float[] _initFac; // 초기 재화 생산 시설 구매 비용
    private float[] _fac;     // 재화 생산 시설 업그레이드 비용
    private int[] _facLevel;  // 재화 생산 시설 레벨

    private float _spoonLevel; // 스푼 업그레이드 비용, 스푼 레벨

    GameObject facScroll, upgScroll; // 스크롤뷰 오브젝트

    FacilityManager fm;

    float timeSpan, checkTime;

    // Use this for initialization
    void Start()
    {
        _money = 0;
        _click = 1;

        _fac = new float[_facNum];
        _fac[0] = 1;
        _fac[1] = 100;
        _fac[2] = 500;
        _fac[3] = 3000;
        _fac[4] = 10000;
        _fac[5] = 40000;
        _fac[6] = 200000;
        _fac[7] = 1700000;
        _fac[8] = 123456789;
        _fac[9] = 4000000000;
        _fac[10] = 75000000000;

        _initFac = new float[_facNum];
        for (int i = 0; i < _facNum; i++)
        {
            _initFac[i] = _fac[i];
        }

        _facLevel = new int[_facNum];
        for (int j = 0; j < _facNum; j++)
        {
            _facLevel[j] = 0;
        }
        _facLevel[0] = 1;

        moneyText = GameObject.Find("current money").GetComponent<Text>();
        earnText  = GameObject.Find("earning money").GetComponent<Text>();
        facScroll = GameObject.Find("Facility Scroll");
        upgScroll = GameObject.Find("Upgrade Scroll");
        
        _spoonLevel = 1;

        upgScroll.SetActive(false); // 업그레이드 스크롤뷰 비활성화

        fm = GameObject.Find("FacilityManager").GetComponent<FacilityManager>();

        timeSpan = 0.0f;
        checkTime = 1.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (moneyText != null)
        {
            moneyText.text = "Current: " + Mathf.Floor(GetMoney() * 100) / 100;     //소수 셋째자리에서 버림 표기
        }
        if (earnText != null)
        {
            earnText.text = "Earning: " + GetCurrentEarn();
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
        return _money;
    }
    
    public void SetMoney(float extra)  // 재화 수정
    {
        _money = GetMoney() + extra;
    }

    public void OnClickButtonMoneyUP()  // 클릭 시 재화 증가
    {
        SetMoney(_click);
        //Debug.Log("money + 10, now: " + GetMoney());
    }

    public void OnClickButtonFacility(Button b)  // 재화 생산시설 구매 버튼
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
        if (GetMoney() >= _fac[num])
        {
            Text facText = GameObject.Find("facility" + (num + 1)).GetComponentInChildren<Text>();
            // 구매 여부 확인
            if (!fm.GetIsPurchased()[num])
            {
                fm.newFacObj(num);
                fm.SetIsPurchased(num, true);
                facText.text = facText.text + "\nLv.1";
            }
            // 재화 소모
            SetMoney((-1) * _fac[num]);
            // 구매 가격 증가
            _fac[num] = _initFac[num] * Mathf.Pow(1.13f, _facLevel[num]++);

            // 텍스트 업데이트
            b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + Mathf.Floor(_fac[num] * 100) / 100; // 버튼, 소수 셋째 자리에서 버림 표기
            facText.text = facText.text.Remove(facText.text.LastIndexOf(".") + 1) + _facLevel[num]; // 레벨
        }
    }
    public void OnClickButtonMenu(Button b)
    {
        // 하단 메뉴 버튼
        if (b.name == "Facilities Button")
        {
            // 재화생산시설 스크롤뷰 활성화
            upgScroll.SetActive(false);
            facScroll.SetActive(true);
        }
        else
        {
            // 업그레이드 스크롤뷰 활성화
            upgScroll.SetActive(true);
            facScroll.SetActive(false);
        }
    }

    public void OnClickButtonSpoon()
    {
        float spoon = 50000 * Mathf.Pow(10, _spoonLevel); // 구매 시 필요한 재화
        if (GetMoney() >= spoon)
        {
            SetMoney(spoon); // 재화 소모
            _spoonLevel++;   // 스푼 레벨 증가

            // 텍스트 업데이트
            Text spoonText = GameObject.Find("spoon").GetComponentInChildren<Text>();
            spoonText.text = "Lv. " + _spoonLevel;
            Text spoonUpg = GameObject.Find("spoon button").GetComponentInChildren<Text>();
            spoonUpg.text = "UPGRADE\n$" + spoon * 10;

            _click *= 2;
        }
    }

    public float GetCurrentEarn()
    {
        float earn = 0;
        int count = 0;
        for (int i = 0; i < 11; i++)
        {
            if (fm.GetIsPurchased()[i])
            {
                count++;
                earn += fm.facEarn[i] * _facLevel[i];
            }
        }
        return earn * Mathf.Pow(2, count) * 13 * Mathf.Pow(2, _spoonLevel - 1);
    }

    public void AutoEarn()
    {
        SetMoney(GetCurrentEarn());
    }
}
