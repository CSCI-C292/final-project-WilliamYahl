using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool win = false;
    [SerializeField] float _speed = 20;

    Vector2 movementVector;
    // Start is called before the first frame update
    void Start()
    {
        movementVector = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!win)
        {
            movementVector = new Vector2(Time.deltaTime * _speed * Input.GetAxis("Horizontal"), Time.deltaTime * _speed * Input.GetAxis("Vertical"));

            GetComponent<Rigidbody2D>().MovePosition(GetComponent<Rigidbody2D>().position + movementVector);
        }
        // transform.position += movementVector;
        /*
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.position + movementVector, .4f);
        if(hit)
        {
            Debug.Log(hit);
            transform.position -= movementVector;
        }
        */


        // GetComponent<Rigidbody2D>().MovePosition(transform.position + movementVector);
        // GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        win = true;
        GameState.Instance.InitiateGameOver();
    }
}
