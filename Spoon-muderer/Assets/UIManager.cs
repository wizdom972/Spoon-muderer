using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private float money;     // 재화
    Text MoneyText;          // 현재 재화 텍스트
    private int facilityNum = 7;  // 재화 생산 시설 개수


    private float[] fac;
    private float fac1, fac2, fac3, fac4, fac5, fac6, fac7;

    // Use this for initialization
    void Start()
    {
        money = 0;

        fac = new float[7];
        fac[0] = 1;
        fac[1] = 10;
        fac[2] = 10;
        fac[3] = 10;
        fac[4] = 10;
        fac[5] = 10;
        fac[6] = 10;

        MoneyText = GameObject.Find("current money").GetComponent<Text>();

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
        int num = b.name[6] - '1';
        if (getMoney() >= fac[num])
        {
            setMoney((-1) * fac[num]);
            
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
            }
            b.GetComponentInChildren<Text>().text = "UPGRADE\n$" + fac[num];
        }
    }
}
