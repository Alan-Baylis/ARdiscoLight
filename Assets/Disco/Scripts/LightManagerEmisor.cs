using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManagerEmisor : MonoBehaviour {

    

    #region Flash light
        [Range(0.01f, 3)][Header("Flash light")]
        [SerializeField]
        private float FlashSpeed = 30f;

        private bool flashing = false;
    
        //slowly too, play with intensity
        [Range(0, 3f)]
        private float MaxIntensity = 1.8f;

        [Range(0, 1f)]
        private float IntensityTime = 0.1f;

        [Range(0, 1f)]
        private float IntensityAdd = 0.1f;

        private bool slowflashing = true;

    #endregion

    #region Speed rotation
        [Space(20)]
        [Header("Picture rotation:")]
        [SerializeField][Range(-5,5)]
        private float speedrotation = 0f;

        private bool Rotate = false;
        private bool RandomRotate = false;
     #endregion

    #region Light components
        [Space(20)]
        [Header("Light and RayLight components:")]
        public Light LuzRotate = null;
        public Light PointLuz = null; 
    #endregion

    #region Colors

    private bool changecolor = false;
            [Space(20)]
            [Header("Colors:")]
            [SerializeField][Range(0f,5f)]
            private float duration = 1.0F;

            [SerializeField]
            private bool colorRamdon = false;

            private ArrayList Colors = new ArrayList();

        #endregion

    #region Pictures
        public ArrayList Pictures = new ArrayList();
    [Space(20)]
    [Header("Pictures:")]
    public bool changepicture = true;
    public bool pictureRandom = true;//Random time between pictures
    public float durationPicture = 1.0f;//time between pictures

    #endregion

    #region Spot Angle
    [Space(20)]
    [Header("Spot Angle")]
    [SerializeField]
    private float MaxLimitAngle = 16f;
    [SerializeField]
    private float MinLimitAngle = 1f;


    [SerializeField][Range(1,500)]
    private float SpeedPingPongAngle = 10;

    private bool PingPongAngleLoop = true;
    private bool SettingAngleLoop = true;
    private bool StopAngleRandom = false;

    #endregion

    void Start()
        {

        #region Loading of colors
        Colors.Add(Color.white);
        Colors.Add(Color.blue);
        Colors.Add(Color.cyan);
        Colors.Add(Color.green);
        Colors.Add(Color.magenta);
        Colors.Add(Color.red);
        Colors.Add(Color.yellow); 
        #endregion

        #region Loading of pictures
        Pictures.Add(null);
        Pictures.Add((Texture2D)Resources.Load("Circle"));
        Pictures.Add((Texture2D)Resources.Load("Star"));
        Pictures.Add((Texture2D)Resources.Load("Stars"));
        Pictures.Add((Texture2D)Resources.Load("Spiral1"));
        Pictures.Add((Texture2D)Resources.Load("Triangle"));
        Pictures.Add((Texture2D)Resources.Load("Line"));
        Pictures.Add((Texture2D)Resources.Load("100money"));
        Pictures.Add((Texture2D)Resources.Load("CameraShoot"));
        Pictures.Add((Texture2D)Resources.Load("Circle2"));
        Pictures.Add((Texture2D)Resources.Load("cross"));
        Pictures.Add((Texture2D)Resources.Load("dice"));
        Pictures.Add((Texture2D)Resources.Load("lifeguard"));
        Pictures.Add((Texture2D)Resources.Load("octogone"));
        Pictures.Add((Texture2D)Resources.Load("puzle"));
        Pictures.Add((Texture2D)Resources.Load("radiation"));
        Pictures.Add((Texture2D)Resources.Load("wheel1"));
        Pictures.Add((Texture2D)Resources.Load("round"));
        Pictures.Add((Texture2D)Resources.Load("SmokeAlarm"));
        Pictures.Add((Texture2D)Resources.Load("spider"));
        Pictures.Add((Texture2D)Resources.Load("sun1"));
        Pictures.Add((Texture2D)Resources.Load("Star2"));
        Pictures.Add((Texture2D)Resources.Load("sun2"));
        Pictures.Add((Texture2D)Resources.Load("radiation2"));
        #endregion



        #region Testing

        Rotate = true;
        StartCoroutine("rotation");
        SetRandomSpeedRotation(true);

        //flashing = true;
        //StartCoroutine("flashlight");

        //StartCoroutine("SlowFlashing");
        //FlashingLight(0.5f);
        //StartSlowFlashing(0.2f, 0.1f);

        SecuenceChangeColor(true);
        

        //SetAngle(1);
        SettingAngleRandom();

        PicturesRandom();
        //PicturesSecuence();
        #endregion

    }
    #region Angle

    public void SettingAnglePingPong()
    {
        PingPongAngleLoop = true;
        StartCoroutine("PingPongAngle");
    }
    /// <summary>
    /// Runs a ping pong with the value of the spot angle. Custom speed.
    /// </summary>
    /// <returns></returns>
    IEnumerator PingPongAngle()
    {

        while (PingPongAngleLoop)
        {
            SetAngle(Mathf.PingPong(Time.time * SpeedPingPongAngle, MaxLimitAngle));

            yield return null;
        }
    }

    public bool SetAngle(float AngleValue)
    {
        bool settedAngle = false;

        //Stop all coroutine for settings Angles.
        PingPongAngleLoop = false;
        StopAngleRandom = true;
        SettingAngleLoop = false;

        if ((AngleValue <= MaxLimitAngle) && (AngleValue >= MinLimitAngle))
        {
            settedAngle = true;
            StartCoroutine("SetAngleSoft",AngleValue);
            //LuzRotate.spotAngle = AngleValue;
        }
        else
        {
            settedAngle = false;
        }


        return settedAngle;
    }


    IEnumerator SetAngleSoft(float value)
    {
        float t = 0f;
        SettingAngleLoop = true;
        float spotAngleBackup = LuzRotate.spotAngle;

        while (SettingAngleLoop)
        {
           
            LuzRotate.spotAngle = Mathf.Lerp(spotAngleBackup, value, t);
            
            t += 1f * Time.deltaTime;

            // now check if the interpolator has reached 1.0     
            if (t > 1.5f)
            {
                SettingAngleLoop = false;
            }

            yield return null;
        }
    }

    public void SettingAngleRandom()
    {
        StopAngleRandom = false;
        StartCoroutine("SetAngleRandom");
    }


    IEnumerator SetAngleRandom()
    {
        float t = 0f;
        

        float spotAngleBackup = LuzRotate.spotAngle;
        float newAngle = Random.Range(MinLimitAngle, MaxLimitAngle);

        while (!StopAngleRandom)
        {

            LuzRotate.spotAngle = Mathf.Lerp(spotAngleBackup, newAngle, t);

            t += 1f * Time.deltaTime;

            // now check if the interpolator has reached 1.0     
            if (t > 1.5f)
            {
                spotAngleBackup = newAngle;
                newAngle= Random.Range(MinLimitAngle, MaxLimitAngle);
                t = 0f;
                yield return null;
            }
            else
            {
                yield return null;
            }

            
        }
    }

    #endregion

    #region Rotation

        /// <summary>
        /// Range between [-5,5]     
        ///     Left rotation less than 0
        ///     0 is not rotation
        ///     Right rotation more than 0
        /// </summary>
        /// <param name="speedrotation"></param>
        public void SetSpeedRotation(float speedrotate)
    {

        if (speedrotate == 0) { Rotate = false; }  //we stop the rotation
        else
        {
            if (Rotate)
            {
                //rotate is working
                speedrotation = speedrotate;
            }
            else
            {   
                //rotate is not working
                speedrotation = speedrotate;
                Rotate = true;
                StartCoroutine("rotation");
                
            }

        }
    }

        public void SetRandomSpeedRotation(bool Randomrotation)
        {
            if (Randomrotation)
            {
                RandomRotate = true;
                StartCoroutine("RandomSpeedRotation");
            }
            else
            {
                RandomRotate = false;
            }

        
        }


        IEnumerator RandomSpeedRotation()
    {
        while (RandomRotate)
        {
            speedrotation = Random.Range(-2f, 2f);

            yield return new WaitForSecondsRealtime(Random.Range(1f, 3f));
        }
    }

        IEnumerator rotation()
        {
            while (Rotate)
            {            
                LuzRotate.transform.Rotate(0, 0, speedrotation*(-1), Space.Self);
            
                yield return null;
            }
        }
    #endregion


    #region Colors

    /// <summary>
    /// runs a secuence of colors.
    /// </summary>
    /// <param name="random">true, the next color is randomized</param>
    public void SecuenceChangeColor(bool random)
    {

        if (random) colorRamdon = true;

        changecolor = true;
        StartCoroutine("StartChangeColor");
    }

    IEnumerator StartChangeColor()
    {
        int first = 0;


        while (changecolor)
        {

            if (first == Colors.Count)
            {
                first = 0;
            }


            ChangeColors(first);

            if (colorRamdon)
            {
                first = Random.Range(0, Colors.Count - 1);
                yield return new WaitForSeconds(Random.Range(0.1f, 2f));
            }
            else
            {first++;
                yield return new WaitForSeconds(duration);
            }

            

        }
    }

    /// <summary>
    /// Sets the color of the ray.
    /// 0 - white
    /// 1 - blue
    /// 2 - cyan 
    /// 3 - green 
    /// 4 - magenta 
    /// 5 - red 
    /// 6 - yellow
    /// </summary>
    /// <param name="numcolor"></param>
    private void ChangeColors(int numcolor)
    {

        Color cl = (Color)Colors[numcolor];

        LuzRotate.color = cl;
        PointLuz.color = cl;
        

    } 
    #endregion

    #region Flashlighing

        public void TurnLightOn()
        {

            flashing = false;
            LuzRotate.enabled = true;
            PointLuz.enabled = true;
            StopSlowFlashing();
        }

        public void TurnLightOff()
        {
            flashing = false;
            LuzRotate.enabled = false;
            PointLuz.enabled = false;
        
    }

        public void FlashingLight(float speedFlash)
        {
            FlashSpeed = speedFlash;

            if (!flashing)
            {
                flashing = true;
                StartCoroutine("flashlight");
            }

        }

        IEnumerator flashlight()
        {
            while (flashing)
            {

                if (LuzRotate.enabled)
                {
                    LuzRotate.enabled = false;
                    PointLuz.enabled = false;

                }
                else
                {
                    LuzRotate.enabled = true;
                    PointLuz.enabled = true;
                
                }

                yield return new WaitForSecondsRealtime(Random.Range(0f, FlashSpeed));

            }

        }


        public void StartSlowFlashing(float add, float time)
        {
            IntensityAdd = add;
            IntensityTime = time;
            slowflashing = true;

        StartCoroutine("SlowFlashing");
        }

        public void StopSlowFlashing()
        {
            slowflashing = false;
            LuzRotate.intensity = MaxIntensity;
        }

        IEnumerator SlowFlashing()
        {

            float added = IntensityAdd;


            while (slowflashing)
            {

                if (LuzRotate.intensity >= MaxIntensity)
                {
                    added = -IntensityAdd;
                }

                if (LuzRotate.intensity <= 0)
                {
                    added = IntensityAdd;
                }

            LuzRotate.intensity += added;

                yield return new WaitForSeconds(IntensityTime);
            }
        }


        #endregion


    #region Change pictures

        public void PicturesRandom()
        {
            changepicture = true;
            pictureRandom = true;
            StartCoroutine("StartChangePicture");
        }

    public void PicturesSecuence()
    {
        changepicture = true;
        
        StartCoroutine("StartChangePicture");
    }

    IEnumerator StartChangePicture()
            {
                int first = 0;


                while (changepicture)
                {
               

                    if (first == Pictures.Count)
                    {
                        first = 0;
                    }

                     ChangePicture(first);
               

                    if (pictureRandom)
                    {
                        first = Random.Range(0, Pictures.Count-1);
                        yield return new WaitForSeconds(Random.value * 2);
                    }
                    else
                    {
                        first++;
                        yield return new WaitForSeconds(durationPicture);
                    }

                

                }
            }

        /// <summary>
        /// posPicture should have between 0 to Pictures.count
        /// </summary>
        /// <param name="posPicture"></param>
        private void ChangePicture(int posPicture)
    {
        if ((posPicture < Pictures.Count) && (posPicture >= 0)){

            LuzRotate.cookie = (Texture2D)Pictures[posPicture];
        }

    }

        #endregion

    
}
