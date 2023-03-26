using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;

public class SendPosition : MonoBehaviour
{
    private float z;

    private string url = "https://bartteam2.000webhostapp.com/updatePosition.php?position=0%3B0%3B";
    private string urlTrap = "https://bartteam2.000webhostapp.com/fetchTraps.php";
    private string urlComplete = "";
    private float timer = 0.0f;
    public float interval = 1.0f;

    private movement _movement = null;

    private string traps;

    bool state1 = true;
    bool state2 = true;
    bool state3 = true;
    bool state4 = true;


    public bool isContinue
    {
        get { return _movement.isContinue; }

        set => _movement.isContinue = value;
    }

    private void Start()
    {
        if (_movement == null)
            _movement = gameObject.GetComponent<movement>();



    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            // Réinitialiser le timer
            timer = 0f;

            // Appeler la fonction UpdatePosition()
            z = gameObject.transform.position.z;
            urlComplete = url + z;
            StartCoroutine(UpdatePosition());
            StartCoroutine(FetchTraps());
            //Debug.Log(urlComplete);
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


    IEnumerator FetchTraps()
    {
        int trap1;
        int trap2;
        int trap3;
        int trap4;

        UnityWebRequest request = UnityWebRequest.Get(urlTrap);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string response = request.downloadHandler.text;
            string[] values = response.Split(';');

            trap1 = int.Parse(values[0]);
            trap2 = int.Parse(values[1]);
            trap3 = int.Parse(values[2]);
            trap4 = int.Parse(values[3]);

            Debug.Log(trap1);

            if (trap1 == 1 && state1)
            {
                Debug.Log("TRAP 1 : ACTIVEEEEEEEEEEEEEER");
                //Loup
                _movement.SpawnCubeForwardPlayer(1);
                state1 = false;
            }
            if (trap2 == 1 && state2)
            {
                Debug.Log("TRAP 2 : ACTIVEEEEEEEEEEEEEER");
                //ARBRE 
                _movement.SpawnCubeOnLane(2, 0);
                _movement.SpawnCubeOnLane(2, 2);
                state2 = false;
                //Invoke("state2=true", 4.0f); //NO SENSE AT ALL WARNING
            }
            if (trap3 == 1 && state3)
            {
                Debug.Log("TRAP 3 : ACTIVEEEEEEEEEEEEEER");
                //None
                _movement.SpawnCubeForwardPlayer(1);
                state3 = false;
            }
            if (trap4 == 1 && state4)
            {
                Debug.Log("TRAP 4 : ACTIVEEEEEEEEEEEEEER");
                _movement.SpawnCubeForwardPlayer(1);
                state4 = false;
            }

        }
    }


}
