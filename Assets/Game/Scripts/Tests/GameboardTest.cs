using UnityEngine;

public class GameboardTest : MonoBehaviour
{
    public GameObject GameboardPrefab;
    
    private void Start()
    {
        var gameboardObject = Instantiate(GameboardPrefab);
        var gameboard = gameboardObject.GetComponent<Gameboard>();

        gameboard.Setup(transform.position, Quaternion.Euler(90f, 0f, 0f), new Vector3(0.6f, 0f, 0.6f));
    }
}