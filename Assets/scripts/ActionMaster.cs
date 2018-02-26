using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {
    
    public enum Action {Tidy, Reset, EndTurn, EndGame};
    public enum BowlResult {Strike, Spare, UnderTen};

    private BowlResult previousBowl;
    private Action actionResult;
    private int[] balls = new int[21];
    private int ball = 1;

    public Action Bowl(int pins) {

        // Add pins to bowl array
        AddToBowls(pins);

        // Always return End Game on ball 21
        if (ball == 21) {
            this.actionResult = Action.EndGame;
        }

        switch (CheckBowlResult(pins)) {
            case BowlResult.Strike:
                if (this.ball >= 19 && this.ball < 21) {
                    this.actionResult = Action.Reset;
                } else if (this.ball < 19) {
                    this.actionResult = Action.EndTurn;
                }
                previousBowl = BowlResult.Strike;
                break;

            case BowlResult.Spare:
                if (this.ball == 20) {
                    actionResult = Action.Reset;
                } else if (this.ball < 20) {
                    actionResult = Action.EndTurn;
                }
                previousBowl = BowlResult.Spare;
                break;

            case BowlResult.UnderTen:
                if (this.ball % 2 != 0 && this.ball != 21) { // If middle of frame
                    actionResult = Action.Tidy;
                } else if (this.ball % 2 == 0) {  // If end of frame
                    if (this.ball == 20) {
                        if (previousBowl == BowlResult.Strike) {
                            actionResult = Action.Tidy;
                        } else {
                            actionResult = Action.EndGame;
                            }
                    } else {
                        actionResult = Action.EndTurn;
                    }
                }
                previousBowl = BowlResult.UnderTen;
                break;

            default:
                throw new UnityException("Not sure what action to return.");
        }

        this.ball++;
        return actionResult;
    }

    private BowlResult CheckBowlResult(int pins) {

        if (pins > 10 || pins < 0) {
            throw new UnityException("Invalid pin count argument");
        }

        if (pins == 10) {
            return BowlResult.Strike;
        }

        return CheckForSpare(pins);
    }

    private BowlResult CheckForSpare(int pins) {
        if (this.ball > 1) {
            int previousBall = this.balls[this.ball - 2];
            if (pins + previousBall == 10) {
                return BowlResult.Spare;
            }
        }
        return BowlResult.UnderTen;
    }

    private void AddToBowls(int pins) {
        this.balls[this.ball - 1] = pins;
    }
}
