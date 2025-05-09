using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespiratorObjective : MonoBehaviour
{
    [SerializeField] bool _isTutorial;

    int _direction = -1;
    [SerializeField] float _speed;
    [SerializeField] Respiration _respiration;
    [SerializeField] GameObject _gameOver;
      
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if(_isTutorial )
        {
            this.transform.Translate(new Vector2(0, _speed * _direction * Time.deltaTime));
        }

        if (_respiration.Start)
        {
            this.transform.Translate(new Vector2(0, _speed * _direction * Time.deltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "RespirationBarTop")
            _direction = -1;
        else if (collision.name == "RespirationBarBottom")
            _direction = 1;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Respirator")
        {

            _respiration.Start = false;
            Destroy(collision.gameObject);
            if(_gameOver != null)
            {
                Debug.Log("Entra");
                _gameOver.SetActive(true);
            }
        }
        else if(collision.name == "RespiratorTutorial")
        {
            collision.gameObject.SetActive(false);
        }

    }
}
