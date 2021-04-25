using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ClipGiver : MonoBehaviour
{
    AudioSource source;

    [SerializeField] float _waitTime, _postTime;

    [SerializeField] bool _onStart = true;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_onStart)
        {
            DoSequence();
        }
    }

    public void DoSequence()
    {
        StartCoroutine(PlayClipSequence());
    }

    IEnumerator PlayClipSequence()
    {
        float time = source.clip.length;

        yield return new WaitForSeconds(_waitTime);

        source.Play();

        yield return new WaitForSeconds(time + _postTime);

        TriggerEvent();

    }

    void TriggerEvent()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
