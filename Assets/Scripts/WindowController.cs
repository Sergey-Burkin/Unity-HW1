using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : MonoBehaviour
{
    
    public float minDelay = 1f;
    public float maxDelay = 5f;
    public List<GameObject> windowsList = new List<GameObject>();

    void Start() {
        StartCoroutine(CallRandomFunction());
    }
    private IEnumerator CallRandomFunction() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            GameObject currentWindow = windowsList[Random.Range(0, windowsList.Count)];
            currentWindow.GetComponent<WindowScript>().Activate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
