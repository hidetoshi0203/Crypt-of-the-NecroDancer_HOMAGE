using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesController : MonoBehaviour
{
    //[SerializeField] private float endX;


    ////private float endX; //�m�[�c�̈ړ����X���W
    //[SerializeField] private float tempo = 2.0f; //�m�[�c�̈ړ�����
    //[SerializeField] public float Heart_range = 0; //�n�[�g�ɐG���͈�
    ////private GameObject heartImage; // �m�[�c�̏I���_�ƂȂ�I�u�W�F�N�g 

    //private Vector3 startPos; //�m�[�c�̐����ʒu
    //private Vector3 endPos; //�m�[�c�̏I���ʒu
    //private float currentTime = 0f; //�m�[�c�̈ړ��̌o�ߎ���

    //private NotesManager notesManager; //NotesManager�̃C���X�^���X
    //private ComboManager comboManager;
    //private ColorChange ColorChange;
    //private toshiPlayer toshiPlayer = null;
    ////private Image a;
    //private void Awake()
    //{
    //    /*heartImage = GameObject.Find("Heart");
    //    endX = heartImage.transform.position.x;
    //    //Heart_range = heartImage.transform.position.x + Heart_range;
    //    */
    //    startPos = transform.position; //���݂̈ʒu�𐶐��ʒu�Ƃ��Đݒ�
    //    endPos = new Vector3(endX, startPos.y, startPos.z); //�m�[�c�̖ړI�n��ݒ�
    //    notesManager = FindObjectOfType<NotesManager>(); //NotesManager���擾
    //    comboManager = FindObjectOfType<ComboManager>();
    //    ColorChange = FindObjectOfType<ColorChange>();
    //}

    //private void Update()
    //{
    //    //Debug.Log(Heart_range);
    //    if (toshiPlayer == null)
    //    {
    //        GameObject inst = GameObject.FindGameObjectWithTag("Player");
    //        toshiPlayer = inst.GetComponent<toshiPlayer>();
    //    }
    //    currentTime += Time.deltaTime; //�o�ߎ��Ԃ��X�V

    //    transform.position = Vector3.Lerp(startPos, endPos, currentTime / tempo); //�m�[�c�̈ʒu���ړ�

    //    //�n�[�g�ɐG�ꂽ�ꍇ�̃`�F�b�N
    //    if (!notesManager.CanInputKey() && transform.position.x >= -Heart_range && transform.position.x <= Heart_range)
    //    {
    //        notesManager.OnTouchHeart();
    //        notesManager.playerCanMove = true; //�n�[�g�ɐG�ꂽ��ɉ���点��悤�Ƀt���O�����Z�b�g

    //    }

    //    //�n�[�g�ɐG��Ă����ԂŃL�[�������ꂽ�Ƃ�
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }

    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.W))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.A))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.S))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }
    //    if (notesManager.CanInputKey() && notesManager.playerCanMove && Input.GetKeyDown(KeyCode.D))
    //    {
    //        KyeDown();
    //        //ColorChange.Changecolor();
    //    }

    //    //�m�[�c���ړ����I�������폜
    //    if (currentTime > tempo)
    //    {
    //        notesManager.OnTimeLimit();
    //        comboManager.ResetCombo();
    //        currentTime = 0f;
    //        Destroy(this.gameObject);

    //    }
    //}

    //private void OnDestroy()
    //{
    //    notesManager.playerCanMove = true; //�m�[�c���폜�����Ƃ��t���O�����Z�b�g
    //}

    //public void KyeDown()
    //{
    //    notesManager.StopTouchSound();
    //    notesManager.PlaySpaceSound(); //�X�y�[�X�L�[���������Ƃ��̉���炷
    //    notesManager.playerCanMove = false; //�t���O���I�t�ɂ��ĉ���点�Ȃ��悤�ɂ���

    //    notesManager.OnTimeLimit();
    //    comboManager.IncreaseCombo();

    //    Destroy(this.gameObject);
    //}

    //public void OffTouchHeart()
    //{
    //    //�n�[�g�̊O�ɂ���Ƃ�
    //    if (transform.position.x < -Heart_range || transform.position.x > Heart_range)
    //    {
    //        comboManager.comboreset = true;
    //    }
    //    else
    //    {
    //        comboManager.comboreset = false;
    //    }

    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("reset");
    //    }



    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.W))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("W_reset");
    //    }

    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.A))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("A_reset");
    //    }
    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.S))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("S_reset");
    //    }
    //    if (comboManager.comboreset && Input.GetKeyDown(KeyCode.D))
    //    {
    //        comboManager.ResetCombo();
    //        //Debug.Log("D_reset");
    //    }
    //}
}