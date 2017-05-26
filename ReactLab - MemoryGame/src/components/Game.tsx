import * as React from "react";

import { Board } from "./Board";
import { Player } from "./Player";

export interface GameProps {
}

export interface GameState {
    matchesRemaining: number;
    playerTurnIndex: number;
    playerScores: Array<number>;
}

export class Game extends React.Component<GameProps, GameState> {

    private _cardCount: number = 16;
    private _columns: number = 4;

    constructor(props: GameProps) {
        super(props);

        this.state = {
            matchesRemaining: this._cardCount / 2,
            playerTurnIndex: 0,
            playerScores: [0, 0]
        };

        this._onMatch = this._onMatch.bind(this);
        this._onMismatch = this._onMismatch.bind(this);
    }

    public render() {

        let { playerScores, playerTurnIndex, matchesRemaining } = this.state;

        return <div className="game">
            <div className="scores">
                <Player name="Player 1" score={playerScores[0]} active={playerTurnIndex === 0} />
                <Player name="Player 2" score={playerScores[1]} active={playerTurnIndex === 1} />
            </div>
            <Board count={this._cardCount} columns={this._columns} onMatch={this._onMatch} onMismatch={this._onMismatch} />
        </div>
    }

    private _onMatch() {

        let { playerScores, playerTurnIndex, matchesRemaining } = this.state;

        console.log('_onMatch ' + playerTurnIndex);

        playerScores[playerTurnIndex] += 1;
        matchesRemaining -= 1;

        this.setState({
            playerScores: playerScores,
            matchesRemaining: matchesRemaining
        });
    }

    private _onMismatch() {
        let { playerTurnIndex, matchesRemaining } = this.state;

        console.log('_onMismatch ' + playerTurnIndex);

        playerTurnIndex = (playerTurnIndex + 1) % 2;
        matchesRemaining -= 1;

        this.setState({
            playerTurnIndex: playerTurnIndex,
            matchesRemaining: matchesRemaining
        });
    }
}
