﻿jQuery(document).ready(function ($) {
    $.connection.hub.start();

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


    function ViewData(AppointmentID, SurName, FirstName, LastName, Registration_Number, National_ID_Number, RoleName, Feelings, Causes, Medical_Type, Doctor, DateTime, Status) {
        var self = this;

        self.appointmentID = ko.observable(AppointmentID);
        self.surname = ko.observable(SurName);
        self.firstName = ko.observable(FirstName);
        self.lastName = ko.observable(LastName);
        self.registration_Number = ko.observable(Registration_Number);
        self.national_ID_Number = ko.observable(National_ID_Number);
        self.roleName = ko.observable(RoleName);
        

        self.feelings = ko.observableArray(Feelings);
        self.causes = ko.observableArray(Causes);

        self.medical_Type = ko.observable(Medical_Type);
        self.doctor = ko.observable(Doctor);
        self.dateTime = ko.observable(DateTime);
        self.status = ko.observable(Status);

        self.whichTemplate = ko.computed(function () {
            return self.status() != 'Not_Confirmed' ? 'y-template' : 'n-template';
        });

        self.AppointmentUrl = ko.computed(function () {
            return "/api/Appointment/Delete?key=" + this.appointmentID();
        }, this);

        
    }


    var viewModel = function () {
        var self = this;

        


















 
        self.role = ko.observable($.cookie('role'));
        self.u = ko.observable($.cookie('user'));
        self.user = ko.computed(function() {
            return self.u() != null ? '<i class="glyphicon glyphicon-user"></i> &nbsp;&nbsp; Hello ' + self.u() : 'Login/Register';
        });

        self.logout = function () {
            $.removeCookie('cookieToken', { path: '/' });
            $.removeCookie('user', { path: '/' });
            $.removeCookie('role', { path: '/' });

            self.u($.cookie('user'));
            self.role($.cookie('role'));
            self.loadOptionsData();
        };

        self.program = ko.observableArray();
        self.year = ko.observable();
        self.semesters = ko.observable();
        self.falcuty = ko.observableArray();
        self.course = ko.observable();
        self.medical_Type = ko.observableArray();
        self.available_Doctor = ko.observable();
        self.ViewDataArray = ko.observableArray();

        self.loadOptionsData = function () {
            self.program([]);
            self.falcuty([]);
            self.medical_Type([]);
            $('#surName').val('').removeAttr('disabled');
            $('#firstName').val('').removeAttr('disabled');
            $('#lastName').val('').removeAttr('disabled');
            $('#National_ID_Number').val('').removeAttr('disabled');
            $('#Registration_Number').val('').removeAttr('disabled');
            $.ajax({
                url: "/api/Appointment/Data",
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'GET',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    200: function (datas) {

                        //if (datas != null) {
                         //   clearInterval(progress);
                         //   $('#pleaseWaitDialog').modal('hide');
                        //}
                        
                       // $('#myTab a[href=#book]').tab('show');
                        //alert(ko.toJSON(datas));
                        $.each(datas, function (index, data) {
                            //alert(ko.toJSON(data.User
                            
                            $.each(data.User, function (index, user) {
                                    $('#surName').val(user.SurName).attr('disabled', true);
                                    $('#firstName').val(user.FirstName).attr('disabled', true);
                                    $('#lastName').val(user.LastName).attr('disabled', true);
                                    $('#National_ID_Number').val(user.National_ID_Number).attr('disabled', true);
                                    $('#Registration_Number').val(user.Registration_Number).attr('disabled', true);
                                
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
                    400: function () { alert("400:"); },
                    401: function () {
                       // clearInterval(progress);
                        //$('#pleaseWaitDialog').modal('hide');
                    },
                    666: function (doc) {
                        //alert(ko.toJSON(doc));
                        $('#myTab a[href=#reschedule]').tab('show');
                        if (doc != null) {
                           // clearInterval(progress);
                           // $('#pleaseWaitDialog').modal('hide');
                        }
                        self.loadEditData();
                    }
                }
            });
        }

        self.sendAppointmentData = function (context) {
            var feelings = ko.toJSON($('#tags1', context).select2("val"));
            var causes = ko.toJSON($('#tags2', context).select2("val"));
        
           // if (!$('#surName', context).val() == "" && !$('#firstName', context).val() == "" && !$('#lastName', context).val() == "" && !$('#National_ID_Number', context).val() == "" && !$('#Registration_Number', context).val() == "" && !$('#Birth_Date', context).val() == "" && !$('#Program option:selected', context).text() == "" && !$('#Year option:selected', context).text() == "" && !$('#Semester option:selected', context).text() == "" && !$('#Faculty option:selected', context).text() == "" && !$('#Course option:selected', context).text() == "" && feelings !== "[]" && causes !== "[]" && !$('#Medical_Type option:selected', context).text() == "" && !$('#Doctor option:selected', context).text() == "") {
                var JSONAppointmentData = "\"Surname\":\"" + $('#surName', context).val() + "\", \"FirstName\":\"" + $('#firstName', context).val() + "\", \"LastName\":\"" + $('#lastName', context).val() + "\", \"National_ID_Number\":\"" + $('#National_ID_Number', context).val() + "\", \"Registration_Number\":\"" + $('#Registration_Number', context).val() + "\", \"Birth_Date\":\"" + $('#Birth_Date', context).val() + "\", \"Program\":\"" + $('#Program option:selected', context).text() + "\", \"Year\":\"" + $('#Year option:selected', context).text() + "\", \"Semester\":\"" + $('#Semester option:selected', context).text() + "\", \"Faculty\":\"" + $('#Faculty option:selected', context).text() + "\", \"Course\":\"" + $('#Course option:selected', context).text() + "\", \"Medical_Type\":\"" + $('#Medical_Type option:selected', context).text() + "\", \"Doctor\":\"" + $('#Doctor option:selected', context).text() + "\", \"Feelings\":" + feelings + "\, \"Causes\":" + causes;
                var JSONAppointmentData2 = { "Surname": $('#surName', context).val(), "FirstName": $('#firstName', context).val(), "LastName": $('#lastName', context).val(), "National_ID_Number": $('#National_ID_Number', context).val(), "Registration_Number": $('#Registration_Number', context).val(), "Birth_Date": $('#Birth_Date', context).val(), "Program": $('#Program option:selected', context).text(), "Year": $('#Year option:selected', context).text(), "Semester": $('#Semester option:selected', context).text(), "Faculty": $('#Faculty option:selected', context).text(), "Course": $('#Course option:selected', context).text(), "Medical_Type": $('#Medical_Type option:selected', context).text(), "Doctor": $('#Doctor option:selected', context).text(), "Feelings": $('#tags1', context).select2("val"), "Possible_Causes": $('#tags2', context).select2("val") };
             alert(JSONAppointmentData);
                $.ajax({
                    url: "/api/Appointment/AppointmentData/Save",
                    data: JSONAppointmentData2, // {"Registration_Number": "ID475478/RUR", "JObject": [{ "Registration_Number": "29", "value": "Country" }, { "Registration_Number": "30", "value": "4,3,5" }] },
                    dataType: 'json',
                    ContentType: "application/json",
                    cache: false,
                    headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
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

        self.loadEditData = function () {
            self.ViewDataArray([]);
            $.ajax({
                url: "/api/Appointment/Details",
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'GET',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    200: function (edits) {

                        //alert(ko.toJSON(edits));

                        $.each(edits, function (index, edit) {
                            if (edit.RoleName == "STUDENT") {
                                $.each(edit.User, function (index, user) {

                                    self.ViewDataArray.push(new ViewData(edit.AppointmentID, user.SurName, user.FirstName, user.LastName, user.Registration_Number, user.National_ID_Number, edit.RoleName, edit.Feelings, edit.Causes, edit.Medical_Type, edit.Doctor, edit.DateTime, edit.Status));

                                    //$('#surNameEdit').val(user.SurName).attr('disabled', true);
                                    //$('#firstNameEdit').val(user.FirstName).attr('disabled', true);
                                    //$('#lastNameEdit').val(user.LastName).attr('disabled', true);
                                    //$('#Registration_NumberEdit').val(user.Registration_Number).attr('disabled', true);
                                    //$('#National_ID_NumberEdit').val(user.National_ID_Number).attr('disabled', true);
                                    //$('#Birth_DateEdit').val(user.Registration_Number).attr('disabled', true);
                                });
                            }

                            if (edit.RoleName == "DOCTOR") {
                                $.each(edit.User, function (index, user) {
                                    self.ViewDataArray.push(new ViewData(edit.AppointmentID, user.SurName, user.FirstName, user.LastName, user.Registration_Number, user.National_ID_Number, edit.RoleName, edit.Feelings, edit.Causes, edit.Medical_Type, edit.Doctor, edit.DateTime, edit.Status));
                                });
                                }
                        });
                    },
                    400: function () { alert("400:"); },
                    401: function () {
                        $('#loginModal').modal('show');
                        $('#mustLogin').text('You need to First Login / Register');
                    }
                }
            });

        }

        self.confirm = function (confirm) {
            //alert(postponed.appointmentID());
            var data = { Status: "Confirm" };
            $.ajax({
                url: "/api/Appointment/Status?key=" + confirm.appointmentID(),
                data: { Status: "Confirmed" },
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'PUT',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    400: function () { alert("400:"); },
                    401: function () {
                        $('#loginModal').modal('show');
                        $('#mustLogin').text('You need to First Login / Register');
                    }
                }
            });
        };

        self.postpone = function (postponed) {
            //alert(postponed.appointmentID());
            var data = { Status: "Postponed" };
            $.ajax({
                url: "/api/Appointment/Status?key=" + postponed.appointmentID(),
                data: { Status: "Postponed" },
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'PUT',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    400: function () { alert("400:"); },
                    401: function () {
                        $('#loginModal').modal('show');
                        $('#mustLogin').text('You need to First Login / Register');
                    }
                }
            });
        };
        
        self.treated = function (treated) {
            //alert(treated.appointmentID());
            var data = { Status: "Treated" };
            $.ajax({
                url: "/api/Appointment/Status?key=" + treated.appointmentID(),
                data: { Status: "Treated" },
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'PUT',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    400: function () { alert("400:"); },
                    401: function () {
                        $('#loginModal').modal('show');
                        $('#mustLogin').text('You need to First Login / Register');
                    }
                }
            });
        };


        self.deleteData = function (url) {
            //alert(url.AppointmentUrl());
            $.ajax({
                url: url.AppointmentUrl(),
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'DELETE',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    204: function (deleteData) {
                       // alert('deleted');
                    },
                    400: function () { alert("400:"); },
                    401: function () {
                        $('#loginModal').modal('show');
                        $('#mustLogin').text('You need to First Login / Register');
                    }
                }
            });
        }

        self.deleteMany = function (url) {
            //alert(url.AppointmentUrl());
            $.ajax({
                url: url.AppointmentUrl(),
                cache: false,
                headers: { authorization: "Bearer   " + $.cookie('cookieToken') },
                type: 'DELETE',
                success: function () { },
                error: function (err) { },
                statusCode: {
                    204: function (deleteData) {
                        // alert('deleted');
                    },
                    400: function () { alert("400:"); },
                    401: function () {
                        $('#loginModal').modal('show');
                        $('#mustLogin').text('You need to First Login / Register');
                    }
                }
            });
        }

        self.reciept = function () {
            var data = this;
            alert(ko.toJSON(data));

            var doc = new jsPDF();
            doc.setFontSize(22);
            doc.setTextColor(0, 0, 255);
            doc.text(40, 20, 'Kisii University Annex');


            doc.setFontSize(16);
            doc.setTextColor(100);
            doc.text(150, 20, 'Serial No. ' + this.appointmentID());

            doc.text(20, 30, 'Full Name');
            doc.text(60, 30, ':');
            doc.text(65, 30, this.firstName() + " " + this.lastName() + " " + this.surname());


            doc.text(20, 40, 'Details');

            doc.text(60, 40, ':');
            doc.text(65, 40, "Stream");
            doc.text(90, 40, "--> " + this.firstName());
            doc.text(60, 50, ':');
            doc.text(65, 50, "Fulculty");
            doc.text(90, 50, "--> " + this.firstName());
            doc.text(60, 60, ':');
            doc.text(65, 60, "Course");
            doc.text(90, 60, "--> " + this.firstName());

            doc.text(20, 70, 'Medical Report');
            doc.text(60, 70, ':');
            doc.text(65, 70, "This Apponintment was " + this.status() + " by " + this.doctor());
            doc.text(60, 80, ':');
            doc.text(65, 80, "The midical report made by a doctor goes here...");

            doc.save('Reciept.pdf');
        }

        self.sendOrEditReport = function () {
            alert("a pop up or modal will appear, or a report box simmirar to a commentation will be appended to the details tab-content space...");
        }

        self.reportComment = function () {
            alert("a pop up or modal will appear, or a report box simmirar to a commentation will be appended to the details tab-content space...");
        }






        var hub = $.connection.hoshub;
        $.connection.hub.start();
        hub.client.deleteAppointment = function (key) {
            //alert('deleted = ' + key);
            self.ViewDataArray.remove(function (item) { return item.appointmentID() === key; });
        };

        hub.client.changedStatus = function (key, status, doctor) {
            /**alert(key + '-' + status);
            var arrayFilter = $.grep(self.ViewDataArray(), function (item) {
                return item.appointmentID() === key;
            });
            var h = arrayFilter[0];
            alert(ko.toJSON(arrayFilter));
            alert(ko.toJSON(h));*/

            var arrayFilter = ko.utils.arrayFirst(self.ViewDataArray(), function (item) {
                return item.appointmentID() === key;
            });
            arrayFilter.status(status);
            arrayFilter.doctor(doctor);
            //alert(ko.toJSON(arrayFilter));
        };

        
        













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
                        //alert(ko.toJSON(result));
                        
                        var tokenCookie = result.access_token;
                        var username = result.userName;
                        var rolename = result.roleName;
                        self.u(username);
                        self.role(rolename);

                        $.removeCookie('cookieToken', { path: '/' });
                        $.removeCookie('user', { path: '/' });
                        $.removeCookie('role', { path: '/' });

                        $.cookie('cookieToken', tokenCookie, { expires: 1, path: '/' });
                        $.cookie('user', username, { expires: 1, path: '/' });
                        $.cookie('role', rolename, { expires: 1, path: '/' });

                        //alert($.cookie('cookieToken'));
                        //alert($.cookie('user'));
                        //alert($.cookie('role'));
                    },
                    error: function (e) {
                        alert(ko.toJSON(e));
                    },
                    statusCode: {
                        200: function () {
                            $('#loginModal').modal('hide');
                            self.loadOptionsData();
                        },
                        400: function () { alert("400: Bad request"); }
                    }
                });
            }
        }
        self.SignUp = function (context) {
            if (!$('input[name="surname"]', context).val() == "" && !$('input[name="firstname"]', context).val() == "" && !$('input[name="lastname"]', context).val() == "" && !$('input[name="registration_number"]', context).val() == "" && !$('input[name="national_id_number"]', context).val() == "" && !$('input[name="roleName"]', context).val() == "" && !$('input[name="password"]', context).val() == "" && !$('input[name="confirmpassword"]', context).val() == "") {
                var SignUpData = "surName=" + $('input[name="surname"]', context).val() + "&firstName=" + $('input[name="firstname"]', context).val() + "&lastName=" + $('input[name="lastname"]', context).val() + "&userName=" + $('input[name="registration_number"]', context).val() + "&National_ID_Number=" + $('input[name="national_id_number"]', context).val() + "&roleName=" + $('input[name="roleName"]', context).val() + "&password=" + $('input[name="password"]', context).val() + "&confirmPassword=" + $('input[name="confirmpassword"]', context).val();
                alert(ko.toJSON(SignUpData));
                $.ajax({
                    url: "/api/account/register",
                    data: SignUpData,
                    ContentType: "application/x-www-form-urlencoded",
                    cache: false,
                    type: 'POST',
                    success: function () { },
                    error: function (err) { alert(ko.toJSON(err)); },
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
        //self.loadOptionsData();




















        $this = this;
        $this.currentPage = ko.observable();
        $this.pageSize = ko.observable();
        $this.currentPageIndex = ko.observable(0);
        //$this.contacts = ko.observableArray();
        $this.sortType = "ascending";
        $this.currentColumn = ko.observable("");
        $this.iconType = ko.observable("");
        $this.currentPage = ko.computed(function () {
            var pagesize = parseInt($this.pageSize(), 10),
            startIndex = pagesize * $this.currentPageIndex(),
            endIndex = startIndex + pagesize;
            return self.ViewDataArray.slice(startIndex, endIndex);
        });
        $this.nextPage = function () {
            if ((($this.currentPageIndex() + 1) * $this.pageSize()) < self.ViewDataArray().length) {
                $this.currentPageIndex($this.currentPageIndex() + 1);
            }
            else {
                $this.currentPageIndex(0);
            }
        };
        $this.previousPage = function () {
            if ($this.currentPageIndex() > 0) {
                $this.currentPageIndex($this.currentPageIndex() - 1);
            }
            else {
                $this.currentPageIndex((Math.ceil(self.ViewDataArray().length / $this.pageSize())) - 1);
            }
        };
        $this.sortTable = function (viewModel, e) {
            var orderProp = $(e.target).attr("data-column")
            $this.currentColumn(orderProp);
            $this.contacts.sort(function (left, right) {
                leftVal = left[orderProp];
                rightVal = right[orderProp];
                if ($this.sortType == "ascending") {
                    return leftVal < rightVal ? 1 : -1;
                }
                else {
                    return leftVal > rightVal ? 1 : -1;
                }
            });
            $this.sortType = ($this.sortType == "ascending") ? "descending" : "ascending";
            $this.iconType(($this.sortType == "ascending") ? "icon-chevron-up" : "icon-chevron-down");
        };




    }
   
    ko.applyBindings(new viewModel());




});

