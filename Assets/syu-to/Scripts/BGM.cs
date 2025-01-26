using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : MonoBehaviour
{
    private AudioSource audioSource; // BGM用AudioSource
    [SerializeField] private int initialBPM = 120; // BGMの元のBPM
    private int currentBPM; // 現在のBPM

    private void Awake()
    {
        // AudioSourceを取得
        audioSource = GetComponent<AudioSource>();
        currentBPM = initialBPM; // 初期BPMをセット
    }

    private void Start()
    {
        // TempoManagerからBPMを同期
        TempoManager tempoManager = FindObjectOfType<TempoManager>();
        if (tempoManager != null)
        {
            UpdatePlaybackSpeed(tempoManager.BPM);
        }
    }

    // TempoManagerからBPMが変更されたらこのメソッドを呼ぶ
    public void UpdatePlaybackSpeed(int newBPM)
    {
        currentBPM = newBPM;
        // 再生速度を調整 (ピッチを変えるが擬似的にBPM制御)
        audioSource.pitch = (float)currentBPM / initialBPM;
    }
}
