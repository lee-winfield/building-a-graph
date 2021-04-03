using UnityEngine;

public class Graph : MonoBehaviour {

    [SerializeField]
    Transform pointPrefab = default;

    void Awake () {
        Instantiate(pointPrefab);
    }
}
