using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class countdown : MonoBehaviour
{
    public int scenenumnext;
    public Text numbercountdown;
    // Start is called before the first frame update
    void Start()
    {
       // Countdown();
    }
    //public void OnEnable()
    //{
    //    Countdown(5);
    //    Debug.Log("canvas called");
    //}
    // Update is called once per frame
    public void callfortimer(int time)
    {
       // Countdown(time);
        StartCoroutine(Countdown(time));
    }

    //private IEnumerator Countdown()
    //{
    //    float duration = startnum; // 3 seconds you can change this 
    //                         //to whatever you want
    //    float normalizedTime = 0;
    //    while (normalizedTime <= 1f)
    //    { 
    //        normalizedTime += Time.deltaTime / duration;
    //     //   Debug.Log(normalizedTime);
    //        yield return null;
    //        Debug.Log(normalizedTime);
    //        numbercountdown.text = normalizedTime.ToString();
    //    }
    //}

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
