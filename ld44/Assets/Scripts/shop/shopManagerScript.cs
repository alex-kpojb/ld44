using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class shopManagerScript : MonoBehaviour
{
    public GameObject cam;
    public BonusSO BS;
    public GameStateSO GSSO;
    public GameObject walkspeed;
    public GameObject dashSpeed;
    public GameObject jumpforce;
    public GameObject jumpMax;
    public GameObject dashTime;
    public GameObject dashMax;
    public GameObject shopHighlight;
    public GameObject Player;
    public GameObject pubble;
    public TextMeshProUGUI item_name;
    public TextMeshProUGUI item_price;
    public TextMeshProUGUI item_description;
    public string walkspeed_description;
    public string dashspeed_description;
    public string jumpforce_description;
    public string jumpMax_description;
    public string dashTime_description;
    public string dashMax_description;

    public string walkspeed_name;
    public string dashspeed_name;
    public string jumpforce_name;
    public string jumpMax_name;
    public string dashTime_name;
    public string dashMax_name;

    public bool shopping;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    public AudioClip buyAudio;
    public AudioClip not_enough_money;
    public List<int> boughtItems;

    void Start()
    {

        //REMOVE THESE ON BUILD
        PlayerPrefs.SetInt("1", 1);
        PlayerPrefs.SetInt("2", 1);
        PlayerPrefs.SetInt("3", 1);
        PlayerPrefs.SetInt("4", 1);
        PlayerPrefs.SetInt("5", 1);
        PlayerPrefs.SetInt("6", 1);
        boughtItems.Add(7);
        deleteBoughtItems();
    }

    void deleteBoughtItems()
    {
        Debug.Log(index);
       if(PlayerPrefs.GetInt("1") == 0)
        {
            walkspeed.SetActive(false);
            boughtItems.Add(1);
        }
        if (PlayerPrefs.GetInt("2") == 0)
        {
            dashSpeed.SetActive(false);
            boughtItems.Add(2);
        }
        if (PlayerPrefs.GetInt("3") == 0)
        {
            jumpforce.SetActive(false);
            boughtItems.Add(3);
        }
        if (PlayerPrefs.GetInt("4") == 0)
        {
            jumpMax.SetActive(false);
            boughtItems.Add(4); 
        }
        if (PlayerPrefs.GetInt("5") == 0)
        {
            dashTime.SetActive(false);
            boughtItems.Add(5);
        }
        if (PlayerPrefs.GetInt("6") == 0)
        {
            dashMax.SetActive(false);
            boughtItems.Add(6);
        }
    }
    void Update()
    {
        GameObject.Find("Menumanager").GetComponent<PauseManager>().inshop = shopping;
        if(cooldown)
        {
            cooldown = false;
        }
       // Transform tempTarget = Player.transform;
        if (Input.GetKeyDown(KeyCode.Space) && pubble.GetComponent<detection>().detected && !shopping)
        {
            shopping = true;
            cooldown = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shopping = false;         
        }

        if(shopping)
        {
            item_description.gameObject.SetActive(true);
            item_name.gameObject.SetActive(true);
            item_price.gameObject.SetActive(true);
            Player.GetComponent<playerController>().freeze = true;
            shopHighlight.SetActive(true);
            management();
            cam.GetComponent<Animator>().SetBool("zoom", true);
        }
        else
        {
            item_description.gameObject.SetActive(false);
            item_name.gameObject.SetActive(false);
            item_price.gameObject.SetActive(false);
            Player.GetComponent<playerController>().freeze = false;
            cam.GetComponent<Animator>().SetBool("zoom", false);
            shopHighlight.SetActive(false);
            target.position = Player.transform.position;
        }

        MoveCamera();
    }
    public int index = 1; //1,2,3 bottom row. 4,5,6 upper row
    void management()
    {
        shopHighlight.transform.position = target.position = GetTarget(index);
       // Debug.Log(target.position);
        moveInShop();
        UI_manager();
        buy();
    }

    bool cooldown;

    void UI_manager()
    {
        switch(index)
        {
            case 1:
                item_description.text = walkspeed_description;
                item_name.text = walkspeed_name;
                item_price.text = BS.Bonuses[2].price.ToString();
                break;

            case 2:
                item_description.text = dashspeed_description;
                item_name.text = dashspeed_name;
                item_price.text = BS.Bonuses[5].price.ToString();
                break;

            case 3:
                item_description.text = jumpforce_description;
                item_name.text = jumpforce_name;
                item_price.text = BS.Bonuses[0].price.ToString();
                break;

            case 4:
                item_description.text = jumpMax_description;
                item_name.text = jumpMax_name;
                item_price.text = BS.Bonuses[1].price.ToString();
                break;

            case 5:
                item_description.text = dashTime_description;
                item_name.text = dashTime_name;
                item_price.text = BS.Bonuses[4].price.ToString();
                break;

            case 6:
                item_description.text = dashMax_description;
                item_name.text = dashMax_name;
                item_price.text = BS.Bonuses[3].price.ToString();
                break;

            default:
                item_description.text = walkspeed_description;
                break;
        }
    }
    
    void buy()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !cooldown)
        {
            int price = getprice(index);
            //float price = GSSO.chestPrice;
            // if price is under current money
            if (boughtItems.Contains(index))
            {
                return;
            }
            if(price <= GSSO.moneyCurrent)
            {
               // GameObject.Find("AudioController").GetComponent<AudioSource>().PlayOneShot(buyAudio);
                GSSO.moneyCurrent -= price;
                PlayerPrefs.SetInt(index.ToString(), 0);
                deleteBoughtItems();
                //APPLY BOOST

                string name = getname(index);
                int k = getSOindex(index);
                GSSO.ApplyBonus(name, BS.Bonuses[k].value);
            }
            else
            {
             //   GameObject.Find("AudioController").GetComponent<AudioSource>().PlayOneShot(not_enough_money);
                //Playsound
            }
        }
    }
    int getSOindex(int i)
    {
        int p = 0;
        switch (i)
        {
            case 1:
                p = 2;
                break;

            case 2:
                p = 5;
                break;

            case 3:
                p = 0;
                break;

            case 4:
                i = 3;
                break;

            case 5:
                p = 4;
                break;

            case 6:
                p = 1;
                break;

            default:
                p = 2;
                break;
        }
        return p;
    }
    string getname(int i)
    {
        string p;
        switch (i)
        {
            case 1:
                p = BS.Bonuses[2].name;
                break;

            case 2:
                p = BS.Bonuses[5].name;
                break;

            case 3:
                p = BS.Bonuses[0].name;
                break;

            case 4:
                p = BS.Bonuses[1].name;
                break;

            case 5:
                p = BS.Bonuses[4].name;
                break;

            case 6:
                p = BS.Bonuses[3].name;
                break;

            default:
                p = BS.Bonuses[2].name;
                break;
        }
        return p;
    }
    int getprice(int i)
    {
        int p;
        switch (i)
        {
            case 1:
                p = BS.Bonuses[2].price;
                break;

            case 2:
                p = BS.Bonuses[5].price;
                break;

            case 3:
                p = BS.Bonuses[0].price;
                break;

            case 4:
                p = BS.Bonuses[1].price;
                break;

            case 5:
                p = BS.Bonuses[4].price;
                break;

            case 6:
                p = BS.Bonuses[3].price;
                break;

            default:
                p = 0;
                break;
        }
        return p;
    }
    void moveInShop()
    {
        if(Input.GetKeyDown(KeyCode.A) && index != 1)
        {
            if(index == 4)
            {
                index = 3;
                return;
            }
            else
            {
                index--;
            }
        }
        if(Input.GetKeyDown(KeyCode.D) && index != 6)
        {
            if (index == 3)
            {
                index = 4;
                return;
            }
            else
            {
                index++;
            }
        }  
        if(Input.GetKeyDown(KeyCode.W) && index !=4 && index != 5 && index != 6) 
        {
            index += 3;
        }
        if (Input.GetKeyDown(KeyCode.S) && index != 1 && index != 2 && index != 3)
        {
            index -= 3;
        }
    }
    Vector2 GetTarget(int i)
    {
        Vector2 pos;
        switch(i)
        {
            case 1:
                pos = walkspeed.transform.position;
                break;

            case 2:
                pos = dashSpeed.transform.position;
                break;

            case 3:
                pos = jumpforce.transform.position;
                break;

            case 4:
                pos = jumpMax.transform.position;
                break;

            case 5:
                pos = dashTime.transform.position;
                break;

            case 6:
                pos = dashMax.transform.position;
                break;

            default:
                pos = dashSpeed.transform.position;
                break;
        }
        return pos;
    }

    void MoveCamera()
    {
            Vector3 point = cam.GetComponent<UnityEngine.Camera>().WorldToViewportPoint(new Vector3(target.position.x,target.position.y,-10));
            Vector3 delta = new Vector3(target.position.x, target.position.y, -10) - cam.GetComponent<UnityEngine.Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = cam.transform.position + delta;
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, destination, ref velocity, dampTime);
    }
}
