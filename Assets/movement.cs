using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class movement : MonoBehaviour
{
    private Vector3 direction;
    private CharacterController controller;
    public float forwardSpeed; // Vitesse de déplacement du joueur
    private int desiredLane = 1;
    public float laneDistance = 4;
    private float centerPosition;

    public GameObject cubePrefab;
    public float cubeSpawnDistance = 10f;
    public Transform m_wayDirection;

    private bool isCollidingWithCube = false; // Ajout de la variable pour vérifier la collision
    public InputActionReference goLeft;
    public InputActionReference goRight;

    public bool isContinue = true;
    private bool oneTime = true;
    

    void Start()
    {
        forwardSpeed = 5;
        //aButton = goLeft.FindAction("AButton");
        goLeft.action.started += OnAButtonStartedLeft;
        goRight.action.started += OnAButtonStartedRight;
        controller = GetComponent<CharacterController>();
        centerPosition = transform.position.x;
        Invoke("DisplayMessage", 30.0f);
    }

    void DisplayMessage()
    {
        Debug.Log(transform.position);
    }

    void Update()
    {
        if (isContinue)
        {
            direction.z = forwardSpeed;
            transform.position += transform.forward * forwardSpeed * Time.deltaTime;
        
            Vector3 targetPosition = transform.position;
          
            if (desiredLane == 0)
            {
                transform.position = new Vector3(centerPosition - laneDistance, transform.position.y, transform.position.z);
                //targetPosition += Vector3.left *laneDistance;
            }
            else if (desiredLane == 1)
            {
                transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
                //targetPosition += Vector3.right * laneDistance;
            }
            else
            {
                transform.position = new Vector3(centerPosition + laneDistance, transform.position.y, transform.position.z);

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
                if (desiredLane == 0)
                {
                    transform.position = new Vector3(centerPosition - laneDistance, transform.position.y, transform.position.z);
                    //targetPosition += Vector3.left *laneDistance;
                }
                else if (desiredLane == 1)
                {
                    transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
                    //targetPosition += Vector3.right * laneDistance;
                }
                else
                {
                    transform.position = new Vector3(centerPosition + laneDistance, transform.position.y, transform.position.z);

                }

                oneTime = false;
            }
            
        }
    
       

    }


    private void FixedUpdate()
    {
        controller.Move(direction * Time.fixedDeltaTime);
    }

    private void OnAButtonStartedLeft(InputAction.CallbackContext context)
    {
        Debug.Log("OnButtonStartedLeft");
        if (!isCollidingWithCube) // Ajout de la condition pour arrêter le mouvement si en collision
        {
            direction.z = forwardSpeed;
            transform.position += m_wayDirection.forward * forwardSpeed * Time.deltaTime;
        }
        else
        {
            direction.z = 0;
        }


        if (Input.GetKeyDown(KeyCode.Q))
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        Vector3 targetPosition = transform.position;
        if (desiredLane == 0)
        {
            transform.position = new Vector3(centerPosition - laneDistance, transform.position.y, transform.position.z);
        }
        else if (desiredLane == 1)
        {
            transform.position = new Vector3(centerPosition, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(centerPosition + laneDistance, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnCube();
        }
    }


    //public Transform m_moveDirection;
    private void FixedUpdate()
    {
        //controller.Move(direction * Time.fixedDeltaTime);
    }

    void SpawnCube()
    {
        Vector3 spawnPosition = transform.position + transform.forward * cubeSpawnDistance;
        spawnPosition.y = 1;

        GameObject cube = Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }

    // Ajout de la détection de collision avec le cube
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            Debug.Log("Colliding with cube", other.gameObject);
            Debug.Log("Colliding with cube self", gameObject);
            Debug.DrawLine(other.transform.position, transform.position, Color.red, 10f);


            // Le joueur est en collision avec le cube
            isCollidingWithCube = true;
        }
    }

    // Ajout de la détection de fin de collision avec le cube
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cube"))
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

