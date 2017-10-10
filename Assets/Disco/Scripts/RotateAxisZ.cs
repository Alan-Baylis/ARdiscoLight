using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Rotates the canion in the Z axis. Limits between: 190�: -50�
/// </summary>
public class RotateAxisZ : MonoBehaviour {


    #region Position limits
        [Header("Position limits")]
        [SerializeField]
        [Range(-50, 190)]
        private float MaxZPosition=190f;

        [SerializeField]
        [Range(-50, 190)]
        private float MinZPosition=-50;
        
    #endregion

    

    /// <summary>
    /// flags that lets to change of position
    /// </summary>
    private bool positionZfinish = true;

    [Space(20)]
    [SerializeField]
    private bool RandomPositions = false;

    [SerializeField]
    private bool RandomSpeed = false;

    private float posZ;

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
    [Range(0f, 300f)]
    private float pausespeed = 1f;

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
        GetNewPositionsZ();
        StartCoroutine("Managerposition");
    }
    
    /// <summary>
    /// Sets the position of axis Z if it is inside of the range.
    /// </summary>
    /// <param name="pos"></param>
    public bool SetPositon(float pos)
    {
        
        bool setted = false;

        if ((pos <= MaxZPosition) && (pos >= MinZPosition))
        {

            posZ = pos;

            positionZfinish = false;

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
    public void SetLimites(float max, float min) {
  
        if (max == min)
        {
            min -= 0.01f;
        }

        if (max < 0) { max = 0; }
        if (min < 0) { min = 0; }


        if (max >360) { max = 360; }
        if (max >360) { max = 360; }
        

    }

    IEnumerator GoesToPosition()
    {
        while (!positionZfinish)
        {

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, posZ), Time.deltaTime * Speed);

            if (posZ < 0)
            {
                if (360 + posZ == Mathf.Round(transform.localEulerAngles.z))
                {

                    positionZfinish = true;
                }
            }
            else
            {
                if (posZ == Mathf.Round(transform.localEulerAngles.z))
                {

                    positionZfinish = true;
                }
            }
            
            yield return null;
        }
        
    }

    #region random position

    IEnumerator Managerposition()
    {
        while (RandomPositions)
        {
            if ((positionZfinish == true) )
            {
                positionZfinish = false;
                
                GetNewPositionsZ();

                StartCoroutine("NewPosition");

               
                    yield return new WaitForSecondsRealtime(pausespeed);//time in that position
                
            }
            yield return null;
        }
    }

    /// <summary>
    /// Gets Round Radom new positions
    /// </summary>
    private void GetNewPositionsZ()
    {        
        if (MinZPosition == MaxZPosition)
        {
            MinZPosition-=1f;
        }
       
            posZ = Mathf.Round(UnityEngine.Random.Range(MinZPosition, MaxZPosition));
        

        SetPositon(posZ);
    }

    IEnumerator NewPosition()
    {

        if (RandomSpeed)
            speed = UnityEngine.Random.Range(0.5f, 5);


        while (!positionZfinish)
        {

            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(0, 0, posZ), Time.deltaTime * Speed);

            if (posZ < 0)
            {
                if (360 + posZ == Mathf.Round(transform.localEulerAngles.z))
                {

                    positionZfinish = true;
                }
            }
            else
            {
                if (posZ == Mathf.Round(transform.localEulerAngles.z))
                {

                    positionZfinish = true;
                }
            }
            
            yield return null;
        }


    }
#endregion

    
}
