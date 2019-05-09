using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    InputHandler input;

    [Header("Movement")]
    Rigidbody rigidbody;
    Stick stick;

    [SerializeField] float speed = 5.0f;
    [SerializeField] float rotationSpeed = 0.2f;

    //Spawning
    [HideInInspector] public bool isAlive = false;
    Vector3 spawnPoint;
    [SerializeField] float respawnTime = 5f;
    float respawnRemaining = 0f;

    void Start()
    {
        spawnPoint = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        input = GetComponent<InputHandler>();
        stick = GetComponentInChildren<Stick>();
    }
    private void Update()
    {
        if (input.ConnectedController)
        {
            respawnRemaining -= Time.deltaTime;
            if (respawnRemaining <= 0f && !isAlive)
            {
                SpawnPlayer();
            }

            if (isAlive)
            {
                //Move
                Vector3 moveDirection = new Vector3(Input.GetAxisRaw(input.Horizontal), 0.0f, -Input.GetAxisRaw(input.Vertical));
                Vector3 velocity = (moveDirection * 100 * speed * Time.deltaTime) + (Vector3.up * rigidbody.velocity.y);
                rigidbody.velocity = velocity;

                //Turn character
                if (moveDirection.sqrMagnitude > 0.0f)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection, Vector3.up), rotationSpeed);
                }

                if (Input.GetButtonDown(input.Action))
                {
                    stick.isAttacking = true;
                }
            }
        }
        else
        {
            
        }
    }

    public void SpawnPlayer()
    {
        isAlive = true;
        transform.position = spawnPoint;
    }

    public void KillPlayer()
    {
        isAlive = false;
        respawnRemaining = respawnTime;
    }
}