using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : MonoBehaviour
{
    private AudioSource audioSource; // BGM�pAudioSource
    [SerializeField] private int initialBPM = 120; // BGM�̌���BPM
    private int currentBPM; // ���݂�BPM

    private void Awake()
    {
        // AudioSource���擾
        audioSource = GetComponent<AudioSource>();
        currentBPM = initialBPM; // ����BPM���Z�b�g
    }

    private void Start()
    {
        // TempoManager����BPM�𓯊�
        TempoManager tempoManager = FindObjectOfType<TempoManager>();
        if (tempoManager != null)
        {
            UpdatePlaybackSpeed(tempoManager.BPM);
        }
    }

    // TempoManager����BPM���ύX���ꂽ�炱�̃��\�b�h���Ă�
    public void UpdatePlaybackSpeed(int newBPM)
    {
        currentBPM = newBPM;
        // �Đ����x�𒲐� (�s�b�`��ς��邪�[���I��BPM����)
        audioSource.pitch = (float)currentBPM / initialBPM;
    }
}
