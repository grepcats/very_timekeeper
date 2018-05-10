// Write your Javascript code.
var seconds_left = 10;
var interval = setInterval(function () {
    document.getElementById('timer1').innerHTML = --seconds_left;
    document.title = seconds_left;

    if (seconds_left <= 0) {
        document.getElementById('timer1').innerHTML = 'task done';
        clearInterval(interval);
    }
}, 1000);
