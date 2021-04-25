using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quote
{
    public string quote;
    public int seconds;
    public string headline;

    public Quote(string _quote, int _seconds, string _headline)
    {
        quote = _quote;
        seconds = _seconds;
        headline = _headline;
    }
}
