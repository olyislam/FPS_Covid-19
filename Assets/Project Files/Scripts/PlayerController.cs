using UnityEngine;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(SphereCollider))]
public class PlayerController : MonoBehaviour
{
#pragma warning disable 649
    [Tooltip("Player Mechanism")]
    private Player player;
    [Tooltip("Player Health")]
    private int Health = 50;


    [SerializeField, Tooltip("AR Camera Reference and it will use for fire direction and position")]
    private Camera ARcamera;

    [Space, Header ("Shield Setup")]
    [SerializeField, Tooltip("The Protected tools from virus attack")]
    private GameObject Shield;
    [SerializeField, Tooltip("Amount of availbale shield that thime use the shield")]
    private int ShieldAmount;

    [Space, Header("Bullet Setup")]
    [SerializeField, Tooltip ("Bullet prefab that spwn when make fire")]
    private GameObject Bullet;
    [SerializeField, Range(2, 200)]
    private float BulletSpeed;



    [SerializeField, Tooltip("This Audio will Play When when Shoot")]
    private AudioClip FireClip;  
    [SerializeField, Tooltip("This Audio will Play damage with virus")]
    private AudioClip DamageClip;
#pragma warning restore 649

    private void Start()
    {
        Initilize();
    }

    //initilize all of the player property
    private void Initilize()
    {
        Health = 50;
        AudioSource Sound = GetComponent<AudioSource>();
        player = new Player(Sound);
        if (ARcamera == null) { ARcamera = Camera.main; }
        
        
        DiActiveShield();
        GameManager.instance.ui.UpdateShield(ShieldAmount);
        GameManager.instance.ui.UpdateHealth(Health);
    }

    private void Update()
    {
        if (GameManager.instance.isGameOver)
            return;


#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F))
            Fire();
        if (Input.GetKeyDown(KeyCode.S))
            ActiveShield();
#endif

        TouchControl();
    }

    //Player Control Fire And shield fromtis methode
    private void TouchControl()
    {

        if (Input.touchCount <= 0)
            return;

        Touch touch = new Touch();
        touch = Input.GetTouch(Input.touchCount - 1);

        if (touch.phase == TouchPhase.Began)
        {
            if (touch.position.x > Screen.width / 2)
                Fire();
            else
                ActiveShield();
        
        }
    }


    //create bullet and apply force and set direction on the bullet
    private void Fire()
    {
        int damagepower = player.GetGunpower();
        player.PlayAudio(FireClip);

        Bullet bullet = player.SpawnBullet(Bullet, ARcamera);
        bullet.GoTo(this.ARcamera.transform.forward, BulletSpeed, damagepower);
    }


    //active shield for protect virus attack
    public void ActiveShield()
    {
        if (Shield.activeInHierarchy || ShieldAmount <= 0)
            return;

        Shield.SetActive(true);
        ShieldAmount--;
        GameManager.instance.ui.UpdateShield(ShieldAmount);
        Invoke("DiActiveShield", 2);
    }

    //Invoke from active shield for deactive shiels
    public void DiActiveShield()
    {
        Shield.SetActive(false);

    }

    //Add Shield amount 
    public void AddShield()
    {
        ShieldAmount++;
        GameManager.instance.ui.UpdateShield(ShieldAmount);
    }


    //update player health from Oncollision with virus
    private void TakeDamage(int amoutn)
    {
        Health -= amoutn;
        GameManager manager = GameManager.instance;
        manager.ui.UpdateHealth(Health);
        manager.isGameOver = player.isDead(Health);
        if (manager.isGameOver)
        { 
            manager.OnGameover();
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        GameObject obj = col.gameObject;
        Debug.Log("VVVVV");
        if (obj.tag == "Virus")
        {
            player.PlayAudio(DamageClip);

            int amount = obj.GetComponent<Virus>().DamagePower;
            this.TakeDamage(amount);//set damage from virus
            
            Destroy(obj);
        }
    }



}
