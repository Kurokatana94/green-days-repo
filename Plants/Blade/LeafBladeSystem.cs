using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafBladeSystem : MonoBehaviour
{
    public LayerMask plantLayers;
    public AudioSource audio;
    public float leafRange;
    private bool hasHit;

    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a < 1) this.enabled = false;

        RangedAttack();
    }

    private void RangedAttack()
    {
        Collider2D[] hitPlant = Physics2D.OverlapCircleAll(transform.position, leafRange, plantLayers);

        foreach (Collider2D plant in hitPlant)
        {
            hasHit = true;

            if (plant.CompareTag("Evil"))
            {
                plant.GetComponent<EvilWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Tulipa"))
            {
                plant.GetComponent<Tulipa>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Bush"))
            {
                plant.GetComponent<Bush>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Green"))
            {
                plant.GetComponent<GreenWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Blade"))
            {
                plant.GetComponent<BladeWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("Gold"))
            {
                plant.GetComponent<GoldenWeed>().GotHit();
                audio.Play();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (transform == null) return;

        Gizmos.DrawWireSphere(transform.position, leafRange);
    }
}
