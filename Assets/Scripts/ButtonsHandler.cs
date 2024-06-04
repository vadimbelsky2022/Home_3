using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonsHandler : MonoBehaviour
{
    public Button button_left;
    public Button button_right;

    public Button button_red;
    public Button button_blue;
    public Button button_yellow;
    public Button button_green;

    public Button button_camera_up;
    public Button button_camera_down;
    public Button button_camera_face;
    public Button button_camera_left;

    public List<GameObject> star_ships;
    public GameObject current_star_ship;
    
    public int current_index = 0;

    public List<MeshRenderer> meshs;
    public MeshRenderer current_mesh;

    private float rotation_speed = 360f;
    private bool dragging = false;

    private Touch touch;
    private Vector2 oldTouchPosition;
    private Vector2 NewTouchPosition;
    private float touch_rotation_speed = 200f;


    // Start is called before the first frame update
    void Start()
    {
        current_star_ship = star_ships[current_index];
        current_mesh = meshs[current_index];
        button_right.onClick.AddListener(ShowNext);
        button_left.onClick.AddListener(ShowPrevious);

        // set colors
        button_red.onClick.AddListener(SetRed);
        button_blue.onClick.AddListener(SetBlue);
        button_yellow.onClick.AddListener(SetYellow);
        button_green.onClick.AddListener(SetGreen);
    }

    // Update is called once per frame
    void Update()
    {
        // mouse and touch handling
        if (Input.GetMouseButtonUp(0))
        {
            dragging = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }

        if (dragging)
        {
            float x = Input.GetAxis("Mouse X") * rotation_speed * Time.deltaTime;

            current_star_ship.transform.Rotate(Vector3.up * x);
        }

        RotateShip();
    }

    void ShowNext()
    {
        current_star_ship.SetActive(false);
        current_index++;
        if (current_index == 4) current_index = 0;
        current_star_ship = star_ships[current_index];
        current_mesh = meshs[current_index];
        current_star_ship.SetActive(true);
    }

    void ShowPrevious()
    {
        current_star_ship.SetActive(false);
        current_index--;
        if (current_index == -1) current_index = 3;
        current_star_ship = star_ships[current_index];
        current_mesh = meshs[current_index];
        current_star_ship.SetActive(true);
    }

    void SetMeshColor(MeshRenderer mesh, Color color)
    {
        mesh.material.shader = Shader.Find("Specular");
        mesh.material.SetColor("_SpecColor", color);
        mesh.material.SetColor("_Color", color);
    }

    void SetRed()
    {
        SetMeshColor(meshs[current_index], Color.red);
    }
    void SetBlue()
    {
        SetMeshColor(meshs[current_index], Color.blue);
    }
    void SetYellow()
    {
        SetMeshColor(meshs[current_index], Color.yellow);
    }
    void SetGreen()
    {
        SetMeshColor(meshs[current_index], Color.green);
    }

    private void RotateShip()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                oldTouchPosition = touch.position;
            }

            else if (touch.phase == TouchPhase.Moved)
            {
                NewTouchPosition = touch.position;
            }

            Vector2 rotDirection = oldTouchPosition - NewTouchPosition;
            //Debug.Log(rotDirection);
            Debug.Log(touch.position.y);

            if(touch.position.x > 250 && touch.position.x < 1000
                && touch.position.y > 100 && touch.position.y < 500)
            {
                if (rotDirection.x < 0)
                {
                    RotateRight();
                }

                else if (rotDirection.x > 0)
                {
                    RotateLeft();
                }
            }
        }
    }

    void RotateLeft()
    {
        float x = touch_rotation_speed * Time.deltaTime;
        current_star_ship.transform.Rotate(Vector3.up * x);
    }

    void RotateRight()
    {
        float x = touch_rotation_speed * Time.deltaTime;
        current_star_ship.transform.Rotate(Vector3.up * (-x));
    }
}
