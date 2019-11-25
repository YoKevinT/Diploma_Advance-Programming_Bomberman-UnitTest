using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class Bomberman2Tests

///Alternative for [SetUp], doesn't need Nunit.Framework
// public class BombermanTests: IPrebuildSetup
{
    //public void Setup()
    //{
    //    throw new System.NotImplementedException();
    //}

    private GameObject game; // Stores Instance of Entire Game
    private Player[] players;

    // Method for getting reference to player by index
    Player GetPlayer(int index)
    {
        // Loops though al player from SetUp function
        foreach (var player in players)
        {
            // Compares the playerNumber with given Index
            if (player.playerNumber == index)
            {
                // Return that player
                return player;
            }
        }
        // All else fails, return null
        return null;
    }

    [SetUp]
    public void SetUp()
    {
        GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(gamePrefab);
        players = Object.FindObjectsOfType<Player>();
    }

    // >> TESTS GO HERE <<
    [UnityTest]
    public IEnumerator PlayerDropsBomb()
    {
        // Get the First Player
        Player player2 = GetPlayer(2);

        // Simulate Bomb Dropping
        player2.DropBomb();

        // Wait for the last frame
        yield return new WaitForEndOfFrame();

        // Check if Bomb Exists in the Scene
        Bomb bomb = Object.FindObjectOfType<Bomb>();

        // Bomb is not null
        Assert.IsTrue(bomb != null, "The Bomb didn't spawn");

        // To Pause the Editor
        // Debug.Break();
    }

    [UnityTest]
    public IEnumerator PlayerMovement()
    {
        Player player2 = GetPlayer(2);
        Vector3 oldPosition = player2.transform.position;
        player2.Move(false, false, true, false);
        yield return new WaitForFixedUpdate();
        Vector3 newPosition = player2.transform.position;
        Assert.IsTrue(oldPosition != newPosition);
    }

    [TearDown]
    public void TearDown()
    {
        // Remove the Game from the Scene
        Object.Destroy(game);
    }
}