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
        console.log(hr + ":" + min + ":" + sec)
        document.title = hr + ":" + min + ":" + sec;

        if (seconds_left <= 0) {
            document.getElementById('timer' + task.taskId).innerHTML = 'task done';
            task.timeRemaining = 0;
            //console.log(task.content);
            $('.start').click(function (event) {
                event.preventDefault();
                console.log($(this).attr('data-request-url'));
                
                $.ajax({
                    url: $(this).attr('data-request-url'),
                    type: 'POST',
                    dataType: 'json',
                    data: { 'incomingId': task.taskId, 'incomingContent': task.content, 'incomingTimeRemaining': task.timeRemaining },
                    success: function (result) {
                        $('#result').html("it worked");
                    }
                });

            });

            clearInterval(interval);
        }
    }, 1000);
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
            console.log(result);
            $('#all-tasks').html(result);
        }
    });
}
