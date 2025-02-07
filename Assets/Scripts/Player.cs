using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    private bool isWalking = false;
    private void Update()
    {
        Vector2 direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction.y = +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction.x = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction.y = -1;
        } 
        if (Input.GetKey(KeyCode.D))
        {
            direction.x = +1;
        }
        direction= direction.normalized;
        Vector3 moveDir = new Vector3(direction.x, 0f, direction.y) * (Time.deltaTime * speed);
        transform.forward = Vector3.Slerp( transform.forward ,moveDir, Time.deltaTime * rotationSpeed); 
        transform.position += moveDir;
        isWalking = moveDir != Vector3.zero;
    }
    
    
    public bool IsWalking()
    {
        return isWalking;   
    }
}
