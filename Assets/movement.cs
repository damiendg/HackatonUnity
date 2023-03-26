using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public Transform m_playerPosition;
    private Vector3 direction;
    //private CharacterController controller;
    public float forwardSpeed;
    public GameObject cubePrefabA;
    public GameObject cubePrefabB;
    public GameObject cubePrefabC;
    public GameObject cubePrefabD;
    private int desiredLane = 1;
    public float laneDistance = 4;
    private float centerPosition;

    public GameObject cubePrefab;
    public float cubeSpawnDistance = 10f;
    public Transform m_wayDirection;
    public TMP_Text looser;

    private bool isCollidingWithCube = false; // Ajout de la variable pour vérifier la collision

    public InputActionReference goLeft;
    public InputActionReference goRight;

    public bool isContinue = true;
    private bool oneTime = true;
    public int gameBegin;


    void Start()
    {
        gameBegin = 0;
        centerPosition = transform.position.x;

        goLeft.action.started += OnAButtonStartedLeft;
        goRight.action.started += OnAButtonStartedRight;
    }


    public Transform m_whatToMove;
    void Update()
    {
        if (gameBegin >= 0)
        {


            if (!isCollidingWithCube) // Ajout de la condition pour arrêter le mouvement si en collision
            {
                direction.z = forwardSpeed;
                m_whatToMove.position += m_wayDirection.forward * forwardSpeed * Time.deltaTime;
            }
            else
            {
                direction.z = 0;
                looser.gameObject.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                desiredLane++;
                if (desiredLane == 3)
                {
                    desiredLane = 2;
                }
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                desiredLane--;
                if (desiredLane == -1)
                {
                    desiredLane = 0;
                }
            }

            if (isContinue)
            {
                direction.z = forwardSpeed;

                Vector3 targetPosition = transform.position;

                if (desiredLane == 0)
                {
                    m_whatToMove.position = new Vector3(centerPosition - laneDistance, m_whatToMove.position.y, m_whatToMove.position.z);
                    //targetPosition += Vector3.left *laneDistance;
                }
                else if (desiredLane == 1)
                {
                    m_whatToMove.position = new Vector3(centerPosition, m_whatToMove.position.y, m_whatToMove.position.z);
                    //targetPosition += Vector3.right * laneDistance;
                }
                else
                {
                    m_whatToMove.position = new Vector3(centerPosition + laneDistance, m_whatToMove.position.y, m_whatToMove.position.z);

                }
                // transform.position = Vector3.Lerp(transform.position, targetPosition, 80*Time.deltaTime);
            }
            else
            {
                if (oneTime)
                {
                    Debug.Log("TERMINATION DE LA PARTIE");
                    forwardSpeed = 0;
                    direction.z = forwardSpeed;
                    transform.position += transform.forward * forwardSpeed * Time.deltaTime;

                    Vector3 targetPosition = transform.position;
                    Debug.Log(desiredLane);
                    GetXValueOfLane(desiredLane, out float xValueOnUnity);
                    transform.position = new Vector3(xValueOnUnity, transform.position.y, transform.position.z);


                    oneTime = false;
                }

            }



            if (Input.GetKeyDown(KeyCode.Space))
            {
                SpawnCubeForwardPlayer(4);

            }
        }
        else
        {
            SceneManager.LoadScene("SampleScene");

        }
    }

    private void respawnBear()
    {
        SpawnCubeForwardPlayer(4);
    }

    private void respawnTrees()
    {
        SpawnCubeForwardPlayer(2);
    }

    public void GetXValueOfLane(int lineNumber, out float xValueOnUnityAxis)
    {
        xValueOnUnityAxis = 0;
        if (lineNumber == 0) xValueOnUnityAxis = centerPosition - laneDistance;
        if (lineNumber == 1) xValueOnUnityAxis = centerPosition;
        if (lineNumber == 2) xValueOnUnityAxis = centerPosition + laneDistance;
    }

    //public Transform m_moveDirection;
    private void FixedUpdate()
    {
        //controller.Move(direction * Time.fixedDeltaTime);
    }

    public void SpawnCubeForwardPlayer(int numPiege)
    {
        Vector3 spawnPosition = m_playerPosition.position + Vector3.forward * cubeSpawnDistance;
        spawnPosition.y = 0;

        if (numPiege == 1)
        {
            cubePrefab = cubePrefabA;
        }
        else if (numPiege == 2)
        {
            cubePrefab = cubePrefabB;
            Invoke("respawnTrees", 10.0f);
        }
        else if (numPiege == 3)
        {
            cubePrefab = cubePrefabC;
        }
        else if (numPiege == 4)
        {
            cubePrefab = cubePrefabD;
            Invoke("respawnBear", 4.0f);
        }

        GameObject objet = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }

    public void SpawnCubeOnLane(int numPiege, int laneNumber)
    {
        float playerZForwardPosition = m_playerPosition.position.z;
        GetXValueOfLane(laneNumber, out float xOfLane);
        Vector3 spawnPosition = new Vector3(xOfLane, 0, playerZForwardPosition + cubeSpawnDistance);

        if (numPiege == 1)
        {
            cubePrefab = cubePrefabA;
        }
        else if (numPiege == 2)
        {
            cubePrefab = cubePrefabB;
            Invoke("respawnTrees", 10.0f);
        }
        else if (numPiege == 3)
        {
            cubePrefab = cubePrefabC;
        }
        else if (numPiege == 4)
        {
            cubePrefab = cubePrefabD;
            Invoke("respawnBear", 4.0f);
        }

        GameObject objet = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }

    // Ajout de la détection de collision avec le cube
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            Debug.DrawLine(other.transform.position, transform.position, Color.red, 10f);
            // Le joueur est en collision avec le cube
            isCollidingWithCube = true;
        }
        if (other.CompareTag("Wolf"))
        {
            Debug.DrawLine(other.transform.position, transform.position, Color.red, 10f);
            // Le joueur est en collision avec le cube
            isCollidingWithCube = true;
        }
        if (other.CompareTag("Barrier"))
        {
            Debug.DrawLine(other.transform.position, transform.position, Color.red, 10f);
            // Le joueur est en collision avec le cube
            isCollidingWithCube = true;
        }
        if (other.CompareTag("Bear"))
        {
            Debug.DrawLine(other.transform.position, transform.position, Color.red, 10f);
            // Le joueur est en collision avec le cube
            isCollidingWithCube = true;
        }
    }

    // Ajout de la détection de fin de collision avec le cube
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            float cubeXPosition = other.transform.position.x;
            float playerXPosition = transform.position.x;

            if (Mathf.Abs(cubeXPosition - playerXPosition) < 1) // Si le cube est sur la même bande que le joueur
            {
                isCollidingWithCube = false;
            }
        }
        if (other.CompareTag("Wolf"))
        {
            float cubeXPosition = other.transform.position.x;
            float playerXPosition = transform.position.x;

            if (Mathf.Abs(cubeXPosition - playerXPosition) < 1) // Si le cube est sur la même bande que le joueur
            {
                isCollidingWithCube = false;
            }
        }
        if (other.CompareTag("Barrier"))
        {
            float cubeXPosition = other.transform.position.x;
            float playerXPosition = transform.position.x;

            if (Mathf.Abs(cubeXPosition - playerXPosition) < 1) // Si le cube est sur la même bande que le joueur
            {
                isCollidingWithCube = false;
            }
        }
        if (other.CompareTag("Bear"))
        {
            float cubeXPosition = other.transform.position.x;
            float playerXPosition = transform.position.x;

            if (Mathf.Abs(cubeXPosition - playerXPosition) < 1) // Si le cube est sur la même bande que le joueur
            {
                isCollidingWithCube = false;
            }
        }

    }


    private void OnAButtonStartedLeft(InputAction.CallbackContext context)
    {
        Debug.Log("OnButtonStartedLeft");

        desiredLane--;
        if (desiredLane == -1)
        {
            desiredLane = 0;
        }


    }


    private void OnAButtonStartedRight(InputAction.CallbackContext context)
    {
        Debug.Log("OnButtonStartedRight");
        desiredLane++;
        if (desiredLane == 3)
        {
            desiredLane = 2;
        }
    }
}