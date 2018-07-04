using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private float money;     // 재화
    Text MoneyText;          // 현재 재화 텍스트

    private float fac1, fac2, fac3, fac4, fac5, fac6, fac7;

	// Use this for initialization
	void Start () {
        money = 0;
        fac1 = 1;
        fac2 = 10;
        fac3 = 10;
        fac4 = 10;
        fac5 = 10;
        fac6 = 10;
        fac7 = 10;
        MoneyText = GameObject.Find("current money").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
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
        char num = b.name[6];
        switch(num)
        {
            case '1':
                if (getMoney() >= fac1)
                {
                    setMoney((-1) * fac1);
                    fac1 += 1;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac1;
                }
                break;
            case '2':
                if (getMoney() >= fac2)
                {
                    setMoney((-1) * fac2);
                    fac2 += 10;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac2;
                }
                break;
            case '3':
                if (getMoney() >= fac3)
                {
                    setMoney((-1) * fac3);
                    fac3 += 10;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac3;
                }
                break;
            case '4':
                if (getMoney() >= fac4)
                {
                    setMoney((-1) * fac4);
                    fac4 += 10;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac4;
                }
                break;
            case '5':
                if (getMoney() >= fac5)
                {
                    setMoney((-1) * fac5);
                    fac5 += 10;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac5;
                }
                break;
            case '6':
                if (getMoney() >= fac6)
                {
                    setMoney((-1) * fac6);
                    fac6 += 10;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac6;
                }
                break;
            case '7':
                if (getMoney() >= fac7)
                {
                    setMoney((-1) * fac7);
                    fac7 += 10;
                    b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac7;
                }
                break;

        }
    }
}
