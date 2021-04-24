using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quote
{
    public string quote;
    public int seconds;

    public Quote(string _quote, int _seconds)
    {
        quote = _quote;
        seconds = _seconds;
    }
}
