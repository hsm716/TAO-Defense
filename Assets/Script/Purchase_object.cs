using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Purchase_object : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public enum Trap { trap1,trap2,trap_block, trap_snowman,trap_roket}
    public Trap trapType;

    [SerializeField]
    private GameObject trap1_obj;
    [SerializeField]
    private GameObject trapLocation;
    private GameObject grabedObj;
    public Player player;
    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 방해물 구매 UI에서 버튼을 드래그 하여 구매하는 방식
        // 각 장애물 타입별 가격 조건이 맞아 떨어질 때 정상작동
        if (trapType == Trap.trap1 && player.coin >= 50)
        {
            trapLocation.SetActive(true); // 방해물을 설치할 수 있는 공간을 표시하는 ui 활성화

            // 설치될 장애물이 collider 비활성화 및 투명한 형태로 생성됨.
            grabedObj = Instantiate(trap1_obj, transform.position, transform.rotation);
            grabedObj.GetComponent<Trap1>().itsCol.enabled = false;
            grabedObj.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        }
        else if (trapType == Trap.trap2 && player.coin >= 150)
        {
            trapLocation.SetActive(true);
            grabedObj = Instantiate(trap1_obj, transform.position, transform.rotation);
            grabedObj.GetComponent<Trap2>().itsCol.enabled = false;
            grabedObj.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        }
        else if (trapType == Trap.trap_block && player.coin >= 200)
        {
            trapLocation.SetActive(true);
            grabedObj = Instantiate(trap1_obj, transform.position, transform.rotation);
            grabedObj.GetComponent<TrapBlock>().itsCol.enabled = false;
            grabedObj.GetComponent<TrapBlock>().hp_img.enabled = false;
            grabedObj.GetComponent<TrapBlock>().hpBack_img.enabled = false;
            grabedObj.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        }
        else if (trapType == Trap.trap_snowman && player.coin >= 300)
        {
            trapLocation.SetActive(true);
            grabedObj = Instantiate(trap1_obj, transform.position, transform.rotation);
            grabedObj.GetComponent<TrapSnowman>().itsCol.enabled = false;
            grabedObj.GetComponent<TrapSnowman>().hp_img.enabled = false;
            grabedObj.GetComponent<TrapSnowman>().hpBack_img.enabled = false;
            grabedObj.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        }
        else if (trapType == Trap.trap_roket && player.coin >= 500)
        {
            trapLocation.SetActive(true);
            grabedObj = Instantiate(trap1_obj, transform.position, transform.rotation);
            grabedObj.GetComponent<TrapRoketShooter>().itsCol.enabled = false;
            grabedObj.GetComponent<TrapRoketShooter>().hp_img.enabled = false;
            grabedObj.GetComponent<TrapRoketShooter>().hpBack_img.enabled = false;
            grabedObj.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0.5f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 설치 예정의 장애물을 마우스 포인터(터치) 좌표에 위치하도록 함
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 각 장애물 타입별 가격 조건이 맞아 떨어질 때 정상작동
        if (trapType == Trap.trap1 && player.coin >= 50)
        {
            
            grabedObj.transform.position = new Vector3(touchPos.x, touchPos.y, -2f);
        }
        else if (trapType == Trap.trap2 && player.coin >= 150)
        {
            grabedObj.transform.position = new Vector3(touchPos.x, touchPos.y, -2f);
        }
        else if (trapType == Trap.trap_block && player.coin >= 200)
        {
            grabedObj.transform.position = new Vector3(touchPos.x, touchPos.y, -2f);
        }
        else if (trapType == Trap.trap_snowman && player.coin >= 300)
        {
            grabedObj.transform.position = new Vector3(touchPos.x, touchPos.y, -2f);
        }
        else if (trapType == Trap.trap_roket && player.coin >= 500)
        {
            grabedObj.transform.position = new Vector3(touchPos.x, touchPos.y, -2f);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hitInformation = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
        trapLocation.SetActive(false);

        // 드래그를 종료한 시점에 마우스(터치) 위치에 검출된 Collider가 SpawnLocation 일 경우에 작동
        if (hitInformation.collider.CompareTag("SpawnLocation"))
        {

            // 이미 해당 위치에 오브젝트가 존재할 경우 구매취소
            if (hitInformation.collider.gameObject.GetComponent<SpawnLocation>().isObj == true)
            {
                Destroy(grabedObj);
            }
            else
            {// 해당 위치가 비어 있을 경우, 구매 처리
                hitInformation.collider.gameObject.GetComponent<SpawnLocation>().isObj = grabedObj;
                
                // Collider 활성화, 위치정보 적용, 플레이어 골드 차감
                if (trapType == Trap.trap1)
                {
                    grabedObj.GetComponent<Trap1>().itsCol.enabled = true;
                    player.LostCoin(50);
                    //player.coin -= 50f;
                    grabedObj.transform.position = hitInformation.collider.transform.position + new Vector3(0f, 0f, -2f);
                }
                else if (trapType == Trap.trap2)
                {
                    grabedObj.GetComponent<Trap2>().itsCol.enabled = true;
                    player.LostCoin(150);
                    //player.coin -= 150f;
                    grabedObj.transform.position = hitInformation.collider.transform.position + new Vector3(0f, 0f, -2f);
                }
                else if (trapType == Trap.trap_block)
                {
                    TrapBlock Block_obj = grabedObj.GetComponent<TrapBlock>();
                    
                    Block_obj.itsCol.enabled = true;
                    // 체력있는 장애물인 경우, 체력바 img 활성화 처리
                    Block_obj.hp_img.enabled = true;
                    Block_obj.hpBack_img.enabled = true;
                    player.LostCoin(200);
                    //player.coin -= 200f;
                    grabedObj.transform.position = hitInformation.collider.transform.position + new Vector3(0f, 0.35f, -2f);
                }
                else if (trapType == Trap.trap_snowman)
                {
                    TrapSnowman Snowman_obj = grabedObj.GetComponent<TrapSnowman>();
                    Snowman_obj.StartCoroutine(Snowman_obj.Attack());
                    Snowman_obj.itsCol.enabled = true;
                    Snowman_obj.hp_img.enabled = true;
                    Snowman_obj.hpBack_img.enabled = true;
                    player.LostCoin(300);
                    //player.coin -= 300f;
                    grabedObj.transform.position = hitInformation.collider.transform.position + new Vector3(0f, 0f, -2f);
                }
                else if (trapType == Trap.trap_roket)
                {
                    TrapRoketShooter RoketShooter = grabedObj.GetComponent<TrapRoketShooter>();
                    RoketShooter.itsCol.enabled = true;
                    RoketShooter.hp_img.enabled = true;
                    RoketShooter.hpBack_img.enabled = true;
                    player.LostCoin(500);
                    //player.coin -= 500f;
                    grabedObj.transform.position = hitInformation.collider.transform.position + new Vector3(0.5f, -0.03f, -2f);
                }
                grabedObj.GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 1f);
            }
        }

        // 드래그가 끝났을 때, 구매예정 오브젝트가 투명이면 구매를 취소함.
        if (grabedObj.GetComponent<SpriteRenderer>().color == new Color(255f, 255f, 255f, 0.5f))
        {
            Destroy(grabedObj);
        }
        
    }
}
