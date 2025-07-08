using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int MaxNumberOfShots = 3;

    private int _usedNumberOfShots;

    [SerializeField] private float _secondsToWaitBeforeDeathCheck = 3f;
    [SerializeField] private GameObject _restartScreenObject;
    [SerializeField] private SlingShotHandler _slingShotHandler;

    private IconHandler _iconHandler;

    private List<Piggie> _piggies = new List<Piggie>();

    [System.Obsolete]
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        _iconHandler = GameObject.FindFirstObjectByType<IconHandler>();

        Piggie[] piggies = FindObjectsOfType<Piggie>();
        for (int i = 0; i < piggies.Length; i++)
        {
            _piggies.Add(piggies[i]);
        }
    }

    public void UsedShot()
    {
        _usedNumberOfShots++;
        _iconHandler.useShot(_usedNumberOfShots);

        CheckForLastShot();
    }

    public bool HasEnoughShots()
    {
        if(_usedNumberOfShots < MaxNumberOfShots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckForLastShot()
    {
        if(_usedNumberOfShots == MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWaitTime());
        }
    }

    private IEnumerator CheckAfterWaitTime()
    {
        yield return new WaitForSeconds(_secondsToWaitBeforeDeathCheck);

        if(_piggies.Count == 0)
        {
            WinGame();
        }
        else
        {
            RestartGame();
        }
    }

    public void RemovePiggie(Piggie baddie)
    {
        _piggies.Remove(baddie);
        CheckForAllDeadPiggies();
    }

    private void CheckForAllDeadPiggies()
    {
        if(_piggies.Count == 0)
        {
            WinGame();
        }
    }

    #region Win/Lose
    private void WinGame()
    {
        _restartScreenObject.SetActive(true);
        _slingShotHandler.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}