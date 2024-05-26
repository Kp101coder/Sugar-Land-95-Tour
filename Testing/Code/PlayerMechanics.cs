using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMechanics : MonoBehaviour
{
    public EntityData data;
    public EntityMovement move;
    public AudioClip deathMusic;
    public AudioClip escapeTrack;
    private Animator animatorRight;
    private Animator animatorLeft;
    public GameObject leftArm;
    private float timeLeftHand = 0;
    private float timeRightHand = 0;
    private ArrayList weapons;
    private int curWeaponIndex;
    public TextMeshProUGUI toolTip;
    private TextMeshProUGUI healthCount;
    private TextMeshProUGUI ammoCount;
    public GameObject UI;
    private int clip = -1;
    private float clipStart = 0;
    private AudioClip[] clips;
    private bool start = false;
    private GameObject[] enemies;
    private float lastTip = 0;
    public float tipSpeed;
    private string[] tips;
    private int tipCount = 0;
    private bool canDie;
    // Start is called before the first frame update
    void Start()
    {
        canDie = true;
        animatorRight = transform.Find("Camera").Find("toyGuyRight").GetComponent<Animator>();
        animatorLeft = transform.Find("Camera").Find("toyGuyLeft").GetComponent<Animator>();
        data = GetComponent<EntityData>();
        move = GetComponent<EntityMovement>();
        curWeaponIndex = 0;
        UI = GameObject.Find("GUI");
        toolTip = UI.transform.Find("ToolTip").GetComponent<TextMeshProUGUI>();
        healthCount = UI.transform.Find("Health").GetComponent<TextMeshProUGUI>();
        ammoCount = UI.transform.Find("Ammo").GetComponent<TextMeshProUGUI>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        UI.transform.Find("Loose").gameObject.SetActive(false);
        UI.transform.Find("Win").gameObject.SetActive(false);
        foreach (GameObject enemy in enemies)
        {
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthCount.SetText(data.health.ToString());
        if(data.health == 0)
        {
           
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            //punch object
            //glory kills
            leftArm.SetActive(true);
            animatorLeft.SetTrigger("OnPunch");
            timeLeftHand = Time.time;
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            //changing weapons
            animatorRight.SetTrigger("OnSwap");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            //Dash Animation
            leftArm.SetActive(true);
            animatorLeft.SetTrigger("OnDash");
            timeLeftHand = Time.time;
        }
        if (leftArm.activeSelf && Time.time - timeLeftHand > animatorLeft.GetCurrentAnimatorStateInfo(0).length+0.1 && !animatorLeft.IsInTransition(0))
        {
            leftArm.SetActive(false);
        }
        if (timeRightHand != 0 && Time.time - timeRightHand > animatorRight.GetCurrentAnimatorStateInfo(0).length + 0.1 && !animatorRight.IsInTransition(0))
        {
            ((GameObject)weapons[curWeaponIndex]).SetActive(true);
            timeRightHand = 0;
        }
    }
    public void nextTrack()
    {

    }

    void OnTriggerStay(Collider collision)
    {
        
    }


    void OnTriggerEnter(Collider collision)
    {

    }

    void OnTriggerExit(Collider collision)
    {

    }
}
