using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class Player : MonoBehaviour
{
    [SerializeField]
    private float z_MoveSpeed = 5;
    private BoxCollider2D z_BoxCollider;
    public Transform customer;
    public Customer CustomerClass;
    public Transform plate;
    public Transform targetPoint; 
    public Sprite plateSprite;
    public Sprite defaultSprite;
    public GameObject correctMark;
    public GameObject wrongMark;
    public TextMeshProUGUI scoreText;
    public SFXSound sfx;

    private bool tookOrder = false;
    private bool addedScore = false;

    private void Start() {
        z_BoxCollider = GetComponent<BoxCollider2D>();
        if (DataManager.start) {
            transform.position = targetPoint.position;
            changeSpriteToPlate();
        }
    }


    public bool areEqual() {
        if (DataManager.arrayFromScene1 == null || DataManager.arrayFromScene2 == null) {
            Debug.LogError("One or both arrays are not initialized!");
            return false;
        }

        if (DataManager.arrayFromScene1.Length != DataManager.arrayFromScene2.Length) {
            return false;
        }

        for (int i = 0; i < DataManager.arrayFromScene1.Length; i++) {
            if (DataManager.arrayFromScene1[i] != DataManager.arrayFromScene2[i]) {
                return false;
            }
        }

        return true;
    }

    private void FixedUpdate() {
        Movement();

        scoreText.text = $"Score: {DataManager.score}";

        if (!DataManager.start && isCustomerAround()) {
            tookOrder = true;
            addedScore = false;
            CustomerClass.showBurger();
            sfx.playOnClick();
        }

        if (DataManager.start && isCustomerAround() && !addedScore) {
            changeToDefaultPlate();
            sfx.playOnClick();

            if (areEqual()) {
                correctMark.SetActive(true);
                DataManager.score++;
            } else {
                wrongMark.SetActive(true);
                DataManager.score--;
            }
            addedScore = true;
            CustomerClass.goBackToTheStart();
        }

        if (isPlateAround()) {
            sfx.playOnClick();
            DataManager.start = true;
            SceneManager.LoadScene("Burger Builder");
            return;
        }
    }

    public void changeSpriteToPlate() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && plateSprite != null) {
            spriteRenderer.sprite = plateSprite;
            Debug.Log("Sprite changed successfully!");
        } else {
            Debug.LogError("SpriteRenderer or newSprite is not assigned!");
        }
    }

     public void changeToDefaultPlate() {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null && defaultSprite != null) {
            spriteRenderer.sprite = defaultSprite;
            Debug.Log("Sprite changed successfully!");
        } else {
            Debug.LogError("SpriteRenderer or newSprite is not assigned!");
        }
    }


    private void Movement(){
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDelta = new Vector2(moveX, moveY);

        if(moveDelta.x > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        RaycastHit2D castResult;
        castResult = Physics2D.BoxCast(transform.position, z_BoxCollider.size, 0, new Vector2(moveX, 0), Mathf.Abs(moveX * Time.fixedDeltaTime * z_MoveSpeed), LayerMask.GetMask("Customer", "BlockMove"));
        if(castResult.collider) {
            moveDelta.x = 0;
        }

        castResult = Physics2D.BoxCast(transform.position, z_BoxCollider.size, 0, new Vector2(0, moveY), Mathf.Abs(moveY * Time.fixedDeltaTime * z_MoveSpeed), LayerMask.GetMask("Customer", "BlockMove"));
        if(castResult.collider) {
            moveDelta.y = 0;
        }

        transform.Translate(moveDelta * Time.deltaTime * z_MoveSpeed);
    }   

    private bool isCustomerAround() {
        return Vector3.Distance(transform.position, customer.position) < 40.0f && Input.GetKey(KeyCode.E) && !tookOrder;
    }

    private bool isPlateAround() {
        return Vector3.Distance(transform.position, plate.position) < 1865.0f && Input.GetKey(KeyCode.E) && tookOrder;
    }
}
