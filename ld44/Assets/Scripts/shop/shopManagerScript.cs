using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopManagerScript : MonoBehaviour
{
    public GameObject cam;

    public GameObject walkspeed;
    public GameObject dashSpeed;
    public GameObject jumpforce;
    public GameObject jumpMax;
    public GameObject dashTime;
    public GameObject dashMax;
    public GameObject shopHighlight;
    public GameObject Player;
    public GameObject pubble;

    public bool shopping;

    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;

    

    void Start()
    {

        //REMOVE THESE
        PlayerPrefs.SetInt("1", 1);
        PlayerPrefs.SetInt("2", 1);
        PlayerPrefs.SetInt("3", 1);
        PlayerPrefs.SetInt("4", 1);
        PlayerPrefs.SetInt("5", 1);
        PlayerPrefs.SetInt("6", 1);

        deleteBoughtItems();
    }

    void deleteBoughtItems()
    {
       if(PlayerPrefs.GetInt("1") == 0)
        {
            walkspeed.SetActive(false);
        }
        if (PlayerPrefs.GetInt("2") == 0)
        {
            dashSpeed.SetActive(false);
        }
        if (PlayerPrefs.GetInt("3") == 0)
        {
            jumpforce.SetActive(false);
        }
        if (PlayerPrefs.GetInt("4") == 0)
        {
            jumpMax.SetActive(false);
        }
        if (PlayerPrefs.GetInt("5") == 0)
        {
            dashTime.SetActive(false);
        }
        if (PlayerPrefs.GetInt("6") == 0)
        {
            dashMax.SetActive(false);
        }
    }
    void Update()
    {
       // Transform tempTarget = Player.transform;
        if (Input.GetKeyDown(KeyCode.Space) && pubble.GetComponent<detection>().detected)
        {
            shopping = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            shopping = false;
        }

        if(shopping)
        {
            Player.GetComponent<playerController>().freeze = true;
            shopHighlight.SetActive(true);
            management();
            cam.GetComponent<Animator>().SetBool("zoom", true);
        }
        else
        {
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
        buy();
    }
    void buy()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            int price = getprice(index);
            // if price is under current money
            PlayerPrefs.SetInt(index.ToString(), 0);
            deleteBoughtItems();
        }
    }
    int getprice(int i)
    {
        int p;
        switch (i)
        {
            case 1:
                p = 0;
                break;

            case 2:
                p = 0;
                break;

            case 3:
                p = 0;
                break;

            case 4:
                p = 0;
                break;

            case 5:
                p = 0;
                break;

            case 6:
                p = 0;
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
