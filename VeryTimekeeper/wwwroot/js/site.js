// Write your Javascript code.

function startTimer() {
    let task = allTasks[0];
    let seconds_left = task.timeRemaining;
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
            let completeItem = allTasks.shift();
            clearInterval(interval);
            document.title = 'task done';
            if (allTasks.length > 0) {
                startTimer();
            }
            
        }
    }, 1000);
}

function moveToNext() {

}
        
        

       

//settimer (on model)
//starttimer
//movetonextitem
//
