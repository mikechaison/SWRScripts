using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public GameObject modelToDisable;
    public MeshCollider colliderToDisable;
    public GameObject modelToEnable;
    public MeshCollider colliderToEnable;
    public GameObject player;
    public CarController playerController;
    public LapController lapController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.gameObject == player)
        {
            modelToDisable.SetActive(false);
            colliderToDisable.enabled = false;
            modelToEnable.SetActive(true);
            colliderToEnable.enabled = true;

            playerController.teleportPosition = modelToDisable.transform.position;
            playerController.teleportPosition.y = -1;
            Vector3 currentEuler = modelToDisable.transform.rotation.eulerAngles;
            Vector3 rotatedEuler = new Vector3(0, currentEuler.y+180, 0);
            playerController.teleportRotation = Quaternion.Euler(rotatedEuler);

            lapController.UpdateCheckpoint();
        }
    }

}
