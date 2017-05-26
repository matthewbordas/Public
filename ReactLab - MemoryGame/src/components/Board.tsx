import * as React from "react";
import * as Lodash from "lodash";
import * as Q from "q";

import { Card, CardProps, CardDisplayStatus } from "./Card";

export interface BoardProps {
    count: number,
    columns: number,
    onMatch: () => void,
    onMismatch: () => void
}

export interface BoardState {
    cards: Array<CardProps>
}

export class Board extends React.Component<BoardProps, BoardState> {

    private _isDisplayingMatchStatus: boolean;
    private _selectedCard1: CardProps;
    private _selectedCard2: CardProps;

    constructor(props: BoardProps) {
        super(props);

        this._onCardClick = this._onCardClick.bind(this);
    }

    private componentWillMount() {
        this._initializeBoard();
    }

    private componentWillReceiveProps(nextProps: BoardProps) {
        if (this.props.count !== nextProps.count) {
            this._initializeBoard();
        }
    }

    private _initializeBoard() {

        let { count, columns } = this.props;

        if (count <= 0) {
            throw new RangeError('Count is less than or equal to zero.')
        }
        if (columns <= 0) {
            throw new RangeError('Columns is less than or equal to zero.')
        }

        if (count % 2 !== 0) {
            throw new RangeError('Count is not an even number.')
        }

        // I create pairs of cards
        let values = new Array<number>();
        for (let i = 0; i < count / 2; i++) {
            values.push(i);
            values.push(i);
        };

        // I use underscore to shuffle (Fisher-Yates)
        values = Lodash.shuffle(values);

        let cards = values.map((value, index) => {
            return {
                id: index,
                value: value,
                status: CardDisplayStatus.Normal,
                onClick: this._onCardClick
            } as CardProps
        });

        this.setState({
            cards: cards
        });
    }

    public render() {

        let { columns } = this.props;
        let { cards } = this.state;

        let renderColumns = (cards: Array<CardProps>) => {
            return cards.map((card: CardProps) => {
                return <Card key={card.id} id={card.id} value={card.value} status={card.status} onClick={card.onClick} />
            });
        };

        let renderRows = () => {

            let rows = Lodash.chunk(cards, columns);

            return rows.map((cards: Array<CardProps>, row: number) => {
                let key = 'row_' + row;
                return <div key={key} className="row">{renderColumns(cards)}</div>
            });
        };

        return <div className="board">
            {renderRows()}
        </div>
    }

    private _showMatch() {
        let { onMatch } = this.props;
        let { cards } = this.state;

        Q.delay(1000).then(() => {

            this._selectedCard1.status = CardDisplayStatus.Matched;
            this._selectedCard2.status = CardDisplayStatus.Matched;
            this.setState({
                cards: cards
            });

            this._selectedCard1 = undefined;
            this._selectedCard2 = undefined;
            this._isDisplayingMatchStatus = false;

            if (onMatch) {
                onMatch();
            }
        });
    }

    private _showMismatch() {

        let { onMismatch } = this.props;
        let { cards } = this.state;

        Q.delay(1000).then(() => {

            this._selectedCard1.status = CardDisplayStatus.Mismatched;
            this._selectedCard2.status = CardDisplayStatus.Mismatched;
            this.setState({
                cards: cards
            });

            Q.delay(1000).then(() => {

                this._selectedCard1.status = CardDisplayStatus.Normal;
                this._selectedCard2.status = CardDisplayStatus.Normal;
                this.setState({
                    cards: cards
                });

                this._selectedCard1 = undefined;
                this._selectedCard2 = undefined;
                this._isDisplayingMatchStatus = false;

                if (onMismatch) {
                    onMismatch();
                }
            });
        });
    }

    public _onCardClick(id: number) {

        let { cards } = this.state;

        if (this._isDisplayingMatchStatus) {
            return;
        }

        let clickedCard = cards[id];

        if (clickedCard.status == CardDisplayStatus.Matched) {
            return;
        }

        this._isDisplayingMatchStatus = true;

        if (this._selectedCard1 == undefined) {

            this._selectedCard1 = clickedCard;
            this._selectedCard1.status = CardDisplayStatus.Revealed;
            this.setState({
                cards: cards
            });

            this._isDisplayingMatchStatus = false;
        }
        else if (this._selectedCard1.id != id && this._selectedCard2 == undefined) {

            this._selectedCard2 = clickedCard;
            this._selectedCard2.status = CardDisplayStatus.Revealed;
            this.setState({
                cards: cards,
            });

            if (this._selectedCard1.value == this._selectedCard2.value) {
                this._showMatch();
            }
            else {
                this._showMismatch();
            }
        }
    }
} 
