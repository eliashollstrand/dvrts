using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DartSpawner : MonoBehaviour
{
    public GameObject dartPrefab;
    public Transform spawnPoint;

    private GameObject currentDart;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    private void Start()
    {
        SpawnNewDart();
    }

    private void SpawnNewDart()
    {
        currentDart = Instantiate(dartPrefab, spawnPoint.position, spawnPoint.rotation);

        grab = currentDart.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        grab.selectEntered.AddListener(OnDartGrabbed);
    }

    private void OnDartGrabbed(SelectEnterEventArgs args)
    {
        grab.selectEntered.RemoveListener(OnDartGrabbed);
        if(GameManager.Instance.newTurnPending) 
            GameManager.Instance.StartNewTurn();
        SpawnNewDart();
    }
}
