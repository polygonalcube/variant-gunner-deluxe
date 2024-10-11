using System.Collections;
using UnityEngine;

public class HurtComponent : MonoBehaviour
{
    public HPComponent healthManager;
    public ScoreComponent scoreManager;
    
    public LayerMask layers;
    public GameObject mr;

    public int damageOverride;
    public bool immediateDeath = true;

    public bool hasIFrames = false;
    public float iSeconds = 0f;
    public float iSecondsSet;

    void Update()
    {
        if (hasIFrames)
        {
            iSeconds -= Time.deltaTime;
            if (iSeconds > 0f)
            {
                if (mr.activeSelf) mr.SetActive(false);
                else mr.SetActive(true);
            }
            else mr.SetActive(true);
        }
        else
        {
            if (mr != null)
            {
                if (!mr.activeSelf) StartCoroutine(BecomeVisible());
            }
        }
    }

    IEnumerator BecomeVisible()
    {
        yield return new WaitForSeconds(1f/30f);
        mr.SetActive(true);
    }
    
    //Needs a trigger collider to be present on the game object.
    void OnTriggerEnter(Collider col)
    {
        if((layers.value & 1<<col.gameObject.layer) == 1<<col.gameObject.layer)
        {
            //DamageComponent hitbox = col.gameObject.TryGetComponent<DamageComponent>(out DamageComponent hitbox);
            if (healthManager != null)
            {
                if (hasIFrames && iSeconds > 0f)
                {
                    //Do nothing
                }
                else if (damageOverride != 0)
                {
                    healthManager.currenthealth -= damageOverride;
                    if (hasIFrames) iSeconds = iSecondsSet;
                }
                else if (col.gameObject.TryGetComponent<HitComponent>(out HitComponent hitbox))
                {
                    healthManager.currenthealth -= hitbox.hitStrength;
                    if (hasIFrames) iSeconds = iSecondsSet;
                }

                if (scoreManager != null) GameManager.gm.score += scoreManager.damagePoints;
                
                if (healthManager.currenthealth <= 0)
                {
                    if (scoreManager != null) GameManager.gm.score += scoreManager.deathPoints;
                    if (immediateDeath) Destroy(this.gameObject);
                }

                if (mr != null && !hasIFrames) mr.SetActive(false);
            }
        }
    }
}
