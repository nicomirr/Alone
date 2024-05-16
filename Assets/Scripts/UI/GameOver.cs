using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    bool _mustHideGameOverCorroutineStarted;
    bool _isHiddingGameOverCorroutineStarted;

    [SerializeField] AudioClip _doorSlam;
    [SerializeField] AudioClip _doorSlamOpen;
    [SerializeField] AudioSource _searchingSound;
    [SerializeField] AudioSource _rain;

    [SerializeField] GameObject _scaryImage;
    [SerializeField] AudioClip _jumpScareSound;
    [SerializeField] GameObject _enemy;
    [SerializeField] AudioClip _breath;
    [SerializeField] AudioClip _footstep;

    [SerializeField] Image _gameOverScreen;
    [SerializeField] GameObject _gameOverText;
    

    void Update()
    {
        GameOverState();
    }

    void GameOverState()
    {
        if (PlayerController.Instance.GetMustHide() && !_mustHideGameOverCorroutineStarted)
        {
            StartCoroutine(MustHideGameOver());
            _mustHideGameOverCorroutineStarted = true;
        }

        if (PlayerController.Instance.GetIsHidding() && !_isHiddingGameOverCorroutineStarted)
        {
            StartCoroutine(IsHiddingGameOver());
            _isHiddingGameOverCorroutineStarted = true;
        }
    }

    IEnumerator IsHiddingGameOver()
    {
        Color color = Color.black;
        color.a = 0;
        _gameOverScreen.color = color;
        _enemy.SetActive(false);
        GameObject.Find("AudioSourceHorror").GetComponent<AudioSource>().Stop();
        _rain.Stop();       
        yield return new WaitForSeconds(0.2f);
        
        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.4f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.4f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.4f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(0.4f);

        AudioSource.PlayClipAtPoint(_footstep, PlayerController.Instance.transform.position);
        yield return new WaitForSeconds(1f);

        color.a = 1;
        _gameOverScreen.color = color;
        AudioSource.PlayClipAtPoint(_jumpScareSound, PlayerController.Instance.transform.position, 0.4f);        
        _scaryImage.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        _scaryImage.SetActive(false);
        yield return new WaitForSeconds(1.8f);

        _gameOverText.SetActive(true);
        yield return new WaitForSeconds(3.5f);

        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();

    }
  
    IEnumerator MustHideGameOver()
    {   
        _searchingSound.Stop();
        GameObject.Find("AudioSourceHorror").GetComponent<AudioSource>().Stop();
        _rain.Stop();
        yield return new WaitForSeconds(1.8f);

        AudioSource.PlayClipAtPoint(_doorSlam, PlayerController.Instance.transform.position, 0.65f);
        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_doorSlam, PlayerController.Instance.transform.position, 0.65f);
        yield return new WaitForSeconds(1.5f);

        AudioSource.PlayClipAtPoint(_doorSlamOpen, PlayerController.Instance.transform.position, 0.65f);
        yield return new WaitForSeconds(1.8f);

        _gameOverText.SetActive(true);
        yield return new WaitForSeconds(3.5f);

        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }
}
