using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class TutorialConductor : MonoBehaviour
{
    public List<string> teleprompterStrings = new List<string>();
    public List<AudioClip> tutorialVA = new List<AudioClip>();

    public Text teleprompter;

    private AudioSource source;

    int currentScreen = 0;

    void Start()
    {
        source = GetComponent<AudioSource>();

        StartCoroutine(DoTutorial());
    }

    void DoNextScreen()
    {
        if (currentScreen < teleprompterStrings.Count)
        {
            teleprompter.text = teleprompterStrings[currentScreen];
        }

        if (currentScreen < tutorialVA.Count)
        {
            source.clip = tutorialVA[currentScreen];
            source.Play();
        }

        currentScreen++;
    }

    IEnumerator DoTutorial()
    {
        currentScreen = 0;

        for (int i = 0; i < teleprompterStrings.Count; i++)
        {
            DoNextScreen();

            yield return new WaitForSeconds(source.clip.length + 0.5f);
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
