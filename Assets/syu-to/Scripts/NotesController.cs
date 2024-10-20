using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] private float endX = 0.5f; // �m�[�c�̈ړ����X���W
    [SerializeField] private float tempo = 2.0f; // �m�[�c�̈ړ�����
    [SerializeField] private float Heart_range = 0; // �n�[�g�ɐG���͈�

    private Vector3 startPos; // �m�[�c�̐����ʒu
    private Vector3 endPos; // �m�[�c�̖ړI�n
    private float currentTime = 0f; // �m�[�c�̈ړ��ɂ�����o�ߎ���

    private bool isTouchingHeart = false; // �n�[�g�ɐG��Ă��邩�ǂ����̏��
    public bool IsTouchingHeart => isTouchingHeart; // �v���p�e�B��ǉ�

    private NotesManager notesManager; // NotesManager�̃C���X�^���X
    private bool canPlaySpaceSound = true; // �X�y�[�X�L�[�̉���点�邩�ǂ����̃t���O
    private bool isTouchSoundPlaying = false; // �n�[�g�ɐG�ꂽ�����Đ������ǂ���

    private void Awake()
    {
        startPos = transform.position; // ���݂̈ʒu�𐶐��ʒu�Ƃ��Đݒ�
        endPos = new Vector3(endX, startPos.y, startPos.z); // �m�[�c�̖ړI�n��ݒ�iY��Z�͂��̂܂܁j
        notesManager = FindObjectOfType<NotesManager>(); // NotesManager���擾
    }

    private void Update()
    {
        currentTime += Time.deltaTime; // �o�ߎ��Ԃ��X�V
        // �m�[�c�̈ʒu���ړ�
        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo);

        // �n�[�g�ɐG�ꂽ�ꍇ�̃`�F�b�N
        if (!isTouchingHeart && transform.position.x >= -Heart_range && transform.position.x <= Heart_range)
        {
            OnTouchHeart(); // �n�[�g�ɐG�ꂽ�ƌ��Ȃ�
        }

        // �n�[�g�ɐG��Ă����ԂŃX�y�[�X�L�[�������ꂽ�Ƃ�
        if (isTouchingHeart && Input.GetKeyDown(KeyCode.Space) && canPlaySpaceSound)
        {
            if (isTouchSoundPlaying)
            {
                notesManager.StopTouchSound(); // �n�[�g�ɐG�ꂽ�����~
            }
            notesManager.PlaySpaceSound(); // �X�y�[�X�L�[���������Ƃ��̉���炷
            canPlaySpaceSound = false; // �t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
        }

        // �m�[�c���ړ����I�������폜
        if (currentTime > tempo)
        {
            Destroy(this.gameObject); // �m�[�c���폜
        }
    }

    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            isTouchingHeart = true; // �n�[�g�ɐG�ꂽ���
            notesManager.PlayTouchSound(); // �n�[�g�ɐG�ꂽ�Ƃ��̉���炷
            isTouchSoundPlaying = true; // �n�[�g�̉����Đ����ł��邱�Ƃ��t���O�ɐݒ�
            canPlaySpaceSound = true; // �n�[�g�ɐG�ꂽ��ɃX�y�[�X�L�[�ŉ���点��悤�Ƀt���O�����Z�b�g
        }
    }

    private void OnDestroy()
    {
        // �m�[�c���폜�����Ƃ��A�t���O�����Z�b�g
        canPlaySpaceSound = true;
    }
}