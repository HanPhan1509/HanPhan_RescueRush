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
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject finish;

    [Header("TSUNAMI")]
    [SerializeField] private Tsunami tsunami;

    private float linePhase = 0.0f;

    private List<Vector3> lstCatPosition = new List<Vector3>();
    private int index = 0;
    private bool isGameOver = false;
    private bool isTap = false;

    private void Awake()
    {
        int currentLevel = DataGameSave.GetLevel();
        model.Speed = DataGameSave.GetSpeed() + currentLevel;
        LoadLevel(currentLevel);
    }

    private void LoadLevel(int currentLevel)
    {
        string levelName = "Levels/Level_" + currentLevel;
        LevelScriptableObject level = Resources.Load<LevelScriptableObject>(levelName);
        if (level != null)
        {
            lstCatPosition = new List<Vector3>(level.lstTransformCat);
            char lastChar = level.name[level.name.Length - 1];
            int lastNumber = int.Parse(lastChar.ToString());
            if (lastNumber == currentLevel)
            {
                DataManager.level = level;
                SceneManager.LoadScene(TypeScene.LevelEditor.ToString(), LoadSceneMode.Additive);
            }
        }
    }

    //private void LoadMap(int currentLevel)
    //{
    //    string folderPath = "Assets/Resources/Levels";
    //    string[] prefabPaths = AssetDatabase.FindAssets("Level_", new[] { folderPath });
    //    foreach (string prefabPath in prefabPaths)
    //    {
    //        string prefabFullPath = AssetDatabase.GUIDToAssetPath(prefabPath);
    //        LevelScriptableObject level = AssetDatabase.LoadAssetAtPath<LevelScriptableObject>(prefabFullPath);
    //        lstCatPosition = level.lstTransformCat;
    //        char lastChar = level.name[level.name.Length - 1];
    //        int lastNumber = int.Parse(lastChar.ToString());
    //        if (lastNumber == currentLevel)
    //        {
    //            DataManager.level = level;
    //            SceneManager.LoadScene(TypeScene.LevelEditor.ToString(), LoadSceneMode.Additive);
    //        }
    //    }
    //}

    private void Start()
    {
        tsunami.Init(GameOver);
        GameUIController.Instance.Init();
        MoveCamera();
        linePhase = model.Phase_1 / 2;
        Instantiate(finish, new Vector3(0, 0, model.Phase_1 + model.Phase_2), Quaternion.identity).GetComponent<Finish>().Init(() =>
        {
            GameUIController.Instance.ShowView(TypeViewUI.E_WinView);
        });
    }

    private void MoveCamera()
    {
        cameraFollow.Run(player, () => StartCoroutine(Countdown()));
    }

    private IEnumerator Countdown()
    {
        GameUIController.Instance.ShowView(TypeViewUI.E_TapView);
        isTap = true;
        yield return new WaitForSeconds(model.TimeStart);
        StartGame();
    }

    private void StartGame()
    {
        isTap = false;
        TsunamiMoving();
        Joystick.Instance.EnableControl();
        GameUIController.Instance.ShowView(TypeViewUI.E_GameView);
    }

    private void Update()
    {
        if (isTap) TapOnScreen();

        if (playerController.transform.position.z > linePhase)
        {
            linePhase += linePhase;
            DataManager.OnLoopPhase?.Invoke();
        }

        if (isGameOver) return;
        playerController.Moving(Joystick.Instance.FormatInput, model.Speed);
    }

    public void TapOnScreen()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchPosition = touch.position;
                if (DataGameSave.GetSpeed() < model.MaxSpeed)
                {
                    model.Speed += model.SpeedUp;
                    DataGameSave.SetSpeed(model.Speed);
                }
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
