using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthBarController : MonoBehaviour
{
    public float health = 1.0f;
    public float damage = 0.0f;

    public Transform healthBarFront;
    public Transform healthBarDMG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damage <= 0.0f)
        {
            StopCoroutine(TakeDamage());
        }
    }

    public void SetDamage(float dmg)
    {
        if (health > 0.005)
        {
            damage = dmg;
            StartCoroutine(TakeDamage());
        }
    }

    IEnumerator TakeDamage()
    {
        for (float hp = health; damage >= 0.0f; hp -= 0.01f)
        {
            damage -= 0.01f;
            health = hp;
            healthBarFront.localScale = new Vector2(hp, healthBarFront.localScale.y);
            yield return null;
        }
    }
}
