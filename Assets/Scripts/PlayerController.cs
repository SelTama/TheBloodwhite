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
        TelekineticErasure();
        PsionicBolt();
        SetBGM();
    }

    private void Movements()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (transform.position.x >= stageDimensions.w)
            {
                transform.position += Vector3.left * tidaSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.position.x <= stageDimensions.x)
            {
                transform.position += Vector3.right * tidaSpeed * Time.deltaTime;

            }
        }

        if (Input.GetKey(KeyCode.W))
        {

            if (transform.position.y <= stageDimensions.y)
            {
                transform.position += Vector3.up * tidaSpeed * Time.deltaTime;
            }
           
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (transform.position.y >= stageDimensions.z)
            {
                transform.position += Vector3.down * tidaSpeed * Time.deltaTime;
            }
            
        }

        else
        {
            transform.position = transform.position;
        }


        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.S))
                animator.SetInteger("activeAnimation", 6);
            else if (Input.GetKey(KeyCode.D))
                animator.SetInteger("activeAnimation", 0);
            else if (Input.GetKey(KeyCode.W))
                animator.SetInteger("activeAnimation", 1);
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                if (comboCount >= 1)
                {
                    animator.SetInteger("activeAnimation", 10);
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (TEIsGo)
                {
                    animator.SetInteger("activeAnimation", 9);
                }
            }
            else
                animator.SetInteger("activeAnimation", 4);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.S))
                animator.SetInteger("activeAnimation", 8);
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("activeAnimation", 0);
            else if (Input.GetKey(KeyCode.W))
                animator.SetInteger("activeAnimation", 3);
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                if (comboCount >= 1)
                {
                    animator.SetInteger("activeAnimation", 10);
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (TEIsGo)
                {
                    animator.SetInteger("activeAnimation", 9);
                }
            }
            else
                animator.SetInteger("activeAnimation", 5);
        }

        else if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.S))
                animator.SetInteger("activeAnimation", 0);
            else if (Input.GetKey(KeyCode.D))
                animator.SetInteger("activeAnimation", 3);
            else if (Input.GetKey(KeyCode.A))
                animator.SetInteger("activeAnimation", 1);
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                if (comboCount >= 1)
                {
                    animator.SetInteger("activeAnimation", 10);
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (TEIsGo)
                {
                    animator.SetInteger("activeAnimation", 9);
                }
            }
            else
                animator.SetInteger("activeAnimation", 2);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
                animator.SetInteger("activeAnimation", 6);
            else if (Input.GetKey(KeyCode.D))
                animator.SetInteger("activeAnimation", 8);
            else if (Input.GetKey(KeyCode.W))
                animator.SetInteger("activeAnimation", 0);
            else if (Input.GetKey(KeyCode.Mouse1))
            {
                if (comboCount >= 1)
                {
                    animator.SetInteger("activeAnimation", 10);
                }
            }
            else if (Input.GetKey(KeyCode.E))
            {
                if (TEIsGo)
                {
                    animator.SetInteger("activeAnimation", 9);
                }
            }
            else
                animator.SetInteger("activeAnimation", 7);
        }

        else if (Input.GetKey(KeyCode.Mouse1))
        {
            if (comboCount >= 1)
            {
                animator.SetInteger("activeAnimation", 10);
            }
        }

        else if (Input.GetKey(KeyCode.E))
        {
            if (TEIsGo)
            {
                animator.SetInteger("activeAnimation", 9);
            }
        }

        else
        {
            animator.SetInteger("activeAnimation", 0);
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

                AudioClip clip = psionicEchoes[Random.Range(0, psionicEchoes.Length)];
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

                Destroy(transform.Find("TelekineticErasure(Clone)").gameObject, 2f);
            }
        }
    }
    private void TelekineticSlash() 
    {
        // insert TSIsGo && if you add CD
        if ( Input.GetMouseButtonDown(1))
        {
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
