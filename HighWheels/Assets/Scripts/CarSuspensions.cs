using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CarSuspensions : MonoBehaviour
{
    [SerializeField] private GameObject suspensionPrefab;
    [SerializeField] private GameObject carBody;
    [SerializeField] private Transform suspensionsParent;
    [SerializeField] private Transform wheelsParent;

    [SerializeField] float yIncreaseAmount;

    private Queue<GameObject> suspensions;

    #region Subscribe and Unsubscribe to collision events

    private void OnEnable()
    {
        EventBroker.OnPickUpSuspension += GainSuspension;
        EventBroker.OnHitToBlock += LoseSuspension;
    }

    private void OnDisable()
    {
        EventBroker.OnPickUpSuspension -= GainSuspension;
        EventBroker.OnHitToBlock -= LoseSuspension;
    }

    #endregion

    private void Awake()
    {
        suspensions = new Queue<GameObject>();
    }

    private void GainSuspension()
    {
        // Adjust Y position value of carBody and suspensions on the car

        if (suspensions.Count == 0)
        {
            carBody.transform.position += new Vector3(0f, yIncreaseAmount + 0.14f, 0f);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.AddComponent<SuspensionRayCheck>();

            suspensions.Enqueue(newSuspension);
        }
        else
        {
            carBody.transform.position += new Vector3(0f, yIncreaseAmount, 0f);

            GameObject newSuspension = Instantiate(suspensionPrefab, suspensionsParent);

            newSuspension.transform.localPosition += new Vector3(0f, suspensions.Last().transform.localPosition.y + yIncreaseAmount, 0f);

            suspensions.Enqueue(newSuspension);
        }
    }

    private void LoseSuspension(int _loseAmount)
    {
        for (int i = 0; i < _loseAmount; i++)
        {
            GameObject suspensionToDelete = suspensions.Peek();

            suspensions.Dequeue();

            Destroy(suspensionToDelete);

            wheelsParent.transform.position = suspensions.Peek().transform.position - new Vector3(0f, 0.26f, 0f);

            if (suspensions.Count == 0)
            {
                wheelsParent.transform.position = Vector3.zero;
            }
        }
    }
}
