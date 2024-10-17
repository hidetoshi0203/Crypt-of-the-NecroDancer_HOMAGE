using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] private float endX = 0.5f; // �m�[�c�̈ړ����X���W
    [SerializeField] private float tempo = 2.0f; // �m�[�c�̈ړ�����

    private Vector3 startPos; // �m�[�c�̐����ʒu
    private Vector3 endPos; // �m�[�c�̖ړI�n
    private float currentTime = 0f; // �m�[�c�̈ړ��ɂ�����o�ߎ���

    private bool isTouchingHeart = false; // �n�[�g�ɐG��Ă��邩�ǂ����̏��
    public bool IsTouchingHeart => isTouchingHeart; // �v���p�e�B��ǉ�

    private NotesManager notesManager; // NotesManager�̃C���X�^���X
    private bool canPlaySpaceSound = true; // �X�y�[�X�L�[�̉���点�邩�ǂ����̃t���O

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
        if (!isTouchingHeart && transform.position.x >= -1f && transform.position.x <= 1f)
        {
            OnTouchHeart(); // �n�[�g�ɐG�ꂽ�ƌ��Ȃ�
        }

        if (isTouchingHeart)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canPlaySpaceSound)
            {
                notesManager.PlaySpaceSound(); // ����炷
                canPlaySpaceSound = false; // �t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
            }
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
        }
    }

    private void OnDestroy()
    {
        // �m�[�c���폜�����Ƃ��A�t���O�����Z�b�g
        canPlaySpaceSound = true;
    }
}