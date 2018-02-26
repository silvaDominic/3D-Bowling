using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ActionMasterTest {

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;
    private ActionMaster actionMaster;

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator ScoreMasterTestWithEnumeratorPasses() {
        // Use the Assert class to test conditions.
        // yield to skip a frame
        yield return null;
    }


    [SetUp]
    public void SetUp() {
        actionMaster = new ActionMaster();
    }

    // ------------------- REGULAR FRAME SCENARIOS ------------------------
    // ** Note UT --> Under-ten **

    //Bowl an under-ten on a regular frame
    // Returns tidy
    [Test]
    public void T01_BowlUTReturnsTidy() {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    // Strike on regular frame
    // Returns end-turn
    [Test]
    public void T02_OneStrikeReturnsEndTurn() {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    // Bowl a spare on a regular frame
    // Returns tidy, end-turn
    [Test]
    public void T03_BowlSpareReturnsEndTurn() {
        Assert.AreEqual(tidy, actionMaster.Bowl(2));
        Assert.AreEqual(endTurn, actionMaster.Bowl(8));
    }

    // --------------------- LAST FRAME SCENARIOS ------------------------

    // Under-ten on last frame
    // Returns tidy and endGame
    [Test]
    public void T04_LastFrame_UTUT() {
        int[] balls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1 };
        foreach (int ball in balls) {
            actionMaster.Bowl(ball);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
        Assert.AreEqual(endGame, actionMaster.Bowl(4));
    }

    // Strike on last frame 10-1, under-ten on last frame 10-2 & 10-3
    // Returns reset, tidy, endGame
    [Test]
    public void T05_LastFrame_StrikeUTUT() {
        int[] balls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1 };
        foreach (int ball in balls) {
            actionMaster.Bowl(ball);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
        Assert.AreEqual(endGame, actionMaster.Bowl(4));
    }

    // Spare on last frame 10-1, strike on last frame 10-3
    // Returns tidy, reset, and endgame
    [Test]
    public void T06_LastFrame_SpareStrike() {
        int[] balls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1 };
        foreach (int ball in balls) {
            actionMaster.Bowl(ball);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
        Assert.AreEqual(reset, actionMaster.Bowl(7));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }

    // Strike on last frame, spare on last frame 10-3
    // Returns reset, tidy, and end game
    [Test]
    public void T07_LastFrame_StrikeSpare() {
        int[] balls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1};
        foreach (int ball in balls) {
            actionMaster.Bowl(ball);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(7));
        Assert.AreEqual(endGame, actionMaster.Bowl(3));
    }

    // All strikes on last frame
    // Returns reset, reset, end game
    [Test]
    public void T08_LastFrame_StrikeStrikeStrike() {
        int[] balls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1 };
        foreach (int ball in balls) {
            actionMaster.Bowl(ball);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
}
