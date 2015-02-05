//jQuery(document).ready(function ($) {

var token;


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
        
           // if (!$('#surName', context).val() == "" && !$('#firstName', context).val() == "" && !$('#lastName', context).val() == "" && !$('#National_ID_Number', context).val() == "" && !$('#Registration_Number', context).val() == "" && !$('#Birth_Date', context).val() == "" && !$('#Program option:selected', context).text() == "" && !$('#Year option:selected', context).text() == "" && !$('#Semester option:selected', context).text() == "" && !$('#Faculty option:selected', context).text() == "" && !$('#Course option:selected', context).text() == "" && feelings !== "[]" && causes !== "[]" && !$('#Medical_Type option:selected', context).text() == "" && !$('#Doctor option:selected', context).text() == "") {
                var JSONAppointmentData = "\"Surname\":\"" + $('#surName', context).val() + "\", \"FirstName\":\"" + $('#firstName', context).val() + "\", \"LastName\":\"" + $('#lastName', context).val() + "\", \"National_ID_Number\":\"" + $('#National_ID_Number', context).val() + "\", \"Registration_Number\":\"" + $('#Registration_Number', context).val() + "\", \"Birth_Date\":\"" + $('#Birth_Date', context).val() + "\", \"Program\":\"" + $('#Program option:selected', context).text() + "\", \"Year\":\"" + $('#Year option:selected', context).text() + "\", \"Semester\":\"" + $('#Semester option:selected', context).text() + "\", \"Faculty\":\"" + $('#Faculty option:selected', context).text() + "\", \"Course\":\"" + $('#Course option:selected', context).text() + "\", \"Medical_Type\":\"" + $('#Medical_Type option:selected', context).text() + "\", \"Doctor\":\"" + $('#Doctor option:selected', context).text() + "\", \"Feelings\":" + feelings + "\, \"Causes\":" + causes;
                var JSONAppointmentData2 = { "Surname": $('#surName', context).val(), "FirstName": $('#firstName', context).val(), "LastName": $('#lastName', context).val(), "National_ID_Number": $('#National_ID_Number', context).val(), "Registration_Number": $('#Registration_Number', context).val(), "Birth_Date": $('#Birth_Date', context).val(), "Program": $('#Program option:selected', context).text(), "Year": $('#Year option:selected', context).text(), "Semester": $('#Semester option:selected', context).text(), "Faculty": $('#Faculty option:selected', context).text(), "Course": $('#Course option:selected', context).text(), "Medical_Type": $('#Medical_Type option:selected', context).text(), "Doctor": $('#Doctor option:selected', context).text(), "Feelings": $('#tags1', context).select2("val"), "Possible_Causes": $('#tags2', context).select2("val") };
            // alert(JSONAppointmentData);
                $.ajax({
                    url: "/api/Appointment/AppointmentData",
                    data: JSONAppointmentData2, // {"Registration_Number": "ID475478/RUR", "JObject": [{ "Registration_Number": "29", "value": "Country" }, { "Registration_Number": "30", "value": "4,3,5" }] },
                    dataType: 'json',
                    ContentType: "application/json",
                    cache: false,
                    headers: { authorization: "Bearer   " + token },
                    type: 'POST',
                    success: function () { $('#loginModal').modal('hide'); },
                    error: function (err) { alert(ko.toJSON(err)); },
                    statusCode: {
                        200: function (data) { alert(ko.toJSON(data)); console.log(ko.toJSON(data)); },
                        401: function () {
                            $('#loginModal').modal('show');
                            $('#mustLogin').text('You need to First Login / Register');
                        }
                    }
                });
           // }
        }






















        self.GetToken = function (context) {
            if (!$('input[name="loginRegistrationNumber"]', context).val() == "" && !$('input[name="loginPassword"]', context).val() == "") {
                var LoginData = "grant_type=password&userName=" + $('input[name="loginRegistrationNumber"]', context).val() + "&password=" + $('input[name="loginPassword"]', context).val();
                $.ajax({
                    url: "/token",
                    data: LoginData,
                    ContentType: "application/x-www-form-urlencoded",
                    cache: false,
                    type: 'POST',
                    success: function (result) {
                        alert(ko.toJSON(result));
                        token = result.access_token;
                        alert(token);
                        //var kookie = result.access_token;
                        //$.removeCookie('cookieToken', { path: '/' });
                        //$.cookie('cookieToken', kookie, { expires: 1, path: '/' });
                        // $('#token').text(token);
                    },
                    error: function () {
                        alert("Error");
                    },
                    statusCode: {
                        200: function () {
                            $('#loginModal').modal('hide');
                        },
                        400: function () { alert("400: Bad request"); }
                    }
                });
            }
        }
        self.SignUp = function (context) {
            if (!$('input[name="surname"]', context).val() == "" && !$('input[name="firstname"]', context).val() == "" && !$('input[name="lastname"]', context).val() == "" && !$('input[name="registration_number"]', context).val() == "" && !$('input[name="password"]', context).val() == "" && !$('input[name="confirmpassword"]', context).val() == "") {
                var SignUpData = "surName=" + $('input[name="surname"]', context).val() + "&firstName=" + $('input[name="firstname"]', context).val() + "&lastName=" + $('input[name="lastname"]', context).val() + "&userName=" + $('input[name="registration_number"]', context).val() + "&password=" + $('input[name="password"]', context).val() + "&confirmPassword=" + $('input[name="confirmpassword"]', context).val();
                alert(ko.toJSON(SignUpData));
                $.ajax({
                    url: "/api/account/register",
                    data: SignUpData,
                    ContentType: "application/x-www-form-urlencoded",
                    cache: false,
                    type: 'POST',
                    success: function () { },
                    error: function (err) { alert(err); },
                    statusCode: {
                        200: function () {
                            $('#mustLogin').text('You Registered Successfully; now You just need to login...');
                            $('#myLoginTab li:eq(0) a').tab('show');
                        },
                        400: function () { alert("400: Bad request"); }
                    }
                });
            }
        }
        self.loadOptionsData();
    }
    ko.applyBindings(new viewModel());
//});