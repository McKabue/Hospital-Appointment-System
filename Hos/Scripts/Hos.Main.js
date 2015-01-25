jQuery(document).ready(function ($) {




    var viewModel = function () {
        var self = this;


        self.GetToken = function (context) {
            if (!$('input[name="username"]', context).val() == "" && !$('input[name="password"]', context).val() == "") {
                var LoginData = "grant_type=password&userName=" + $('input[name="username"]', context).val() + "&password=" + $('input[name="password"]', context).val();
                $.ajax({
                    url: "/token",
                    data: LoginData,
                    ContentType: "application/x-www-form-urlencoded",
                    cache: false,
                    type: 'POST',
                    success: function (result) {
                        //Token = result.access_token;
                        var kookie = result.access_token;
                        $.removeCookie('cookieToken', { path: '/' });
                        $.cookie('cookieToken', kookie, { expires: 1, path: '/' });
                        // $('#token').text(token);
                    },
                    error: function () {
                        alert("Error");
                    },
                    statusCode: {
                        200: function () { $('#loginModal').modal('hide'); },
                        400: function () { alert("400: Bad request"); }
                    }
                });
            }
        }


        self.SignUp = function (context) {
            if (!$('input[name="username"]', context).val() == "" && !$('input[name="firstname"]', context).val() == "" && !$('input[name="lastname"]', context).val() == "" && !$('input[name="email"]', context).val() == "" && !$('input[name="password"]', context).val() == "" && !$('input[name="confirmpassword"]', context).val() == "") {
                var SignUpData = "userName=" + $('input[name="username"]', context).val() + "&firstName=" + $('input[name="firstname"]', context).val() + "&lastName=" + $('input[name="lastname"]', context).val() + "&email=" + $('input[name="email"]', context).val() + "&password=" + $('input[name="password"]', context).val() + "&confirmPassword=" + $('input[name="confirmpassword"]', context).val();
                $.ajax({
                    url: "/api/account/register",
                    data: SignUpData,
                    ContentType: "application/x-www-form-urlencoded",
                    cache: false,
                    type: 'POST',
                    success: function () { },
                    error: function (err) { },
                    statusCode: {
                        200: function () { alert("200: User has been Added"); },
                        400: function () { alert("400: Bad request"); }
                    }
                });
            }
        }

    }
    ko.applyBindings(new viewModel());

});