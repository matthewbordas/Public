
// I generate an NxN 2-dimensional array of numbers
function generateCardGrid(size) {

    var grid = [];

    for (var i = 0; i < size; i++) {

        grid[i] = [];

        for (var j = 0; j < size; j++) {
            grid[i][j] = (i * size) + j;
        }
    }

    return grid;
}

// I create the application
var app = angular.module('concentrationApp', []);

// I create the controller
app.controller('MainCtrl', function ($scope) {    
});



