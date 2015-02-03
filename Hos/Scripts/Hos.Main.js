//jQuery(document).ready(function ($) {
    function Program(Name, Years) {
        var self = this;
        self.name = ko.observable(Name);
        self.years = ko.observable(Years);
        self.semesters = ko.observable();
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

                        if (datas != null) {
                            clearInterval(progress);
                            $('#pleaseWaitDialog').modal('hide');
                        }

                        $.each(datas, function (index, data) {
                            //alert(ko.toJSON(data.User));
                            $.each(data.User, function (index, user) {
                                $('#firstName').val(user.FirstName).attr('disabled', true);
                            });
                            $.each(data.Programs, function (index, program) {
                                self.program.push(new Program(program.Name, program.Years));
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

        self.sendAppointmentData = function (context) {
            var feelings = ko.toJSON($('#tags1', context).select2("val"));
            var causes = ko.toJSON($('#tags2', context).select2("val"));
        
            if (!$('#surName', context).val() == "" && !$('#firstName', context).val() == "" && !$('#lastName', context).val() == "" && !$('#National_ID_Number', context).val() == "" && !$('#Registration_Number', context).val() == "" && !$('#Birth_Date', context).val() == "" && !$('#Program option:selected', context).text() == "" && !$('#Year option:selected', context).text() == "" && !$('#Semester option:selected', context).text() == "" && !$('#Faculty option:selected', context).text() == "" && !$('#Course option:selected', context).text() == "" && feelings !== "[]" && causes !== "[]" && !$('#Medical_Type option:selected', context).text() == "" && !$('#Doctor option:selected', context).text() == "") {

                var JSONAppointmentData = "\"Surname\":\"" + $('#surName', context).val() + "\", \"FirstName\":\"" + $('#firstName', context).val() + "\", \"LastName\":\"" + $('#lastName', context).val() + "\", \"National_ID_Number\":\"" + $('#National_ID_Number', context).val() + "\", \"Registration_Number\":\"" + $('#Registration_Number', context).val() + "\", \"Birth_Date\":\"" + $('#Birth_Date', context).val() + "\", \"Program\":\"" + $('#Program option:selected', context).text() + "\", \"Year\":\"" + $('#Year option:selected', context).text() + "\", \"Semester\":\"" + $('#Semester option:selected', context).text() + "\", \"Faculty\":\"" + $('#Faculty option:selected', context).text() + "\", \"Course\":\"" + $('#Course option:selected', context).text() + "\", \"Medical_Type\":\"" + $('#Medical_Type option:selected', context).text() + "\", \"Doctor\":\"" + $('#Doctor option:selected', context).text() + "\", \"Feelings\":" + feelings + "\, \"Causes\":" + causes;
                alert([JSONAppointmentData]);
                $.ajax({
                    url: "/api/account/register",
                    data: [JSONAppointmentData],
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