using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class MicrophoneHandler : MonoBehaviour
{

    AudioSource audioSource;
    public string device = "Headset Microphone (2- CORSAIR HS70 PRO Wireless Gaming Headset)";
    public GameObject audioVisualPrefab;
    List<GameObject> visualizer = new List<GameObject>();
    List<float> loBandSamples = new List<float>();
    List<float> totalBandSamples = new List<float>();

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        print("Connected Devices: ");
        foreach (string device in Microphone.devices)
        {
            print(device);
        }
    }

    private void Start()
    {
        InitializeVisualizer();
        audioSource.clip = Microphone.Start(device, false, 5, 44100);
    }

    private void Update()
    {
        PrintBands();

        if (!Microphone.IsRecording(device) && !audioSource.isPlaying)
        {
            print("Deepness Score: " + ListMedian(loBandSamples) / ListMedian(totalBandSamples));
            loBandSamples.Clear();
            totalBandSamples.Clear();
            audioSource.Play();
        }
    }

    void PrintBands()
    {
        float[] spectrum = new float[1024];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
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

        loBandSamples.Add(l1 + l2);
        totalBandSamples.Add(l1 + l2 + l3 + l4 + l5 + l6 + l7);
    }

    void InitializeVisualizer()
    {
        for (int i = 0; i < 7; i++)
        {
            GameObject cube = Instantiate(audioVisualPrefab, new Vector2(i * 1.5f, -1f), Quaternion.identity);
            visualizer.Add(cube);
        }
    }

    float ListMedian(List<float> list)
    {
        List<float> sortedList = list;
        sortedList.Sort();

        int index = sortedList.Count / 2;

        return sortedList[index];
    }
}
