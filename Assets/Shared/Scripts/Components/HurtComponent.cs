using System.Collections;
using UnityEngine;

public class HurtComponent : MonoBehaviour
{
    public HPComponent healthManager;
    public ScoreComponent scoreManager;
    
    public LayerMask dangerousLayers;
    public GameObject meshRenderer;

    public bool immediateDeath = true;

    public bool usesInvincibilityFrames = false;
    public float invincibilitySeconds = 0f;
    public float invincibilitySecondsSet;

    void Update()
    {
        if (usesInvincibilityFrames)
        {
            invincibilitySeconds -= Time.deltaTime;
            if (invincibilitySeconds > 0f)
            {
                meshRenderer.SetActive(!meshRenderer.activeSelf);
            }
            else meshRenderer.SetActive(true);
        }
        else
        {
            if (meshRenderer == null)
            {
                return;
            }
            if (!meshRenderer.activeSelf)
            {
                StartCoroutine(BecomeVisible());
            }
        }
    }

    IEnumerator BecomeVisible()
    {
        yield return new WaitForSeconds(1f/30f);
        meshRenderer?.SetActive(true);
    }
    
    //Needs a trigger collider to be present on the game object.
    void OnTriggerEnter(Collider otherCollider)
    {
        bool otherColliderLayerIsDangerous = (dangerousLayers.value & 1 << otherCollider.gameObject.layer) == 1 << otherCollider.gameObject.layer;
        if (otherColliderLayerIsDangerous && otherCollider.gameObject.TryGetComponent<HitComponent>(out HitComponent hitbox))
        {
            if (healthManager == null)
            {
                return;
            }
            healthManager.currenthealth -= hitbox.hitStrength;
            if (usesInvincibilityFrames) 
            {
                invincibilitySeconds = invincibilitySecondsSet;
            }

            if (scoreManager != null)
            {
                GameManager.gm.score += scoreManager.damagePoints;
            }

            if (healthManager.IsDead())
            {
                if (scoreManager != null)
                {
                    GameManager.gm.score += scoreManager.deathPoints;
                }

                if (immediateDeath)
                {
                    Destroy(gameObject);
                }
            }

            /*if (mr != null && !hasIFrames)
            {
                mr.SetActive(false);
            }*/
        }
    }
}
