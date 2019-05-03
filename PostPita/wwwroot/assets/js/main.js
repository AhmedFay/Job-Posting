$(document).ready(function(){

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

    // select an specific applicant start
    $(".chBox").on("click", ".selectApplicant", function(){
        if( $(".specificApplicant").attr("disabled") ){
            $(".specificApplicant").removeAttr("disabled");
        }
        else{
            $(".specificApplicant").attr("disabled", "disabled");
        }
    });

    //remove img drag
    $('img').on('dragstart', function(event) { event.preventDefault(); });

});