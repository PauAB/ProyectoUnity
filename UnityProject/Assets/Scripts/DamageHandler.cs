using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour {

    private List<int> burnTickTimers = new List<int>();
    private HpBar hpBarScript;

    bool alive;

	void Start ()
    {
        hpBarScript = GetComponent<HpBar>();

        if (hpBarScript.currentHp > 0)
            alive = true;
	}

    void Update()
    {
        if (hpBarScript.currentHp <= 0)
            alive = false;

        if (!alive)
            gameObject.SetActive(false);
    }

    public void ApplyBurn(int ticks, float damagePerSecond)
    {       
        if (burnTickTimers.Count <= 0)
        {
            burnTickTimers.Add(ticks);
            StartCoroutine(Burn(damagePerSecond));
        }
        else
            burnTickTimers.Add(ticks);
    }

    IEnumerator Burn(float damage)
    {
        while (burnTickTimers.Count > 0)
        {
            for (int i = 0; i < burnTickTimers.Count; i++)
            {
                burnTickTimers[i]--;
            }

            hpBarScript.currentHp -= damage;
            hpBarScript.SetHpBar();

            burnTickTimers.RemoveAll(i => i == 0);

            yield return new WaitForSeconds(1f);
        }
    }
}
