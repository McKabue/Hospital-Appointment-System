﻿/// <reference path="jquery-2.1.1.intellisense.js" />





//jQuery(document).ready(function ($) {




//////////////////BOOTSTRAP COLLAPSE////////////////
    $('body').on('show.bs.collapse', '.collapse', function () {
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
/*var progress = setInterval(function () {
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
}, 1200);*/

////////////////BOOTSTRAP SELECT///////////////////$('.selectpicker').selectpicker();

////////////////LOGIN MODAL///////////////////
$('.close').click(function(){
    $('#mustLogin').text('');
});

////////////////Reschedule page///////////////////
$('table').on('click', '.buttonActiveClass li a', function (e) {
	    $('.buttonActiveClass').find('li.active').removeClass('active');
	    $(this).addClass('active');
	    if($(this).text() == "Select"){
	        $(this).parent().parent().parent().siblings().find('input[type=checkbox]').prop('checked', 'checked');
	        if ($(this).parent().parent().parent().siblings().find('input[type=checkbox]').is(':checked')){
	            $(this).parent().parent().parent().parent().addClass('highlighted');
	            $(this).parent().parent().parent().parent().siblings().addClass('highlighted');
	            $('.collapse.in').collapse('hide');
	            $(this).parent().parent().parent().find('button').addClass('disabled');
	        }
	    }
	   /* if($(this).text() == "Delete"){
	        var c = confirm('Are you sure to delete this Appointment?');
	        if(c){
	            $('.collapse.in').collapse('hide');
	            $(this).parents('tr').remove();
	        }
	    }*/
	  
	})

		$('#filter').keyup(function () {

		    var rex = new RegExp($(this).val(), 'i');
		    var here = $('tbody input[type=checkbox]').parent().parent().siblings('td');
		    $('.collapse.in').collapse('hide');
			
		    //$('tbody input[type=checkbox]').parent().parent('td').hide();
		    $('tbody input[type=checkbox]').parents('tr').hide();

		    $('tbody input[type=checkbox]').parents('tr').filter(function () {
		        return rex.test($(this).text());
		    }).show();
			
          

		})

	$('#checkAll').click(function(){
	    if ($(this).is(':checked')){
	        $('.collapse.in').collapse('hide');
	        $('tbody input[type=checkbox]').prop('checked', true);
	        $('tbody input[type=checkbox]').parents('td').addClass('highlighted');
	        $('tbody input[type=checkbox]').parents().siblings('td').addClass('highlighted');
	        $('tbody input[type=checkbox]').parents().find('button').addClass('disabled');
	        $('#deleteAll').removeClass('disabled');
	    }else if (!$(this).is(':checked')){
	        $('tbody input[type=checkbox]').prop('checked', false);
	        $('tbody td').removeClass('highlighted');
	        $('tbody button').removeClass('disabled');
	    }
		
	});

    $('#deleteAll').click(function(){
        var del = $('tbody tr').find('input[type=checkbox]');
        //var c = confirm('You are about to delete ALL Appointments...Are you Sure?');
        //if(c){
        del.each(function(){
            if($(this).is(':checked')) {
                $(this).parents('tr').remove();;
            }
        });
        //}
    });

    $('table').on('click', 'td input[type="checkbox"]', function () {
        if ($(this).is(':checked')) {
            $(this).parent().parent().addClass('highlighted');
            $(this).parent().parent().siblings().addClass('highlighted');
            $('.collapse.in').collapse('hide');
            $(this).parent().parent().find('button').addClass('disabled');
        } else if ($(this).parent().parent().is('.highlighted')) {
            $(this).parent().parent().removeClass('highlighted');
            $(this).parent().parent().siblings().removeClass('highlighted');
            $(this).parent().parent().find('button').removeClass('disabled');
        }
    });


    
    $('select[name="loginRole"]').on('change', function () {
        if ($('option:selected', this).text() == 'ADMIN') {
            //$('input[name="loginRegistrationNumber"]').attr('pattern', "[A-Za-z0-9]{10}");
            //$('input[name="loginRegistrationNumber"]').attr('oninvalid', "setCustomValidity('Hey Admin, Enter  a Valid Username!')");
            $('input[name="loginRegistrationNumber"]').attr('pattern', "");
            $('input[name="loginRegistrationNumber"]').attr('oninvalid', "");
            $('#rolespan').text('User Name');
            $('input[name="loginRegistrationNumber"]').attr('placeholder', "User Name");
        }
        if ($('option:selected', this).text() == 'DOCTOR') {
            //$('input[name="loginRegistrationNumber"]').attr('pattern', "[A-Za-z0-9]{10}");
            //$('input[name="loginRegistrationNumber"]').attr('oninvalid', "setCustomValidity('Hey Admin, Enter  a Valid Username!')");
            $('input[name="loginRegistrationNumber"]').attr('pattern', "");
            $('input[name="loginRegistrationNumber"]').attr('oninvalid', ""); 
            $('#rolespan').text('User Name');
            $('input[name="loginRegistrationNumber"]').attr('placeholder', "User Name");
        }
        if ($('option:selected', this).text() == 'STUDENT') {
            $('input[name="loginRegistrationNumber"]').attr('pattern', "(?:[A-Za-z]{2}[0-9]{2}|[A-Za-z]{2}[Pp]{1}[0-9]{2})\/[0-9]{5}\/([1-2]{1}[90]{1}[0-9]{2}|[901]{1}[0-9]{1})");
            $('input[name="loginRegistrationNumber"]').attr('oninvalid', "setCustomValidity('Please enter a valid Admission Number!')");
            $('#rolespan').text('Reg. Number');
            $('input[name="loginRegistrationNumber"]').attr('placeholder', "Registration Number");
        }
    });

    $('select[name="registerRole"]').on('change', function () {
        if ($('option:selected', this).text() == 'ADMIN') {
            //$('input[name="loginRegistrationNumber"]').attr('pattern', "[A-Za-z0-9]{10}");
            //$('input[name="loginRegistrationNumber"]').attr('oninvalid', "setCustomValidity('Hey Admin, Enter  a Valid Username!')");
            $('input[name="registration_number"]').attr('pattern', "");
            $('input[name="registration_number"]').attr('oninvalid', "");
            $('input[name="registration_number"]').text("");
            $('#rolespan2').text('User Name');
            $('input[name="registration_number"]').attr('placeholder', "User Name");
        }
        if ($('option:selected', this).text() == 'DOCTOR') {
            //$('input[name="loginRegistrationNumber"]').attr('pattern', "[A-Za-z0-9]{10}");
            //$('input[name="loginRegistrationNumber"]').attr('oninvalid', "setCustomValidity('Hey Admin, Enter  a Valid Username!')");
            $('input[name="registration_number"]').attr('pattern', "");
            $('input[name="registration_number"]').attr('oninvalid', "");
            $('input[name="registration_number"]').text("");
            $('#rolespan2').text('User Name');
            $('input[name="registration_number"]').attr('placeholder', "User Name");
        }
        if ($('option:selected', this).text() == 'STUDENT') {
            $('input[name="registration_number"]').attr('pattern', "(?:[A-Za-z]{2}[0-9]{2}|[A-Za-z]{2}[Pp]{1}[0-9]{2})\/[0-9]{5}\/([1-2]{1}[90]{1}[0-9]{2}|[901]{1}[0-9]{1})");
            $('input[name="registration_number"]').attr('oninvalid', "setCustomValidity('Please enter a valid Admission Number!')");
            $('input[name="registration_number"]').text("");
            $('#rolespan2').text('Reg. Number');
            $('input[name="registration_number"]').attr('placeholder', "Registration Number");
        }
    });


////////////////LOGIN and LOGOUT switch///////////////////
    /*if (!$.cookie('cookieToken') == null) {
        $('#login a[href=#loginModal]').text('Logout');
        and i will add a loading bar
    }*/ 



















//});