using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

public class SendPosition : MonoBehaviour
{
    private float z;
    
    private string url = "https://bartteam.000webhostapp.com/updatePosition.php?position=0%3B0%3B";
    private string urlComplete = "";
    private float timer = 0.0f;
    public float interval = 1.0f;

    private movement _movement = null;
    
    public bool isContinue
    {
        get { return _movement.isContinue; }

        set => _movement.isContinue = value;
    }

    private void Start()
    {
        if (_movement == null)
            _movement = GameObject.Find("Cube").GetComponent<movement>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > interval) {
            // Réinitialiser le timer
            timer = 0f;

            // Appeler la fonction UpdatePosition()
            z = gameObject.transform.position.z;
            urlComplete = url + z;
            StartCoroutine(UpdatePosition());
            Debug.Log(urlComplete);
            urlComplete = "";
        }

        if (gameObject.transform.position.z >= 153)
        {
            isContinue = false;
        }
        
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            UnityWebRequest request = UnityWebRequest.Get(urlComplete);
            yield return request.SendWebRequest();
          
        }
    }
}
