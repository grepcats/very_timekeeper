function startTimer() {
    let task = allTasks[0];
    let tasks = allTasks;
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
        console.log(seconds_left)
        document.title = hr + ":" + min + ":" + sec;
       
        if (seconds_left <= 0) {
            task.timeRemaining = 0;
            $.ajax({
                url: $(".start").attr('data-request-url'),
                type: 'POST',
                dataType: 'json',
                data: { 'incomingId': task.taskId, 'incomingContent': task.content, 'incomingTimeRemaining': task.timeRemaining }
            });
            setTimeout(resetTasks, 1000);
            setTimeout(createFinishedTaskList, 2000);
            setTimeout(startTimer, 3000);
            clearInterval(interval);
        };
    }, 1000);

   

    //createFinishedTaskList();
    //startTimer();
}



function resetTasks() {
    var htmlTaskIds = $(".single-task").map(function () {
        return this.id;
    }).toArray().toString();
    //console.log(htmlTaskIds);
    //console.log($(".reset").attr('data-request-url'))
    $.ajax({
        url: $(".reset").attr('data-request-url'),
        type: 'GET',
        dataType: 'html',
        data: { 'taskIds': htmlTaskIds },
        success: function (result) {
            //console.log(result);
            $('#all-tasks').html(result);
        }
    });

    
    console.log("resettasks run");
    

    
}

function createFinishedTaskList() {
    $.ajax({
        url: $("#finished-tasks").attr('data-request-url'),
        type: 'GET',
        dataType: 'html',
        success: function (result) {
            //console.log(result);
            $('#finished-tasks').html(result);
        }
    });

    console.log("createfinishedtasklist run");
}
