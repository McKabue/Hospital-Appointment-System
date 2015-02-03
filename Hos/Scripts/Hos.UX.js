//jQuery(document).ready(function ($) {

//////////////////BOOTSTRAP COLLAPSE////////////////
$('.collapse').on('show.bs.collapse', function () {
    $('.collapse.in').collapse('hide');
});

////////////////////SELECT2////////////////////////
$("#tags1,#tags2").select2({
    allowClear: true,
    placeholder: "Write Tags Here...",
    //createSearchChoice:function(term, data) {return {id:term, text:term};},
    //createSearchChoice:function(term, data) { if ($(data).filter(function() { return this.text.localeCompare(term)===0; }).length===0) {return {id:term, text:term};} },
    multiple: true,
    tags: [],
    tokenSeparators: [","]
    //maximumInputLength: 15,
    //maximumSelectionSize: 5
    //data: [{id: 0, text: 'story', locked: true},{id: 1, text: 'bug'},{id: 2, text: 'task'}]
});

//////////////BOOTSTRAP PROGRESS BAR///////////////
var progress = setInterval(function () {
    var $bar = $('.progress-bar');
    $('#pleaseWaitDialog').modal();

    if ($bar.width() >= 800) {
        //clearInterval(progress);
        //$('#pleaseWaitDialog').modal('hide');
        //$('.progress').removeClass('animate');
    } else {
        $bar.width($bar.width() + 80);
    }
    $bar.text($bar.width() / 8 + 10 +"%");
}, 1200);

////////////////BOOTSTRAP SELECT///////////////////$('.selectpicker').selectpicker();

////////////////LOGIN MODAL///////////////////
$('.close').click(function(){
    $('#mustLogin').text('');
});
//});