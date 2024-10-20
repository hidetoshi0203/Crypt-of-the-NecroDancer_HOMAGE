using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    [SerializeField] private float endX = 0.5f; //�m�[�c�̈ړ����X���W
    [SerializeField] private float tempo = 2.0f; //�m�[�c�̈ړ�����
    [SerializeField] private float Heart_range = 0; //�n�[�g�ɐG���͈�

    private Vector3 startPos; //�m�[�c�̐����ʒu
    private Vector3 endPos; //�m�[�c�̏I���ʒu
    private float currentTime = 0f; //�m�[�c�̈ړ��̌o�ߎ���

    private bool isTouchingHeart = false; //�n�[�g�ɐG��Ă��邩�ǂ���
    public bool IsTouchingHeart => isTouchingHeart;

    private NotesManager notesManager; //NotesManager�̃C���X�^���X
    private bool canPlaySpaceSound = true; //�X�y�[�X�L�[�̉���点�邩�ǂ���
    private bool isTouchSoundPlaying = false; //�n�[�g�ɐG�ꂽ�����Đ������ǂ���

    private void Awake()
    {
        startPos = transform.position; //���݂̈ʒu�𐶐��ʒu�Ƃ��Đݒ�
        endPos = new Vector3(endX, startPos.y, startPos.z); //�m�[�c�̖ړI�n��ݒ�
        notesManager = FindObjectOfType<NotesManager>(); //NotesManager���擾
    }

    private void Update()
    {
        currentTime += Time.deltaTime; //�o�ߎ��Ԃ��X�V
        
        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo);�@//�m�[�c�̈ʒu���ړ�

        
        if (!isTouchingHeart && transform.position.x >= -Heart_range && transform.position.x <= Heart_range) //�n�[�g�ɐG�ꂽ�ꍇ�̃`�F�b�N
        {
            OnTouchHeart();
        }

        
        if (isTouchingHeart && Input.GetKeyDown(KeyCode.Space) && canPlaySpaceSound) //�n�[�g�ɐG��Ă����ԂŃX�y�[�X�L�[�������ꂽ�Ƃ�
        {
            if (isTouchSoundPlaying)
            {
                notesManager.StopTouchSound(); //�n�[�g�ɐG�ꂽ�����~
            }
            notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
            canPlaySpaceSound = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
        }

        
        if (currentTime > tempo) //�m�[�c���ړ����I�������폜
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTouchHeart()
    {
        if (!isTouchingHeart)
        {
            isTouchingHeart = true; //�n�[�g�ɐG�ꂽ���
            notesManager.PlayTouchSound(); // �n�[�g�ɐG�ꂽ�Ƃ��̉���炷
            isTouchSoundPlaying = true; //�n�[�g�̉����Đ����ł��邱�Ƃ��t���O�ɐݒ�
            canPlaySpaceSound = true; //�n�[�g�ɐG�ꂽ��ɃX�y�[�X�L�[�ŉ���点��悤�Ƀt���O�����Z�b�g
        }
    }

    private void OnDestroy()
    {
        canPlaySpaceSound = true; //�m�[�c���폜�����Ƃ��t���O�����Z�b�g
    }
}