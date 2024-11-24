using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class IngredientSpawner : MonoBehaviour
{
    public GameObject[] ingredientPrefabs; 
    public Transform canvasTransform;
    public GameObject correct;
    public GameObject incorrect;
    private float lastSpawnY = -345f; 
    private float[,] spacingBetween = new float[,] {
    {40f, 40f, 40f, 90f, 80f, 25f, 70f}, // done
    {40f, 40f, 40f, 40f, 40f, 40f, 40f}, // no need
    {40f, 90f, 30f, 70f, 60f, 15f, 60f}, // done
    {40f, 90f, 60f, 90f, 70f, 40f, 60f}, // done
    {40f, 90f, 40f, 80f, 60f, 15f, 65f}, // done
    {40f, 90f, 30f, 80f, 55f, 20f, 65f}, // done
    {40f, 90f, 30f, 55f, 40f, 20f, 40f},
    {0f, 0f, 0f, 0f, 0f, 0f, 0f}
};
    private int lastIndex = 7;
    private List<int> list = new List<int>();
    int size = 0;
    bool submitted = false;

    private int bunClickCount = 0;

    public void SpawnIngredient(int ingredientIndex) {
        if (canvasTransform == null) {
            Debug.LogError("CanvasTransform is not assigned!");
            return;
        }

        if (ingredientIndex == 0) {
            SpawnBun(ingredientIndex);
            lastIndex = ingredientIndex;
            return;
        }

        if (ingredientIndex < 0 || ingredientIndex >= ingredientPrefabs.Length) {
            Debug.LogError("Invalid ingredient index!");
            return;
        }

        SpawnAtPosition(ingredientPrefabs[ingredientIndex], ingredientIndex);
        list.Add(ingredientIndex);
        size++;
        lastIndex = ingredientIndex;
    }

    private void SpawnBun(int ingredientIndex) {
        int bunPrefabIndex = bunClickCount == 0 ? 0 : 1; 
        bunClickCount++;
        list.Add(bunPrefabIndex);
        size++;

        if (bunPrefabIndex >= ingredientPrefabs.Length || ingredientPrefabs[bunPrefabIndex] == null) {
            Debug.LogError("Bun prefab is not assigned or invalid!");
            return;
        }

        SpawnAtPosition(ingredientPrefabs[bunPrefabIndex], bunPrefabIndex);
    }

    private void SpawnAtPosition(GameObject ingredientPrefab, int ingredientIndex) {
        if (ingredientPrefab == null) {
            Debug.LogError("Ingredient prefab is null!");
            return;
        }

        Vector3 spawnPosition = new Vector3(30, lastSpawnY + spacingBetween[lastIndex, ingredientIndex], 0);

        GameObject newIngredient = Instantiate(ingredientPrefab, canvasTransform);

        RectTransform rectTransform = newIngredient.GetComponent<RectTransform>();
        if (rectTransform != null) {
            rectTransform.anchoredPosition = spawnPosition;
            rectTransform.localScale = Vector3.one;
        } else {
            Debug.LogError("Prefab does not have a RectTransform component!");
        }

        lastSpawnY += spacingBetween[lastIndex, ingredientIndex];

        Debug.Log($"Spawned {newIngredient.name} inside the canvas at position: {rectTransform.anchoredPosition}");
    }

    public void submitBurger() {
        int[] submittedAnswer = new int[size];
        for (int i = 0; i < size; i++) {
            submittedAnswer[i] = list[i];
        }
        DataManager.arrayFromScene2 = submittedAnswer;

        Debug.Log("Array from Scene 1: " + string.Join(", ", DataManager.arrayFromScene1));
        Debug.Log("Array from Scene 2: " + string.Join(", ", DataManager.arrayFromScene2));

        SceneManager.LoadScene("main");

        // SceneManager.LoadScene("main");
    }
}