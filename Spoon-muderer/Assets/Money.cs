using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money {

    float num;
    char letter1, letter2;

    public Money()
    {
        num = 0;
        letter1 = ' ';
        letter2 = 'a';
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

    public void MoneyRule()
    {
        if (num >= 10000)
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
                addNum.num = addNum.num * 10000;
                addNum.letter2 = (char)(addNum.letter2 - 1);
            }
            else if (this.letter2 > addNum.letter2)
            {
                this.num = this.num * 10000;
                this.letter2 = (char)(this.letter2 - 1);
            }
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

    public void SubMoney()
    {
        // 뺄셈 구현
    }

    public void Print()
    {
        Debug.Log(this.num + "" + this.letter1 + "" + this.letter2);
    }
}
