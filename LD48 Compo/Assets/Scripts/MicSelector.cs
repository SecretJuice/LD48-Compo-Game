using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicSelector : MonoBehaviour
{

    public Dropdown dropdown;

    void Start()
    {
        SetDropDownOptions();
        SetSelectedDevice(0);
    }

    List<Dropdown.OptionData> MakeOptionsList()
    {
        string[] devices = Microphone.devices;

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();

        foreach(string device in devices)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData(device);

            options.Add(optionData);
        }

        return options;

    }

    void SetDropDownOptions()
    {
        dropdown.options = MakeOptionsList();
    }

    public void SetSelectedDevice(int index)
    {
        string device = Microphone.devices[index];

        PlayerPrefs.SetString("ActiveDevice", device);

        print(device);
    }
}
