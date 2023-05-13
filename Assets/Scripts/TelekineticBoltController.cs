using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelekineticBoltController : MonoBehaviour
{
    public Animator animator;
    public int bolterFireSequence = 2;
      

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (animator.GetInteger("bolterSequence") <= 3)
            {
                bolterFireSequence++;
                animator.SetInteger("bolterSequence", bolterFireSequence);

                if (animator.GetInteger("bolterSequence") >= 3)
                {
                    bolterFireSequence = -1;
                }

            }

        }

    }
}
