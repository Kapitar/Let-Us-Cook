using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    int[] burger = new int[7];
    public Transform targetPoint; 
    public Transform backPoint;
    public float speed = 5f;

    private bool isMoving = true;
    private bool goBack = false;
    public BurgerBuild burgerBuild;
    public GameObject buttonCook;
    public GameObject questionMark;
    public GameObject plate;
    public GameObject panel;
    public GameObject correctMark;
    public GameObject wrongMark;

    public void goBackToTheStart() {
        Debug.Log("Go Back");
        goBack = true;
        isMoving = true;
    }

    void Update() {
        if(isMoving && goBack) {
            Debug.Log("GO");
            MoveBack();
        } else if (DataManager.start) {
            transform.position = targetPoint.position;
        } else if (isMoving) {
            MoveToPoint();
        }
    }

    private void MoveToPoint() {
        if (targetPoint == null) {
            Debug.LogError("Target point is not assigned!");
            return;
        }

        transform.localScale = new Vector3(1, 1, 1);
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f) {
            isMoving = false;
            Debug.Log("Sprite reached the target point!");

            questionMark.SetActive(true);
        }
    }

    public void MoveBack() {
        if (backPoint == null) {
            Debug.LogError("Target point is not assigned!");
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 moveDelta = new Vector2(moveX, moveY);
        transform.localScale = new Vector3(-1, 1, 1);

        transform.position = Vector3.MoveTowards(transform.position, backPoint.position, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, backPoint.position) < 0.1f) {
            goBack = false;
            DataManager.start = false;
            correctMark.SetActive(false);
            wrongMark.SetActive(false);
            Debug.Log("Sprite reached the target point!");
        }
    }

    public void showBurger() {
        buttonCook.SetActive(!buttonCook.activeSelf);
        plate.SetActive(true);
        panel.SetActive(true);
        burger[0] = 0;
        for (int i = 1; i < 6; i++) {
            burger[i] = Random.Range(2, 7);
        }
        burger[6] = 1;
        for (int i = 0; i < 7; i++) {
            Debug.Log(burger[i]);
            burgerBuild.SpawnIngredient(burger[i]);
        }
        DataManager.arrayFromScene1 = burger;
    }
}