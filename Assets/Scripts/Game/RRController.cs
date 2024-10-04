using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Watermelon;

public class RRController : MonoBehaviour
{
    [SerializeField] private RRModel model;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController;

    [Header("TSUNAMI")]
    [SerializeField] private Tsunami tsunami;

    private List<Vector3> lstCatPosition = new List<Vector3>();
    private int  index = 0;
    private bool isGameOver = false;
    private bool isTap = false;

    private void Awake()
    {
        int currentLevel = DataGameSave.GetLevel();
        LoadMap(currentLevel);
    }

    private void LoadMap(int currentLevel)
    {
        string folderPath = "Assets/Resources/Levels";
        string[] prefabPaths = AssetDatabase.FindAssets("Level_", new[] { folderPath });
        foreach (string prefabPath in prefabPaths)
        {
            string prefabFullPath = AssetDatabase.GUIDToAssetPath(prefabPath);
            LevelScriptableObject level = AssetDatabase.LoadAssetAtPath<LevelScriptableObject>(prefabFullPath);
            lstCatPosition = level.lstTransformCat;
            char lastChar = level.name[level.name.Length - 1];
            int lastNumber = int.Parse(lastChar.ToString());
            if (lastNumber == currentLevel)
            {
                DataManager.level = level;
                SceneManager.LoadScene(TypeScene.LevelEditor.ToString(), LoadSceneMode.Additive);
            }    
        }
    }

    private void Start()
    {
        tsunami.Init(GameOver);

        GameUIController.Instance.Init();
        GameUIController.Instance.ShowView(TypeViewUI.None);
        MoveCamera();
    }

    private void MoveCamera()
    {
        cameraFollow.Run(() =>
        {
            GameUIController.Instance.ShowView(TypeViewUI.E_TapView);
            isTap = true;
            StartCoroutine(Countdown());
        });
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(model.TimeStart);
        StartGame();
    }

    private void StartGame()
    {
        GameUIController.Instance.ShowView(TypeViewUI.E_GameView);
        TsunamiMoving();
    }

    private void Update()
    {
        if (isGameOver) return;
        playerController.Moving(Joystick.Instance.FormatInput, model.Speed);

        if(isTap) TapOnScreen();
    }

    public void TapOnScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = touch.position;
                model.Speed += model.SpeedUp;
                GameUIController.Instance.ShowPill(touchPosition, model.SpeedUp);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            model.Speed += model.SpeedUp;
            GameUIController.Instance.ShowPill(mousePosition, model.SpeedUp);
        }
    }

    private void TsunamiMoving()
    {
        tsunami.Moving(model.TsunamiTime[index], new Vector3(0, 0, model.LineStreet[index]), () =>
        {
            index++;
            TsunamiMoving();
        });
    }

    private void GameOver()
    {
        isGameOver = true;
        Joystick.Instance.DisableControl();
        GameUIController.Instance.ShowView(TypeViewUI.E_GameoverView);
    }    
}
