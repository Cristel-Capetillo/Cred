using UnityEngine;

namespace Core {
    public class ModelSpawner : MonoBehaviour {
        Transform spawnLocation;
        SelectedClient selectedClient;

        void Awake() {
            spawnLocation = GameObject.Find("ModelSpawnLocation").transform;
            selectedClient = FindObjectOfType<SelectedClient>();
        }

        void Start() {
            if (selectedClient == null) return;
            Instantiate(selectedClient.client.model, spawnLocation);
        }
    }
}
