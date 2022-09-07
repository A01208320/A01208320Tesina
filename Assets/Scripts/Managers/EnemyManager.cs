using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    [SerializeField] private GameObject enemy;

    private void Awake() {
        GameManager.instance.enemy = this;
    }

    public void selectEnemy() {

    }

    public void step() {
        switch (GameManager.instance.difficulty) {
            case GameManager.Difficulty.practice:
                if (transform.childCount == 0) {
                    spawnEnemies();
                }
                break;
            default:
                return;
        }
    }

    public void spawnEnemies() {
        int amount;
        float x_min, x_max, z_min, z_max;
        switch (GameManager.instance.difficulty) {
            case GameManager.Difficulty.practice:
                amount = 1;
                x_min = x_max = 0;
                z_min = 20;
                z_max = 200;
                break;
            default:
                amount = 0;
                x_min = x_max = z_min = z_max = 0;
                break;
        }
        for (; 0 < amount; amount--) {
            Vector3 pos = new Vector3(0, 0, Random.Range(z_min, z_max));
            GameObject g = Instantiate(enemy, pos, Quaternion.identity);
            g.transform.SetParent(transform);
            GameManager.instance.ui.setDistance(Vector3.Distance(g.transform.position, GameManager.instance.cannon.transform.position).ToString());
        }
    }


}
