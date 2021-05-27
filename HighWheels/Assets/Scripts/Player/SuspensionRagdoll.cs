using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SuspensionRagdoll : MonoBehaviour
{
    [SerializeField] float breakForce;

    List<Transform> suspensionChilds;
    List<Collider> suspensionColliders;

    List<GameObject> frontSuspensions;
    public List<GameObject> rearSuspensions;

    Vector3 randomForceDirection;

    private void OnEnable()
    {
        EventBroker.OnGameOver += ActivateRagdoll;
    }

    private void OnDisable()
    {
        EventBroker.OnGameOver -= ActivateRagdoll;
    }

    private void Awake()
    {
        InitializeLists();
    }

    public void ActivateRagdoll()
    {
        transform.parent = null;

        
        suspensionChilds.ForEach(x => x.gameObject.AddComponent<Rigidbody>());

        List<Rigidbody> suspensionRigidbodies = transform.GetComponentsInChildren<Rigidbody>().ToList();

        suspensionColliders.ForEach(x => x.enabled = true);

        suspensionRigidbodies.ForEach(x => x.AddForce(GetRandomForceDirection() * breakForce));

        Destroy(gameObject, 5f);
    }

    private void InitializeLists()
    {
        suspensionChilds = transform.GetComponentsInChildren<Transform>().ToList();
        suspensionColliders = transform.GetComponentsInChildren<Collider>().ToList();

        frontSuspensions = new List<GameObject>();
        rearSuspensions = new List<GameObject>();

        foreach (var suspension in suspensionChilds)
        {
            if (suspension.transform.localPosition.z > 0)
            {
                frontSuspensions.Add(suspension.gameObject);
            }
            else if (suspension.transform.localPosition.z < 0)
            {
                rearSuspensions.Add(suspension.gameObject);
            }
        }
    }

    public void BreakFrontSuspensions()
    {
        frontSuspensions.ForEach(x => x.gameObject.AddComponent<Rigidbody>());
        frontSuspensions.ForEach(x => x.GetComponent<Collider>().enabled = true);
        frontSuspensions.ForEach(x => x.GetComponent<Rigidbody>().AddForce(GetRandomForceDirection() * breakForce));
    }

    private Vector3 GetRandomForceDirection()
    {
        randomForceDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(0f, -1f));

        return randomForceDirection;
    }
}
