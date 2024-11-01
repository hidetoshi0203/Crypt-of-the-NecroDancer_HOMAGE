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

    private NotesManager notesManager; //NotesManager�̃C���X�^���X

    private void Awake()
    {
        startPos = transform.position; //���݂̈ʒu�𐶐��ʒu�Ƃ��Đݒ�
        endPos = new Vector3(endX, startPos.y, startPos.z); //�m�[�c�̖ړI�n��ݒ�
        notesManager = FindObjectOfType<NotesManager>(); //NotesManager���擾
    }

    private void Update()
    {
        currentTime += Time.deltaTime; //�o�ߎ��Ԃ��X�V

        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo); //�m�[�c�̈ʒu���ړ�


        if (!notesManager.CanInputKey() && transform.position.x >= -Heart_range && transform.position.x <= Heart_range) //�n�[�g�ɐG�ꂽ�ꍇ�̃`�F�b�N
        {
            notesManager.OnTouchHeart();
            notesManager.canMove = true; //�n�[�g�ɐG�ꂽ��ɃX�y�[�X�L�[�ŉ���点��悤�Ƀt���O�����Z�b�g
        }

        if (notesManager.CanInputKey() && Input.GetKeyDown(KeyCode.Space) && notesManager.canMove) //�n�[�g�ɐG��Ă����ԂŃX�y�[�X�L�[�������ꂽ�Ƃ�
        {
            notesManager.StopTouchSound();
            notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
            notesManager.canMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���
        }


        if (currentTime > tempo) //�m�[�c���ړ����I�������폜
        {
            notesManager.OnTimeLimit();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        notesManager.canMove = true; //�m�[�c���폜�����Ƃ��t���O�����Z�b�g
    }
}