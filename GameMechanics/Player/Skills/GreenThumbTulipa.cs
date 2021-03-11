using UnityEngine;
using Random = UnityEngine.Random;

public class GreenThumbTulipa : MonoBehaviour
{
    //General variables
    [Header("References")]
    public float slashRange;
    public Transform slashPoint;
    public Animator animator;
    public LayerMask plantLayers;
    public float slashRate = 3f;
    private float nextSlashTime;
    public AudioSource audio;
    public GameObject icon;

    //Bool used to check if reset timer in cutter run mode
    [HideInInspector]
    public bool hasHit;

    private void Start()
    {
        icon.SetActive(true);
    }

    private void Update()
    {
        if (Time.time >= nextSlashTime)
        {
            if (Input.GetButtonDown("Slash"))
            {
                Slash();
                nextSlashTime = Time.time + 1f / slashRate;
            }
        }
    }

    //Function that will check the area around the slashpoint to find targets and if found any, apply the damage
    private void Slash()
    {
        animator.SetTrigger("IsSlashing");

        Collider2D[] hitPlant = Physics2D.OverlapCircleAll(slashPoint.position, slashRange, plantLayers);

        foreach(Collider2D plant in hitPlant)
        {
            hasHit = true;

            if (plant.CompareTag("Evil"))
            {
                plant.GetComponent<EvilWeed>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Tulipa"))
            {
                int n = Random.Range(1, 100);
                if (n < 50)
                {
                    plant.GetComponent<RedTulipa>().GotHit();
                    audio.Play();
                }
            }else if (plant.CompareTag("Bush"))
            {
                plant.GetComponent<Bush>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Green"))
            {
                plant.GetComponent<GreenWeed>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Blade"))
            {
                plant.GetComponent<BladeWeed>().GotHit();
                audio.Play();
            }else if (plant.CompareTag("Gold"))
            {
                plant.GetComponent<GoldenWeed>().GotHit();
                audio.Play();
            }
            else if (plant.CompareTag("SpecialTulipa"))
            {
                int n = Random.Range(1, 100);
                if (n < 50)
                {
                    plant.GetComponent<BlueTulipa>().GotHit();
                    audio.Play();
                }
            }
        }
    }

    //Small funciotn used to check the slash area in edit mode
    private void OnDrawGizmosSelected()
    {
        if (slashPoint == null) return;

        Gizmos.DrawWireSphere(slashPoint.position, slashRange);
    }
}
