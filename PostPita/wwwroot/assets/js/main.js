$(document).ready(function () {

    // required skills tags start
    $('.add-tag').on('keyup', function (event) {
        var keyboardKey = event.keyCode || event.which;
        if (keyboardKey === 188) {
            var thisValue = $(this).val();
            //.slice(0, -1);
            $(this).siblings('.tags').append('<span class="tag-span">' + thisValue + '<i class="fas fa-times"></i></span>');
            $(this).val('');

            var vals = $('.tag-span').text();
            $('.tag-value').val(vals.slice(0, -1));

            //$(".tag-value").val($(".tag-value").val()+","+thisValue);
        }
    });

    $('.tags').on('click', '.tag-span i', function () {
        $(this).parent('.tag-span').remove();

        var vals = $('.tag-span').text();
        $(".tag-value").val(vals.slice(0, -1));

    });
    // required skills tags end

    // posted jobs modal start
    //to reduce text in post
    $(".mainSectionPostedJobs .PostedJobsContent").each(function () {
        var allDesc = $(this).find(".PostedJobsDescription").text();
        $(this).find(".allDescription").text(allDesc);

        if ($(this).find(".PostedJobsDescription").text().length > 140) {
            var trimmedText = $(this).find(".PostedJobsDescription").text().substr(0, 140);
            $(this).find(".PostedJobsDescription").text(trimmedText + "....");
        }
    });

    //call data to details modal
    $('.mainSectionPostedJobs .PostedJobsDetails').click(function () {
        var id = $(this).parents(".PostedJobsContent").find(".postID").text();
        $.ajax({
            type: "POST",
            url: '/Search/PostModal/' + id,
            dataType: 'html',
            success: function (data) {
                $('.mainSectionPostedJobs .modal-body').html(data);
            }
        });
    });
    // posted jobs modal end

    // select an specific applicant start
    $(".chBox").on("click", ".selectApplicant", function () {
        if ($(".specificApplicant").attr("disabled")) {
            $(".specificApplicant").removeAttr("disabled");
        }
        else {
            $(".specificApplicant").attr("disabled", "disabled");
        }
    });

    //toastr options
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": false,
        "positionClass": "toast-bottom-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    //ajax to send random notification 
    $("#testAjax").click(function (e) {
        e.preventDefault();
        $.ajax({
            //type: "GET",
            dataType: "json",
            url: 'https://jsonplaceholder.typicode.com/todos',
            success: function (Response) {
                toastr.success(Response[Math.floor(Math.random() * Response.length)].title, 'test ajax');
            }

        });

    });

    //remove img drag
    $('img').on('dragstart', function (event) { event.preventDefault(); });

});

function timeSince(date) {

    var seconds = Math.floor((new Date() - date) / 1000);

    var interval = Math.floor(seconds / 31536000);

    if (interval > 1) {
        return interval + " years";
    }
    interval = Math.floor(seconds / 2592000);
    if (interval > 1) {
        return interval + " months";
    }
    interval = Math.floor(seconds / 86400);
    if (interval > 1) {
        return interval + " days";
    }
    interval = Math.floor(seconds / 3600);
    if (interval > 1) {
        return interval + " hours";
    }
    interval = Math.floor(seconds / 60);
    if (interval > 1) {
        return interval + " minutes";
    }
    return Math.floor(seconds) + " seconds";
}

function time_ago(time) {

    switch (typeof time) {
        case 'number':
            break;
        case 'string':
            time = +new Date(time);
            break;
        case 'object':
            if (time.constructor === Date) time = time.getTime();
            break;
        default:
            time = +new Date();
    }
    var time_formats = [
        [60, 'seconds', 1], // 60
        [120, '1 minute ago', '1 minute from now'], // 60*2
        [3600, 'minutes', 60], // 60*60, 60
        [7200, '1 hour ago', '1 hour from now'], // 60*60*2
        [86400, 'hours', 3600], // 60*60*24, 60*60
        [172800, 'Yesterday', 'Tomorrow'], // 60*60*24*2
        [604800, 'days', 86400], // 60*60*24*7, 60*60*24
        [1209600, 'Last week', 'Next week'], // 60*60*24*7*4*2
        [2419200, 'weeks', 604800], // 60*60*24*7*4, 60*60*24*7
        [4838400, 'Last month', 'Next month'], // 60*60*24*7*4*2
        [29030400, 'months', 2419200], // 60*60*24*7*4*12, 60*60*24*7*4
        [58060800, 'Last year', 'Next year'], // 60*60*24*7*4*12*2
        [2903040000, 'years', 29030400], // 60*60*24*7*4*12*100, 60*60*24*7*4*12
        [5806080000, 'Last century', 'Next century'], // 60*60*24*7*4*12*100*2
        [58060800000, 'centuries', 2903040000] // 60*60*24*7*4*12*100*20, 60*60*24*7*4*12*100
    ];
    var seconds = (+new Date() - time) / 1000,
        token = 'ago',
        list_choice = 1;

    if (seconds == 0) {
        return 'Just now'
    }
    if (seconds < 0) {
        seconds = Math.abs(seconds);
        token = 'from now';
        list_choice = 2;
    }
    var i = 0,
        format;
    while (format = time_formats[i++])
        if (seconds < format[0]) {
            if (typeof format[2] == 'string')
                return format[list_choice];
            else
                return Math.floor(seconds / format[2]) + ' ' + format[1] + ' ' + token;
        }
    return time;
}