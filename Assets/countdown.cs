using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    public float startnum;
    public Text numbercountdown;
    // Start is called before the first frame update
    void Start()
    {
       // Countdown();
    }
    private void OnEnable()
    {
        Countdown();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Countdown()
    {
        float duration = startnum; // 3 seconds you can change this 
                             //to whatever you want
        float normalizedTime = 0;
        while (normalizedTime <= 1f)
        {
            numbercountdown.text = normalizedTime.ToString();
            normalizedTime += Time.deltaTime / duration;
            Debug.Log(normalizedTime+ "biep");
            yield return null;
            Debug.Log(normalizedTime);
        }
    }
}
