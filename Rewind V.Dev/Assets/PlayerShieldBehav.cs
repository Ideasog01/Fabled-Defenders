using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShieldBehav : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void LateUpdate()
    {
        float mouseY = Input.GetAxis("Mouse Y") * 100 * Time.deltaTime;

        if(this.transform.eulerAngles.z < 60 && this.transform.eulerAngles.z > -60)
        {
            this.transform.Rotate(Vector3.forward * mouseY);
        }
        if(this.transform.eulerAngles.z > 60)
        {
            this.transform.Rotate(Vector3.forward * -mouseY);
        }


        

    }
}
