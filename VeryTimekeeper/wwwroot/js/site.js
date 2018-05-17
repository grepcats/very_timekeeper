function startTimer() {
    if (allTasks.length == 0) {
        return;
    }

    let task = allTasks[0];
    let tasks = allTasks;
    console.log(task);
    let seconds_left = task.timeRemaining;
    var interval = setInterval(function () {
        let min = 0;
        let sec = 0;
        let hr = 0;

		--seconds_left;
		
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
		document.getElementById('timer' + task.taskId).innerHTML = hr + ":" + min + ":" + sec;
        document.title = hr + ":" + min + ":" + sec + " - " + task.content;
       
        if (seconds_left <= 0) {
            task.timeRemaining = 0;
            console.log($(".start").attr('data-request-url'));
            mySound = new sound("../sounds/chime.mp3")
            mySound.play();
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

function sound(src) {
    this.sound = document.createElement("audio");
    this.sound.src = src;
    this.sound.setAttribute("preload", "auto");
    this.sound.setAttribute("controls", "none");
    this.sound.style.display = "none";
    document.body.appendChild(this.sound);
    this.play = function () {
        this.sound.play();
    }
    this.stop = function () {
        this.sound.pause();
    }
}