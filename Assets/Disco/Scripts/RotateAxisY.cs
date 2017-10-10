using System.Collections;
using UnityEngine;


/// <summary>
/// Rotates the canion in axis Y
/// </summary>
public class RotateAxisY : MonoBehaviour {



    #region Position limits
    [Header("Position limits")]
    [SerializeField][Range (1, 360)] private float MaxYPosition = 0f;
        [SerializeField][Range (1, 360)] private float MinYPosition = 360f;
    #endregion

    
    /// <summary>
    /// flags to change of position
    /// </summary>
    private bool positionYfinish = true;

    [Space(20)]
    [SerializeField]
    private bool RandomPositions = false;

    [SerializeField]
    private bool RandomSpeed = false;

    /// <summary>
    /// Target position
    /// </summary>
    private float posY;

    [SerializeField]
    [Range(0.1f, 5f)]
    private float speed = 0.1f;

    /// <summary>
    /// It is truncate to the range[0.1-5]
    /// </summary>
    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            if (value <= 0.1f) { speed = 0.1f; speed = value; }
            else
            {
                if (value >= 5f) { speed = 5f; speed = value; }
                else
                {
                    speed = value;
                }
            }

        }
    }

    /// <summary>
    /// 0 infinity and more are the seconds
    /// it is used with random position process.
    /// </summary>
    [SerializeField]
    [Range(-1f, 300f)]
    private float pausespeed = 0f;

    /// <summary>
    /// It is truncate to the range[0-10]
    /// </summary>
    public float Pausespeed
    {
        get
        {
            return pausespeed;
        }

        set
        {
            if (value <= 0f) { pausespeed = 0f; pausespeed = value; }
            else
            {
                if (value >= 300f) { pausespeed = 300f; pausespeed = value; }
                else
                {
                    pausespeed = value;
                }
            }

        }
    }




    void Start()
    {
        RandomPositions = true;

        GetNewPositionsY();

        StartCoroutine("Managerposition");
    }

    

    /// <summary>
    /// Sets the position of axis Y if it is inside of the range.
    /// </summary>
    /// <param name="pos"></param>
    public bool SetPositon(float pos)
    {
        
        bool setted = false;

        if ((pos <= MaxYPosition) && (pos >= MinYPosition))
        {

            posY = pos;

            positionYfinish = true;

            StartCoroutine("GoesToPosition");

            setted = true;

        }

        return setted;
    }

    /// <summary>
    /// Sets the limits truncating them to the range[0:360]
    /// </summary>
    /// <param name="max"></param>
    /// <param name="min"></param>
    public void SetLimites(float max, float min)
    {

        if (max == min)
        {
            min -= 0.01f;
        }

        if (max < 0) { max = 0; }
        if (min < 0) { min = 0; }


        if (max > 360) { max = 360; }
        if (max > 360) { max = 360; }


    }

    IEnumerator GoesToPosition()
    {
        while (!positionYfinish)
        {

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, posY, 0), Time.deltaTime * speed);

            if (posY < 0)
            {
                if (360 + posY == Mathf.Round(transform.localEulerAngles.y))
                {

                    positionYfinish = true;
                }
            }
            else
            {
                if (posY == Mathf.Round(transform.localEulerAngles.y))
                {

                    positionYfinish = true;
                }
            }



            yield return null;
        }

    }

    #region Random
    IEnumerator Managerposition()
    {

        while (RandomPositions)
        {
            if ((positionYfinish == true) )
            {
                positionYfinish = false;
               
                GetNewPositionsY();

                StartCoroutine("NewPosition");

               
                    yield return new WaitForSecondsRealtime(pausespeed);//time in that position
                

            }else
                yield return null;
        }
    }

    /// <summary>
    /// Gets Round Radom new positions
    /// </summary>
    private void GetNewPositionsY()
    {
        if (MinYPosition == MaxYPosition)
        {
            MinYPosition -= 1f;
        }

        posY = Mathf.Round(Random.Range(MinYPosition, MaxYPosition));
    }


    IEnumerator NewPosition()
    {

        if (RandomSpeed)
            speed = UnityEngine.Random.Range(0.5f, 5);

        while (!positionYfinish)
        {

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, posY, 0), Time.deltaTime * speed);

            if (posY < 0)
            {
                if (360 + posY == Mathf.Round(transform.localEulerAngles.y))
                {

                    positionYfinish = true;
                }
            }
            else
            {
                if (posY == Mathf.Round(transform.localEulerAngles.y))
                {

                    positionYfinish = true;
                }
            }


            yield return null;
            //yield return new WaitForSeconds(speed2);
        }


    }

    #endregion
}
