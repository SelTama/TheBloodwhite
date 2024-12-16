using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class PlayerController : MonoBehaviour
{
    public delegate void Cooldown();
    public static event Cooldown TEOnCooldown;
    //public static event Cooldown TSOnCooldown;

    public bool TEIsGo = true;
    //public bool TSIsGo = true;




    public int comboCount = 0;
    public Animator animator;
    public Rigidbody2D rayCastPointObj;
    public GameObject camObj;
    public GameObject doubtpurge;
    public GameObject bolt;
    public int batterySequence = 0;
    private float tidaSpeed = 6f;
    private Vector3 reticule;
    public Vector4 stageDimensions;

    private float movementSpeedX = 6f;
    private float movementSpeedY = 6f;
    public float animatorSpeedX;
    public float animatorSpeedY;

    public List<PsiBolterModel> PsiBolterBatteryList = new List<PsiBolterModel>();
    public AudioClip[] psionicEchoes;

    AudioSource BGMAudioSource;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        camObj = GameObject.FindWithTag("MainCamera"); 
        stageDimensions = GameObject.FindWithTag("BG").GetComponent<BackgroundMovement>().stageDimensions;
        BGMAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    void Update()
    {
        Movements();
        TelekineticSlash();
        PsionicBolt();
        SetBGM();
        TelekineticErasure();
    }

    private void Movements()
    {
        animator.SetFloat("X", animatorSpeedX);
        animator.SetFloat("Y", animatorSpeedY);

        if (Input.GetKey(KeyCode.D))
        {         
            if (transform.position.x <= stageDimensions.x)
            {               
                transform.position += Vector3.right * tidaSpeed * Time.deltaTime;
            }
            //ANIMATIONS
            animatorSpeedX = Mathf.Clamp( Mathf.SmoothDamp(animatorSpeedX,1,ref movementSpeedX,.2f), -1, 1);

            if (!Input.GetKey(KeyCode.W) && animatorSpeedY >= 0)
            {
                animatorSpeedY = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedY, 0, ref movementSpeedY, .4f), -1, 1);
            }
            if (!Input.GetKey(KeyCode.S) && animatorSpeedY <= 0)
            {
                animatorSpeedY = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedY, 0, ref movementSpeedY, .4f), -1, 1);
            }
        }


        if (Input.GetKey(KeyCode.A))
        {            
            if (transform.position.x >= stageDimensions.w)
            {
                transform.position += Vector3.left * tidaSpeed * Time.deltaTime;
            }
            //ANIMATIONS
            animatorSpeedX = Mathf.Clamp( Mathf.SmoothDamp(animatorSpeedX, -1, ref movementSpeedX, .2f), -1, 1);

            if (!Input.GetKey(KeyCode.W) && animatorSpeedY >= 0)
            {
                animatorSpeedY = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedY, 0, ref movementSpeedY, .4f), -1, 1);
            }
            if (!Input.GetKey(KeyCode.S) && animatorSpeedY <= 0)
            {
                animatorSpeedY = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedY, 0, ref movementSpeedY, .4f), -1, 1);
            }
        }

        if (Input.GetKey(KeyCode.W))
        {           
            if (transform.position.y <= stageDimensions.y)
            {
                
                transform.position += Vector3.up * tidaSpeed * Time.deltaTime;
            }
            //ANIMATIONS
            animatorSpeedY = Mathf.Clamp( Mathf.SmoothDamp(animatorSpeedY, 1, ref movementSpeedY, .2f), -1, 1);

            if (!Input.GetKey(KeyCode.D) && animatorSpeedX >= 0)
            {
                animatorSpeedX = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedX, 0, ref movementSpeedX, .4f), -1, 1);
            }
            if (!Input.GetKey(KeyCode.A) && animatorSpeedX <= 0)
            {
                animatorSpeedX = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedX, 0, ref movementSpeedX, .4f), -1, 1); 
            }
        }

        if (Input.GetKey(KeyCode.S))
        {         
            if (transform.position.y >= stageDimensions.z)
            {                
                transform.position += Vector3.down * tidaSpeed * Time.deltaTime;
            }
            //ANIMATIONS
            animatorSpeedY = Mathf.Clamp( Mathf.SmoothDamp(animatorSpeedY, -1, ref movementSpeedY, .2f), -1, 1);

            if (!Input.GetKey(KeyCode.D) && animatorSpeedX >= 0)
            {
                animatorSpeedX = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedX, 0, ref movementSpeedX, .4f), -1, 1);
            }
            if (!Input.GetKey(KeyCode.A) && animatorSpeedX <= 0)
            {
                animatorSpeedX = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedX, 0, ref movementSpeedX, .4f), -1, 1);
            }
        }

       

        else
        {
            animatorSpeedY = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedY, 0, ref movementSpeedY, .6f), -1, 1);
            animatorSpeedX = Mathf.Clamp(Mathf.SmoothDamp(animatorSpeedX, 0, ref movementSpeedX, .6f), -1, 1);
            transform.position = transform.position;
        }

    }

    private void PsiBatteryFire()
    {
        if (Input.GetMouseButton(0))
        {
            //hangi noktadan çıkartıyorsan, o nokta vektorun hedef noktası oluyor
            reticule = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 direction = (reticule - PsiBolterBatteryList[batterySequence].Battery.transform.position).normalized;
            //Debug.DrawRay(PsiBolterBatteryList[batterySequence].Battery.transform.position, direction, Color.green);
            RaycastHit2D hit = Physics2D.Raycast(PsiBolterBatteryList[batterySequence].Battery.transform.position, direction, Mathf.Infinity);
            //if you get shitty bugs on hit youll have to cehck if youre hitting yourself or not bc youve written this code with having hitting yourself for granted

            if (!hit.transform.CompareTag("PsiBolt"))
            {
                // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                // from to rotation iki nokta arasındaki açıyı x,y duzlemine gore belirlemek için
                GameObject psionicBolt = Instantiate(bolt, PsiBolterBatteryList[batterySequence].Battery.transform.position, Quaternion.identity);
                //get your colliders specified in detail, 
                Physics2D.IgnoreCollision(GetComponent<Rigidbody2D>().GetComponent<CapsuleCollider2D>(), psionicBolt.GetComponent<Collider2D>());
                //check "is trigger" if the collision happens on other atk items

                AudioClip clip = psionicEchoes[UnityEngine.Random.Range(0, psionicEchoes.Length)];
                GetComponent<AudioSource>().PlayOneShot(clip);


                psionicBolt.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 5000f);
            }
        }
    }



    private void TelekineticErasure()
    {
        if (TEIsGo)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (TEOnCooldown != null)
                    TEOnCooldown();
                GetComponentInChildren<TelekineticBoltController>().animator.SetInteger("bolterSequence", -1);
                var TelekineticErasure = Instantiate(Resources.Load("Prefabs/TidaAttacks/TelekineticErasure" , typeof(GameObject)), rayCastPointObj.transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity,transform) as GameObject;
                animator.SetTrigger("TelekineticErasure");
                Destroy(transform.Find("TelekineticErasure(Clone)").gameObject, 2f);
            }
        }
    }


    private void TelekineticSlash() 
    {
        // insert TSIsGo && if you add CD
        if ( Input.GetMouseButtonDown(1))
        {
            if (comboCount == 0)
            {
                animator.SetTrigger("TelekineticSlash");

            }
            GetComponentInChildren<TelekineticBoltController>().animator.SetInteger("bolterSequence", -1);
            comboCount++;
            StartCoroutine(TidaStartsMeleeCombo());

        }
    }

    private void PsionicBolt()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (batterySequence <= PsiBolterBatteryList.Count - 1)
            {
                PsiBatteryFire();
                GetComponentInChildren<TelekineticBoltController>().animator.SetInteger("bolterSequence", batterySequence);
            }
            if (batterySequence == PsiBolterBatteryList.Count - 1)
            {
                PsiBatteryFire();
                GetComponentInChildren<TelekineticBoltController>().animator.SetInteger("bolterSequence", batterySequence);
                batterySequence = -1;
            }
            batterySequence++;
        }
    }

    private void SetBGM() 
    {

        if (GetComponent<TidaStateScript>().tidaIsInCombat == false)
        {
            if (BGMAudioSource.clip != GetComponent<TidaStateScript>().GetCruiseModeMusic())
            {
                BGMAudioSource.clip = GetComponent<TidaStateScript>().GetCruiseModeMusic();
                BGMAudioSource.Play();
            }
        }


        else if (GetComponent<TidaStateScript>().tidaIsInCombat == true)
        {
            if (BGMAudioSource.clip != GetComponent<TidaStateScript>().GetCombatModeMusic())
            {
                BGMAudioSource.clip = GetComponent<TidaStateScript>().GetCombatModeMusic();
                BGMAudioSource.Play();
            }
        }

    }




    IEnumerator TidaStartsMeleeCombo()
    {
        GetComponentInChildren<TelekineticBoltController>().animator.SetInteger("bolterSequence", -1);
        if (comboCount > 2 ) {
            comboCount = 2;
        }
        Debug.Log(comboCount);

        doubtpurge.GetComponent<Animator>().SetInteger("isSlashing", comboCount);
        yield return new WaitForSeconds(.8f);
        if (comboCount > 1)
        {
            animator.SetTrigger("TelekineticSlash2");
        }
        //if (TSOnCooldown != null)
        //    TSOnCooldown();
        comboCount = 0;
        doubtpurge.GetComponent<Animator>().SetInteger("isSlashing", 0);
    }


    [System.Serializable]
    public class PsiBolterModel
    {
        public GameObject Battery;
    }


    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        transform.position = new Vector3(0, 0, 0);
        stageDimensions = GameObject.FindWithTag("BG").GetComponent<BackgroundMovement>().stageDimensions;
    }


}
