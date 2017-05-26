import * as React from "react";

export enum CardDisplayStatus {
    Normal,
    Revealed,
    Matched,
    Mismatched
}

export interface CardProps {
    id: number,
    value: number,
    status: CardDisplayStatus,
    onClick?: (id: number) => void;
}

export interface CardState {
}

export class Card extends React.Component<CardProps, CardState> {

    constructor(props: CardProps) {
        super(props);
    }

    public render() {

        console.log('card render');

        let { id, value, status, onClick } = this.props;

        onClick = onClick ? onClick : () => { };

        var cardClassName = "card";

        let valueText = '' + value;
        
        switch (status) {
            case CardDisplayStatus.Revealed:
                cardClassName += " revealed";
                break;
            case CardDisplayStatus.Matched:
                cardClassName += " matched";
                break;
            case CardDisplayStatus.Mismatched:
                cardClassName += " mismatched";
                break;
            default:
                valueText = '';
                break;
        }

        return <div className={cardClassName} onClick={() => onClick(id)}>
            <div className="card-value">{valueText}</div>
        </div>
    }
} 
