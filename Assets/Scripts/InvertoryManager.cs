using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public AbstractObject[] Objects = new AbstractObject[4];

    public Transform GunLocation;
    public GameObject gunNow;

    public int equip = 0;

    public GameObject[] Icons = new GameObject[4];
    private bool[] up_down = new bool[4];

    private int[,] Bullets = new int[4,2];

    public GameObject WeaponCamera;

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        status = ManagerStatus.Started;
    }

    public void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            up_down[i] = true;
        }

        equip = 0;    
    }

    public void Update()
    {      
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            UpIcon(equip);

            Hide();
            equip = 0;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UpIcon(equip);

            Hide();
            equip = 1;
            Equip();

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UpIcon(equip);

            Hide();
            equip = 2;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            UpIcon(equip);

            Hide();
            equip = 3;
            Equip();
        }    

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (equip == 0)
                return;

            if (Objects[equip - 1] == null)
                return;

            UpIcon(equip);

            Hide();
            equip--;
            Equip();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (equip == Objects.Length - 1)
                return;

            if (Objects[equip + 1] == null)
                return;

            UpIcon(equip);

            Hide();
            equip++;
            Equip();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UpIcon(equip);
            Hide();
            Drop();
        }
    }

    public void FixedUpdate()
    {
        MoveGun();
    }
    private void MoveGun()
    {
        if (gunNow == null)
            return;

        gunNow.transform.position = GunLocation.position;
        gunNow.transform.rotation = GunLocation.rotation;
    }

    private void Equip()
    {
        if (Objects[equip] == null )
            return;

        Objects[equip].Equiped = true;
        DownIcon(equip);

        gunNow = Instantiate(Objects[equip].Prefab, GunLocation);
        gunNow.gameObject.GetComponent<Rigidbody>().isKinematic = true;


        GameObject obj;

        for(int i = 0;i<gunNow.transform.childCount;i++)
        {
            obj = gunNow.transform.GetChild(i).gameObject;
            obj.layer = 11;
        }

        WeaponCamera.active = true;

        gunNow.active = true;

        gunNow.GetComponent<AbstractObject>().BulletsCount = Bullets[equip, 0];
        gunNow.GetComponent<AbstractObject>().AllBulletsCount = Bullets[equip, 1];
    }
    private void Hide()
    {
        if (gunNow == null)
            return;


        Bullets[equip, 0] = gunNow.GetComponent<AbstractObject>().BulletsCount;
        Bullets[equip, 1] = gunNow.GetComponent<AbstractObject>().AllBulletsCount;

        gunNow.gameObject.layer = 0;
        WeaponCamera.active = false;

        Destroy(gunNow);
    }

    private void Drop()
    {
        if (gunNow == null)
            return;

        for (int i = 0; i < Objects.Length; i++)
        {

            if (Objects[i] == gunNow)
            {
                if (i != equip)
                    return;
                else
                    break;
            }
        }


        UpIcon(equip);
        Destroy(gunNow.gameObject); // gunNow.GameObject
        
        GameObject a = Instantiate(Objects[equip].Prefab, transform.position, transform.rotation);
        a.active = true;
        a.GetComponent<AbstractObject>().Equiped = false;

        a.GetComponent<AbstractObject>().BulletsCount = Bullets[equip,0];
        a.GetComponent<AbstractObject>().AllBulletsCount = Bullets[equip, 1];

        Destroy(Objects[equip].gameObject); // Objects[equip].gameObject
    }

    private void UpIcon(int count)
    {
        if (up_down[count])
            return;

        up_down[count] = true;
        Icons[count].GetComponent<Animation>().Play("Up");
    }

    private void DownIcon(int count)
    {
        if (!up_down[count])
            return;

        up_down[count] = false;
        Icons[count].GetComponent<Animation>().Play("Down");
    }

    public bool AddGun(AbstractObject gun)
    {
        if (gun.Aspect == Aspects.AssaultRifle)
        {
            if (Objects[0] != null)
                return false;

            UpIcon(equip);
            Hide();

            Objects[0] = null;
            Objects[0] = gun;
            Debug.Log("Pistol - " + gun.name);

            equip = 0;

            Bullets[equip, 0] = gun.BulletsCount;
            Bullets[equip, 1] = gun.AllBulletsCount;
   
            Equip();

            return true;
        }
        else if (gun.Aspect == Aspects.Pistol)
        {
            if (Objects[1] != null)
                return false;

            UpIcon(equip);
            Hide();

            Objects[1] = null;
            Objects[1] = gun;
            Debug.Log("Pistol - " + gun.name);

            equip = 1;

            Bullets[equip, 0] = gun.BulletsCount;
            Bullets[equip, 1] = gun.AllBulletsCount;

            Equip();
       
            return true;
        }
        else if (gun.Aspect == Aspects.Knife)
        {
            if (Objects[2] != null)
                return false;

            UpIcon(equip);
            Hide();

            Objects[2] = null;
            Objects[2] = gun;
            Debug.Log("Knife - " + gun.name);

            equip = 2;

            Bullets[equip, 0] = gun.BulletsCount;
            Bullets[equip, 1] = gun.AllBulletsCount;

            Equip();

            return true;
        }
        else if (gun.Aspect == Aspects.Additional)
        {
            if (Objects[3] != null)
                return false;

            UpIcon(equip);
            Hide();

            Objects[3] = null;
            Objects[3] = gun;
            Debug.Log("Additional - " + gun.name);

            equip = 3;

            Bullets[equip, 0] = gun.BulletsCount;
            Bullets[equip, 1] = gun.AllBulletsCount;

            Equip();

            return true;
        }
        return false;
    }


}
