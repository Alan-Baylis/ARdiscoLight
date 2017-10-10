using System.Collections;
using UnityEngine;

public class rotatepicture : MonoBehaviour{

    #region Flash light
        [Range(0.01f,3)]
        public float FlashSpeed = 1f;

        private Light luz=null;
        private LineRenderer RayLuz=null;

        private bool flashing = false;

   

    //slowly too, play with intensity
    [Range(0, 3f)]
    public float Intensity = 2.40f;

    [Range(0, 1f)]
    public float IntensityTime = 0.1f;

    [Range(0, 1f)]
    public float IntensityAdd = 0.1f;

    public bool slowflashing = true;

    #endregion

    #region Speed rotation

        public float speedrotation = 0.01f;
        public bool rightrotation = true;

    #endregion


    #region Colors
    public bool changecolor = true;

    public float duration = 1.0F;
    public bool colorrando = false;

    private ArrayList Colors = new ArrayList();

    #endregion

    #region Pictures
        public ArrayList Pictures = new ArrayList();
    public bool changepicture = true;
    public bool picturerando = true;
    public float durationPicture = 1.0f;

    #endregion


    void Start()
    {
        luz = transform.GetComponentInChildren<Light>();
        RayLuz = transform.GetComponentInChildren<LineRenderer>();


        Colors.Add(Color.blue);
        Colors.Add(Color.cyan);
        Colors.Add(Color.green);
        Colors.Add(Color.magenta);
        Colors.Add(Color.red);
        Colors.Add(Color.white);
        Colors.Add(Color.yellow);




        Pictures.Add(null);
        Pictures.Add((Texture2D)Resources.Load("Circle"));
        Pictures.Add((Texture2D)Resources.Load("Star"));
        Pictures.Add((Texture2D)Resources.Load("Stars"));
        Pictures.Add((Texture2D)Resources.Load("Spiral2"));
        Pictures.Add((Texture2D)Resources.Load("Triangle"));
        Pictures.Add((Texture2D)Resources.Load("Line"));
        Pictures.Add((Texture2D)Resources.Load("100money"));
        Pictures.Add((Texture2D)Resources.Load("CameraShoot"));
        Pictures.Add((Texture2D)Resources.Load("CameraShootColors"));
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



        StartCoroutine("rotation");


        // StartCoroutine("flashlight");

        //StartCoroutine("SlowFlashing");
        //FlashingLight(Random.Range(0.1f,0.5f));

        StartCoroutine("StartChangeColor");
        StartCoroutine("StartChangePicture");

        
    }


    #region Rotation



    IEnumerator rotation()
    {
        while (true)
        {
            transform.Rotate(0, 0, -1f);

            yield return new WaitForSeconds(0.01f);
        }
    }
    #endregion

    IEnumerator StartChangeColor()
    {
        int first = 0;


        while (changecolor)
        {

            if (first == Colors.Count - 1)
            {
                first = 0;
            }


            ChangeColors(first);

            if (colorrando)
            {
                yield return new WaitForSeconds(Random.Range(1f,5f));
            }
            else
            {
                yield return new WaitForSeconds(duration);
            }

            first++;

        }
    }


    private void ChangeColors(int first)
    {
        
            Color cl = (Color)Colors[first];

            luz.color = cl;

            if (RayLuz != null) RayLuz.startColor = cl;
            if (RayLuz != null) RayLuz.endColor = new Color(cl.r, cl.g, cl.b, 0); ;
        

    }

    #region Flashlighing

    public void TurnLightOn()
    {
        
        flashing = false;
        luz.enabled = true;
        StopSlowFlashing();
    }

    public void TurnLightOff()
    {
        flashing = false;
        luz.enabled = false;
    }

    public void FlashingLight(float speedFlash)
    {
        FlashSpeed = speedFlash;
       
        if( !flashing)
        {
            flashing = true;
            StartCoroutine("flashlight");
        }
       
    }

    IEnumerator flashlight()
    {
        

            while (flashing)
            {
                
                    if (luz.enabled)
                    {
                        luz.enabled = false;

                RayLuz.enabled = false;
               // RayLuz.startColor = new Color(RayLuz.startColor.r, RayLuz.startColor.g, RayLuz.startColor.b, 0);
                    }
                    else
                    {
                        luz.enabled = true;

                //RayLuz.startColor = new Color(RayLuz.startColor.r, RayLuz.startColor.g, RayLuz.startColor.b, 255);
                if (RayLuz != null) RayLuz.enabled = true;
                    }

                    yield return new WaitForSecondsRealtime(FlashSpeed);
                
            }
        
    }


    public void StartSlowFlashing(float add,float time)
    {
        IntensityAdd = add;
        IntensityTime = time;
        slowflashing = true;
    }

    public void StopSlowFlashing()
    {
        slowflashing = false;
        luz.intensity = Intensity;
    }

    IEnumerator SlowFlashing()
    {

        float added = IntensityAdd;
       

        while (slowflashing)
        {    

            if (luz.intensity >= Intensity)
            {
                added = -IntensityAdd;
            }

            if (luz.intensity <= 0)
            {
                added = IntensityAdd;
            }

                luz.intensity += added;

            yield return new WaitForSeconds(IntensityTime);
        }
    }


    #endregion


    #region Change pictures


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
            

            if (picturerando)
            {
                yield return new WaitForSeconds(Random.value * 2);
            }
            else
            {
                yield return new WaitForSeconds(durationPicture);
            }

            first++;

        }
    }





    private void ChangePicture(int posPicture)
    {
        luz.cookie =  (Texture2D) Pictures[posPicture];
    }

#endregion

}
