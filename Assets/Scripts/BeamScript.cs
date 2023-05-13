using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{

    public bool BeamActive = false;
    public GameObject Beam;



    void Update()
    {
        if (!BeamActive)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Beam.gameObject.SetActive(true);
            }
            else
                Beam.gameObject.SetActive(false);

        }

        
    }


    
}
