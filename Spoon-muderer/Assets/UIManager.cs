using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private float money;
    Text MoneyText;

	// Use this for initialization
	void Start () {
        money = 0;
        MoneyText = GameObject.Find("current money").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (MoneyText != null)
        {
            MoneyText.text = "Current: " + getMoney();
        }
	}

    public float getMoney()
    {
        return money;
    }
    
    public void setMoney(float extra)
    {
        money = getMoney() + extra;
    }

    public void onClickButtonMoneyUP()
    {
        setMoney(10);
        Debug.Log("money + 10, now: " + getMoney());
    }
}
