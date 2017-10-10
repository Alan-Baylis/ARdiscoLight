using System.Collections;
using UnityEngine;

public class rotateMouse : MonoBehaviour {

    public float rotSpeed = 50;

    #region Position limits
    public float maxXposition;
    public float minXposition;

    public float maxYposition ;
    public float minYposition ;
    #endregion

    #region flags to change of position
     
    private bool positionXfinish = true;
    private bool positionYfinish = true;

    #endregion

    public float posX;
    public float posY;


    public float speed  = 0f;
    public float speed2 = 0.001f;
    


    void Start()
    {
         maxXposition = -17f;
         minXposition = 90f;

        maxYposition =-60;
        minYposition = 60;
        positionXfinish = true;
        positionYfinish = true;


        StartCoroutine("Managerposition");
    }

    #region movement


    IEnumerator Managerposition()
    {
        GetNewPositionsXY();

        while (true)
        {
            if ((positionYfinish == true)||(positionXfinish==true))
            {
                positionYfinish = false;
                positionXfinish = false;
                GetNewPositionsXY();
                StartCoroutine("NewPosition");
            }
            yield return null;
        }
    }

    /// <summary>
    /// Gets Round Radom new positions
    /// </summary>
    private void GetNewPositionsXY()
    {
        posX =Mathf.Round( Random.Range(minXposition, maxXposition));
        posY =Mathf.Round( Random.Range(minYposition, maxYposition));
    }


    IEnumerator NewPosition()
    {

       // bool searchposition = true;
        speed = Random.Range(1, 5);
        speed2 = Random.Range(0.001f, 0.01f);
        



        // Debug.Log(transform.name + ": " + Mathf.Round(transform.eulerAngles.x) + " new position: " + posX+" : "+posY);

        while (!positionXfinish && !positionYfinish)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(posX, posY, 0), Time.deltaTime * speed);

            if (posY < 0)
            {
                if (360 + posY == Mathf.Round(transform.eulerAngles.y))
                {
                    
                    positionYfinish = true;
                }
            }
            else
            {
               if (posY == Mathf.Round(transform.eulerAngles.y))
                    {
                    
                        positionYfinish = true;
                    }
            }

            if (posX < 0)
            {
                if (360 + posX == Mathf.Round(transform.eulerAngles.x))
                {
                   
                    positionXfinish = true;
                }
            }
            else
            {
                if (posX == Mathf.Round(transform.eulerAngles.x))
                {
                    
                    positionXfinish = true;
                }
            }

            yield return new WaitForSeconds(speed2);
        }


    }

    #endregion


    private void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX);
        transform.Rotate(Vector3.right, rotY);
    }
}
