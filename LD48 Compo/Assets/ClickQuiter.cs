using UnityEngine;

public class ClickQuiter : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }
}
