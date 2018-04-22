using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Linq;

public class ActionMasterTest {

    private List<int> pinFalls;
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
        pinFalls = new List<int>();
    }

    // ------------------- REGULAR FRAME SCENARIOS ------------------------
    // ** Note UT --> Under-ten **

    //Bowl an under-ten on a regular frame
    // Returns tidy
    [Test]
    public void T01_BowlUTReturnsTidy() {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    // Strike on regular frame
    // Returns end-turn
    [Test]
    public void T02_OneStrikeReturnsEndTurn() {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    // Bowl a spare on a regular frame
    // Returns end-turn
    [Test]
    public void T03_BowlSpareReturnsEndTurn() {
        int[] rolls = { 2, 8 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    // Bowl a unique spare of 0-10 on a regular frame
    // Returns end-turn
    [Test]
    public void T04_BowlUniqueSpareReturnsEndTurn() {
        int[] rolls = { 0, 10 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    // --------------------- LAST FRAME SCENARIOS ------------------------

    // Under-ten on last frame
    // Returns endGame
    [Test]
    public void T04_LastFrame_UTUT() {
        int[] rolls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1, 3, 4 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    // Strike on last frame 10-1, under-ten on last frame 10-2 & 10-3
    // Returns endGame
    [Test]
    public void T05_LastFrame_StrikeUTUT() {
        int[] rolls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1, 10, 3, 4 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    // Spare on last frame 10-1, strike on last frame 10-3
    // Returns end-game
    [Test]
    public void T06_LastFrame_SpareStrike() {
        int[] rolls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1, 3, 7, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    // Strike on last frame, spare on last frame 10-3
    // Returns end-game
    [Test]
    public void T07_LastFrame_StrikeSpare() {
        int[] rolls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1, 10, 7, 3 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    // All strikes on last frame
    // Returns end-game
    [Test]
    public void T08_LastFrame_StrikeStrikeStrike() {
        int[] rolls = { 3, 4, 6, 2, 4, 1, 3, 5, 2, 4, 2, 2, 1, 4, 5, 3, 6, 1, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }

    // Perfect game
    // Returns end-game
    [Test]
    public void T10_BowlPerfectGame() {
        int[] rolls = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 };
        Assert.AreEqual(endGame, ActionMaster.NextAction(rolls.ToList()));
    }
}
