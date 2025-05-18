using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;

public class CheckpointContoller : MonoBehaviour
{
    [Serializable]
    public struct Checkpoint
    {
        public GameObject checkpointModel;
        public MeshCollider checkpointCollider;
    }

    public List<Checkpoint> checkpoints;
    public GameObject player;
    public TextMeshProUGUI lapText;

    void Start()
    {
        for (int i=0; i<checkpoints.Count; i++)
        {
            if (i!=1)
            {
                checkpoints[i].checkpointModel.SetActive(false);
                checkpoints[i].checkpointCollider.enabled = false;
            }

            Checkpoint checkpoint = checkpoints[i];
            Checkpoint next_checkpoint;
            if (i==checkpoints.Count-1)
            {
                next_checkpoint = checkpoints[0];
            }
            else
            {
                next_checkpoint = checkpoints[i+1];
            }

            CheckpointTrigger trigger = checkpoint.checkpointCollider.gameObject.AddComponent<CheckpointTrigger>();
            trigger.player = player;
            trigger.playerController = player.GetComponent<CarController>();
            trigger.lapController = lapText.GetComponent<LapController>();
            trigger.modelToDisable = checkpoint.checkpointModel;
            trigger.colliderToDisable = checkpoint.checkpointCollider;
            trigger.modelToEnable = next_checkpoint.checkpointModel;
            trigger.colliderToEnable = next_checkpoint.checkpointCollider;

            checkpoint.checkpointCollider.isTrigger = true;            
        }
    }
}
