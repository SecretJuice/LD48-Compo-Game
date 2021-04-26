using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;

public class MicrophoneHandler : MonoBehaviour
{
    private string device;
    public GameObject audioVisualPrefab;

    public MusicHandler musicHandler;

    public Teleprompter teleprompter;
    public TimerIndicator indicator;
    public GameObject button;

    public GameObject finishContents;
    public GameObject povContents;

    public Text headlineText;
    public Image mouthImage;

    public Text dayText;
    public Text ratingsText;
    public Text totalRatingsText;

    List<float> ratings = new List<float>();

    List<GameObject> visualizer = new List<GameObject>();
    List<float> loBandSamples = new List<float>();
    List<float> totalBandSamples = new List<float>();

    List<AudioSource> audioSources = new List<AudioSource>();

    int currentScreenEvaluation = -1;
    int currentNewsDay = -1;

    private void Awake()
    {
        device = PlayerPrefs.GetString("ActiveDevice");
    }

    private void Start()
    {
        // InitializeVisualizer();
        //InitializeDay(0);
        //StartCoroutine(RecordDay());

        //StartNextDay();

        ratingsText.gameObject.SetActive(false);
        
    }

    private void Update()
    {
        if (currentScreenEvaluation >= 0)
        {
            PrintBands(currentScreenEvaluation);

            if (!IsAudioSourcePlaying())
            {
                //print("Deepness Score: " + GetDeepnessScore());
                //loBandSamples.Clear();
                //totalBandSamples.Clear();
                //audioSources[currentScreenEvaluation].Play();
            }
        }

        
    }

    public void StartNextDay()
    {
        currentNewsDay++;
        currentScreenEvaluation = -1;

        dayText.text = "Day " + (currentNewsDay + 2);

        button.SetActive(false);
        ratingsText.gameObject.SetActive(false);
        //indicator.gameObject.SetActive(true);
        povContents.SetActive(true);

        InitializeDay(currentNewsDay);

        StartCoroutine(RecordDay());

    }

    float GetDeepnessScore()
    {
        return loBandSamples.Average() / totalBandSamples.Average();
    }

    bool IsAudioSourcePlaying()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.isPlaying)
            {
                return true;
            }
        }

        return false;
    }

    void PrintBands(int screen)
    {
        if (!audioSources[screen].isPlaying)
        {
            return;
        }

        float[] spectrum = new float[1024];
        audioSources[screen].GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float l1 = (spectrum[0] + spectrum[2] + spectrum[4] + spectrum[5]) * 200;
        float l2 = (spectrum[10] + spectrum[11] + spectrum[12]) + spectrum[13] * 200;
        float l3 = (spectrum[20] + spectrum[21] + spectrum[22]) * 200;
        float l4 = (spectrum[40] + spectrum[41] + spectrum[42] + spectrum[43]) * 200;
        float l5 = (spectrum[80] + spectrum[81] + spectrum[82] + spectrum[83]) * 200;
        float l6 = (spectrum[160] + spectrum[161] + spectrum[162] + spectrum[163]) * 200;
        float l7 = (spectrum[320] + spectrum[321] + spectrum[322] + spectrum[323]) * 200;
        //Debug.Log(l1 + ", " + l2 + ", " + l3 + ", " + l4 + ", " + l5 + ", " + l6 + ", " + l7 + " Recording: " + Microphone.IsRecording(device));
        visualizer[0].transform.localScale = new Vector2(1, l1);
        visualizer[1].transform.localScale = new Vector2(1, l2);
        visualizer[2].transform.localScale = new Vector2(1, l3);
        visualizer[3].transform.localScale = new Vector2(1, l4);
        visualizer[4].transform.localScale = new Vector2(1, l5);
        visualizer[5].transform.localScale = new Vector2(1, l6);
        visualizer[6].transform.localScale = new Vector2(1, l7);

        mouthImage.transform.localScale = new Vector2(Mathf.Clamp(0.03f * (l1 + l2 + l3 + l4 + l5 + l6 + l7), 0.02f, 0.2f), Mathf.Clamp(0.015f * (l1 + l2 + l3 + l4 + l5 + l6 + l7), 0.02f, 0.2f));

        loBandSamples.Add(l1 + l2);
        totalBandSamples.Add(l1 + l2 + l3 + l4 + l5 + l6 + l7);
    }

    public int RecordScreen()
    {
        // audioSource.clip = Microphone.Start(device, false, 5, 44100);

        (int currentScreen, int seconds) = teleprompter.AdvanceScreen();

        audioSources[currentScreen].clip = Microphone.Start(device, false, seconds, 44100);

        indicator.SetMaxValue(seconds);

        return seconds;


    }

    IEnumerator RecordDay()
    {
        if (audioSources.Count == 0)
        {
            StopCoroutine(RecordDay());
        }

        foreach (AudioSource source in audioSources)
        {
            int time = RecordScreen();

            yield return new WaitForSeconds(time);
        }

        StartCoroutine(EvaluateDay());
    }

    IEnumerator EvaluateDay()
    {
        InitializeVisualizer();
        //indicator.gameObject.SetActive(false);
        povContents.gameObject.SetActive(false);
        finishContents.gameObject.SetActive(true);

        musicHandler.SetMusic(0, true, 0.6f);

        float deepnessForDay = 0f;

        foreach (AudioSource source in audioSources)
        {
            currentScreenEvaluation++;
            audioSources[currentScreenEvaluation].Play();

            headlineText.text = teleprompter.GetHeadline(currentScreenEvaluation).ToUpper();

            yield return new WaitForSeconds(source.clip.length + 0.25f);

            print("Deepness Score: " + GetDeepnessScore());
            deepnessForDay += GetDeepnessScore();
            loBandSamples.Clear();
            totalBandSamples.Clear();
        }

        print("Deepness for today: " + deepnessForDay);

        ratings.Add(deepnessForDay * 10);

        if (currentNewsDay == 2)
        {
            EndGame();
            yield break;
        }

        //indicator.gameObject.SetActive(false);
        button.SetActive(true);
        finishContents.SetActive(false);

        musicHandler.StopMusic();

        ClearVisualizer();

        ratingsText.gameObject.SetActive(true);
        ratingsText.text = "Today's ratings: " + (deepnessForDay * 10);

    }

    void EndGame()
    {

        finishContents.SetActive(false);

        musicHandler.StopMusic();

        ClearVisualizer();

        totalRatingsText.gameObject.SetActive(true);
        totalRatingsText.text = "Total Ratings: " + ratings.Sum();


        //endgameystuff
    }

    void InitializeDay(int day)
    {
        int screenCount = teleprompter.SetDay(day);

        foreach(AudioSource source in audioSources)
        {
            Destroy(source);
        }


        audioSources.Clear();
        

        if (screenCount == -1)
        {
            return;
        }

        for(int i = 0; i < screenCount; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            audioSources.Add(source);
        }
    }

    void InitializeVisualizer()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject cube = Instantiate(audioVisualPrefab, new Vector2(i * 1.5f, -1f), Quaternion.identity);
            visualizer.Add(cube);
        }
    }

    void ClearVisualizer()
    {
        foreach(GameObject cube in visualizer)
        {
            Destroy(cube);
        }

        visualizer.Clear();
    }

    float ListMedian(List<float> list)
    {
        List<float> sortedList = list;
        sortedList.Sort();

        int index = sortedList.Count / 2;

        return sortedList[index];
    }
}