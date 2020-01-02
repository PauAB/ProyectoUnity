using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour {

    public Image hpBar;

    public float totalHp = 100f;
    public float currentHp;
	
	void Start ()
    {
        currentHp = totalHp;
	}
	
    public void SetHpBar()
    {       
        float hp = currentHp / totalHp;
        hpBar.transform.localScale = new Vector3(Mathf.Clamp(hp, 0f, 1f), hpBar.transform.localScale.y, hpBar.transform.localScale.z);
    }
}
