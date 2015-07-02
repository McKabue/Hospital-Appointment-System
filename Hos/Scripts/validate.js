$(document).ready(function () {
    $('#bookform')
    .bootstrapValidator({
        //live: 'disabled',
        submitButtons: '#submit',
        message: 'Invalid input',
        fields: {
            birthdate: {
                message: 'The date is not valid',
                validators: {
                    notEmpty: {
                        message: 'The date is required and can\'t be empty'
                    },
                    stringLength: {
                        max: 10,
                        message: 'The date must be more than 10 characters'
                    },
                    /*remote: {
                    url: 'remote.php',
                    message: 'The username is not available'
                    },*/
                    regexp: {
                        regexp: /[01]{1}[0-9]{1}\/[0123]{1}[0-9]{1}\/[1-2]{1}[90]{1}[0-9]{2}/,
                        message: '...month/data/year, like this 07/23/1993 '
                    }
                }
            }
        }
    })
    .on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();
        // Get the form instance
        var $form = $(e.target);
        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');
    });
});