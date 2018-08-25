using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {


    public static int i_width, i_height;

    private Money _money;     // 재화
    private Money _click;     // 클릭 당 얻는 재화
    Text moneyText;           // 현재 재화 텍스트
    Text earnText;            // 현재 생산 텍스트
    private int _facNum;      // 재화 생산 시설 개수

    private List<Money> _initFac;    // 초기 재화 생산 시설 구매 비용
    private List<Money> _fac;        // 재화 생산 시설 업그레이드 비용
    
    private int[] _facLevel;   // 재화 생산 시설 레벨

    private float _clickLevel, _spoonLevel; // 클릭 업그레이드 레벨, 스푼 레벨

    private int _currentClick, _feverClick, _feverLevel;
    private bool _isFeverTime, _hasSetFever;
    private Money feverTmp;

    GameObject fever, feverBar, feverText; // 피버 오브젝트

    GameObject facScroll, cliScroll, fevScroll, spoScroll; // 스크롤뷰 오브젝트

    FacilityManager fm;

    float delta;
    float timeSpan, checkTime, timeFever;

    // Use this for initialization
    void Start()
    {

        i_width = Screen.width;
        i_height = Screen.height;

        _money = new Money();
        _click = new Money(1);
        
        _fac = new List<Money>();
        _fac.Add(new Money(100));
        _fac.Add(new Money(500));
        _fac.Add(new Money(1, 'b'));
        _fac.Add(new Money(50, 'b'));
        _fac.Add(new Money(432, 'b'));
        _fac.Add(new Money(3200, 'b'));
        _fac.Add(new Money(1.54f, 'c'));
        _fac.Add(new Money(22, 'c'));
        _fac.Add(new Money(300, 'c'));
        _fac.Add(new Money(8412, 'c'));
        _fac.Add(new Money(20.18f, 'd'));
        
        /*
        _fac = new List<Money>();
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        _fac.Add(new Money(1));
        */

        _facNum = _fac.Count;

        _initFac = new List<Money>();
        for (int i = 0; i < _facNum; i++)
        {
            _initFac.Add(_fac[i]);
        }

        _facLevel = new int[_facNum];
        for (int j = 0; j < _facNum; j++)
        {
            _facLevel[j] = 0;
        }
        _facLevel[0] = 1;

        moneyText = GameObject.Find("Current Money").GetComponent<Text>();
        earnText  = GameObject.Find("Earning Money").GetComponent<Text>();
        facScroll = GameObject.Find("Facility Scroll");
        cliScroll = GameObject.Find("Click Scroll");
        fevScroll = GameObject.Find("Fever Scroll");
        spoScroll = GameObject.Find("Spoon Scroll");

        _clickLevel = 1;
        _spoonLevel = 1;

        _currentClick = 0;
        _feverLevel = 1;
        _feverClick = (int) (1000 * Mathf.Pow(0.89f, _feverLevel)) + 1;
        _isFeverTime = false;
        _hasSetFever = false;

        // 스크롤뷰 비활성화
        cliScroll.SetActive(false);
        fevScroll.SetActive(false);
        spoScroll.SetActive(false);

        fever = GameObject.Find("Fever Image");
        fever.SetActive(false);
        feverBar = GameObject.Find("Fever Bar");
        feverText = GameObject.Find("Fever Text");
        feverText.GetComponent<Text>().text = _feverClick - _currentClick + "";

        fm = GameObject.Find("FacilityManager").GetComponent<FacilityManager>();

        Debug.Log("have to click " + _feverClick + " times");
        delta = (i_width / (float)_feverClick);
        Debug.Log("delta: " + delta);
        timeSpan = 0.0f;
        checkTime = 1.0f;
        timeFever = 0.0f;

        i_width = Screen.width;
        i_height = i_width * 4 / 3;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (moneyText != null)
        {
            moneyText.text = "Current: " + GetMoney().Print();     
        }
        if (earnText != null)
        {
            earnText.text = "Earning: " + GetCurrentEarn().Print();
        }
        timeSpan += Time.deltaTime;
        if (timeSpan > checkTime)
        {
            AutoEarn();
            timeSpan = 0;
        }

        // fever time
        if (_isFeverTime)
        {
            //Debug.Log("FEVER!!! " + _feverClick);
            feverText.GetComponent<Text>().text = "";
            if (!_hasSetFever)
            {
                feverTmp = new Money(_click);
                _click.num *= 10;
                _click.MoneyRule();
                _hasSetFever = true;
            }
            timeFever += Time.deltaTime;
            float duration = 5 * Mathf.Pow(1.07f, _feverLevel);
            fever.SetActive(_isFeverTime);
            if (timeFever > duration)
            {
                Debug.Log("Fever Time End in " + timeFever);
                feverBar.transform.position = new Vector3(i_width / 2, feverBar.transform.position.y, feverBar.transform.position.z);
                delta = (i_width / (float)_feverClick);
                timeFever = 0;
                _currentClick = 0;
                _click = new Money(feverTmp);
                feverTmp = null;
                feverText.GetComponent<Text>().text = _feverClick - _currentClick + "";
                fever.SetActive(false);
                _hasSetFever = false;
                _isFeverTime = false;
            }
        }
        else
        {
            FeverTime();
        }
	}

    public Money GetMoney()   // 재화 반환
    {
        return _money;
    }
    
    public void SetMoney(Money extra, bool add)  // 재화 수정
    {
        if (add)
        {
            // 덧셈
            GetMoney().AddMoney(extra);
        }
        else
        {
            // extra가 재화보다 작을 경우 뺄셈
            if (GetMoney().IsBiggerThan(extra))
            {
                GetMoney().SubMoney(extra);
            }
        }
    }

    public void OnClickButtonMoneyUP()  // 클릭 시 재화 증가
    {
        SetMoney(_click, true);
        _currentClick++;
        feverBar.transform.position = new Vector3((float)(feverBar.transform.position.x + delta), feverBar.transform.position.y, feverBar.transform.position.z);
        //Debug.Log("money + 10, now: " + GetMoney());
        //Debug.Log("delta: " + delta);
        //Debug.Log("fever: " + feverBar.transform.position);
        feverText.GetComponent<Text>().text = _feverClick - _currentClick + "";
    }

    public void OnClickButtonMenu(Button b)
    {
        // 하단 메뉴 버튼
        if (b.name == "Facilities Button")
        {
            // 재화생산시설 스크롤뷰 활성화
            facScroll.SetActive(true);
            cliScroll.SetActive(false);
            fevScroll.SetActive(false);
            spoScroll.SetActive(false);
        }
        else if (b.name == "Click Button")
        {
            // 클릭 스크롤뷰 활성화
            facScroll.SetActive(false);
            cliScroll.SetActive(true);
            fevScroll.SetActive(false);
            spoScroll.SetActive(false);
        }
        else if (b.name == "Fever Button")
        {
            // 피버 스크롤뷰 활성화
            facScroll.SetActive(false);
            cliScroll.SetActive(false);
            fevScroll.SetActive(true);
            spoScroll.SetActive(false);
        }
        else if (b.name == "Spoon Button")
        {
            // 스푼 스크롤뷰 활성화
            facScroll.SetActive(false);
            cliScroll.SetActive(false);
            fevScroll.SetActive(false);
            spoScroll.SetActive(true);
        }
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
        if (GetMoney().IsBiggerThan(_fac[num]))
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
            SetMoney(_fac[num], false);
            // 구매 가격 증가
            _fac[num] = new Money(_initFac[num].num * Mathf.Pow(1.13f, _facLevel[num]++), _initFac[num].letter1, _initFac[num].letter2);
            _fac[num].MoneyRule();

            // 텍스트 업데이트
            b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + _fac[num].Print(); // 버튼, 소수 셋째 자리에서 버림 표기
            facText.text = facText.text.Remove(facText.text.LastIndexOf(".") + 1) + _facLevel[num]; // 레벨
        }
    }

    public void OnClickButtonClick()
    {
        // Click 업그레이드
        Money click = new Money(243 * Mathf.Pow(2.5f, _clickLevel)); // 구매 시 필요한 재화
        if (GetMoney().IsBiggerThan(click))
        {
            SetMoney(click, false); // 재화 소모
            _clickLevel++;   // 스푼 레벨 증가

            // 텍스트 업데이트
            Text clickText = GameObject.Find("Click").GetComponentInChildren<Text>();
            clickText.text = "Lv. " + _clickLevel;
            Text clickUpg = GameObject.Find("Click Upgrade").GetComponentInChildren<Text>();
            click.num = click.num * 2.5f;
            click.MoneyRule();
            clickUpg.text = "UPGRADE\n$" + click.Print();

            _click.num = _click.num * 2.5f;
            _click.MoneyRule();
            Debug.Log(_click.Print());
        }
    }

    public void OnClickButtonFever()
    {
        Money fever = new Money(1);
        //Money fever = new Money(10 * Mathf.Pow(10000, _feverLevel));
        // fever 업그레이드
        if (GetMoney().IsBiggerThan(fever))
        {
            SetMoney(fever, false);
            _feverLevel++;

            int feverTmp = _feverClick;
            _feverClick = (int)(1000 * Mathf.Pow(0.89f, _feverLevel));
            delta = (float)(i_width * (feverTmp - _currentClick)) / (float)(feverTmp * (_feverClick - _currentClick));

            Text feverText = GameObject.Find("Fever").GetComponentInChildren<Text>();
            feverText.text = "Lv. " + _feverLevel;
            Text feverUgp = GameObject.Find("Fever Upgrade").GetComponentInChildren<Text>();
            fever.num = fever.num * 10000;
            fever.MoneyRule();
            feverUgp.text = "UPGRADE\n$" + fever.Print();
        }
    }

    public void OnClickButtonSpoon()
    {
        // Spoon 업그레이드
        Money spoon = new Money(50000 * Mathf.Pow(10, _spoonLevel)); // 구매 시 필요한 재화
        if (GetMoney().IsBiggerThan(spoon))
        {
            SetMoney(spoon, false); // 재화 소모
            _spoonLevel++;   // 스푼 레벨 증가

            // 텍스트 업데이트
            Text spoonText = GameObject.Find("Spoon").GetComponentInChildren<Text>();
            spoonText.text = "Lv. " + _spoonLevel;
            Text spoonUpg = GameObject.Find("Spoon Upgrade").GetComponentInChildren<Text>();
            spoon.num = spoon.num * 10;
            spoon.MoneyRule();
            spoonUpg.text = "UPGRADE\n$" + spoon.Print();

            _click.num = _click.num * 4;
            _click.MoneyRule(); 
            Debug.Log(_click.Print());
        }
    }

    public void FeverTime()
    {
        if (_currentClick >= _feverClick)
        {
            _isFeverTime = true;
        }
    }

    public Money GetCurrentEarn()
    {
        Money earn = new Money();
        int count = 0;
        for (int i = 0; i < 11; i++)
        {
            if (fm.GetIsPurchased()[i])
            {
                count++;
                earn.AddMoney(new Money(fm.facEarn[i] * _facLevel[i]));
            }
        }
        earn.num = earn.num * Mathf.Pow(1.5f, count);
        earn.MoneyRule();
        earn.num = earn.num * 13;
        earn.MoneyRule();
        earn.num = earn.num * Mathf.Pow(2, _spoonLevel - 1);
        earn.MoneyRule();
        return earn;
    }

    public void AutoEarn()
    {
        SetMoney(GetCurrentEarn(), true);
    }
}
