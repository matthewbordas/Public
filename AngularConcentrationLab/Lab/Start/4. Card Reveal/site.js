
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
app.controller('MainCtrl', function ($scope) {
    $scope.cardGrid = generateCardGrid(6);    
});





