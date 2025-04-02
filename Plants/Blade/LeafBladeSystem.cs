using UnityEngine;

public class LeafBladeSystem : MonoBehaviour
{
    public LayerMask plantLayers;
    public AudioSource audio;
    public float leafRange;
    [HideInInspector]
    public bool greenThumbActive = false;
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
                if (greenThumbActive)
                {
                    int n = Random.Range(1, 100);
                    if (n < 50)
                    {
                        plant.GetComponent<RedTulipa>().GotHit();
                        audio.Play();
                    }
                }
                else
                {
                    plant.GetComponent<BlueTulipa>().GotHit();
                    audio.Play();
                }
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
            else if (plant.CompareTag("SpecialTulipa"))
            {
                if (greenThumbActive)
                {
                    int n = Random.Range(1, 100);
                    if (n < 50)
                    {
                        plant.GetComponent<RedTulipa>().GotHit();
                        audio.Play();
                    }
                }
                else
                {
                    plant.GetComponent<BlueTulipa>().GotHit();
                    audio.Play();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (transform == null) return;

        Gizmos.DrawWireSphere(transform.position, leafRange);
    }
}
