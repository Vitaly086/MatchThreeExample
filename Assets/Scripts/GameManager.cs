using GameBoard;
using Items;
using LevelSettings;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GlobalConstants;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LevelsSettingsProvider _levelsSettingsProvider;
    [SerializeField]
    private ItemsSetProvider _itemsSetProvider;
    [SerializeField]
    private GameBoardController _gameBoardController;
    [SerializeField]
    private ItemsMover _itemsMover;
    [SerializeField]
    private AudioManager _audioManager;
    [SerializeField]
    private ScoreManager _scoreManager;
    [SerializeField]
    private MovesManager _movesManager;
    [SerializeField]
    private Button _backButton;
    [SerializeField]
    private GameOverPopup _gameOverPopup;

    private MatchFinder _matchFinder;

    private void Awake()
    {
        InitializeGameObjects();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void InitializeGameObjects()
    {
        _matchFinder = new MatchFinder();
        _gameOverPopup.gameObject.SetActive(false);

        SetupLevel();
        InitializeGameBoard();
        SubscribeEvents();
    }

    private void SetupLevel()
    {
        var level = PlayerPrefs.GetInt(CURRENT_LEVEL, 1);
        var itemTypesNumber = _levelsSettingsProvider.GetItemTypesCount(level);
        var itemsSet = _itemsSetProvider.GetItemsSet();
        _gameBoardController.Initialize(itemsSet, itemTypesNumber, _matchFinder);
    }

    private void InitializeGameBoard()
    {
        var gameBoardIndexProvider = new GameBoardIndexProvider(_gameBoardController);
        var items = _gameBoardController.CreateGameBoard();
        _itemsMover.Initialize(items, gameBoardIndexProvider);
    }

    private void SubscribeEvents()
    {
        _itemsMover.ItemsSwapped += OnItemsSwapped;
        _gameBoardController.ReFill += OnItemsFellDown;
        _gameBoardController.ItemsDestroyed += OnItemDestroyed;
        _backButton.onClick.AddListener(LoadMenuScene);
        _movesManager.MovesOver += OnMovesOver;
    }

    private void OnItemsSwapped(Item[,] items)
    {
        HandleItemSwap(items);
    }

    private void OnItemsFellDown(Item[,] items)
    {
        HandleItemFallDown(items);
    }

    private void OnItemDestroyed(int value)
    {
        _scoreManager.IncreaseScore(value);
        _audioManager.PlayItemDestroyingClip();
    }

    private void OnMovesOver()
    {
        ShowGameOverPopup();
    }

    private void HandleItemSwap(Item[,] items)
    {
        if (_matchFinder.HasMatches(items))
        {
            _gameBoardController.DestroyMatches(_matchFinder.Matches);
        }
        else
        {
            _itemsMover.ReSwapItems();
            _movesManager.DecreaseMoves();
        }
    }

    private void HandleItemFallDown(Item[,] items)
    {
        if (_matchFinder.HasMatches(items))
        {
            _gameBoardController.DestroyMatches(_matchFinder.Matches);
        }
        else
        {
            _movesManager.DecreaseMoves();
        }
    }

    private void ShowGameOverPopup()
    {
        _gameOverPopup.gameObject.SetActive(true);
        _gameOverPopup.SetFinalScore(_scoreManager.Score);
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadSceneAsync(MENU_SCENE);
    }

    private void UnsubscribeEvents()
    {
        if (_itemsMover != null) _itemsMover.ItemsSwapped -= OnItemsSwapped;
        if (_gameBoardController != null) _gameBoardController.ReFill -= OnItemsFellDown;
        if (_gameBoardController != null) _gameBoardController.ItemsDestroyed -= OnItemDestroyed;
        if (_backButton != null) _backButton.onClick.RemoveListener(LoadMenuScene);
        if (_movesManager != null) _movesManager.MovesOver -= OnMovesOver;
    }
}