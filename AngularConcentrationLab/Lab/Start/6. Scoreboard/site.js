
// I generated an NxN 2-dimensional array of cards
function generateCardGrid(size) {

    // I create pairs of cards
    var cards = [];
    for (var i = 0; i < (size * size) ; i++) {
        cards[i] = {
            index: i,
            value: Math.floor(i / 2)
        }
    }

    // I use underscore to shuffle (Fisher-Yates)
    var cards = _.shuffle(cards);

    // I lay the cards out in a size/size grid
    var grid = [];
    for (var i = 0; i < size; i++) {
        grid[i] = [];
        for (var j = 0; j < size; j++) {
            grid[i][j] = cards[(i * size) + j];
        };
    }

    return grid;
}

// I create the application
var app = angular.module('concentrationApp', []);

// I create the controller
app.controller('MainCtrl', function ($scope, $timeout) {       

    $scope.startGame = function () {

        $scope.cardGrid = generateCardGrid(6);

        $scope.player1 = {
            name: 'Player 1',
            score: 0
        };

        $scope.player2 = {
            name: 'Player 2',
            score: 0
        };

        $scope.currentPlayer = $scope.player1;
    }

    function nextTurn() {       
    }


    var cardSelection1, cardSelection2;

    $scope.toggleCard = function (card) {
        if (!cardSelection1) {
            cardSelection1 = card;
            cardSelection1.revealed = true;
        }
        else if (!cardSelection2 && cardSelection1 != card) {
            cardSelection2 = card;
            cardSelection2.revealed = true;

            $timeout(function () {
                if (cardSelection1.value == cardSelection2.value)
                {
                    cardSelection1.matched = true;
                    cardSelection2.matched = true;
                    cardSelection1 = undefined;
                    cardSelection2 = undefined;

                    $scope.currentPlayer.score++;
                    nextTurn();
                }
                else {

                    cardSelection1.mismatched = true;
                    cardSelection2.mismatched = true;

                    $timeout(function () {
                        cardSelection1.revealed = false;
                        cardSelection2.revealed = false;
                        cardSelection1.mismatched = undefined;
                        cardSelection2.mismatched = undefined;
                        cardSelection1 = undefined;
                        cardSelection2 = undefined;

                        nextTurn();
                    }, 500);
                    
                }
            }, 500);

        }
    };

    $scope.startGame();
});

app.directive('scoreboard', function() {
    return {               
        replace: true,
        restrict: 'E',        
        scope: false,
        templateUrl: 'scoreboard.html'
    }
});

app.directive('player', function () {
    return {       
        replace: true,
        restrict: 'E',
        scope: {
            player: '=',
            currentPlayer: '='
        },
        templateUrl: 'player.html'
    }
});





