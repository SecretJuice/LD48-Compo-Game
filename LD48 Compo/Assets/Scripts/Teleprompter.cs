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

    public (int screen, int seconds) AdvanceScreen()
    {
        teleprompterText.text = quotes[currentDay][currentScreen].quote;
        currentScreen++;

        return (currentScreen - 1, quotes[currentDay][currentScreen - 1].seconds);

    }

    public int SetDay(int day)
    {
        if (day >= quotes.Count)
        {
            return -1;
        }

        currentDay = day;
        currentScreen = 0;

        return quotes[day].Count;
    }

    public string GetHeadline(int screen)
    {
        return quotes[currentDay][screen].headline;
    }

    void AddQuotes()
    {
        quotes.Add(new List<Quote>());
        quotes.Add(new List<Quote>());
        quotes.Add(new List<Quote>());

        quotes[0].Add(new Quote("Good morning America! We've got a lot happening this morning.", 5, "Good morning"));
        quotes[0].Add(new Quote("Mac n' Cheese has been officially declared a public menace in Bosnia.", 5, "Krafty Chaos"));
        quotes[0].Add(new Quote("In other news, Florida man jumps directly into alligator's esophagus.", 6, "'Florida Man'"));
        quotes[0].Add(new Quote("This just in, our Cameraman Dave needs to pee and insists on letting us know about it.", 7, "Too much information"));
        quotes[0].Add(new Quote("That's all for today folks! Tune in on the next news hour to hear about the President's professionally kept duck pond.", 8, "Perfectly justifiable use of federal resources"));

        quotes[1].Add(new Quote("Good morning America! This is another set of quotes.", 5, "Good morning"));
        quotes[1].Add(new Quote("They should not stay here, because they are dumb.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[1].Add(new Quote("If you see this set of quotes, it means I messed up.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[1].Add(new Quote("Please, let me know if you see these quotes here so I can fix it ASAP.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[1].Add(new Quote("Have another beautiful day folks! We'll see you next time.", 5, "Dumb game jam participant forgets to make real content"));

        quotes[2].Add(new Quote("Good morning America! This is another set of dummy quotes. For the thrid day.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[2].Add(new Quote("They should not stay here, because they are dumb.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[2].Add(new Quote("If you see this set of quotes, it means I messed up.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[2].Add(new Quote("Please, let me know if you see these quotes here so I can fix it ASAP.", 5, "Dumb game jam participant forgets to make real content"));
        quotes[2].Add(new Quote("Have another beautiful day folks! We'll see you next time.", 5, "Dumb game jam participant forgets to make real content"));


    }
}
