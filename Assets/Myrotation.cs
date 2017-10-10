using UnityEngine;

public class Myrotation : MonoBehaviour {
    
    
         public float speed;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation,transform.rotation, step);
    }
}
