using UnityEngine;

public class DiamondPickUp : MonoBehaviour, ICollideable
{
    [SerializeField] GameObject diamondPickUpParticle;

    public void ExicuteCollisionActions()
    {
        Debug.Log("PickUp diamond");
        PlayPartcleEffects();

        EventBroker.CallOnPickUpDiamond();
        Destroy(gameObject);
    }

    private void PlayPartcleEffects()
    {
        GameObject particleEffect = Instantiate(diamondPickUpParticle, transform.position, Quaternion.identity);

        Destroy(particleEffect, 2f);
    }
}
