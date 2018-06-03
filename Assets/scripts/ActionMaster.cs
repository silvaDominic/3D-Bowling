using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {
    
    public enum Action {Tidy, Reset, EndTurn, EndGame};
    public enum BowlResult {Strike, Spare, UnderTen};

    private BowlResult previousBowlResult; // Used for evaluating special cases
    private Action actionResult;
    private int[] bowls = new int[21];
    private int bowl = 1;

    public static Action NextAction(List<int> pinFalls) {

        ActionMaster actionMaster = new ActionMaster();
        Action currentAction = new Action();

        foreach (int pinFall in pinFalls) {
            currentAction = actionMaster.Bowl(pinFall);
        }

        return currentAction;
    }

    private Action Bowl(int pins) {

        // Add pins to bowl array
        AddToBowls(pins);

        // Always return End Game on ball 21
        if (bowl == 21) {
            this.actionResult = Action.EndGame;
        }

        switch (EvaluateBowl(pins)) {
            // Handles actions for strike conditions
            case BowlResult.Strike:
                if (this.bowl >= 19 && this.bowl < 21) {
                    this.actionResult = Action.Reset;
                } else if (this.bowl < 19) {
                    bowl++;
                    this.actionResult = Action.EndTurn;
                }
                previousBowlResult = BowlResult.Strike;
                break;

            // Handles action for spare conditions
            case BowlResult.Spare:
                if (this.bowl == 20) {
                    actionResult = Action.Reset;
                } else if (this.bowl < 20) {
                    actionResult = Action.EndTurn;
                }
                previousBowlResult = BowlResult.Spare;
                break;

            // Handles action for under-ten conditions
            case BowlResult.UnderTen:
                if (this.bowl % 2 != 0 && this.bowl != 21) { // If middle of frame
                    actionResult = Action.Tidy;
                } else if (this.bowl % 2 == 0) {  // If end of frame
                    if (this.bowl == 20) { // Special case for bowl 20
                        if (previousBowlResult == BowlResult.Strike) {
                            actionResult = Action.Tidy;
                        } else {
                            actionResult = Action.EndGame;
                            }
                    } else {
                        actionResult = Action.EndTurn;
                    }
                }
                previousBowlResult = BowlResult.UnderTen;
                break;

            default:
                throw new UnityException("Not sure what action to return.");
        }

        if (bowl != 21) {
            this.bowl++;
        }
        return actionResult;
    }

    private BowlResult EvaluateBowl(int pins) {

        if (pins > 10 || pins < 0) {
            throw new UnityException("Invalid pin count argument of " + pins);
        }

        if (pins == 10) {
            return BowlResult.Strike;
        }

        return CheckForSpare(pins);
    }

    private BowlResult CheckForSpare(int pins) {
        if (this.bowl > 1) {
            int lastBowl = this.bowls[this.bowl - 2];
            if (pins + lastBowl == 10) {
                return BowlResult.Spare;
            }
        }
        return BowlResult.UnderTen;
    }

    private void AddToBowls(int pins) {
        this.bowls[this.bowl - 1] = pins;
    }

    public int GetBowlCount() {
        return this.bowl;
    }

    public int GetBowlValue() {
        return this.bowls[this.bowl];
    }
}
