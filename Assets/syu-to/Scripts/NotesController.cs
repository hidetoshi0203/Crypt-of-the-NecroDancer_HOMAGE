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
    private ComboManager comboManager;

    private void Awake()
    {
        startPos = transform.position; //���݂̈ʒu�𐶐��ʒu�Ƃ��Đݒ�
        endPos = new Vector3(endX, startPos.y, startPos.z); //�m�[�c�̖ړI�n��ݒ�
        notesManager = FindObjectOfType<NotesManager>(); //NotesManager���擾
        comboManager = FindObjectOfType<ComboManager>();
    }

    private void Update()
    {
        currentTime += Time.deltaTime; //�o�ߎ��Ԃ��X�V

        transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo); //�m�[�c�̈ʒu���ړ�

        //�n�[�g�ɐG�ꂽ�ꍇ�̃`�F�b�N
        if (!notesManager.CanInputKey() && transform.position.x >= -Heart_range && transform.position.x <= Heart_range)
        {
            notesManager.OnTouchHeart();
            notesManager.playerCanMove = true; //�n�[�g�ɐG�ꂽ��ɉ���点��悤�Ƀt���O�����Z�b�g

        }

        //�n�[�g�ɐG��Ă����ԂŃL�[�������ꂽ�Ƃ�
        if (notesManager.CanInputKey() && Input.GetKeyDown(KeyCode.Space) && notesManager.playerCanMove)
        {
            notesManager.StopTouchSound();
            notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
            notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���

            notesManager.OnTimeLimit();
            comboManager.IncreaseCombo();

            Destroy(this.gameObject);
        }

        //�m�[�c���ړ����I�������폜
        if (currentTime > tempo)
        {
            notesManager.OnTimeLimit();
            comboManager.ResetCombo();
            Destroy(this.gameObject);

        }
    }

    private void OnDestroy()
    {
        notesManager.playerCanMove = true; //�m�[�c���폜�����Ƃ��t���O�����Z�b�g
    }
}