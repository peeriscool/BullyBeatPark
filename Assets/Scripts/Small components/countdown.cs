using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class countdown : MonoBehaviour
{
    public int scenenumnext;
    public Text numbercountdown;

    void Start()
    {
    }

    public void callfortimer(int time)
    {
        StartCoroutine(Countdown(time));
    }

    IEnumerator Countdown(int seconds)
    {
        int counter = seconds;
        while (counter > 0)
        {
            numbercountdown.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }
        SceneManager.LoadScene(scenenumnext);
    }
}
