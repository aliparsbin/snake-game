using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public GameObject snakeBodyPart;
    float vertical, horizontal;
    Vector2 snakeMovement;
    float movementSpeed = 5f;
    float rotation = 0;
    bool axesRotated = false;

    int c_x, c_y = 1;
    enum Rotation
    {
        Up,
        Right,
        Down,
        Left
    }
    Rotation r;

    // Start is called before the first frame update

    Vector2 GetInput()
    {
        horizontal = 0;
        vertical = 0;
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            vertical = 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vertical = -1;
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            horizontal = 1;
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            horizontal = -1;
        }




        //    horizontal = Input.GetAxis("Horizontal");
        //vertical = Input.GetAxis("Vertical");

        return new Vector2(horizontal, vertical);
    }


    void Start()
    {
        snakeMovement = new Vector2(1, 0);

        r = Rotation.Right;    
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 axis = GetInput();
        rotation = 0;
        

        if (axis.x != 0)
        {
            snakeMovement = new Vector2(axis.x, 0);
            axesRotated = false;

            if ((r == Rotation.Right && axis.x > 0) || r == Rotation.Left && axis.x < 0)
            {
                rotation = 0;
            }


            else if ((r == Rotation.Left && axis.x > 0) || (r == Rotation.Right && axis.x < 0))
            {
                rotation = 180;
            }


            else if ((axis.x > 0 && r == Rotation.Up) || (axis.x < 0 && r == Rotation.Down))
            {
                rotation =  -90;
            }

            else if ((axis.x < 0 && r == Rotation.Up) || (axis.x > 0 && r == Rotation.Down))
            {
                rotation = 90;
            }

            if (axis.x > 0)
            {
                r = Rotation.Right;
                c_x = 1;
            }
            else
            {
                r = Rotation.Left;
                c_x = -1;
            }

        }

        else if (axis.y != 0)
        {
            snakeMovement = new Vector2(0, axis.y);
            axesRotated = true;

            if ((r == Rotation.Up && axis.y > 0) || r == Rotation.Down && axis.y < 0)
            {
               
                rotation = 0;
            }


            else if ((r == Rotation.Down && axis.y > 0) || (r == Rotation.Up && axis.y < 0))
            {
                rotation = 180;
            }


            else if ((axis.y < 0 && r == Rotation.Right) || (axis.y > 0 && r == Rotation.Left))
            {
                rotation =  -90;
            }


            else if ((axis.y > 0 && r == Rotation.Right) || (axis.y < 0 && r == Rotation.Left))
            {
                rotation = 90;
            }

            if (axis.y > 0)
            {
                r = Rotation.Up;
                c_y = 1;

            }
            else
            {
                r = Rotation.Down;
                c_y = -1;
            }


        }

        if(!axesRotated)
        {
            transform.Translate(new Vector3(snakeMovement.x * c_x * Time.deltaTime * movementSpeed,
            snakeMovement.y * c_y * Time.deltaTime * movementSpeed, 0));
        }


        else
        {
            transform.Translate(new Vector3(snakeMovement.y *c_y * Time.deltaTime * movementSpeed,
           snakeMovement.x *c_x * Time.deltaTime * movementSpeed, 0));
        }







        transform.Rotate(0, 0, rotation);
    }


    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        if (other.CompareTag("Food"))
        {
            GameObject snakePart = Instantiate<GameObject>(snakeBodyPart);
            snakePart.transform.position = transform.position;
            snakePart.transform.SetParent(transform);

            float pos_x = 0;


            if(transform.childCount > 1)
            {
               pos_x = transform.GetChild(transform.childCount - 1).localPosition.x - transform.childCount * 1.2f;
            }

            else
            {

                pos_x = -1.2f;
            }

            snakePart.transform.localPosition = new Vector3(pos_x, 0, 0);
        }
    }



}
