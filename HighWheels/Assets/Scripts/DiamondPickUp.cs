using UnityEngine;

public class DiamondPickUp : MonoBehaviour, ICollideable
{
    [SerializeField] GameObject diamondPickUpParticle;

    public void ExicuteCollisionActions()
    {
        PlayPartcleEffects();

        EventBroker.CallOnPickUpDiamond();
        Destroy(gameObject);
    }

    private void PlayPartcleEffects()
    {
        Instantiate(diamondPickUpParticle, transform.position, Quaternion.identity);
    }
}
