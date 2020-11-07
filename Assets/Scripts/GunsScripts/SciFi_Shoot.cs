using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SciFi_Shoot : AbstractObject
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

    private Animation anim;
    public Animation barrolAnimation;

    private MouseLook PlayerMouse, CameraMouse;

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

        g = GameObject.Find("Main Camera");
        CameraMouse = g.GetComponent<MouseLook>();

        soundSource = GetComponent<AudioSource>();

        anim = GetComponent<Animation>();
      
    }

    public void Update()
    {
        if (Equiped)
        {
            if (!anim.isPlaying)
                anim.Play("Inactive");

            BulletsNowText.text = BulletsCount.ToString();
            BulletsAllText.text = AllBulletsCount.ToString();

            if (BulletsCount == 0)
                Reload();

            if (Input.GetKeyDown(KeyCode.Mouse0) && !delay && !barrolAnimation.IsPlaying("Reload") && !anim.IsPlaying("Shoot"))
            {
                if (AllBulletsCount <= 0 &&BulletsCount <= 0)
                {
                    soundSource.Stop();
                    soundSource.PlayOneShot(TriggerSound);
                    return;
                } 


                anim.Play("Shoot");

                barrolAnimation.Play("BarrolRotation");
                Shoot();
            }
           
          
            if (Input.GetKeyDown(KeyCode.R) && !barrolAnimation.IsPlaying("Reload"))
            {
                Reload();
            }
        }
    }



    private void Reload()
    {
        if (BulletsCount == 12)
            return;

        if (AllBulletsCount == 0)
            return;

        soundSource.PlayOneShot(ReloadSound);
        barrolAnimation.Play("Reload");

        int buf = 12 - BulletsCount;

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
        delay = true;

        PlayerMouse.Otdacha(Random.Range(-1, 1f),0);
        CameraMouse.Otdacha(0,Random.Range(0f, 1f));

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
                hit.rigidbody.AddForce(-hit.normal * 1200f);
            }

            if (hit.transform.GetComponent<FragGrenade>() != null)
            {
                hit.transform.GetComponent<FragGrenade>().explode = true;
            }

            if (hit.transform.GetComponent<EnemyStats>() != null)
            {
                hit.transform.GetComponent<EnemyStats>().Damage(damage);
            }

            if (hit.transform.GetComponent<Destructible>() != null)
            {
                hit.transform.GetComponent<Destructible>().DestroyGM();
            }

            GameObject g = Instantiate(ImpactGoPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(g,0.2f);
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
        yield return new WaitForSeconds(0.2f);
        delay = false;
    }

    private void OnDestroy()
    {
        BulletsNowText.text = " ";
        BulletsAllText.text = " ";
    }
}
