using UnityEngine;
using UnityEngine.InputSystem;

public class FishingSpot : MonoBehaviour
{
    public float interactDistance = 2.5f; // how close the player has to be

    private Transform player;
    private bool playerNear;
    private bool isFishing;

    void Start()
    {
        // Finds the object named "Player" in the scene
        GameObject p = GameObject.Find("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // Is the player close enough?
        playerNear = Vector3.Distance(player.position, transform.position) <= interactDistance;

        // Press E to start/stop fishing
        if (playerNear && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            isFishing = !isFishing;
            Debug.Log(isFishing ? "Started fishing" : "Stopped fishing");
        }

        // Walking away stops fishing
        if (!playerNear) isFishing = false;
    }

    // Draws the message on screen
    void OnGUI()
    {
        if (!playerNear) return;

        string message = isFishing
            ? "Fishing... (fishing mini-game goes here) - Press E to stop"
            : "Press E to fish";

        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 28;
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(0, Screen.height - 120, Screen.width, 60), message, style);
    }
}
