using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Runner
{
    public class GameManager : Singleton<GameManager>
    {
        public int score = 0;
        public float gameSpeed = 3f;
        [SerializeField] private MapLooper mapLooper;
        [SerializeField] private MonsterSpawner monsterSpawner;
        [SerializeField] private SamuraiController samuraiController;

        [SerializeField] private GameObject gameOverUI;
        [SerializeField] private TextMeshProUGUI scoreUI;
        
        private Coroutine _scoreCoroutine;
        public void GameStart()
        {
            mapLooper.enabled = true;
            monsterSpawner.enabled = true;
            samuraiController.GameStart();
            _scoreCoroutine = StartCoroutine(ScoreUpdater());
        }

        public void GameEnd()
        {
            gameOverUI.SetActive(true);
            StopCoroutine(_scoreCoroutine);
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        IEnumerator ScoreUpdater()
        {
            score = 0;
            var wfs = new WaitForSeconds(1f);
            while (true)
            {
                score += 1;
                scoreUI.text = $"{score}";
                yield return wfs;
            }
        }
    }
}
