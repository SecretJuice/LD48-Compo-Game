using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MicDescriptionInjector : Editor
{

    [MenuItem("MyMenu/InjectMicDescription #&g")]
    public static void InjectMicDescription()
    {
        typeof(PlayerSettings.macOS).GetProperty("microphoneUsageDescription", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static).SetValue(null, "Record Voice for Game Mechanics");
        Debug.Log("Description Injected?");
    }

    //typeof(PlayerSettings.macOS).GetProperty("microphoneUsageDescription", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static).SetValue(null, "YOUR DESCRIPTION GOES HERE");



}
