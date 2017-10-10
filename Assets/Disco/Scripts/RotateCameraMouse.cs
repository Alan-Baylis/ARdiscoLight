using UnityEngine;
[AddComponentMenu("Camera-Control/Mouse Look")]
public class RotateCameraMouse : MonoBehaviour {

    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f; // rotation around the up/y axis
    private float rotX = 0.0f; // rotation around the right/x axis



    //move with butons
    // VARIABLES
    public float panSpeed = 3.0f;

    private Vector3 mouseOrigin;
    private bool isPanning;
    private float speed = 10f;

    void Start()
    {
        Cursor.visible = false;


        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {

        #region moves the camera with the mouse axis
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;

#endregion


        

        if (Input.GetMouseButtonDown(1))
        {
            //right click was pressed    
            isPanning = true;
            mouseOrigin = Input.mousePosition;
        }

       


        // cancel on button release
        if (!Input.GetMouseButton(1))
        {
            isPanning = false;
        }

        //move camera on X & Y
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            // update x and y but not z
            //Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            Vector3 move = new Vector3(0, pos.y * panSpeed,0);

            Camera.main.transform.Translate(move, Space.Self);
        }



        #region keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0,0, -speed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0,0, speed * Time.deltaTime));
        }
        #endregion

        //Exit app
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
