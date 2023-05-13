using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorBetaBoltScript : MonoBehaviour
{

    public float DMG = 5f;

    public GameObject playerObj;
    private void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        StartCoroutine(SeekThePlayer());
        //float angle = Mathf.Atan2(transform.position.y,playerObj.transform.position.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0,0,angle);
    }

    IEnumerator SeekThePlayer()
    {        
        Vector3 aimCorrection = playerObj.GetComponent<Rigidbody2D>().transform.position - transform.position;
        Vector3 aimBreakoff = transform.position - playerObj.transform.position;


        if (Vector3.Distance(transform.position, playerObj.GetComponent<Rigidbody2D>().transform.position) <= 40f)
        {
            transform.position = transform.position += Vector3.up * Time.deltaTime * .3f;
            yield return new WaitForSeconds(.6f);

            transform.position = transform.position += aimCorrection * Time.deltaTime * 5f;
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
        else
        {
            transform.position = transform.position += aimBreakoff * Time.deltaTime * 2f;
            Destroy(gameObject , 1f );
        }

        //transform.position = transform.position += aimCorrection * 8f * Time.deltaTime;
        //yield return new WaitForSeconds(.3f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "PsiBolt") 
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "TelekineticSlash")
        {
            Debug.Log("took a psi blade to the BOLT");
            Destroy(gameObject);
        }
    }
}
