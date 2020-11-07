using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class M4_Shoot : AbstractObject
{
    public int damage = 15;
    private float range = 100f;

    public GameObject ParticularPrefab;
    private ParticleSystem prefabParticular;

    public GameObject ImpactGoPrefab;

    private Camera fpsCamera;

    private Text BulletsNowText, BulletsAllText;

    private AudioSource soundSource;
    [SerializeField] private AudioClip ShootSound;
    [SerializeField] private AudioClip ReloadSound;
    [SerializeField] private AudioClip TriggerSound;

    private bool delay = false;

    private Animator animator;

    public int MagazineBulletsCount = 30;
    public float DelayBetweenShoots = 0.05f;

    private MouseLook PlayerMouse, CameraMouse;
    public float velocityY, velocityX;


    bool reload = false;
    bool reloadInZoom = false;
    bool zoom = false;

    private MoveController moveController;

    private GameObject aim;

    private Camera scopeCam;

    public void Start()
    {
        prefabParticular = ParticularPrefab.GetComponent<ParticleSystem>();

        GameObject g = GameObject.Find("Main Camera");
        fpsCamera = g.GetComponent<Camera>();

        g = GameObject.Find("BulletsCount");
        BulletsNowText = g.GetComponent<Text>();

        g = GameObject.Find("AllBullets");
        BulletsAllText = g.GetComponent<Text>();

        g = GameObject.Find("Player");
        PlayerMouse = g.GetComponent<MouseLook>();
        moveController = g.GetComponent<MoveController>();

        g = GameObject.Find("Main Camera");
        CameraMouse = g.GetComponent<MouseLook>();

        g = GameObject.Find("Scope Camera");
        scopeCam = g.GetComponent<Camera>();
        scopeCam.enabled = false;

        aim = GameObject.Find("AIM");


        soundSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        if (Equiped)
        {
            animator.enabled = true;
        }
        else
        {
            animator.enabled = false;
        }
    }

    public void Update()
    {
        if (Equiped)
        {
            BulletsNowText.text = BulletsCount.ToString();
            BulletsAllText.text = AllBulletsCount.ToString();

            if (BulletsCount == 0 && !reload)
                Reload();

            if (Input.GetKey(KeyCode.Mouse0) && !delay && !reload)
            {             
                if (AllBulletsCount <= 0 && BulletsCount <= 0)
                {
                    if (!soundSource.isPlaying)
                        soundSource.PlayOneShot(TriggerSound);
                    return;
                }

                if (reloadInZoom)
                    return;

                Shoot();
            }


            if (Input.GetKeyDown(KeyCode.R) && !reload)
            {
                Reload();

            } 

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                scopeCam.enabled = true;

                aim.active = false;

                moveController.buf /= 2;
                animator.SetTrigger("Zoom");
            }
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                scopeCam.enabled = false;

                aim.active = true;

                moveController.buf *= 2;
                animator.SetTrigger("DeZoom");
            }
        }
    }

    private void Reload()
    {
        if (BulletsCount == MagazineBulletsCount)
            return;

        if (AllBulletsCount == 0)
            return;

        if (zoom)
        {
            animator.SetTrigger("ReloadInZoom");
        }
        else
        {
            animator.SetTrigger("Reload");
        }

        
        soundSource.PlayOneShot(ReloadSound);
       

        int buf = MagazineBulletsCount - BulletsCount;

        if (buf > AllBulletsCount)
        {
            BulletsCount += AllBulletsCount;
            AllBulletsCount = 0;
            return;
        }

        AllBulletsCount -= buf;
        BulletsCount += buf;

        if (AllBulletsCount < 0)
            AllBulletsCount = 0;

        BulletsNowText.text = BulletsCount.ToString();
        BulletsAllText.text = AllBulletsCount.ToString();
    }

    private void Shoot()
    {
       
        animator.SetTrigger("Shoot");


        delay = true;

        PlayerMouse.Otdacha(Random.Range(-velocityX, velocityX), 0);
        CameraMouse.Otdacha(0, Random.Range(0f, velocityY));

        soundSource.PlayOneShot(ShootSound);

        BulletsCount--;
        BulletsNowText.text = BulletsCount.ToString();

        prefabParticular.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {

            PlayerManager pm = hit.transform.GetComponent<PlayerManager>();

            if (pm != null)
            {
                pm.MinusHealth(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 1600f);
            }

            if (hit.transform.GetComponent<Destructible>() != null)
            {
                hit.transform.GetComponent<Destructible>().DestroyGM();
            }

            if (hit.transform.GetComponent<FragGrenade>() != null)
            {
                hit.transform.GetComponent<FragGrenade>().explode = true;
            }
                
            if (hit.transform.GetComponent<EnemyStats>() != null)
            {
                hit.transform.GetComponent<EnemyStats>().Damage(damage);
            }

            GameObject g = Instantiate(ImpactGoPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(g, 0.2f);
        }
        StartCoroutine("Delay");
    }

    public override void AddBullets(int b)
    {
        AllBulletsCount += b;
        BulletsAllText.text = AllBulletsCount.ToString();
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(DelayBetweenShoots);
        delay = false;
    }

    public void ReloadStart()
    {
        Debug.Log("Start");
        reload = true;
    }
    public void ReloadEnd()
    {
        Debug.Log("End");
        reload = false;
    }

    public void ZoomStart()
    {     
        zoom = true;
        velocityX = 1f;
        velocityY = 1f;
    }
    public void ZoomEnd()
    {
        zoom = false;
        velocityX = 2f;
        velocityY = 2f;
    }

    public void ReloadInZoomStart()
    {
        reloadInZoom = true;
    }
    public void ReloadInZoomEnd()
    {
        reloadInZoom = false;
    }

    private void OnDestroy()
    {
        BulletsNowText.text = " ";
        BulletsAllText.text = " ";
    }
}
