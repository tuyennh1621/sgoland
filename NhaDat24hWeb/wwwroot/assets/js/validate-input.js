jQuery.validator.addMethod('CheckMail', function (email, element) {
    email = email.replace(/\s+/g, "");
    return email.match(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[\d0-9]{1,3}\.[\d0-9]{1,3}\.[\d0-9]{1,3}\.[\d0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/);

}, function (params, element) {
    return $(element).data('msg-email');
});

jQuery.validator.addMethod('phoneVI', function (phone_number, element) {
    phone_number = phone_number.replace(/\s+/g, "");
    return this.optional(element) || (phone_number.length == 10) && phone_number.match(/(((\+|)84)|0)(3|5|7|8|9)+([\d0-9]{8})\b/);

}, function (params, element) {
    return $(element).data('msg-number');
});

jQuery.validator.addMethod("notEqual", function (value, element, param) {
    return this.optional(element) || value != $(param).val();
}, "Vui lòng nhập mật khẩu khác mật khẩu hiện tại");

jQuery.validator.addMethod('CheckDecimal', function (Decimal, element) {
    Decimal = Decimal.replace(/\s+/g, "");
    return Decimal.match(/^\d*(\.\d+)?$/);

}, "Vui lòng nhập đúng theo hướng dẫn");

jQuery.validator.addMethod('Bonus', function (Bonus, element) {
    Bonus = Bonus.replace(/\s+/g, "");
    return (Bonus < 100);

}, "Hoa hồng không được quá 100%");


jQuery.validator.addMethod('CCCD', function (cccd, element) {
    cccd = cccd.replace(/\s+/g, "");
    return this.optional(element) || (cccd.length >= 9 && cccd.length <= 12) && cccd.match(/^\d+$/);

}, "Số CMTND/CCCD phải >=9 hoặc <=12 và phải nhập số");

jQuery.validator.addMethod("CheckSpecialChar", function (value, element) {
    return this.optional(element) || !value.match(/[#*%&]/);
}, "Mật khẩu không được chứa kí tự đặc biệt");

jQuery.validator.addMethod("greaterThan",
    function (value, element, params) {

        if (!/Invalid|NaN/.test(new Date(value))) {
            return new Date(value) > new Date($(params).val());
        }

        return isNaN(value) && isNaN($(params).val())
            || (Number(value) > Number($(params).val()));
    }, 'Must be greater than {0}.');

function getLocation() {
    if (navigator.geolocation) {

        navigator.geolocation.getCurrentPosition(setPosition);
    } else {
        //x.innerHTML = "Geolocation is not supported by this browser.";
    }
}
function setPosition(position) {
    localStorage.setItem('latitude', position.coords.latitude);
    localStorage.setItem('longitude', position.coords.longitude);
}
getLocation();
function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[#&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
}

$("#Login").submit(function (e) {
    e.preventDefault();
    $('#Login').validate();
    if ($('#Login').valid()) {
        var email = document.getElementById("loginemail").value;
        var password = document.getElementById("password_account").value;
        let lat = '0';
        let lon = '0';
        if (localStorage.getItem('latitude')) {
            lat = localStorage.getItem('latitude');
        }
        if (localStorage.getItem('longitude')) {
            lon = localStorage.getItem('longitude');
        }
        $.ajax({
            type: 'POST',
            url: '/partnerlogin',
            data: {
                email: email,
                password: password,
                Lat: lat,
                Lon: lon
            },
            cache: false,
            async: true,
            success: function (result) {
                if (result == 2) {
                    window.location.href = '/dashboard';

                } else if (result == 1) {
                    swal("Thông báo!", "Email hoặc mật khẩu không chính xác", "error");
                    return;
                } else if (result == 3) {
                    swal("Thông báo!", "Đăng nhập thất bại. Tài khoản đang chờ xác nhận", "error");
                    return;
                } else if (result == 4) {
                    swal("Thông báo!", "Đăng nhập thất bại. Tài khoản không tồn tại hoặc đã xóa", "error");
                    return;
                } else {
                    swal("Thông báo!", "Đăng nhập thất bại. Tài khoản đang tạm khóa", "error");
                }
            },
            error: function () {
                //    alert('Failed to receive the Data');
            }
        })
    }
});

//function LoginWithAccount() {
//    var email = document.getElementById("email_account").value;
//    var password = document.getElementById("password_account").value;
//    window.location.href = '/partnerlogin?email=' + email + '&password=' + password;
//};

$('#Email').on('input', function () {
    if ($('#Email').valid()) {

        var email = $('#Email').val();

        $.ajax({
            type: 'POST',
            url: '/Home/CheckEmailExisting',
            data: { email: email },
            cache: false,
            async: true,
            beforeSend: function () {
                $("#Email").addClass("loading");
            },
            success: function (result) {
                $("#Email").removeClass("loading");
                console.log(result.status);
                if (result.status >= 2) {

                    $("#Email").val('');
                    swal("Thông báo!", "Email " + email + " đã được đăng ký, xin hãy chọn 1 email khác", "error");
                    $("#Email").focus();
                    return;
                }
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }
});



$('#Mobile').on('input', function () {
    if ($('#Mobile').valid()) {

        var mobile = $('#Mobile').val();

        $.ajax({
            type: 'POST',
            url: '/Home/CheckPhoneExisting',
            data: { phone: mobile },
            cache: false,
            async: true,
            beforeSend: function () {
                $("#Mobile").addClass("loading");
            },
            success: function (result) {
                $("#Mobile").removeClass("loading");

                if (result.status >= 2) {

                    $("#Mobile").val('');
                    swal("Thông báo!", "Số điện thoại " + mobile + " đã được đăng ký, xin hãy chọn số điện thoại khác", "error");
                    $("#Mobile").focus();
                    return;
                }
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }
});

$('#Refid').blur('input', function () {
    if ($('#Refid').valid()) {

        var refid = $('#Refid').val();

        if (refid != '') {
            $.ajax({
                type: 'POST',
                url: '/Home/CheckidExisting',
                data: { id: refid },
                cache: false,
                async: true,
                success: function (result) {
                    if (result == 0) {
                        $("#Refid").val('');
                        swal("Thông báo!", "Mã giới thiệu " + refid + " không tồn tại, vui lòng nhập chính xác mã bạn được giới thiệu", "error");
                        $("#Refid").focus();
                        return;
                    }
                },
                error: function () {
                    alert('Failed to receive the Data');
                }
            })
        }
    }
});

$("#AddUser").validate({
    rules: {
        Mobile: {
            phoneVI: true
        },
        Email: {
            CheckMail: true
        },
        Address: "required",
        filesAvatar1: "required",
        filesAvatar2: "required",
        CCCD: {
            CCCD: true,
        },
    },
    messages: {
        Fullname: "Vui lòng nhập họ tên",
        Mobile: {
            required: "Vui lòng nhập số điện thoại"
        },
        Email: {
            required: "Vui lòng nhập email"
        },
        Address: "Vui lòng nhập địa chỉ",
        Day: "Chọn ngày",
        Month: "Chọn tháng",
        Year: "Chọn năm",
        CCCD: {
            required: "Vui lòng nhập CMTND/CCCD",
        },
        GioiTinh: "Vui lòng chọn giới tính",
        filesAvatar1: "Vui lòng chọn avatar",
        filesAvatar2: "Vui lòng chọn avatar",
        filesCMT1: "Vui lòng chụp CMT/CCCD Mặt trước",
        filesCMT2: "Vui lòng chụp CMT/CCCD Mặt sau",
        IdCongty: "Vui lòng chọn công ty",
        DepartmentId: "Vui lòng chọn phòng ban",
        GIOITHIEU: "Vui lòng giới thiệu sơ qua về bản thân"
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },

});
$("#AddUser").on("submit", function (event) {
    event.preventDefault();
    $("#AddUser").validate();
    if ($("#AddUser").valid()) {

        event.preventDefault();

        var _data = new FormData();

        $.each($("input[type='file']")[0].files, function (i, file) {
            _data.append('filesAvatar1', file);
        });

        $.each($("input[type='file']")[1].files, function (i, file) {
            _data.append('filesAvatar2', file);
        });

        $.each($("input[type='file']")[2].files, function (i, file) {
            _data.append('filesCMT1', file);
        });

        $.each($("input[type='file']")[3].files, function (i, file) {
            _data.append('filesCMT2', file);
        });

        $.each($("input[type='file']")[4].files, function (i, file) {
            _data.append('filesCV', file);
        });

        $("input[type='text'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });

        $("input[type='date'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });

        $("input[type='email'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });

        $("#IdCongty option:selected").each(function (x, y) {
            _data.append("IdCongty", $(y).val());
        });

        $("#GioiTinh option:selected").each(function (x, y) {
            _data.append("GioiTinh", $(y).val());
        });

        $("#DepartmentId option:selected").each(function (x, y) {
            _data.append("DepartmentId", $(y).val());
        });

        $("#Day option:selected").each(function (x, y) {
            _data.append("Day", $(y).val());
        });

        $("#Month option:selected").each(function (x, y) {
            _data.append("Month", $(y).val());
        });

        $("#Year option:selected").each(function (x, y) {
            _data.append("Year", $(y).val());
        });

        $("textarea").each(function (x, y) {

            _data.append($(y).attr("name"), $(y).val());
        });


        $("input[type='number'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });

        //if ($("#Refid").val() != '') {

        //    $.ajax({
        //        type: 'POST',
        //        url: '/Home/CheckidExisting',
        //        data: { refid: $("#Refid").val() },
        //        cache: false,
        //        async: true,
        //        success: function (result) {
        //            if (result == "0") {
        //                $("#Refid").val('0');
        //            }
        //        },
        //        error: function () {
        //            alert('Failed to receive the Data');
        //        }
        //    })
        //}



        console.log('data' + _data);
        $.ajax({
            type: 'POST',
            url: '/Home/CreateCtvCong',
            data: _data,
            cache: false,
            async: true,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $('.preloader').show();
            },
            success: function (result) {
                $('.preloader').hide();
                $('#AddUser input').val('');
                $('#AddUser textarea').val('');
                $('#AddUser #img-filesAvatar1').attr("src", "/assets/img/1232435434.jpg");
                $('#AddUser #img-filesAvatar2').attr("src", "/assets/img/et54y6565.jpg");
                $('#AddUser #img-filesCMT1').attr("src", "/assets/img/214356.png");
                $('#AddUser #img-filesCMT2').attr("src", "/assets/img/242442342.png");
                $('#AddUser #linkfile').text("");
                swal("Thông báo!", "Đăng ký thành công. Chúng tôi sẽ liên hệ với bạn trong thời gian sớm nhất. Xin chân thành cảm ơn.", "success");
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }
});

$("#ChangePW").validate({
    rules: {
        oldpassword: "required",
        newpassword: {
            required: true,
            minlength: 8,
            notEqual: "#old_password",
            CheckSpecialChar: true
        },
        repassword: {
            required: true,
            equalTo: "#new_password"
        },
    },
    messages: {
        oldpassword: "Vui lòng nhập mật khẩu hiện tại",
        newpassword: {
            required: "Vui lòng nhập mật khẩu",
            minlength: "Mật khẩu mới phải lớn hơn 8 kí tự",
        },
        repassword: {
            required: "Vui lòng nhập lại mật khẩu",
            equalTo: "Không đúng với mật khẩu mới"
        },
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },
});

$("#Login").validate({
    rules: {
        loginemail: {
            required: true,
            CheckMail: true
        },
        loginpassword: "required",
    },
    messages: {
        loginemail: {
            required: "Bạn chưa nhập email",
        },
        loginpassword: "Bạn chưa nhập mật khẩu",
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },
});

$("#addLandRE").validate({
    rules: {
        S: {
            CheckDecimal: true
        },
        FrontLenght: {
            CheckDecimal: true
        },
        Lenght: {
            CheckDecimal: true
        },
        EntranceLenght: {
            CheckDecimal: true
        },
        BonusPercent: {
            CheckDecimal: true,
            Bonus: true
        },
        Phone: {
            phoneVI: true
        },
        SourcePhone: {
            phoneVI: true
        },
    },
    messages: {
        AnhBDS: "Vui lòng chọn ảnh",
        AnhSD: "Vui lòng chọn ảnh",
        Name: "Vui lòng nhập tên BĐS",
        IdType: "Vui lòng chọn kiểu BĐS",
        IdProvince: "Vui lòng chọn tỉnh thành",
        IdDistrict: "Vui lòng chọn quận huyện",
        IdWard: "Vui lòng chọn xã phường",
        Address: "Vui lòng chọn nhập địa chỉ chi tiết",
        S: {
            required: "Vui lòng nhập diện tích",
        },
        FrontLenght: {
            required: "Vui lòng nhập chiều dài mặt tiền",
        },
        Lenght: {
            required: "Vui lòng nhập chiều dài",
        },
        EntranceLenght: {
            required: "Vui lòng nhập chiều dài ngõ vào",
        },
        OfferPrice: "Vui lòng nhập giá hợp đồng",
        LastPrice: "Vui lòng nhập giá chốt",
        BonusPercent: {
            required: "Vui lòng nhập % hoa hồng",
        },
        Phone: {
            required: "Vui lòng nhập điện thoại chủ nhà",
        },
        Manager: "Vui lòng nhập người tạo",
        OwnerName: "Vui lòng nhập tên chủ nhà",
        SourcePhone: {
            required: "Vui lòng nhập điện thoại",
        },
        OwnerCCCD: "Vui lòng nhập căn cước công dân",
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },
});
$("#addLandRE").on("submit", function (event) {
    event.preventDefault();
    $("#addLandRE").validate();
    if ($("#addLandRE").valid()) {
        event.preventDefault();

        var _data = new FormData();

        $.each($("input[type='file']")[0].files, function (i, file) {
            _data.append('AnhBDS', file);
        });
        $.each($("input[type='file']")[1].files, function (i, file) {
            _data.append('AnhSD', file);
        });
        $("input[type='hidden'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("input[type='text'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("textarea").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("#IdType option:selected").each(function (x, y) {
            _data.append("IdType", $(y).val());
        });

        $("#IdProvince option:selected").each(function (x, y) {
            _data.append("IdProvince", $(y).val());
        });
        $("#IdDistrict option:selected").each(function (x, y) {
            _data.append("IdDistrict", $(y).val());
        });
        $("#IdWard option:selected").each(function (x, y) {
            _data.append("IdWard", $(y).val());
        });

        $("#NumberFloor option:selected").each(function (x, y) {
            _data.append("NumberFloor", $(y).val());
        });
        $("#NumberBedRoom option:selected").each(function (x, y) {
            _data.append("NumberBedRoom", $(y).val());
        });
        $("#NumberWc option:selected").each(function (x, y) {
            _data.append("NumberWc", $(y).val());
        });
        $("#NumberBalcony option:selected").each(function (x, y) {
            _data.append("NumberBalcony", $(y).val());
        });

        $("#DirectionHouse option:selected").each(function (x, y) {
            _data.append("DirectionHouse", $(y).val());
        });

        $("#Source option:selected").each(function (x, y) {
            _data.append("Source", $(y).val());
        });

        console.log(_data);
        $.ajax({
            type: 'POST',
            url: '/land/add-new-landre-submit',
            data: _data,
            cache: false,
            async: true,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $('.preloader').show();
            },
            success: function (result) {
                console.log(result);
                $('.preloader').hide();
                //   $('#addLandRE input').val('');
                if ($("#IdR").val() > 0) {
                    swal("Thông báo!", "Cập nhật bất động sản thành công", "success").then(function () {
                        window.location = "/properties";
                    });;
                }
                else {
                    swal("Thông báo!", "Thêm mới bất động sản thành công", "success");
                    $('#addLandRE input').val('');
                    $("div.element_select select").val("").change();
                    $('#anhBDS').html("");
                    $('#anhSD').html("");
                    $('#oldImageList').val('');
                    $('#oldImageList2').val('');
                    $('textarea').val('');
                }
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }
});


$("#addLandPJ").validate({
    rules: {
        Phone: {
            phoneVI: true,
        },
        Email: {
            CheckMail: true,
        },
    },
    messages: {
        Name: "Vui lòng nhập tên dự án",
        IdType: "Vui lòng chọn loại dự án",
        IdProvince: "Vui lòng chọn tỉnh thành",
        IdDistrict: "Vui lòng chọn quận huyện",
        IdWard: "Vui lòng chọn xã phường",
        Address: "Vui lòng chọn nhập địa chỉ chi tiết",
        Detail: "Vui lòng mô tả dự án của bạn",
        ImplementCompany: "Vui lòng nhập tên đơn vị triển khai dự án",
        Manager: "Vui lòng nhập họ tên",
        Phone: {
            required: "Vui lòng nhập số điện thoại",
        },
        Email: {
            required: "Vui lòng nhập email",
        },
        OfferPrice: {
            maxlength: "Giá trị quá lớn",
        },
        Priceperm2: {
            maxlength: "Giá/m2 quá lớn",
        },
        S: {
            required: "Vui lòng nhập diện tích căn nhỏ nhất dự án",
            maxlength: "Diện tích quá lớn",
        },
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },
});


$("#ContactForm").validate({
    rules: {
        fullname: "required",
        email: {
            required: true,
            CheckMail: true
        },
        phone: {
            required: true,
            phoneVI: true
        },
        caption: "required",
    },
    messages: {
        fullname: "Vui lòng nhập họ tên",
        email: {
            required: "Vui lòng nhập email",
        },
        phone: {
            required: "Vui lòng nhập số điện thoại",
        },
        caption: "Vui lòng nhập tiêu đề",
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },

});

$("#emailForm").on("submit", function (event) {

    event.preventDefault();

    var _data = new FormData(); //$("#emailForm").serialize();


    //var fileInput = $('#formFile')[0];
    //var file = fileInput.files[0];
    //_data.append("formFile", file);


    $.each($("input[type='file']")[0].files, function (i, file) {
        _data.append('filesAvatar', file);
    });

    $.each($("input[type='file']")[1].files, function (i, file) {
        _data.append('filesCMT1', file);
    });

    $.each($("input[type='file']")[2].files, function (i, file) {
        _data.append('filesCMT2', file);
    });

    $("input[type='text'").each(function (x, y) {
        _data.append($(y).attr("name"), $(y).val());
    });


    console.log(_data);


    $.ajax({
        type: 'POST',
        url: '/Home/TestCtv',
        data: _data,
        cache: false,
        async: true,
        processData: false,
        contentType: false,
        beforeSend: function () {
            $('.preloader').show();
        },
        success: function (result) {
            $('.preloader').hide();
            $('#AddUser input').val('');
            alert('Dang ky thanh cong');
        },
        error: function () {
            alert('Failed to receive the Data');
        }
    })
});

$(document).ready(function () {
    $('.upload__inputfile').each(function () {
        $(this).on('change', function (e) {

            var imgArray = [];
            var arr = [];

            let files = e.target.files;
            let filesArr = Array.prototype.slice.call(files);

            if (filesArr.length > 20) filesArr = filesArr.slice(0, 20);
            if (imgArray.length > 20) imgArray = imgArray.slice(0, 20);
            if (arr.length > 20) arr = arr.slice(0, 20);
            filesArr.forEach(function (f, index) {
                if (!f.type.match('image.*')) {
                    swal("Thông báo!", "Bạn chỉ được đăng file ảnh (.png|.jpg|.jpeg|.gif)", "error");
                    return;
                }
                else {
                    if (f.size > 3072000 || f.name.match(/@/)) {
                        swal("Thông báo!", "File của bạn lớn hơn 3Mb hoặc chứa kí tự đặc biệt", "error");
                        return;
                    }
                    $('#img-' + e.delegateTarget.id).attr('src', URL.createObjectURL(f));
                    $('#' + e.delegateTarget.id + '-error').addClass("hide");
                }
            });
        });
    });
});

$(document).ready(function () {
    $('.upload__cv').each(function () {
        $(this).on('change', function (e) {

            var cvArray = [];
            var arr = [];

            let files = e.target.files;
            let filesArr = Array.prototype.slice.call(files);

            if (filesArr.length > 20) filesArr = filesArr.slice(0, 20);
            if (cvArray.length > 20) cvArray = cvArray.slice(0, 20);
            if (arr.length > 20) arr = arr.slice(0, 20);
            filesArr.forEach(function (f, index) {
                if (f.type.match('application/pdf') || f.type.match('image.*')) {
                    if (f.size > 10072000) {
                        swal("Thông báo!", "Bạn chỉ được đăng file ảnh có dung lượng bé hơn 10M", "error");
                        $("#linkfile").html('');
                        return;
                    }

                    $("#linkfile").html('Đã chọn file ' + f.name);
                    //$('#' + e.delegateTarget.id + '-error').addClass("hide");
                }
                else {
                    swal("Thông báo!", "Bạn chỉ được đăng file có định dạng (.pdf|.png|.jpg|.jpeg|.gif)", "error");
                    $("#linkfile").html('');
                    return;
                }
            });
        });
    });
});

$("#addCompany").on("submit", function (event) {
    debugger
    event.preventDefault();
    $("#addCompany").validate();
    if ($("#addCompany").valid()) {
        event.preventDefault();

        var _data = new FormData();

        $.each($("input[type='file']")[0].files, function (i, file) {
            _data.append('fileAvt', file);
        });
        $("input[type='hidden'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("input[type='number'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("input[type='text'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });

        $("textarea").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });

        console.log(_data);
        $.ajax({
            type: 'POST',
            url: '/company/add-company-submit',
            data: _data,
            cache: false,
            async: true,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $('.preloader').show();
            },
            success: function (result) {
                console.log(result);
                $('.preloader').hide();
                if ($("#Id").val() > 0) {
                    swal("Thông báo!", "Cập nhật công ty thành công", "success");
                }
                else {
                    swal("Thông báo!", "Thêm mới công ty thành công", "success");
                    $('#addCompany input').val('');
                }
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }
});

function ImgUploadBDS() {
    var imgWrap = "";
    var imgArray = [];
    var arr = [];
    $('#uploadBDS').each(function () {
        $(this).on('change', function (e) {
            $('#anhBDS').html('');
            imgWrap = "";
            imgArray = [];
            arr = [];
            imgWrap = $(this).closest('#render_img_BDS').find('#anhBDS');
            let maxLength = $(this).attr('data-max_length');

            let files = e.target.files;
            let filesArr = Array.prototype.slice.call(files);
            let iterator = 0;
            if (filesArr.length > 20) filesArr = filesArr.slice(0, 20);
            if (imgArray.length > 20) imgArray = imgArray.slice(0, 20);
            if (arr.length > 20) arr = arr.slice(0, 20);
            filesArr.forEach(function (f, index) {

                if (!f.type.match('image.*') && !f.type.match('application/pdf')) {
                    swal("Thông báo", "Bạn chỉ được phép đăng ảnh!", "error");
                    return;
                }
                if (f.name == "" || f.name.match(/@/)) {
                    swal("Thông báo", "Tên file không được để trống hoặc có kí tự đặc biệt", "error");
                    return false;
                }

                else {
                    debugger;
                    imgArray.push(f);
                    arr.push(f.name);
                    console.log(arr);
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div></div></div></div>";
                        imgWrap.append(html);
                        iterator++;
                    }
                    reader.readAsDataURL(f);
                    //}
                }
            });
        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                arr.splice(i, 1);
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}
function ImgUploadSD() {
    var imgWrap = "";
    var imgArray = [];
    var arr = [];
    $('#uploadSD').each(function () {
        $(this).on('change', function (e) {
            $('#anhSD').html('');
            imgWrap = "";
            imgArray = [];
            arr = [];
            imgWrap = $(this).closest('#render_img_SD').find('#anhSD');
            let maxLength = $(this).attr('data-max_length');

            let files = e.target.files;
            let filesArr = Array.prototype.slice.call(files);
            let iterator = 0;
            if (filesArr.length > 20) filesArr = filesArr.slice(0, 20);
            if (imgArray.length > 20) imgArray = imgArray.slice(0, 20);
            if (arr.length > 20) arr = arr.slice(0, 20);
            filesArr.forEach(function (f, index) {

                if (!f.type.match('image.*') && !f.type.match('application/pdf')) {
                    swal("Thông báo", "Bạn chỉ được phép đăng ảnh!", "error");
                    return;
                }
                if (f.name == "" || f.name.match(/@/)) {
                    swal("Thông báo", "Tên file không được để trống hoặc có kí tự đặc biệt", "error");
                    return false;
                }

                else {
                    debugger;
                    imgArray.push(f);
                    arr.push(f.name);
                    console.log(arr);
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div></div></div></div>";
                        imgWrap.append(html);
                        iterator++;
                    }
                    reader.readAsDataURL(f);
                    //}
                }
            });
        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                arr.splice(i, 1);
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}

$("#SubmitEditUser").validate({
    rules: {
        Mobile: {
            phoneVI: true
        },
        Email: {
            CheckMail: true
        },
        Diachi: "required",
        Gioithieu: "required",
        Socmtnd: {
            CCCD: true,
        },
    },
    messages: {
        FullName: "Vui lòng nhập họ tên",
        Mobile: {
            required: "Vui lòng nhập số điện thoại"
        },
        Email: {
            required: "Vui lòng nhập email"
        },
        Diachi: "Vui lòng nhập địa chỉ",
        Socmtnd: {
            required: "Vui lòng nhập CMTND/CCCD",
        },
        Ngaysinh: "Vui lòng nhập ngày sinh",
        GioiTinh: "Vui lòng chọn giới tính",
        Gioithieu: "Vui lòng giới thiệu sơ qua về bản thân"
    },
    errorElement: "label",
    errorPlacement: function (error, element) {
        // Add the `help-block` class to the error element
        error.addClass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertAfter(element.parent("label"));
        } else {
            error.insertAfter(element);
        }
    },

});


$("#AddDeposit").validate({
    rules: {
        PaymentDeadline: {
            greaterThan: "#DepositDate",
        },
        Bonus: {
            Bonus: true,
        }
    },
    messages: {
        Name: "Vui lòng nhập tên dự án",
        IdRE: "Vui lòng chọn dự án",
        DepositDate: "Vui lòng chọn ngày đặt cọc",
        PaymentDeadline: {
            required: "Vui lòng chọn hạn thanh toán",
            greaterThan: "Hạn thanh toán phải sau ngày đặt cọc"
        },
        DepositValue: {
            required: "Vui lòng nhập số tiền cọc",
            minlength: "Tiền cọc phải lớn hơn một triệu",
        },
        TotalValue: {
            required: "Vui lòng nhập tổng giá trị hợp đồng",
            minlength:"Tổng giá trị phải lớn hơn một triệu",
        },
        Contract: "Vui lòng chọn ảnh",
        IdType: "Vui lòng chọn loại dự án",
        Status: "Vui lòng chọn trạng thái",
        Bonus: {
            required: "Vui lòng nhập % hoa hồng",
        }
    },
    errorelement: "label",
    errorplacement: function (error, element) {
        // add the `help-block` class to the error element
        error.addclass("help-block");

        if (element.prop("type") === "checkbox") {
            error.insertafter(element.parent("label"));
        } else {
            error.insertafter(element);
        }
    },

});

$("#AddDeposit").on("submit", function (event) {
    event.preventDefault();
    $("#AddDeposit").validate();
    if ($("#AddDeposit").valid()) {
        event.preventDefault();

        var _data = new FormData();

        $.each($("input[type='file']")[0].files, function (i, file) {
            _data.append('Contract', file);
        });
        $("input[type='number'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("input[type='text'").each(function (x, y) {
            if ($(y).attr("name") == "DepositValue" || $(y).attr("name") == "TotalValue") {
                _data.append($(y).attr("name"), Number($(y).val().replace(/[^0-9]+/g, "")));

            } else {
                _data.append($(y).attr("name"), $(y).val());
            }
        });
        $("textarea").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("#IdRE option:selected").each(function (x, y) {
            _data.append("IdRE", $(y).val());
        });
        $("input[type='date'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        $("input[type='hidden'").each(function (x, y) {
            _data.append($(y).attr("name"), $(y).val());
        });
        console.log(_data);
        $.ajax({
            type: 'POST',
            url: '/deposit/submit',
            data: _data,
            cache: false,
            async: true,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $('.preloader').show();
            },
            success: function (result) {
                $('.preloader').hide();
                $('#AddDeposit input').val('');
                $('textarea').val('');
                $("div.element_select select").val("").change();
                $("#imgContract").html("");
                swal("Thông báo!", "Thêm mới thành công", "success");
            },
            error: function () {
                alert('Failed to receive the Data');
            }
        })
    }
});

function ImgUpload() {
    var imgWrap = "";
    var imgArray = [];
    var arr = [];
    $('#Contract').each(function () {
        $(this).on('change', function (e) {
            $('#imgContract').html('');
            imgWrap = "";
            imgArray = [];
            arr = [];
            imgWrap = $(this).closest('#render_img').find('#imgContract');
            let maxLength = $(this).attr('data-max_length');

            let files = e.target.files;
            let filesArr = Array.prototype.slice.call(files);
            let iterator = 0;
            if (filesArr.length > 20) filesArr = filesArr.slice(0, 20);
            if (imgArray.length > 20) imgArray = imgArray.slice(0, 20);
            if (arr.length > 20) arr = arr.slice(0, 20);
            filesArr.forEach(function (f, index) {

                if (!f.type.match('image.*') && !f.type.match('application/pdf')) {
                    swal("Thông báo", "Bạn chỉ được phép đăng ảnh!", "error");
                    return;
                }
                if (f.name == "") {
                    swal("Thông báo", "Tên file không được để trống hoặc có kí tự đặc biệt", "error");
                    return false;
                }

                else {
                    debugger;
                    imgArray.push(f);
                    arr.push(f.name);
                    console.log(arr);
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div></div></div></div>";
                        imgWrap.append(html);
                        iterator++;
                    }
                    reader.readAsDataURL(f);
                    //}
                }
            });
        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                arr.splice(i, 1);
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}

function EditDepositImg() {
    var imgWrap = "";
    var imgArray = [];
    var arr = [];
    $('#EditImg').each(function () {
        $(this).on('change', function (e) {
            $('#EditContract').html('');
            imgWrap = "";
            imgArray = [];
            arr = [];
            imgWrap = $(this).closest('#edit_img').find('#EditContract');
            let maxLength = $(this).attr('data-max_length');

            let files = e.target.files;
            let filesArr = Array.prototype.slice.call(files);
            let iterator = 0;
            if (filesArr.length > 20) filesArr = filesArr.slice(0, 20);
            if (imgArray.length > 20) imgArray = imgArray.slice(0, 20);
            if (arr.length > 20) arr = arr.slice(0, 20);
            filesArr.forEach(function (f, index) {

                if (!f.type.match('image.*') && !f.type.match('application/pdf')) {
                    swal("Thông báo", "Bạn chỉ được phép đăng ảnh!", "error");
                    return;
                }
                if (f.name == "") {
                    swal("Thông báo", "Tên file không được để trống hoặc có kí tự đặc biệt", "error");
                    return false;
                }

                else {
                    debugger;
                    imgArray.push(f);
                    arr.push(f.name);
                    console.log(arr);
                    var reader = new FileReader();
                    reader.onload = function (e) {
                        var html = "<div class='upload__img-box'><div style='background-image: url(" + e.target.result + ")' data-number='" + $(".upload__img-close").length + "' data-file='" + f.name + "' class='img-bg'><div></div></div></div>";
                        imgWrap.append(html);
                        iterator++;
                    }
                    reader.readAsDataURL(f);
                    //}
                }
            });
        });
    });

    $('body').on('click', ".upload__img-close", function (e) {
        var file = $(this).parent().data("file");
        for (var i = 0; i < imgArray.length; i++) {
            if (imgArray[i].name === file) {
                arr.splice(i, 1);
                imgArray.splice(i, 1);
                break;
            }
        }
        $(this).parent().parent().remove();
    });
}



