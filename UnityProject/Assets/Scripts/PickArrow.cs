using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickArrow : MonoBehaviour {

    public GameObject arrowPrefab;
    public Image keyBackground;
    public Text keyText;

    private float respawnCd = 5f;
    private float currentCd;

    GameObject arrow;
    Vector3 arrowPosition;
    AudioSource audioSource;
    Color newBgColor;
    Color newTextColor;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();

        newBgColor = keyBackground.color;
        newTextColor = keyText.color;
        currentCd = respawnCd;

        arrowPosition = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);

        Instantiate(arrowPrefab, arrowPosition, Quaternion.Euler(-90f, 0f, 0f), transform);

        if (GetComponentInChildren<Rotate>().gameObject != null)
            arrow = GetComponentInChildren<Rotate>().gameObject;

        newBgColor.a = 0f;
        newTextColor.a = 0f;        
        keyBackground.color = newBgColor;
        keyText.color = newTextColor;
    }
		
	void Update ()
    {        
        if (arrow == null)
        {
            currentCd -= Time.deltaTime;
            newBgColor.a = 0f;
            newTextColor.a = 0f;
            keyBackground.color = newBgColor;
            keyText.color = newTextColor;
        }

        if (currentCd <= 0)
        {
            Instantiate(arrowPrefab, arrowPosition, Quaternion.Euler(-90f, 0f, 0f), transform);

            if (GetComponentInChildren<Rotate>().gameObject != null)
                arrow = GetComponentInChildren<Rotate>().gameObject;

            currentCd = respawnCd;
        }
	}

    void OnTriggerEnter(Collider other)
    {                
        if (other.gameObject.tag == "Player")
        {
            if (arrow != null)
            {
                newBgColor.a = 1f;
                newTextColor.a = 1f;
                keyBackground.color = newBgColor;
                keyText.color = newTextColor;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            newBgColor.a = 0f;
            newTextColor.a = 0f;
            keyBackground.color = newBgColor;
            keyText.color = newTextColor;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (arrow != null)
            {
                newBgColor.a = 1f;
                newTextColor.a = 1f;
                keyBackground.color = newBgColor;
                keyText.color = newTextColor;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                int amount = 0;

                if (other.GetComponent<PlayerManager>() != null)
                {
                    if (arrow != null)
                    {
                        if (arrow.name == "ArrowHookPick(Clone)")
                            amount = 2;
                        else if (arrow.name == "ArrowFirePick(Clone)")
                            amount = 3;
                        else if (arrow.name == "ArrowSteelPick(Clone)")
                            amount = 5;

                        other.GetComponent<PlayerManager>().PickArrow(arrow.name, amount);

                        audioSource.Play();
                    }
                }

                Destroy(arrow);
            }
        }                    
    }   
}
