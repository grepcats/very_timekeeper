// Write your Javascript code.

function startTimer() {
    console.log(allTasks);

    allTasks.forEach(function (task) {
        var seconds_left = 3601;
        

        var interval = setInterval(function () {
            let min = 0;
            let sec = 0;
            let hr = 0;

            document.getElementById('timer' + task.taskId).innerHTML = --seconds_left;
            if (seconds_left >= 3600) {
                hr = Math.floor(seconds_left / 3600)
                min = seconds_left % 60;
                if (min >= 60) {
                    min = Math.floor(seconds_left / 60);
                    sec = seconds_left % 60;
                }
            } else if (seconds_left >= 60) {
                min = Math.floor(seconds_left / 60);
                sec = seconds_left % 60;
            }
            else {
                sec = seconds_left;
            }
            console.log(hr + ":" + min + ":" + sec)
            document.title = hr + ":" + min + ":" + sec;

            if (seconds_left <= 0) {
                document.getElementById('timer' + task.taskId).innerHTML = 'task done';
                clearInterval(interval);
            }
        }, 1000);
    })
}

//settimer (on model)
//starttimer
//movetonextitem
//
