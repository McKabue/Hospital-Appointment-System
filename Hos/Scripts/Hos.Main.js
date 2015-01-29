//jQuery(document).ready(function ($) {


function Program(Name, Years) {
        var self = this;
        self.name = ko.observable(Name);
        self.years = ko.observable(Years);
        self.semesters = ko.observable();
       // self.semester = ko.observable(Years.Semesters);
    }

    function Falcuty(Name, Courses) {
        var self = this;
        self.name = ko.observable(Name);
        self.courses = ko.observable(Courses);
    }

    function Medical_Types(Name, Available_Doctors) {
        var self = this;
        self.name = ko.observable(Name);
        self.available_Doctors = ko.observable(Available_Doctors);
    }

    var viewModel = function () {
        var self = this;
        self.program = ko.observableArray();
        //self.year = ko.observableArray();
        //self.Years = ko.observable();
        self.year = ko.observable();
        self.semesters = ko.observable();
        self.falcuty = ko.observableArray();
        self.course = ko.observable();
        self.medical_Type = ko.observableArray();
        self.available_Doctor = ko.observable();
        self.loadOptionsData = function () {
            $.ajax({
                url: "/api/Appointment/Data",
                cache: false,
                type: 'GET',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    200: function (datas) {
                        $.each(datas, function (index, data) {
                            //alert(ko.toJSON(data.User));
                            $.each(data.User, function (index, user) {
                                $('#firstName').val(user.FirstName).attr('disabled', true);
                            });

                            $.each(data.Programs, function (index, program) {
                                self.program.push(new Program(program.Name, program.Years));

                                //$.each(program.Years, function (index, year) {
                                //    self.year.push(new Year(year.Semesters));
                                //});
                            });

                            $.each(data.Faculties, function (index, falcuty) {
                                self.falcuty.push(new Falcuty(falcuty.Name, falcuty.Courses));
                            });

                            $.each(data.Medical_Types, function (index, medical_Type) {
                                self.medical_Type.push(new Medical_Types(medical_Type.Name, medical_Type.Available_Doctors));
                            });
                        });
                        
                       
                    },
                    400: function () { alert("400:"); }
                }
            });
        }









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
                    error: function (err) { alert(err); },
                    statusCode: {
                        200: function () { alert("200: User has been Added"); },
                        400: function () { alert("400: Bad request"); }
                    }
                });
            }
        }




        self.loadOptionsData();
    }
    ko.applyBindings(new viewModel());

//});