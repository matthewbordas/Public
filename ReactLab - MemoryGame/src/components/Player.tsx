import * as React from "react";

export interface PlayerProps {
    name: string,
    score: number,
    active: boolean
}

export interface PlayerState {
}

export class Player extends React.Component<PlayerProps, PlayerState> {

    constructor(props: PlayerProps) {
        super(props);
    }

    public render() {

        let { name, score, active } = this.props;

        let playerClassName = active ? 'player active' : 'player';
        
        return <div className={playerClassName}>
            <div className="name">{name}</div>
            <div className="score">{score}</div>
        </div>;
    }
} 
