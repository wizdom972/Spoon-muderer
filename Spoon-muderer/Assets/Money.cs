using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money
{

    public float num;
    public char letter1, letter2;

    public Money()
    {
        num = 0;
        letter1 = ' ';
        letter2 = 'a';
    }

    public Money(float n)
    {
        num = n;
        letter1 = ' ';
        letter2 = 'a';
        this.MoneyRule();
    }

    public Money(float n, char letter)
    {
        num = n;
        letter1 = ' ';
        letter2 = letter;
    }

    public Money(float n, char l1, char l2)
    {
        num = n;
        letter1 = l1;
        letter2 = l2;
    }

    public Money(Money m)
    {
        num = m.num;
        letter1 = m.letter1;
        letter2 = m.letter2;
    }

    public void MoneyRule()
    {
        while (num >= 10000)
        {
            if (letter2 == 'z')
            {
                if (letter1 == ' ')
                {
                    letter1 = 'A';
                }
                else
                {
                    letter1 = (char)(letter1 + 1);
                }
                letter2 = 'a';
            }
            else
            {
                letter2 = (char)(letter2 + 1);
            }
            num = num / 10000;
        }
    }

    public void AddMoney(Money addNum)
    {
        if (this.letter1 == addNum.letter1)
        {
            if (this.letter2 < addNum.letter2)
            {
                if (this.letter2 == addNum.letter2 - 1)
                {
                    addNum.num = addNum.num * 10000;
                    addNum.letter2 = (char)(addNum.letter2 - 1);
                    this.num = this.num + addNum.num;
                }
                else
                {
                    this.num = addNum.num;
                    this.letter2 = addNum.letter2;
                }
            }
            else if (this.letter2 > addNum.letter2)
            {
                if (this.letter2 - 1 == addNum.letter2)
                {
                    this.num = this.num * 10000;
                    this.letter2 = (char)(this.letter2 - 1);
                    this.num = this.num + addNum.num;
                }
            }
            else
                this.num = this.num + addNum.num;
        }
        else if (this.letter1 > addNum.letter1)
        {
            if (addNum.letter1 == ' ' && addNum.letter2 == 'z')
            {
                if (this.letter1 == 'A' && this.letter2 == 'a')
                {
                    this.num = this.num * 10000 + addNum.num;
                    this.letter1 = ' ';
                    this.letter2 = 'z';
                }
            }
            else if (this.letter1 - addNum.letter1 == 1)
            {
                if (this.letter2 == 'a' && addNum.letter2 == 'z')
                {
                    this.num = this.num * 10000 + addNum.num;
                    this.letter1 = (char)(this.letter1 - 1);
                    this.letter2 = 'z';
                }
            }
        }
        else
        {
            if (this.letter1 == ' ' && this.letter2 == 'z')
            {
                if (addNum.letter1 == 'A' && addNum.letter2 == 'a')
                {
                    this.num = addNum.num * 10000 + this.num;
                }
                else
                {
                    this.num = addNum.num;
                    this.letter1 = addNum.letter1;
                    this.letter2 = addNum.letter2;
                }
            }
            else if (addNum.letter1 - this.letter1 == 1)
            {
                if (addNum.letter2 == 'a' && this.letter2 == 'z')
                {
                    this.num = addNum.num * 10000 + this.num;
                }
                else
                {
                    this.num = addNum.num;
                    this.letter1 = addNum.letter1;
                    this.letter2 = addNum.letter2;
                }
            }
            else
            {
                this.num = addNum.num;
                this.letter1 = addNum.letter1;
                this.letter2 = addNum.letter2;
            }
        }
        this.MoneyRule();
    }

    public bool SubMoney(Money num)
    {
        // 뺄셈 구현
        if (this.letter1 == num.letter1)
        {
            if (this.letter2 == num.letter2)
            {
                if (this.num >= num.num)
                {
                    this.num = this.num - num.num;
                }
                else
                {
                    Debug.Log("cannot subtract the money.");
                    return false;
                }
            }
            else if (this.letter2 > num.letter2)
            {
                if (this.letter2 - num.letter2 == 1)
                {
                    this.letter2 = (char)(this.letter2 - 1);
                    this.num = this.num * 10000 - num.num;
                }
            }
            else
            {
                Debug.Log("cannot subtract the money.");
                return false;
            }
        }
        else if (this.letter1 > num.letter1)
        {
            if ((this.letter1 - num.letter1 == 1 || (this.letter1 == 'A' && num.letter1 == ' ')) && (this.letter2 == 'a' && num.letter2 == 'z'))
            {
                this.letter1 = num.letter1;
                this.letter2 = 'z';
                this.num = this.num * 10000 - num.num;
            }
        }
        else
        {
            Debug.Log("cannot subtract the money.");
            return false;
        }
        this.MoneyRule();
        return true;
    }

    public string Print()
    {
        //소수 셋째자리에서 버림 표기
        return ((Mathf.Floor(this.num * 100) / 100).ToString() + this.letter1) + this.letter2;
    }

    public bool IsBiggerThan(Money money)
    {
        if (this.letter1 > money.letter1)
            return true;
        else if (this.letter1 < money.letter1)
            return false;
        else
        {
            if (this.letter2 > money.letter2)
                return true;
            else if (this.letter2 < money.letter2)
                return false;
            else
            {
                if (this.num >= money.num)
                    return true;
                else
                    return false;
            }
        }
    }
}
