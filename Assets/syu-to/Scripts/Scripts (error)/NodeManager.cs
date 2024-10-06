using UnityEngine;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private TempoManager tempoManager;
    [SerializeField] private GameObject leftNode;
    [SerializeField] private Transform leftGenerateTrans;
    [SerializeField] private GameObject rightNode;
    [SerializeField] private Transform rightGenerateTrans;

    private float nextGenerateTime = 1f;
    private float generateTime = 1f;
    private void Awake()
    {
        generateTime = tempoManager.Tempo;
        nextGenerateTime = Time.time + generateTime;
    }
    private void Update()
    {
        if (Time.time > nextGenerateTime)
        {
            Debug.Log(Time.time);
            Instantiate(leftNode, leftGenerateTrans.position, Quaternion.identity, this.transform);
            Instantiate(rightNode, rightGenerateTrans.position, Quaternion.identity, this.transform);
            nextGenerateTime += generateTime;
        }
    }

}
