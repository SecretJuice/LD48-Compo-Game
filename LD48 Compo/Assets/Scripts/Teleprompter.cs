using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleprompter : MonoBehaviour
{

    public List<List<Quote>> quotes = new List<List<Quote>>();

    public Text teleprompterText;

     int currentDay = 0;
    int currentScreen = 0;

    private void Awake()
    {
        AddQuotes();
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //AdvanceScreen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public (int screen, int seconds) AdvanceScreen()
    {
        teleprompterText.text = quotes[currentDay][currentScreen].quote;
        currentScreen++;

        return (currentScreen - 1, quotes[currentDay][currentScreen - 1].seconds);

    }

    public int SetDay(int day)
    {
        currentDay = day;
        currentScreen = 0;
        return quotes[day].Count;
    }

    void AddQuotes()
    {
        quotes.Add(new List<Quote>());
        quotes.Add(new List<Quote>());
        quotes.Add(new List<Quote>());

        quotes[0].Add(new Quote("Good morning America! We've got a lot happening this morning.", 5));
        quotes[0].Add(new Quote("Mac n' Cheese has been officially declared a public menace in Bonsia.", 6));
        quotes[0].Add(new Quote("In other news, Florida man jumps directly into alligator's asophagus.", 6));
        quotes[0].Add(new Quote("This just in, our Cameraman Dave needs to pee and insits on letting us know about it.", 8));


    }
}
